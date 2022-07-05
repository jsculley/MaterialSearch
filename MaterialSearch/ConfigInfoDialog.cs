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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
namespace org.duckdns.buttercup.MaterialSearch
{
    public partial class ConfigInfoDialog : Form
    {
        private ConfigInfo configInfo;
        //public ConfigInfo SelectedConfigs { get; private set; }
        private int[] currentSelections;
        public ConfigInfoDialog(ConfigInfo configInfo)
        {
            this.configInfo = configInfo;
            InitializeComponent();
            this.configNameListBox.DataSource = configInfo.ConfigNames;
            this.configNameListBox.Enabled = this.specifyConfigRadioButton.Checked;
        }

        private void selectAllButton_Click(object sender, EventArgs e)
        {
            //Store current selections
            currentSelections = new int[configNameListBox.SelectedItems.Count];
            configNameListBox.SelectedIndices.CopyTo(currentSelections,0);
            //Select all configs
            for (int i = 0; i < configNameListBox.Items.Count; i++)
            {
                configNameListBox.SetSelected(i, true);
            }
        }

        private void resetSelectionButton_Click(object sender, EventArgs e)
        {
            configNameListBox.ClearSelected();
            //Restore current selections
            foreach (int i in currentSelections)
            {
                configNameListBox.SetSelected(i, true);
            }
        }

        private void specifyConfigRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            configNameListBox.Enabled = specifyConfigRadioButton.Checked;
            selectAllButton.Enabled = specifyConfigRadioButton.Checked;
            resetSelectionButton.Enabled = specifyConfigRadioButton.Checked;
            if (specifyConfigRadioButton.Checked)
            {
                this.configInfo.AppliesTo = Target.SELECTED;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedObjectCollection selectedConfigs = configNameListBox.SelectedItems;
            selectedConfigs.Cast<string>();
            foreach (string s in selectedConfigs)
            {
                configInfo.selectConfig(s);
                Debug.Print(s);
            }
        }

        private void thisConfigRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (thisConfigRadioButton.Checked)
            {
                this.configInfo.AppliesTo = Target.CURRENT;
            }
        }

        private void allConfigRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.allConfigRadioButton.Checked)
            {
                this.configInfo.AppliesTo = Target.ALL;
            }
        }
    }
}
