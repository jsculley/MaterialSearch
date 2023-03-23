/*
The MIT License (MIT)

Copyright(c) 2022, Jim Sculley

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;

namespace org.duckdns.buttercup.MaterialSearch
{
    public partial class ConfigInfoDialog : Form
    {
        private List<TargetInfo> targetInfoList;

        /// <summary>
        /// The form constructor
        /// </summary>
        /// <param name="targetInfoList">a list of <see cref="TargetInfo"/> objects that
        /// wil be used to populate the form</param>
        public ConfigInfoDialog(List<TargetInfo> targetInfoList)
        {
            this.targetInfoList = targetInfoList;
            InitializeComponent();
            buildConfigTree();
            this.configTreeView.ExpandAll();
        }

        /// <summary>
        /// Build a TreeNode structure from the configurations of the documents where  materials
        /// are to be applied.  If the selections came from an assembly document, each document 
        /// with selections is a top level node.  A document's configurations and derived
        /// configurations appear beneath it.  By default, check marks are applied for the active
        /// configuration if selections came form a part document.  If selections were made in an
        /// assembly document, check marks are applied to the configuration of the selected instance
        /// in the assembly.  If multiple instances of the same component but with different
        /// configurations are selected, each configuration will have a check mark.
        /// </summary>
        private void buildConfigTree()
        {
            foreach (TargetInfo ti in targetInfoList)
            {
                TreeNode tn = new TreeNode(ti.TargetDoc.GetTitle());
                tn.Name = ti.TargetDoc.GetTitle();
                tn.ImageIndex = 0;
                tn.SelectedImageIndex = 0;
                List<string> configNames = new List<string>(ti.TargetDoc.GetConfigurationNames());
                configNames.Reverse();
                while (configNames.Count > 0)
                {
                    for (int i = configNames.Count - 1; i >= 0; i--)
                    {
                        Configuration nextConfig = ti.TargetDoc.GetConfigurationByName(configNames[i]);
                        if (nextConfig.GetParent() == null) //top level node
                        {
                            TreeNode nextNode = tn.Nodes.Add(configNames[i]);
                            nextNode.Name = configNames[i];
                            if (ti.isDesignTableConfig(configNames[i]))
                            {
                                nextNode.ImageIndex = (nextConfig.GetChildrenCount() > 0) ? 4 : 3;
                                nextNode.SelectedImageIndex = nextNode.ImageIndex;
                            }
                            else
                            {
                                nextNode.ImageIndex = (nextConfig.GetChildrenCount() > 0) ? 2 : 1;
                                nextNode.SelectedImageIndex = nextNode.ImageIndex;
                            }
                            nextNode.Checked = ti.TargetConfigs.Contains(configNames[i]);
                            configNames.Remove(configNames[i]);
                            continue;
                            
                        }
                        Configuration parentConfig = nextConfig.GetParent();
                        TreeNode parentNode = parentNode = flattenTree(tn.Nodes).FirstOrDefault(r => r.Text.Equals(parentConfig.Name));
                        if (parentNode != null)
                        {
                            TreeNode nextNode = parentNode.Nodes.Add(configNames[i]);
                            nextNode.Name = configNames[i];
                            nextNode.ImageIndex = (nextConfig.GetChildrenCount() > 0) ? 2 : 1;
                            nextNode.Checked = ti.TargetConfigs.Contains(configNames[i]);
                            configNames.Remove(configNames[i]);
                            continue;
                        }
                    }
                }
                this.configTreeView.Nodes.Add(tn);
            }
        }
      
        /// <summary>
        /// Squashes a TreeNodeCollection so that all descendants are in one IEnumerable list
        /// </summary>
        /// <param name="nodes">the </param>
        /// <returns>the flattened IEnumerable of TreeNodes</returns>
        private static IEnumerable<TreeNode> flattenTree(TreeNodeCollection nodes)
        {
            return nodes.Cast<TreeNode>()
                        .Concat(nodes.Cast<TreeNode>()
                                    .SelectMany(x => flattenTree(x.Nodes)));
        }


        /// <summary>
        /// Selects all the nodes in the tree
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void selectAllButton_Click(object sender, EventArgs e)
        {
            foreach (TreeNode Node in configTreeView.Nodes)
            {
                Node.Checked = true;
            }
        }

        /// <summary>
        /// Resets the tree to it's initial state where only configs associated
        /// with the users original selections are checked
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event args</param>
        private void resetSelectionButton_Click(object sender, EventArgs e)
        {
            //Clear any document node checks, which will clear all sub node checks
            foreach (TreeNode tn in configTreeView.Nodes)
            {
                tn.Checked = false;
            }
            foreach (TargetInfo ti in targetInfoList)
            {
                TreeNode docNode = configTreeView.Nodes[ti.TargetDoc.GetTitle()];
                foreach(TreeNode tn in flattenTree(docNode.Nodes))
                {
                    tn.Checked = ti.TargetConfigs.Contains(tn.Text);
                }
            }
        }

        /// <summary>
        /// Update the list of <see cref="TargetInfo"/> objects with any newly
        /// selected configurations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, EventArgs e)
        {
            foreach (TargetInfo ti in this.targetInfoList)
            {
                TreeNode docNode = configTreeView.Nodes[ti.TargetDoc.GetTitle()];
                foreach (TreeNode childNode in flattenTree(docNode.Nodes))
                {
                    if (childNode.Checked && !ti.TargetConfigs.Contains(childNode.Name))
                    {
                        ti.TargetConfigs.Add(childNode.Name);
                    }
                    else if (!childNode.Checked && ti.TargetConfigs.Contains(childNode.Name))
                    {
                        ti.TargetConfigs.Remove(childNode.Name);
                    }
                }
            }
        }


        /// <summary>
        /// Update child nodes when the parent is checked or unchecked
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event args</param>
        private void toggleChildNodes(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode nextChild in e.Node.Nodes)
            {
                nextChild.Checked = e.Node.Checked;
            }
        }
    }
}
