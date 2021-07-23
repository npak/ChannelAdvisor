namespace ChannelAdvisor
{
    partial class MorrisExtractor
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
            this.tabPages = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.SKUsList = new System.Windows.Forms.RichTextBox();
            this.scrapeSKUs = new System.Windows.Forms.RadioButton();
            this.scrapeAll = new System.Windows.Forms.RadioButton();
            this.OutputFolderLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.OutputFolderText = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtExtractFileUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FilePrefixText = new System.Windows.Forms.TextBox();
            this.FilePrefixLabel = new System.Windows.Forms.Label();
            this.countOfProductsText = new System.Windows.Forms.TextBox();
            this.countOfProductsLabel = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tabPages.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.tabPage1);
            this.tabPages.Controls.Add(this.tabPage2);
            this.tabPages.Location = new System.Drawing.Point(17, 14);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(535, 457);
            this.tabPages.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.SKUsList);
            this.tabPage1.Controls.Add(this.scrapeSKUs);
            this.tabPage1.Controls.Add(this.scrapeAll);
            this.tabPage1.Controls.Add(this.OutputFolderLabel);
            this.tabPage1.Controls.Add(this.StartButton);
            this.tabPage1.Controls.Add(this.OutputFolderButton);
            this.tabPage1.Controls.Add(this.OutputFolderText);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(527, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Morris Extractor";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // SKUsList
            // 
            this.SKUsList.Enabled = false;
            this.SKUsList.Location = new System.Drawing.Point(9, 56);
            this.SKUsList.Name = "SKUsList";
            this.SKUsList.Size = new System.Drawing.Size(501, 226);
            this.SKUsList.TabIndex = 22;
            this.SKUsList.Text = "";
            // 
            // scrapeSKUs
            // 
            this.scrapeSKUs.AutoSize = true;
            this.scrapeSKUs.Location = new System.Drawing.Point(9, 32);
            this.scrapeSKUs.Name = "scrapeSKUs";
            this.scrapeSKUs.Size = new System.Drawing.Size(163, 18);
            this.scrapeSKUs.TabIndex = 21;
            this.scrapeSKUs.Text = "Scrape specified SKUs";
            this.scrapeSKUs.UseVisualStyleBackColor = true;
            // 
            // scrapeAll
            // 
            this.scrapeAll.AutoSize = true;
            this.scrapeAll.Checked = true;
            this.scrapeAll.Location = new System.Drawing.Point(9, 8);
            this.scrapeAll.Name = "scrapeAll";
            this.scrapeAll.Size = new System.Drawing.Size(136, 18);
            this.scrapeAll.TabIndex = 20;
            this.scrapeAll.TabStop = true;
            this.scrapeAll.Text = "Scrape entire site";
            this.scrapeAll.UseVisualStyleBackColor = true;
            this.scrapeAll.CheckedChanged += new System.EventHandler(this.scrapeAll_CheckedChanged);
            // 
            // OutputFolderLabel
            // 
            this.OutputFolderLabel.AutoSize = true;
            this.OutputFolderLabel.Location = new System.Drawing.Point(6, 305);
            this.OutputFolderLabel.Name = "OutputFolderLabel";
            this.OutputFolderLabel.Size = new System.Drawing.Size(99, 14);
            this.OutputFolderLabel.TabIndex = 26;
            this.OutputFolderLabel.Text = "Output Folder:";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(226, 369);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 25;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(477, 298);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(33, 28);
            this.OutputFolderButton.TabIndex = 24;
            this.OutputFolderButton.Text = "...";
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // OutputFolderText
            // 
            this.OutputFolderText.Location = new System.Drawing.Point(111, 302);
            this.OutputFolderText.Name = "OutputFolderText";
            this.OutputFolderText.ReadOnly = true;
            this.OutputFolderText.Size = new System.Drawing.Size(360, 22);
            this.OutputFolderText.TabIndex = 23;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtExtractFileUrl);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.SaveButton);
            this.tabPage2.Controls.Add(this.FilePrefixText);
            this.tabPage2.Controls.Add(this.FilePrefixLabel);
            this.tabPage2.Controls.Add(this.countOfProductsText);
            this.tabPage2.Controls.Add(this.countOfProductsLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(527, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Settings";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // txtExtractFileUrl
            // 
            this.txtExtractFileUrl.Location = new System.Drawing.Point(42, 30);
            this.txtExtractFileUrl.Name = "txtExtractFileUrl";
            this.txtExtractFileUrl.Size = new System.Drawing.Size(408, 22);
            this.txtExtractFileUrl.TabIndex = 37;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 14);
            this.label1.TabIndex = 36;
            this.label1.Text = "Expract file location";
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(226, 394);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 35;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FilePrefixText
            // 
            this.FilePrefixText.Location = new System.Drawing.Point(42, 143);
            this.FilePrefixText.Name = "FilePrefixText";
            this.FilePrefixText.Size = new System.Drawing.Size(161, 22);
            this.FilePrefixText.TabIndex = 34;
            // 
            // FilePrefixLabel
            // 
            this.FilePrefixLabel.AutoSize = true;
            this.FilePrefixLabel.Location = new System.Drawing.Point(39, 126);
            this.FilePrefixLabel.Name = "FilePrefixLabel";
            this.FilePrefixLabel.Size = new System.Drawing.Size(72, 14);
            this.FilePrefixLabel.TabIndex = 33;
            this.FilePrefixLabel.Text = "File Prefix:";
            // 
            // countOfProductsText
            // 
            this.countOfProductsText.Location = new System.Drawing.Point(42, 88);
            this.countOfProductsText.Name = "countOfProductsText";
            this.countOfProductsText.Size = new System.Drawing.Size(100, 22);
            this.countOfProductsText.TabIndex = 26;
            // 
            // countOfProductsLabel
            // 
            this.countOfProductsLabel.AutoSize = true;
            this.countOfProductsLabel.Location = new System.Drawing.Point(39, 71);
            this.countOfProductsLabel.Name = "countOfProductsLabel";
            this.countOfProductsLabel.Size = new System.Drawing.Size(245, 14);
            this.countOfProductsLabel.TabIndex = 25;
            this.countOfProductsLabel.Text = "Number of products per spreadsheet:";
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(477, 477);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 1;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // MorrisExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 512);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.tabPages);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MorrisExtractor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Morris Extractor";
            this.tabPages.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox FilePrefixText;
        private System.Windows.Forms.Label FilePrefixLabel;
        private System.Windows.Forms.TextBox countOfProductsText;
        private System.Windows.Forms.Label countOfProductsLabel;
        private System.Windows.Forms.RichTextBox SKUsList;
        private System.Windows.Forms.RadioButton scrapeSKUs;
        private System.Windows.Forms.RadioButton scrapeAll;
        private System.Windows.Forms.Label OutputFolderLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Button OutputFolderButton;
        private System.Windows.Forms.TextBox OutputFolderText;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox txtExtractFileUrl;
        private System.Windows.Forms.Label label1;
    }
}