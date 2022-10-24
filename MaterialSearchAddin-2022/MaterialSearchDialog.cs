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
        /// <summary>
        /// All databases that will be listed in this form
        /// </summary>
        private List<MaterialDatabaseDescriptor> materialDatabases;

        /// <summary>
        /// Information abot the configurations that a material will be applied to
        /// </summary>
        private List<ConfigInfo> configInfoList;

        /// <summary>
        /// The currently selected material
        /// </summary>
        public MaterialSearchResult SelectedMaterial { get; private set; }


        /// <summary>
        /// Construct a dialog from the given databases with a <see cref="ConfigInfo"/> object that
        /// act to transfer configuration info back to the caller
        /// </summary>
        /// <param name="materialDatabases">the material databases that should appear in the dialog</param>
        /// <param name="configInfo">the <see cref="ConfigInfo"/> object that will be populated for the caller</param>
        public MaterialSearchDialog(List<MaterialDatabaseDescriptor> materialDatabases, List<ConfigInfo> configInfoList)
        {
            this.materialDatabases = materialDatabases;
            this.configInfoList = configInfoList;
            this.materialDatabases.Sort();
            InitializeComponent();
            if (configInfoList.Count == 0)
            {
                this.configButton.Enabled = false;
            }
            applySavedSettings();
        }

        /// <summary>
        /// Update the dialog controls based on values read from saved settings
        /// </summary>
        private void applySavedSettings()
        {
            this.databaseNameList.DataSource = this.materialDatabases;
            this.databaseNameList.ClearSelected();
            this.Location = Properties.Settings.Default.Location;
            this.Size = Properties.Settings.Default.Size;
            if (Properties.Settings.Default.DatabasesToSearch.Contains("All"))
            {
                this.searchAllRadioButton.Checked = true;
            }
            else
            {
                this.searchSelectedRadioButton.Checked = true;
            }
        }

        
        /// <summary>
        /// Search the databases for the text in the seardh field
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
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
                    if (materialName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
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
                    if (descriptionAttr.Value.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        materialDescription = descriptionAttr.Value;
                        matches.Add(new MaterialSearchResult(mdd.Name, materialName, materialDescription));
                    }
                }
            }
            resultDataGridView.DataSource = matches;
            resultsLabel.Text = "Results: " + matches.Count;
        }

        /// <summary>
        /// Adjust the state and content of the database list box based on user selections
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void updateListBoxStatus(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb.Checked)
            {
                databaseNameList.Enabled = true;
                updateListBoxSelections();
            } else
            {
                databaseNameList.ClearSelected();
                databaseNameList.Enabled = false;
            }
               
        }

        /// <summary>
        /// Update the list box by reading the user's saved settings
        /// </summary>
        private void updateListBoxSelections()
        {
            System.Collections.Specialized.StringCollection selectedDatabaseNames = Properties.Settings.Default.DatabasesToSearch;
            if (selectedDatabaseNames.Contains("All"))
            {
                return;
            }
            else
            {
                databaseNameList.BeginUpdate();

                foreach (MaterialDatabaseDescriptor d in materialDatabases)
                {
                    if (selectedDatabaseNames.Contains(d.Name))
                    {
                        databaseNameList.SetSelected(databaseNameList.Items.IndexOf(d), true);
                    }
                }
                databaseNameList.EndUpdate();
            }
        }

        /// <summary>
        /// Enable the search button if any text exists in the search box, otherwise disable it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateSearchButton(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            searchButton.Enabled = (tb.Text != string.Empty);
            performSearch(sender, e);
        }

        /// <summary>
        /// Perom the search if enter is pressed
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void enterPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                performSearch(sender, e);
            }
        }

        /// <summary>
        /// If a table row is selected, enable the Apply button, otherwise disable it
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void updateApplyButton(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            applyButton.Enabled = resultDataGridView.SelectedRows.Count > 0;
        }

        /// <summary>
        /// Update the <see cref=">SelectedMaterial"/> property when a table row is selected
        /// </summary>
        /// <param name="sender"the event source</param>
        /// <param name="e">the event arguments</param>
        private void updateSelection(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection selRows = resultDataGridView.SelectedRows;
            if (selRows.Count > 0)
            {
                this.SelectedMaterial = selRows[0].DataBoundItem as MaterialSearchResult;
            }
        }

        /// <summary>
        /// Display the configuration information dialog
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void showConfigInfoDialog(object sender, EventArgs e)
        {
            ConfigInfoDialog cid = new ConfigInfoDialog(this.configInfoList,false);
            DialogResult dr = cid.ShowDialog();
        }

        /// <summary>
        /// Save user settintgs when the form is closing
        /// </summary>
        /// <param name="sender">the event source</param>
        /// <param name="e">the event arguments</param>
        private void saveSettings(object sender, FormClosingEventArgs e)
        {
            //Store form location/size info
            Form f = sender as MaterialSearchDialog;
            Properties.Settings.Default.Location = f.Location;
            Properties.Settings.Default.Size = this.Size;
            //Store selected databases
            System.Collections.Specialized.StringCollection databaseNames = new System.Collections.Specialized.StringCollection();
            if (this.searchAllRadioButton.Checked)
            {
                databaseNames.Add("All");
            } 
            else
            {
                foreach (object obj in databaseNameList.SelectedItems)
                {
                    databaseNames.Add(obj.ToString());
                }
            }
            Properties.Settings.Default.DatabasesToSearch = databaseNames;
            Properties.Settings.Default.Save();
        }

        private void applyAndClose(object sender, DataGridViewCellEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
