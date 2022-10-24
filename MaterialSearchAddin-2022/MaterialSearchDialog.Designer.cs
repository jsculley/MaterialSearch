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
namespace org.duckdns.buttercup.MaterialSearch
{
    partial class MaterialSearchDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MaterialSearchDialog));
            this.applyButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.databaseNameList = new System.Windows.Forms.ListBox();
            this.searchTermTextBox = new System.Windows.Forms.TextBox();
            this.searchForLabel = new System.Windows.Forms.Label();
            this.searchScopeGroupBox = new System.Windows.Forms.GroupBox();
            this.searchSelectedRadioButton = new System.Windows.Forms.RadioButton();
            this.searchAllRadioButton = new System.Windows.Forms.RadioButton();
            this.openLibraryButton = new System.Windows.Forms.Button();
            this.resultDataGridView = new System.Windows.Forms.DataGridView();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.configButton = new System.Windows.Forms.Button();
            this.libraryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materialNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.materialSearchResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.searchScopeGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchResultBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // applyButton
            // 
            this.applyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.applyButton.Enabled = false;
            this.applyButton.Location = new System.Drawing.Point(368, 383);
            this.applyButton.Name = "applyButton";
            this.applyButton.Size = new System.Drawing.Size(75, 23);
            this.applyButton.TabIndex = 7;
            this.applyButton.Text = "Apply";
            this.applyButton.UseVisualStyleBackColor = true;
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Location = new System.Drawing.Point(449, 383);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 8;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // searchButton
            // 
            this.searchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchButton.Enabled = false;
            this.searchButton.Location = new System.Drawing.Point(530, 22);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(83, 23);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.performSearch);
            // 
            // databaseNameList
            // 
            this.databaseNameList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.databaseNameList.Enabled = false;
            this.databaseNameList.FormattingEnabled = true;
            this.databaseNameList.Location = new System.Drawing.Point(78, 20);
            this.databaseNameList.Name = "databaseNameList";
            this.databaseNameList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.databaseNameList.Size = new System.Drawing.Size(517, 95);
            this.databaseNameList.Sorted = true;
            this.databaseNameList.TabIndex = 5;
            // 
            // searchTermTextBox
            // 
            this.searchTermTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchTermTextBox.Location = new System.Drawing.Point(15, 24);
            this.searchTermTextBox.Name = "searchTermTextBox";
            this.searchTermTextBox.Size = new System.Drawing.Size(507, 20);
            this.searchTermTextBox.TabIndex = 0;
            this.searchTermTextBox.TextChanged += new System.EventHandler(this.updateSearchButton);
            this.searchTermTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterPressed);
            // 
            // searchForLabel
            // 
            this.searchForLabel.AutoSize = true;
            this.searchForLabel.Location = new System.Drawing.Point(12, 6);
            this.searchForLabel.Name = "searchForLabel";
            this.searchForLabel.Size = new System.Drawing.Size(59, 13);
            this.searchForLabel.TabIndex = 11;
            this.searchForLabel.Text = "Search for:";
            // 
            // searchScopeGroupBox
            // 
            this.searchScopeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.searchScopeGroupBox.Controls.Add(this.searchSelectedRadioButton);
            this.searchScopeGroupBox.Controls.Add(this.searchAllRadioButton);
            this.searchScopeGroupBox.Controls.Add(this.databaseNameList);
            this.searchScopeGroupBox.Location = new System.Drawing.Point(12, 50);
            this.searchScopeGroupBox.Name = "searchScopeGroupBox";
            this.searchScopeGroupBox.Size = new System.Drawing.Size(601, 137);
            this.searchScopeGroupBox.TabIndex = 2;
            this.searchScopeGroupBox.TabStop = false;
            this.searchScopeGroupBox.Text = "Material libraries to search";
            // 
            // searchSelectedRadioButton
            // 
            this.searchSelectedRadioButton.AutoSize = true;
            this.searchSelectedRadioButton.Location = new System.Drawing.Point(7, 44);
            this.searchSelectedRadioButton.Name = "searchSelectedRadioButton";
            this.searchSelectedRadioButton.Size = new System.Drawing.Size(65, 17);
            this.searchSelectedRadioButton.TabIndex = 4;
            this.searchSelectedRadioButton.Text = "selected";
            this.searchSelectedRadioButton.UseVisualStyleBackColor = true;
            this.searchSelectedRadioButton.CheckedChanged += new System.EventHandler(this.updateListBoxStatus);
            // 
            // searchAllRadioButton
            // 
            this.searchAllRadioButton.AutoSize = true;
            this.searchAllRadioButton.Location = new System.Drawing.Point(7, 20);
            this.searchAllRadioButton.Name = "searchAllRadioButton";
            this.searchAllRadioButton.Size = new System.Drawing.Size(35, 17);
            this.searchAllRadioButton.TabIndex = 3;
            this.searchAllRadioButton.Text = "all";
            this.searchAllRadioButton.UseVisualStyleBackColor = true;
            // 
            // openLibraryButton
            // 
            this.openLibraryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openLibraryButton.DialogResult = System.Windows.Forms.DialogResult.Ignore;
            this.openLibraryButton.Location = new System.Drawing.Point(12, 383);
            this.openLibraryButton.Name = "openLibraryButton";
            this.openLibraryButton.Size = new System.Drawing.Size(94, 23);
            this.openLibraryButton.TabIndex = 10;
            this.openLibraryButton.Text = "Open Library";
            this.openLibraryButton.UseVisualStyleBackColor = true;
            // 
            // resultDataGridView
            // 
            this.resultDataGridView.AllowUserToAddRows = false;
            this.resultDataGridView.AllowUserToDeleteRows = false;
            this.resultDataGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.resultDataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.resultDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.resultDataGridView.AutoGenerateColumns = false;
            this.resultDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.resultDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.resultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.resultDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.libraryDataGridViewTextBoxColumn,
            this.materialNameDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn});
            this.resultDataGridView.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.resultDataGridView.DataSource = this.materialSearchResultBindingSource;
            this.resultDataGridView.Location = new System.Drawing.Point(12, 227);
            this.resultDataGridView.MultiSelect = false;
            this.resultDataGridView.Name = "resultDataGridView";
            this.resultDataGridView.ReadOnly = true;
            this.resultDataGridView.RowHeadersVisible = false;
            this.resultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.resultDataGridView.Size = new System.Drawing.Size(595, 150);
            this.resultDataGridView.TabIndex = 6;
            this.resultDataGridView.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.applyAndClose);
            this.resultDataGridView.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.updateApplyButton);
            this.resultDataGridView.SelectionChanged += new System.EventHandler(this.updateSelection);
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Location = new System.Drawing.Point(12, 209);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(42, 13);
            this.resultsLabel.TabIndex = 17;
            this.resultsLabel.Text = "Results";
            // 
            // configButton
            // 
            this.configButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.configButton.Location = new System.Drawing.Point(530, 383);
            this.configButton.Name = "configButton";
            this.configButton.Size = new System.Drawing.Size(75, 23);
            this.configButton.TabIndex = 9;
            this.configButton.Text = "Config...";
            this.configButton.UseVisualStyleBackColor = true;
            this.configButton.Click += new System.EventHandler(this.showConfigInfoDialog);
            // 
            // libraryDataGridViewTextBoxColumn
            // 
            this.libraryDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.libraryDataGridViewTextBoxColumn.DataPropertyName = "Library";
            this.libraryDataGridViewTextBoxColumn.FillWeight = 33F;
            this.libraryDataGridViewTextBoxColumn.HeaderText = "Library";
            this.libraryDataGridViewTextBoxColumn.Name = "libraryDataGridViewTextBoxColumn";
            this.libraryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // materialNameDataGridViewTextBoxColumn
            // 
            this.materialNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.materialNameDataGridViewTextBoxColumn.DataPropertyName = "MaterialName";
            this.materialNameDataGridViewTextBoxColumn.FillWeight = 33F;
            this.materialNameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.materialNameDataGridViewTextBoxColumn.Name = "materialNameDataGridViewTextBoxColumn";
            this.materialNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.FillWeight = 33F;
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // materialSearchResultBindingSource
            // 
            this.materialSearchResultBindingSource.DataSource = typeof(org.duckdns.buttercup.MaterialSearch.MaterialSearchResult);
            // 
            // MaterialSearchDialog
            // 
            this.AcceptButton = this.searchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 418);
            this.Controls.Add(this.configButton);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.resultDataGridView);
            this.Controls.Add(this.applyButton);
            this.Controls.Add(this.openLibraryButton);
            this.Controls.Add(this.searchScopeGroupBox);
            this.Controls.Add(this.searchForLabel);
            this.Controls.Add(this.searchTermTextBox);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.closeButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MaterialSearchDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Material Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.saveSettings);
            this.searchScopeGroupBox.ResumeLayout(false);
            this.searchScopeGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.resultDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.materialSearchResultBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.ListBox databaseNameList;
        private System.Windows.Forms.TextBox searchTermTextBox;
        private System.Windows.Forms.Label searchForLabel;
        private System.Windows.Forms.GroupBox searchScopeGroupBox;
        private System.Windows.Forms.RadioButton searchSelectedRadioButton;
        private System.Windows.Forms.RadioButton searchAllRadioButton;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.DataGridView resultDataGridView;
        private System.Windows.Forms.Button openLibraryButton;
        private System.Windows.Forms.BindingSource materialSearchResultBindingSource;
        private System.Windows.Forms.Button configButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn libraryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn materialNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
    }
}