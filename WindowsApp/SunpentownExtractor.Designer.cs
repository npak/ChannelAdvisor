namespace ChannelAdvisor
{
    partial class SunpentownExtractor
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
            this.sunpentownExtractorTabControl = new System.Windows.Forms.TabControl();
            this.sunpentownExtractorTab = new System.Windows.Forms.TabPage();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.auctionTitleLengthText = new System.Windows.Forms.TextBox();
            this.CAStoreTitleLengthText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.imageURLsText = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.excelFileText = new System.Windows.Forms.TextBox();
            this.excelFileButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.outputFolderText = new System.Windows.Forms.TextBox();
            this.outputFolderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.supplierCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.warehouseLocation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.DCCodeText = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.filePrefixText = new System.Windows.Forms.TextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.sunpentownExtractorTabControl.SuspendLayout();
            this.sunpentownExtractorTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // sunpentownExtractorTabControl
            // 
            this.sunpentownExtractorTabControl.Controls.Add(this.sunpentownExtractorTab);
            this.sunpentownExtractorTabControl.Controls.Add(this.settingsTab);
            this.sunpentownExtractorTabControl.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.sunpentownExtractorTabControl.Location = new System.Drawing.Point(12, 12);
            this.sunpentownExtractorTabControl.Name = "sunpentownExtractorTabControl";
            this.sunpentownExtractorTabControl.Padding = new System.Drawing.Point(3, 3);
            this.sunpentownExtractorTabControl.SelectedIndex = 0;
            this.sunpentownExtractorTabControl.Size = new System.Drawing.Size(568, 473);
            this.sunpentownExtractorTabControl.TabIndex = 0;
            // 
            // sunpentownExtractorTab
            // 
            this.sunpentownExtractorTab.Controls.Add(this.outputFolderButton);
            this.sunpentownExtractorTab.Controls.Add(this.outputFolderText);
            this.sunpentownExtractorTab.Controls.Add(this.label5);
            this.sunpentownExtractorTab.Controls.Add(this.button1);
            this.sunpentownExtractorTab.Controls.Add(this.excelFileButton);
            this.sunpentownExtractorTab.Controls.Add(this.excelFileText);
            this.sunpentownExtractorTab.Controls.Add(this.label4);
            this.sunpentownExtractorTab.Location = new System.Drawing.Point(4, 25);
            this.sunpentownExtractorTab.Name = "sunpentownExtractorTab";
            this.sunpentownExtractorTab.Padding = new System.Windows.Forms.Padding(3);
            this.sunpentownExtractorTab.Size = new System.Drawing.Size(560, 444);
            this.sunpentownExtractorTab.TabIndex = 0;
            this.sunpentownExtractorTab.Text = "Sunpentown Extractor";
            this.sunpentownExtractorTab.UseVisualStyleBackColor = true;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.saveButton);
            this.settingsTab.Controls.Add(this.filePrefixText);
            this.settingsTab.Controls.Add(this.label9);
            this.settingsTab.Controls.Add(this.DCCodeText);
            this.settingsTab.Controls.Add(this.label8);
            this.settingsTab.Controls.Add(this.warehouseLocation);
            this.settingsTab.Controls.Add(this.label7);
            this.settingsTab.Controls.Add(this.supplierCode);
            this.settingsTab.Controls.Add(this.label6);
            this.settingsTab.Controls.Add(this.imageURLsText);
            this.settingsTab.Controls.Add(this.label3);
            this.settingsTab.Controls.Add(this.CAStoreTitleLengthText);
            this.settingsTab.Controls.Add(this.auctionTitleLengthText);
            this.settingsTab.Controls.Add(this.label2);
            this.settingsTab.Controls.Add(this.label1);
            this.settingsTab.Location = new System.Drawing.Point(4, 25);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(560, 444);
            this.settingsTab.TabIndex = 1;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            this.settingsTab.Enter += new System.EventHandler(this.settingsTab_Enter);
            // 
            // closeButton
            // 
            this.closeButton.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.closeButton.Location = new System.Drawing.Point(501, 511);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(148, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Auction Title Length:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(336, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "CA Store Title Length:";
            // 
            // auctionTitleLengthText
            // 
            this.auctionTitleLengthText.Location = new System.Drawing.Point(60, 47);
            this.auctionTitleLengthText.Name = "auctionTitleLengthText";
            this.auctionTitleLengthText.Size = new System.Drawing.Size(145, 23);
            this.auctionTitleLengthText.TabIndex = 1;
            // 
            // CAStoreTitleLengthText
            // 
            this.CAStoreTitleLengthText.Location = new System.Drawing.Point(339, 47);
            this.CAStoreTitleLengthText.Name = "CAStoreTitleLengthText";
            this.CAStoreTitleLengthText.Size = new System.Drawing.Size(145, 23);
            this.CAStoreTitleLengthText.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(57, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Image URLs:";
            // 
            // imageURLsText
            // 
            this.imageURLsText.Location = new System.Drawing.Point(60, 110);
            this.imageURLsText.Name = "imageURLsText";
            this.imageURLsText.Size = new System.Drawing.Size(433, 23);
            this.imageURLsText.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(73, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Excel file:";
            // 
            // excelFileText
            // 
            this.excelFileText.Location = new System.Drawing.Point(151, 19);
            this.excelFileText.Name = "excelFileText";
            this.excelFileText.ReadOnly = true;
            this.excelFileText.Size = new System.Drawing.Size(337, 23);
            this.excelFileText.TabIndex = 1;
            // 
            // excelFileButton
            // 
            this.excelFileButton.Location = new System.Drawing.Point(494, 16);
            this.excelFileButton.Name = "excelFileButton";
            this.excelFileButton.Size = new System.Drawing.Size(33, 28);
            this.excelFileButton.TabIndex = 2;
            this.excelFileButton.Text = "...";
            this.excelFileButton.UseVisualStyleBackColor = true;
            this.excelFileButton.Click += new System.EventHandler(this.excelFileButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(244, 410);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(40, 345);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Output Folder:";
            // 
            // outputFolderText
            // 
            this.outputFolderText.Location = new System.Drawing.Point(151, 342);
            this.outputFolderText.Name = "outputFolderText";
            this.outputFolderText.ReadOnly = true;
            this.outputFolderText.Size = new System.Drawing.Size(337, 23);
            this.outputFolderText.TabIndex = 3;
            // 
            // outputFolderButton
            // 
            this.outputFolderButton.Location = new System.Drawing.Point(494, 339);
            this.outputFolderButton.Name = "outputFolderButton";
            this.outputFolderButton.Size = new System.Drawing.Size(33, 28);
            this.outputFolderButton.TabIndex = 4;
            this.outputFolderButton.Text = "...";
            this.outputFolderButton.UseVisualStyleBackColor = true;
            this.outputFolderButton.Click += new System.EventHandler(this.outputFolderButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(57, 160);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 6;
            this.label6.Text = "Supplier Code:";
            // 
            // supplierCode
            // 
            this.supplierCode.Location = new System.Drawing.Point(60, 179);
            this.supplierCode.Name = "supplierCode";
            this.supplierCode.Size = new System.Drawing.Size(145, 23);
            this.supplierCode.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(336, 160);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(148, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Warehouse Location:";
            // 
            // warehouseLocation
            // 
            this.warehouseLocation.Location = new System.Drawing.Point(339, 179);
            this.warehouseLocation.Name = "warehouseLocation";
            this.warehouseLocation.Size = new System.Drawing.Size(145, 23);
            this.warehouseLocation.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(57, 224);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "DC Code:";
            // 
            // DCCodeText
            // 
            this.DCCodeText.Location = new System.Drawing.Point(60, 243);
            this.DCCodeText.Name = "DCCodeText";
            this.DCCodeText.Size = new System.Drawing.Size(145, 23);
            this.DCCodeText.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(57, 285);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 12;
            this.label9.Text = "File Prefix:";
            // 
            // filePrefixText
            // 
            this.filePrefixText.Location = new System.Drawing.Point(60, 304);
            this.filePrefixText.Name = "filePrefixText";
            this.filePrefixText.Size = new System.Drawing.Size(145, 23);
            this.filePrefixText.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(243, 410);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // SunpentownExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 546);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.sunpentownExtractorTabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SunpentownExtractor";
            this.Text = "Sunpentown Extractor";
            this.sunpentownExtractorTabControl.ResumeLayout(false);
            this.sunpentownExtractorTab.ResumeLayout(false);
            this.sunpentownExtractorTab.PerformLayout();
            this.settingsTab.ResumeLayout(false);
            this.settingsTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl sunpentownExtractorTabControl;
        private System.Windows.Forms.TabPage sunpentownExtractorTab;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox CAStoreTitleLengthText;
        private System.Windows.Forms.TextBox auctionTitleLengthText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox imageURLsText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox excelFileText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button excelFileButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox outputFolderText;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button outputFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox filePrefixText;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox DCCodeText;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox warehouseLocation;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox supplierCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveButton;
    }
}