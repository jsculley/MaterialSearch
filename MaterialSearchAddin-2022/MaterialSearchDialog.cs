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
using System.Windows.Forms;
using System.Xml.Linq;

namespace org.duckdns.buttercup.MaterialSearch
{
    public partial class MaterialSearchDialog : Form
    {
        private List<MaterialDatabaseDescriptor> materialDatabases;
        private ConfigInfo configInfo;

        public MaterialSearchDialog(List<MaterialDatabaseDescriptor> materialDatabases, ConfigInfo configInfo)
        {
            this.materialDatabases = materialDatabases;
            this.configInfo = configInfo;
            materialDatabases.Sort();
            InitializeComponent();
            databaseNameList.DataSource = this.materialDatabases;
            databaseNameList.ClearSelected();
        }
       
        public MaterialSearchResult SelectedMaterial { get; private set; }

        private void performSearch(object sender, EventArgs e)
        {
            if (searchTermTextBox.Text == String.Empty)
            {
                return;
            }
            IEnumerable<MaterialDatabaseDescriptor> databasesToSearch = null;
            if (searchAllRadioButton.Checked)
            {
                databasesToSearch = this.materialDatabases;
            } else
            {
                databasesToSearch = this.materialDatabases.FindAll(x => databaseNameList.SelectedItems.Contains(x));
            }
            List<MaterialSearchResult> matches = new List<MaterialSearchResult>();
            foreach(MaterialDatabaseDescriptor mdd in databasesToSearch)
            {
                //Get all materials
                IEnumerable<XElement> allMaterials = mdd.Database.Descendants("material");
                foreach (XElement nextMaterial in allMaterials)
                {
                    string materialDescription = "";
                    string searchText = searchTermTextBox.Text;
                    string materialName = nextMaterial.Attribute("name").Value;
                    if (nextMaterial.Attribute("name").Value.IndexOf(searchTermTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        XAttribute descAttr = nextMaterial.Attribute("description");
                        if (descAttr != null)
                        {
                            materialDescription = descAttr.Value;
                        }
                        matches.Add(new MaterialSearchResult(mdd.Name, materialName, materialDescription));
                        continue;
                    }
                    XAttribute descriptionAttr = nextMaterial.Attribute("description");
                    if (descriptionAttr == null)
                    {
                        continue;
                    }
                    if (descriptionAttr.Value.IndexOf(searchTermTextBox.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        matches.Add(new MaterialSearchResult(mdd.Name, materialName, materialDescription));
                    }
                }
            }
            resultDataGridView.DataSource = matches;
            resultsLabel.Text = "Results: " + matches.Count;
        }

        private void updateListBoxStatus(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                databaseNameList.Enabled = true;
            } else
            {
                databaseNameList.ClearSelected();
                databaseNameList.Enabled = false;
            }
               
        }

        private void updateSearchButton(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            searchButton.Enabled = (tb.Text != string.Empty);
        }

        private void enterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                performSearch(sender, e);
            }
        }

        private void updateApplyButton(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            applyButton.Enabled = resultDataGridView.SelectedRows.Count > 0;
        }

        private void updateSelection(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selRows = resultDataGridView.SelectedRows;
            if (selRows.Count > 0)
            {
                this.SelectedMaterial = selRows[0].DataBoundItem as MaterialSearchResult;
            }
        }

        private void showConfigInfoDialog(object sender, EventArgs e)
        {
            ConfigInfoDialog cid = new ConfigInfoDialog(this.configInfo);
            DialogResult dr = cid.ShowDialog();
        }
    }
}
