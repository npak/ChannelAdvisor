namespace ChannelAdvisor
{
    partial class PicnicTimeExtractor
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TabPages = new System.Windows.Forms.TabControl();
            this.ExtractorTabPage = new System.Windows.Forms.TabPage();
            this.SKUsList = new System.Windows.Forms.RichTextBox();
            this.scrapeSKUs = new System.Windows.Forms.RadioButton();
            this.scrapeAll = new System.Windows.Forms.RadioButton();
            this.ProductDataFileText = new System.Windows.Forms.TextBox();
            this.OutputFolderLabel = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.MAPPolicyFileText = new System.Windows.Forms.TextBox();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.ProductDataFileButton = new System.Windows.Forms.Button();
            this.OutputFolderText = new System.Windows.Forms.TextBox();
            this.ProductDataFileLabel = new System.Windows.Forms.Label();
            this.MAPPolicyFileLabel = new System.Windows.Forms.Label();
            this.MAPPolicyFileButton = new System.Windows.Forms.Button();
            this.SettingsTabPage = new System.Windows.Forms.TabPage();
            this.SaveButton = new System.Windows.Forms.Button();
            this.FilePrefixText = new System.Windows.Forms.TextBox();
            this.FilePrefixLabel = new System.Windows.Forms.Label();
            this.DCCodeText = new System.Windows.Forms.TextBox();
            this.DCCodeLabel = new System.Windows.Forms.Label();
            this.WarehouseLocationText = new System.Windows.Forms.TextBox();
            this.WarehouseLocationLabel = new System.Windows.Forms.Label();
            this.SupplierCodeLabel = new System.Windows.Forms.Label();
            this.SupplierCodeText = new System.Windows.Forms.TextBox();
            this.SellerCostAdditionText = new System.Windows.Forms.TextBox();
            this.SellerCostAdditionLabel = new System.Windows.Forms.Label();
            this.ImageURLsText = new System.Windows.Forms.TextBox();
            this.ImageURLsLabel = new System.Windows.Forms.Label();
            this.CAStoreTitleLengthText = new System.Windows.Forms.TextBox();
            this.CAStoreTitleLengthLabel = new System.Windows.Forms.Label();
            this.AuctionTitleText = new System.Windows.Forms.TextBox();
            this.AuctionTitleLengthLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.TabPages.SuspendLayout();
            this.ExtractorTabPage.SuspendLayout();
            this.SettingsTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Excel files|*.xls*";
            this.openFileDialog1.InitialDirectory = "D:\\";
            // 
            // TabPages
            // 
            this.TabPages.Controls.Add(this.ExtractorTabPage);
            this.TabPages.Controls.Add(this.SettingsTabPage);
            this.TabPages.Location = new System.Drawing.Point(12, 12);
            this.TabPages.Name = "TabPages";
            this.TabPages.SelectedIndex = 0;
            this.TabPages.Size = new System.Drawing.Size(570, 477);
            this.TabPages.TabIndex = 1;
            // 
            // ExtractorTabPage
            // 
            this.ExtractorTabPage.Controls.Add(this.SKUsList);
            this.ExtractorTabPage.Controls.Add(this.scrapeSKUs);
            this.ExtractorTabPage.Controls.Add(this.scrapeAll);
            this.ExtractorTabPage.Controls.Add(this.ProductDataFileText);
            this.ExtractorTabPage.Controls.Add(this.OutputFolderLabel);
            this.ExtractorTabPage.Controls.Add(this.StartButton);
            this.ExtractorTabPage.Controls.Add(this.MAPPolicyFileText);
            this.ExtractorTabPage.Controls.Add(this.OutputFolderButton);
            this.ExtractorTabPage.Controls.Add(this.ProductDataFileButton);
            this.ExtractorTabPage.Controls.Add(this.OutputFolderText);
            this.ExtractorTabPage.Controls.Add(this.ProductDataFileLabel);
            this.ExtractorTabPage.Controls.Add(this.MAPPolicyFileLabel);
            this.ExtractorTabPage.Controls.Add(this.MAPPolicyFileButton);
            this.ExtractorTabPage.Location = new System.Drawing.Point(4, 25);
            this.ExtractorTabPage.Name = "ExtractorTabPage";
            this.ExtractorTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExtractorTabPage.Size = new System.Drawing.Size(562, 448);
            this.ExtractorTabPage.TabIndex = 0;
            this.ExtractorTabPage.Text = "Picnic Time Extractor";
            this.ExtractorTabPage.UseVisualStyleBackColor = true;
            // 
            // SKUsList
            // 
            this.SKUsList.Enabled = false;
            this.SKUsList.Location = new System.Drawing.Point(20, 176);
            this.SKUsList.Name = "SKUsList";
            this.SKUsList.Size = new System.Drawing.Size(526, 169);
            this.SKUsList.TabIndex = 8;
            this.SKUsList.Text = "";
            // 
            // scrapeSKUs
            // 
            this.scrapeSKUs.AutoSize = true;
            this.scrapeSKUs.Location = new System.Drawing.Point(20, 150);
            this.scrapeSKUs.Name = "scrapeSKUs";
            this.scrapeSKUs.Size = new System.Drawing.Size(173, 20);
            this.scrapeSKUs.TabIndex = 7;
            this.scrapeSKUs.Text = "Scrape specified SKUs";
            this.scrapeSKUs.UseVisualStyleBackColor = true;
            this.scrapeSKUs.CheckedChanged += new System.EventHandler(this.scrapeSKUs_CheckedChanged);
            // 
            // scrapeAll
            // 
            this.scrapeAll.AutoSize = true;
            this.scrapeAll.Checked = true;
            this.scrapeAll.Location = new System.Drawing.Point(20, 124);
            this.scrapeAll.Name = "scrapeAll";
            this.scrapeAll.Size = new System.Drawing.Size(139, 20);
            this.scrapeAll.TabIndex = 6;
            this.scrapeAll.TabStop = true;
            this.scrapeAll.Text = "Scrape entire file";
            this.scrapeAll.UseVisualStyleBackColor = true;
            this.scrapeAll.CheckedChanged += new System.EventHandler(this.scrapeAll_CheckedChanged);
            // 
            // ProductDataFileText
            // 
            this.ProductDataFileText.Location = new System.Drawing.Point(151, 19);
            this.ProductDataFileText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProductDataFileText.Name = "ProductDataFileText";
            this.ProductDataFileText.ReadOnly = true;
            this.ProductDataFileText.Size = new System.Drawing.Size(356, 23);
            this.ProductDataFileText.TabIndex = 2;
            // 
            // OutputFolderLabel
            // 
            this.OutputFolderLabel.AutoSize = true;
            this.OutputFolderLabel.Location = new System.Drawing.Point(40, 368);
            this.OutputFolderLabel.Name = "OutputFolderLabel";
            this.OutputFolderLabel.Size = new System.Drawing.Size(105, 16);
            this.OutputFolderLabel.TabIndex = 19;
            this.OutputFolderLabel.Text = "Output Folder:";
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(244, 410);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 11;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // MAPPolicyFileText
            // 
            this.MAPPolicyFileText.Location = new System.Drawing.Point(151, 76);
            this.MAPPolicyFileText.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MAPPolicyFileText.Name = "MAPPolicyFileText";
            this.MAPPolicyFileText.ReadOnly = true;
            this.MAPPolicyFileText.Size = new System.Drawing.Size(356, 23);
            this.MAPPolicyFileText.TabIndex = 4;
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(513, 365);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(33, 28);
            this.OutputFolderButton.TabIndex = 10;
            this.OutputFolderButton.Text = "...";
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // ProductDataFileButton
            // 
            this.ProductDataFileButton.Location = new System.Drawing.Point(513, 16);
            this.ProductDataFileButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ProductDataFileButton.Name = "ProductDataFileButton";
            this.ProductDataFileButton.Size = new System.Drawing.Size(33, 28);
            this.ProductDataFileButton.TabIndex = 3;
            this.ProductDataFileButton.Text = "...";
            this.ProductDataFileButton.UseVisualStyleBackColor = true;
            this.ProductDataFileButton.Click += new System.EventHandler(this.ProductDataFileButton_Click);
            // 
            // OutputFolderText
            // 
            this.OutputFolderText.Location = new System.Drawing.Point(151, 368);
            this.OutputFolderText.Name = "OutputFolderText";
            this.OutputFolderText.ReadOnly = true;
            this.OutputFolderText.Size = new System.Drawing.Size(356, 23);
            this.OutputFolderText.TabIndex = 9;
            // 
            // ProductDataFileLabel
            // 
            this.ProductDataFileLabel.AutoSize = true;
            this.ProductDataFileLabel.Location = new System.Drawing.Point(17, 22);
            this.ProductDataFileLabel.Name = "ProductDataFileLabel";
            this.ProductDataFileLabel.Size = new System.Drawing.Size(128, 16);
            this.ProductDataFileLabel.TabIndex = 13;
            this.ProductDataFileLabel.Text = "Product Data File:";
            // 
            // MAPPolicyFileLabel
            // 
            this.MAPPolicyFileLabel.AutoSize = true;
            this.MAPPolicyFileLabel.Location = new System.Drawing.Point(33, 79);
            this.MAPPolicyFileLabel.Name = "MAPPolicyFileLabel";
            this.MAPPolicyFileLabel.Size = new System.Drawing.Size(112, 16);
            this.MAPPolicyFileLabel.TabIndex = 14;
            this.MAPPolicyFileLabel.Text = "MAP Policy File:";
            // 
            // MAPPolicyFileButton
            // 
            this.MAPPolicyFileButton.Location = new System.Drawing.Point(513, 73);
            this.MAPPolicyFileButton.Name = "MAPPolicyFileButton";
            this.MAPPolicyFileButton.Size = new System.Drawing.Size(33, 28);
            this.MAPPolicyFileButton.TabIndex = 5;
            this.MAPPolicyFileButton.Text = "...";
            this.MAPPolicyFileButton.UseVisualStyleBackColor = true;
            this.MAPPolicyFileButton.Click += new System.EventHandler(this.MAPPolicyFileButton_Click);
            // 
            // SettingsTabPage
            // 
            this.SettingsTabPage.Controls.Add(this.SaveButton);
            this.SettingsTabPage.Controls.Add(this.FilePrefixText);
            this.SettingsTabPage.Controls.Add(this.FilePrefixLabel);
            this.SettingsTabPage.Controls.Add(this.DCCodeText);
            this.SettingsTabPage.Controls.Add(this.DCCodeLabel);
            this.SettingsTabPage.Controls.Add(this.WarehouseLocationText);
            this.SettingsTabPage.Controls.Add(this.WarehouseLocationLabel);
            this.SettingsTabPage.Controls.Add(this.SupplierCodeLabel);
            this.SettingsTabPage.Controls.Add(this.SupplierCodeText);
            this.SettingsTabPage.Controls.Add(this.SellerCostAdditionText);
            this.SettingsTabPage.Controls.Add(this.SellerCostAdditionLabel);
            this.SettingsTabPage.Controls.Add(this.ImageURLsText);
            this.SettingsTabPage.Controls.Add(this.ImageURLsLabel);
            this.SettingsTabPage.Controls.Add(this.CAStoreTitleLengthText);
            this.SettingsTabPage.Controls.Add(this.CAStoreTitleLengthLabel);
            this.SettingsTabPage.Controls.Add(this.AuctionTitleText);
            this.SettingsTabPage.Controls.Add(this.AuctionTitleLengthLabel);
            this.SettingsTabPage.Location = new System.Drawing.Point(4, 25);
            this.SettingsTabPage.Name = "SettingsTabPage";
            this.SettingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTabPage.Size = new System.Drawing.Size(562, 448);
            this.SettingsTabPage.TabIndex = 1;
            this.SettingsTabPage.Text = "Settings";
            this.SettingsTabPage.UseVisualStyleBackColor = true;
            this.SettingsTabPage.Enter += new System.EventHandler(this.SettingsTabPage_Enter);
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(244, 408);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 18;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // FilePrefixText
            // 
            this.FilePrefixText.Location = new System.Drawing.Point(66, 365);
            this.FilePrefixText.Name = "FilePrefixText";
            this.FilePrefixText.Size = new System.Drawing.Size(161, 23);
            this.FilePrefixText.TabIndex = 15;
            // 
            // FilePrefixLabel
            // 
            this.FilePrefixLabel.AutoSize = true;
            this.FilePrefixLabel.Location = new System.Drawing.Point(63, 345);
            this.FilePrefixLabel.Name = "FilePrefixLabel";
            this.FilePrefixLabel.Size = new System.Drawing.Size(77, 16);
            this.FilePrefixLabel.TabIndex = 14;
            this.FilePrefixLabel.Text = "File Prefix:";
            // 
            // DCCodeText
            // 
            this.DCCodeText.Location = new System.Drawing.Point(60, 304);
            this.DCCodeText.Name = "DCCodeText";
            this.DCCodeText.Size = new System.Drawing.Size(167, 23);
            this.DCCodeText.TabIndex = 13;
            // 
            // DCCodeLabel
            // 
            this.DCCodeLabel.AutoSize = true;
            this.DCCodeLabel.Location = new System.Drawing.Point(60, 285);
            this.DCCodeLabel.Name = "DCCodeLabel";
            this.DCCodeLabel.Size = new System.Drawing.Size(70, 16);
            this.DCCodeLabel.TabIndex = 12;
            this.DCCodeLabel.Text = "DC Code:";
            // 
            // WarehouseLocationText
            // 
            this.WarehouseLocationText.Location = new System.Drawing.Point(339, 243);
            this.WarehouseLocationText.Name = "WarehouseLocationText";
            this.WarehouseLocationText.Size = new System.Drawing.Size(167, 23);
            this.WarehouseLocationText.TabIndex = 11;
            // 
            // WarehouseLocationLabel
            // 
            this.WarehouseLocationLabel.AutoSize = true;
            this.WarehouseLocationLabel.Location = new System.Drawing.Point(336, 224);
            this.WarehouseLocationLabel.Name = "WarehouseLocationLabel";
            this.WarehouseLocationLabel.Size = new System.Drawing.Size(148, 16);
            this.WarehouseLocationLabel.TabIndex = 10;
            this.WarehouseLocationLabel.Text = "Warehouse Location:";
            // 
            // SupplierCodeLabel
            // 
            this.SupplierCodeLabel.AutoSize = true;
            this.SupplierCodeLabel.Location = new System.Drawing.Point(57, 224);
            this.SupplierCodeLabel.Name = "SupplierCodeLabel";
            this.SupplierCodeLabel.Size = new System.Drawing.Size(104, 16);
            this.SupplierCodeLabel.TabIndex = 9;
            this.SupplierCodeLabel.Text = "Supplier Code:";
            // 
            // SupplierCodeText
            // 
            this.SupplierCodeText.Location = new System.Drawing.Point(60, 243);
            this.SupplierCodeText.Name = "SupplierCodeText";
            this.SupplierCodeText.Size = new System.Drawing.Size(167, 23);
            this.SupplierCodeText.TabIndex = 8;
            // 
            // SellerCostAdditionText
            // 
            this.SellerCostAdditionText.Location = new System.Drawing.Point(60, 179);
            this.SellerCostAdditionText.Name = "SellerCostAdditionText";
            this.SellerCostAdditionText.Size = new System.Drawing.Size(100, 23);
            this.SellerCostAdditionText.TabIndex = 7;
            // 
            // SellerCostAdditionLabel
            // 
            this.SellerCostAdditionLabel.AutoSize = true;
            this.SellerCostAdditionLabel.Location = new System.Drawing.Point(57, 160);
            this.SellerCostAdditionLabel.Name = "SellerCostAdditionLabel";
            this.SellerCostAdditionLabel.Size = new System.Drawing.Size(143, 16);
            this.SellerCostAdditionLabel.TabIndex = 6;
            this.SellerCostAdditionLabel.Text = "Seller Cost Addition:";
            // 
            // ImageURLsText
            // 
            this.ImageURLsText.Location = new System.Drawing.Point(60, 110);
            this.ImageURLsText.Name = "ImageURLsText";
            this.ImageURLsText.Size = new System.Drawing.Size(446, 23);
            this.ImageURLsText.TabIndex = 5;
            // 
            // ImageURLsLabel
            // 
            this.ImageURLsLabel.AutoSize = true;
            this.ImageURLsLabel.Location = new System.Drawing.Point(57, 91);
            this.ImageURLsLabel.Name = "ImageURLsLabel";
            this.ImageURLsLabel.Size = new System.Drawing.Size(90, 16);
            this.ImageURLsLabel.TabIndex = 4;
            this.ImageURLsLabel.Text = "Image URLs:";
            // 
            // CAStoreTitleLengthText
            // 
            this.CAStoreTitleLengthText.Location = new System.Drawing.Point(339, 47);
            this.CAStoreTitleLengthText.Name = "CAStoreTitleLengthText";
            this.CAStoreTitleLengthText.Size = new System.Drawing.Size(100, 23);
            this.CAStoreTitleLengthText.TabIndex = 3;
            // 
            // CAStoreTitleLengthLabel
            // 
            this.CAStoreTitleLengthLabel.AutoSize = true;
            this.CAStoreTitleLengthLabel.Location = new System.Drawing.Point(336, 28);
            this.CAStoreTitleLengthLabel.Name = "CAStoreTitleLengthLabel";
            this.CAStoreTitleLengthLabel.Size = new System.Drawing.Size(157, 16);
            this.CAStoreTitleLengthLabel.TabIndex = 2;
            this.CAStoreTitleLengthLabel.Text = "CA Store Title Length:";
            // 
            // AuctionTitleText
            // 
            this.AuctionTitleText.Location = new System.Drawing.Point(60, 47);
            this.AuctionTitleText.Name = "AuctionTitleText";
            this.AuctionTitleText.Size = new System.Drawing.Size(100, 23);
            this.AuctionTitleText.TabIndex = 1;
            // 
            // AuctionTitleLengthLabel
            // 
            this.AuctionTitleLengthLabel.AutoSize = true;
            this.AuctionTitleLengthLabel.Location = new System.Drawing.Point(57, 28);
            this.AuctionTitleLengthLabel.Name = "AuctionTitleLengthLabel";
            this.AuctionTitleLengthLabel.Size = new System.Drawing.Size(148, 16);
            this.AuctionTitleLengthLabel.TabIndex = 0;
            this.AuctionTitleLengthLabel.Text = "Auction Title Length:";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(507, 513);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // PicnicTimeExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 550);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.TabPages);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PicnicTimeExtractor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Picnic Time Extractor";
            this.TabPages.ResumeLayout(false);
            this.ExtractorTabPage.ResumeLayout(false);
            this.ExtractorTabPage.PerformLayout();
            this.SettingsTabPage.ResumeLayout(false);
            this.SettingsTabPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl TabPages;
        private System.Windows.Forms.TabPage ExtractorTabPage;
        private System.Windows.Forms.TabPage SettingsTabPage;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.TextBox ProductDataFileText;
        private System.Windows.Forms.Label OutputFolderLabel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox MAPPolicyFileText;
        private System.Windows.Forms.Button OutputFolderButton;
        private System.Windows.Forms.Button ProductDataFileButton;
        private System.Windows.Forms.TextBox OutputFolderText;
        private System.Windows.Forms.Label ProductDataFileLabel;
        private System.Windows.Forms.Label MAPPolicyFileLabel;
        private System.Windows.Forms.Button MAPPolicyFileButton;
        private System.Windows.Forms.TextBox CAStoreTitleLengthText;
        private System.Windows.Forms.Label CAStoreTitleLengthLabel;
        private System.Windows.Forms.TextBox AuctionTitleText;
        private System.Windows.Forms.Label AuctionTitleLengthLabel;
        private System.Windows.Forms.TextBox ImageURLsText;
        private System.Windows.Forms.Label ImageURLsLabel;
        private System.Windows.Forms.TextBox SellerCostAdditionText;
        private System.Windows.Forms.Label SellerCostAdditionLabel;
        private System.Windows.Forms.Label SupplierCodeLabel;
        private System.Windows.Forms.TextBox SupplierCodeText;
        private System.Windows.Forms.TextBox WarehouseLocationText;
        private System.Windows.Forms.Label WarehouseLocationLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.TextBox FilePrefixText;
        private System.Windows.Forms.Label FilePrefixLabel;
        private System.Windows.Forms.TextBox DCCodeText;
        private System.Windows.Forms.Label DCCodeLabel;
        private System.Windows.Forms.RichTextBox SKUsList;
        private System.Windows.Forms.RadioButton scrapeSKUs;
        private System.Windows.Forms.RadioButton scrapeAll;
    }
}