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
    partial class ConfigInfoDialog
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
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.configNameListBox = new System.Windows.Forms.ListBox();
            this.resetSelectionButton = new System.Windows.Forms.Button();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.thisConfigRadioButton = new System.Windows.Forms.RadioButton();
            this.allConfigRadioButton = new System.Windows.Forms.RadioButton();
            this.specifyConfigRadioButton = new System.Windows.Forms.RadioButton();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(285, 549);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 0;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(371, 549);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Controls.Add(this.configNameListBox);
            this.groupBox1.Controls.Add(this.resetSelectionButton);
            this.groupBox1.Controls.Add(this.selectAllButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 116);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(434, 427);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Specify the configurations  to be modified";
            // 
            // configNameListBox
            // 
            this.configNameListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.configNameListBox.FormattingEnabled = true;
            this.configNameListBox.Location = new System.Drawing.Point(6, 19);
            this.configNameListBox.Name = "configNameListBox";
            this.configNameListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.configNameListBox.Size = new System.Drawing.Size(422, 160);
            this.configNameListBox.TabIndex = 2;
            // 
            // resetSelectionButton
            // 
            this.resetSelectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetSelectionButton.Enabled = false;
            this.resetSelectionButton.Location = new System.Drawing.Point(332, 398);
            this.resetSelectionButton.Name = "resetSelectionButton";
            this.resetSelectionButton.Size = new System.Drawing.Size(96, 23);
            this.resetSelectionButton.TabIndex = 1;
            this.resetSelectionButton.Text = "Reset Selection";
            this.resetSelectionButton.UseVisualStyleBackColor = true;
            this.resetSelectionButton.Click += new System.EventHandler(this.resetSelectionButton_Click);
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAllButton.Enabled = false;
            this.selectAllButton.Location = new System.Drawing.Point(6, 398);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 0;
            this.selectAllButton.Text = "Select All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            this.selectAllButton.Click += new System.EventHandler(this.selectAllButton_Click);
            // 
            // thisConfigRadioButton
            // 
            this.thisConfigRadioButton.AutoSize = true;
            this.thisConfigRadioButton.Checked = true;
            this.thisConfigRadioButton.Location = new System.Drawing.Point(48, 12);
            this.thisConfigRadioButton.Name = "thisConfigRadioButton";
            this.thisConfigRadioButton.Size = new System.Drawing.Size(109, 17);
            this.thisConfigRadioButton.TabIndex = 3;
            this.thisConfigRadioButton.TabStop = true;
            this.thisConfigRadioButton.Text = "This configuration";
            this.thisConfigRadioButton.UseVisualStyleBackColor = true;
            this.thisConfigRadioButton.CheckedChanged += new System.EventHandler(this.thisConfigRadioButton_CheckedChanged);
            // 
            // allConfigRadioButton
            // 
            this.allConfigRadioButton.AutoSize = true;
            this.allConfigRadioButton.Location = new System.Drawing.Point(48, 35);
            this.allConfigRadioButton.Name = "allConfigRadioButton";
            this.allConfigRadioButton.Size = new System.Drawing.Size(105, 17);
            this.allConfigRadioButton.TabIndex = 4;
            this.allConfigRadioButton.Text = "All configurations";
            this.allConfigRadioButton.UseVisualStyleBackColor = true;
            this.allConfigRadioButton.CheckedChanged += new System.EventHandler(this.allConfigRadioButton_CheckedChanged);
            // 
            // specifyConfigRadioButton
            // 
            this.specifyConfigRadioButton.AutoSize = true;
            this.specifyConfigRadioButton.Location = new System.Drawing.Point(48, 58);
            this.specifyConfigRadioButton.Name = "specifyConfigRadioButton";
            this.specifyConfigRadioButton.Size = new System.Drawing.Size(129, 17);
            this.specifyConfigRadioButton.TabIndex = 5;
            this.specifyConfigRadioButton.Text = "Specify configurations";
            this.specifyConfigRadioButton.UseVisualStyleBackColor = true;
            this.specifyConfigRadioButton.CheckedChanged += new System.EventHandler(this.specifyConfigRadioButton_CheckedChanged);
            // 
            // treeView1
            // 
            this.treeView1.CheckBoxes = true;
            this.treeView1.Location = new System.Drawing.Point(6, 186);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(422, 182);
            this.treeView1.TabIndex = 3;
            // 
            // ConfigInfoDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(458, 584);
            this.Controls.Add(this.specifyConfigRadioButton);
            this.Controls.Add(this.allConfigRadioButton);
            this.Controls.Add(this.thisConfigRadioButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Name = "ConfigInfoDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ConfigInfoDialog";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox configNameListBox;
        private System.Windows.Forms.Button resetSelectionButton;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.RadioButton thisConfigRadioButton;
        private System.Windows.Forms.RadioButton allConfigRadioButton;
        private System.Windows.Forms.RadioButton specifyConfigRadioButton;
        private System.Windows.Forms.TreeView treeView1;
    }
}