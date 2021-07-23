namespace ChannelAdvisor
{
    partial class EMGExtractor
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
            this.StartButton = new System.Windows.Forms.Button();
            this.EMGExtractorTabControl = new System.Windows.Forms.TabControl();
            this.EMGExtractorTab = new System.Windows.Forms.TabPage();
            this.SelectFolderLabel = new System.Windows.Forms.Label();
            this.SelectFolderButton = new System.Windows.Forms.Button();
            this.SelectFolderText = new System.Windows.Forms.TextBox();
            this.SKURichTextBox = new System.Windows.Forms.RichTextBox();
            this.UPCRadioButton = new System.Windows.Forms.RadioButton();
            this.SKURadioButton = new System.Windows.Forms.RadioButton();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.URLText = new System.Windows.Forms.TextBox();
            this.URLLabel = new System.Windows.Forms.Label();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.FilePrefixText = new System.Windows.Forms.TextBox();
            this.FilePrefixLabel = new System.Windows.Forms.Label();
            this.DCCodeText = new System.Windows.Forms.TextBox();
            this.DCCodeLabel = new System.Windows.Forms.Label();
            this.WarehouseLocationText = new System.Windows.Forms.TextBox();
            this.SupplierCodeText = new System.Windows.Forms.TextBox();
            this.WarehouseLoctionLabel = new System.Windows.Forms.Label();
            this.SupplierCodeLabel = new System.Windows.Forms.Label();
            this.MaxClassificationLengthText = new System.Windows.Forms.TextBox();
            this.MaxClassificationLengthLabel = new System.Windows.Forms.Label();
            this.DefaultWarrantyValueText = new System.Windows.Forms.TextBox();
            this.WarrantyLabelText = new System.Windows.Forms.TextBox();
            this.DefaultWarrantyValueLabel = new System.Windows.Forms.Label();
            this.WarrantyLabelLabel = new System.Windows.Forms.Label();
            this.MaxCAStoreTitleLengthText = new System.Windows.Forms.TextBox();
            this.MaxCAStoreTitleLengthLabel = new System.Windows.Forms.Label();
            this.MaxTitleLengthText = new System.Windows.Forms.TextBox();
            this.MaxTitleLengthLabel = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.fromFileCheckBox = new System.Windows.Forms.CheckBox();
            this.selectedFileText = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.fileDialog = new System.Windows.Forms.OpenFileDialog();
            this.wholeFileRadio = new System.Windows.Forms.RadioButton();
            this.skusRadio = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EMGExtractorTabControl.SuspendLayout();
            this.EMGExtractorTab.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(166, 400);
            this.StartButton.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(105, 19);
            this.StartButton.TabIndex = 11;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // EMGExtractorTabControl
            // 
            this.EMGExtractorTabControl.Controls.Add(this.EMGExtractorTab);
            this.EMGExtractorTabControl.Controls.Add(this.SettingsTab);
            this.EMGExtractorTabControl.Location = new System.Drawing.Point(13, 11);
            this.EMGExtractorTabControl.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EMGExtractorTabControl.Name = "EMGExtractorTabControl";
            this.EMGExtractorTabControl.SelectedIndex = 0;
            this.EMGExtractorTabControl.Size = new System.Drawing.Size(426, 449);
            this.EMGExtractorTabControl.TabIndex = 2;
            this.EMGExtractorTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.EMGExtractorTabControl_Selected);
            // 
            // EMGExtractorTab
            // 
            this.EMGExtractorTab.Controls.Add(this.groupBox1);
            this.EMGExtractorTab.Controls.Add(this.skusRadio);
            this.EMGExtractorTab.Controls.Add(this.wholeFileRadio);
            this.EMGExtractorTab.Controls.Add(this.selectFileButton);
            this.EMGExtractorTab.Controls.Add(this.selectedFileText);
            this.EMGExtractorTab.Controls.Add(this.fromFileCheckBox);
            this.EMGExtractorTab.Controls.Add(this.SelectFolderLabel);
            this.EMGExtractorTab.Controls.Add(this.SelectFolderButton);
            this.EMGExtractorTab.Controls.Add(this.SelectFolderText);
            this.EMGExtractorTab.Controls.Add(this.SKURichTextBox);
            this.EMGExtractorTab.Controls.Add(this.StartButton);
            this.EMGExtractorTab.Location = new System.Drawing.Point(4, 22);
            this.EMGExtractorTab.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EMGExtractorTab.Name = "EMGExtractorTab";
            this.EMGExtractorTab.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.EMGExtractorTab.Size = new System.Drawing.Size(418, 423);
            this.EMGExtractorTab.TabIndex = 0;
            this.EMGExtractorTab.Text = "EMG Extractor";
            this.EMGExtractorTab.UseVisualStyleBackColor = true;
            // 
            // SelectFolderLabel
            // 
            this.SelectFolderLabel.AutoSize = true;
            this.SelectFolderLabel.Location = new System.Drawing.Point(22, 343);
            this.SelectFolderLabel.Name = "SelectFolderLabel";
            this.SelectFolderLabel.Size = new System.Drawing.Size(114, 13);
            this.SelectFolderLabel.TabIndex = 8;
            this.SelectFolderLabel.Text = "Folder to save file:";
            // 
            // SelectFolderButton
            // 
            this.SelectFolderButton.Location = new System.Drawing.Point(374, 359);
            this.SelectFolderButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectFolderButton.Name = "SelectFolderButton";
            this.SelectFolderButton.Size = new System.Drawing.Size(27, 21);
            this.SelectFolderButton.TabIndex = 10;
            this.SelectFolderButton.Text = "...";
            this.SelectFolderButton.UseVisualStyleBackColor = true;
            this.SelectFolderButton.Click += new System.EventHandler(this.SelectFolderButton_Click);
            // 
            // SelectFolderText
            // 
            this.SelectFolderText.Location = new System.Drawing.Point(7, 359);
            this.SelectFolderText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SelectFolderText.Name = "SelectFolderText";
            this.SelectFolderText.ReadOnly = true;
            this.SelectFolderText.Size = new System.Drawing.Size(362, 21);
            this.SelectFolderText.TabIndex = 9;
            // 
            // SKURichTextBox
            // 
            this.SKURichTextBox.Enabled = false;
            this.SKURichTextBox.Location = new System.Drawing.Point(7, 188);
            this.SKURichTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SKURichTextBox.Name = "SKURichTextBox";
            this.SKURichTextBox.Size = new System.Drawing.Size(404, 143);
            this.SKURichTextBox.TabIndex = 8;
            this.SKURichTextBox.Text = "";
            // 
            // UPCRadioButton
            // 
            this.UPCRadioButton.AutoSize = true;
            this.UPCRadioButton.Location = new System.Drawing.Point(3, 40);
            this.UPCRadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.UPCRadioButton.Name = "UPCRadioButton";
            this.UPCRadioButton.Size = new System.Drawing.Size(232, 17);
            this.UPCRadioButton.TabIndex = 5;
            this.UPCRadioButton.TabStop = true;
            this.UPCRadioButton.Text = "Use EMG UPCs as inventory number";
            this.UPCRadioButton.UseVisualStyleBackColor = true;
            this.UPCRadioButton.CheckedChanged += new System.EventHandler(this.UPCRadioButton_CheckedChanged);
            // 
            // SKURadioButton
            // 
            this.SKURadioButton.AutoSize = true;
            this.SKURadioButton.Location = new System.Drawing.Point(3, 16);
            this.SKURadioButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SKURadioButton.Name = "SKURadioButton";
            this.SKURadioButton.Size = new System.Drawing.Size(232, 17);
            this.SKURadioButton.TabIndex = 4;
            this.SKURadioButton.TabStop = true;
            this.SKURadioButton.Text = "Use EMG SKUs as inventory number";
            this.SKURadioButton.UseVisualStyleBackColor = true;
            this.SKURadioButton.CheckedChanged += new System.EventHandler(this.SKURadioButton_CheckedChanged);
            // 
            // SettingsTab
            // 
            this.SettingsTab.Controls.Add(this.URLText);
            this.SettingsTab.Controls.Add(this.URLLabel);
            this.SettingsTab.Controls.Add(this.SaveSettingsButton);
            this.SettingsTab.Controls.Add(this.FilePrefixText);
            this.SettingsTab.Controls.Add(this.FilePrefixLabel);
            this.SettingsTab.Controls.Add(this.DCCodeText);
            this.SettingsTab.Controls.Add(this.DCCodeLabel);
            this.SettingsTab.Controls.Add(this.WarehouseLocationText);
            this.SettingsTab.Controls.Add(this.SupplierCodeText);
            this.SettingsTab.Controls.Add(this.WarehouseLoctionLabel);
            this.SettingsTab.Controls.Add(this.SupplierCodeLabel);
            this.SettingsTab.Controls.Add(this.MaxClassificationLengthText);
            this.SettingsTab.Controls.Add(this.MaxClassificationLengthLabel);
            this.SettingsTab.Controls.Add(this.DefaultWarrantyValueText);
            this.SettingsTab.Controls.Add(this.WarrantyLabelText);
            this.SettingsTab.Controls.Add(this.DefaultWarrantyValueLabel);
            this.SettingsTab.Controls.Add(this.WarrantyLabelLabel);
            this.SettingsTab.Controls.Add(this.MaxCAStoreTitleLengthText);
            this.SettingsTab.Controls.Add(this.MaxCAStoreTitleLengthLabel);
            this.SettingsTab.Controls.Add(this.MaxTitleLengthText);
            this.SettingsTab.Controls.Add(this.MaxTitleLengthLabel);
            this.SettingsTab.Location = new System.Drawing.Point(4, 25);
            this.SettingsTab.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.SettingsTab.Size = new System.Drawing.Size(419, 407);
            this.SettingsTab.TabIndex = 1;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.UseVisualStyleBackColor = true;
            // 
            // URLText
            // 
            this.URLText.Location = new System.Drawing.Point(9, 32);
            this.URLText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.URLText.MaxLength = 100;
            this.URLText.Name = "URLText";
            this.URLText.Size = new System.Drawing.Size(361, 21);
            this.URLText.TabIndex = 20;
            // 
            // URLLabel
            // 
            this.URLLabel.AutoSize = true;
            this.URLLabel.Location = new System.Drawing.Point(6, 16);
            this.URLLabel.Name = "URLLabel";
            this.URLLabel.Size = new System.Drawing.Size(34, 13);
            this.URLLabel.TabIndex = 19;
            this.URLLabel.Text = "URL:";
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(176, 389);
            this.SaveSettingsButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(66, 19);
            this.SaveSettingsButton.TabIndex = 18;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // FilePrefixText
            // 
            this.FilePrefixText.Location = new System.Drawing.Point(8, 344);
            this.FilePrefixText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FilePrefixText.MaxLength = 20;
            this.FilePrefixText.Name = "FilePrefixText";
            this.FilePrefixText.Size = new System.Drawing.Size(152, 21);
            this.FilePrefixText.TabIndex = 17;
            // 
            // FilePrefixLabel
            // 
            this.FilePrefixLabel.AutoSize = true;
            this.FilePrefixLabel.Location = new System.Drawing.Point(5, 329);
            this.FilePrefixLabel.Name = "FilePrefixLabel";
            this.FilePrefixLabel.Size = new System.Drawing.Size(68, 13);
            this.FilePrefixLabel.TabIndex = 16;
            this.FilePrefixLabel.Text = "File Prefix:";
            // 
            // DCCodeText
            // 
            this.DCCodeText.Location = new System.Drawing.Point(5, 292);
            this.DCCodeText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DCCodeText.MaxLength = 30;
            this.DCCodeText.Name = "DCCodeText";
            this.DCCodeText.Size = new System.Drawing.Size(154, 21);
            this.DCCodeText.TabIndex = 15;
            // 
            // DCCodeLabel
            // 
            this.DCCodeLabel.AutoSize = true;
            this.DCCodeLabel.Location = new System.Drawing.Point(3, 277);
            this.DCCodeLabel.Name = "DCCodeLabel";
            this.DCCodeLabel.Size = new System.Drawing.Size(64, 13);
            this.DCCodeLabel.TabIndex = 14;
            this.DCCodeLabel.Text = "DC Code:";
            // 
            // WarehouseLocationText
            // 
            this.WarehouseLocationText.Location = new System.Drawing.Point(214, 240);
            this.WarehouseLocationText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WarehouseLocationText.MaxLength = 30;
            this.WarehouseLocationText.Name = "WarehouseLocationText";
            this.WarehouseLocationText.Size = new System.Drawing.Size(154, 21);
            this.WarehouseLocationText.TabIndex = 13;
            // 
            // SupplierCodeText
            // 
            this.SupplierCodeText.Location = new System.Drawing.Point(5, 240);
            this.SupplierCodeText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SupplierCodeText.MaxLength = 30;
            this.SupplierCodeText.Name = "SupplierCodeText";
            this.SupplierCodeText.Size = new System.Drawing.Size(154, 21);
            this.SupplierCodeText.TabIndex = 12;
            // 
            // WarehouseLoctionLabel
            // 
            this.WarehouseLoctionLabel.AutoSize = true;
            this.WarehouseLoctionLabel.Location = new System.Drawing.Point(212, 225);
            this.WarehouseLoctionLabel.Name = "WarehouseLoctionLabel";
            this.WarehouseLoctionLabel.Size = new System.Drawing.Size(127, 13);
            this.WarehouseLoctionLabel.TabIndex = 11;
            this.WarehouseLoctionLabel.Text = "Warehouse Location:";
            // 
            // SupplierCodeLabel
            // 
            this.SupplierCodeLabel.AutoSize = true;
            this.SupplierCodeLabel.Location = new System.Drawing.Point(3, 225);
            this.SupplierCodeLabel.Name = "SupplierCodeLabel";
            this.SupplierCodeLabel.Size = new System.Drawing.Size(93, 13);
            this.SupplierCodeLabel.TabIndex = 10;
            this.SupplierCodeLabel.Text = "Supplier Code:";
            // 
            // MaxClassificationLengthText
            // 
            this.MaxClassificationLengthText.Location = new System.Drawing.Point(5, 136);
            this.MaxClassificationLengthText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxClassificationLengthText.MaxLength = 3;
            this.MaxClassificationLengthText.Name = "MaxClassificationLengthText";
            this.MaxClassificationLengthText.Size = new System.Drawing.Size(88, 21);
            this.MaxClassificationLengthText.TabIndex = 9;
            // 
            // MaxClassificationLengthLabel
            // 
            this.MaxClassificationLengthLabel.AutoSize = true;
            this.MaxClassificationLengthLabel.Location = new System.Drawing.Point(3, 120);
            this.MaxClassificationLengthLabel.Name = "MaxClassificationLengthLabel";
            this.MaxClassificationLengthLabel.Size = new System.Drawing.Size(129, 13);
            this.MaxClassificationLengthLabel.TabIndex = 8;
            this.MaxClassificationLengthLabel.Text = "Classification Length:";
            // 
            // DefaultWarrantyValueText
            // 
            this.DefaultWarrantyValueText.Location = new System.Drawing.Point(214, 188);
            this.DefaultWarrantyValueText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.DefaultWarrantyValueText.MaxLength = 50;
            this.DefaultWarrantyValueText.Name = "DefaultWarrantyValueText";
            this.DefaultWarrantyValueText.Size = new System.Drawing.Size(154, 21);
            this.DefaultWarrantyValueText.TabIndex = 7;
            // 
            // WarrantyLabelText
            // 
            this.WarrantyLabelText.Location = new System.Drawing.Point(5, 188);
            this.WarrantyLabelText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.WarrantyLabelText.MaxLength = 50;
            this.WarrantyLabelText.Name = "WarrantyLabelText";
            this.WarrantyLabelText.Size = new System.Drawing.Size(154, 21);
            this.WarrantyLabelText.TabIndex = 6;
            // 
            // DefaultWarrantyValueLabel
            // 
            this.DefaultWarrantyValueLabel.AutoSize = true;
            this.DefaultWarrantyValueLabel.Location = new System.Drawing.Point(212, 173);
            this.DefaultWarrantyValueLabel.Name = "DefaultWarrantyValueLabel";
            this.DefaultWarrantyValueLabel.Size = new System.Drawing.Size(146, 13);
            this.DefaultWarrantyValueLabel.TabIndex = 5;
            this.DefaultWarrantyValueLabel.Text = "Default Warranty Value:";
            // 
            // WarrantyLabelLabel
            // 
            this.WarrantyLabelLabel.AutoSize = true;
            this.WarrantyLabelLabel.Location = new System.Drawing.Point(3, 173);
            this.WarrantyLabelLabel.Name = "WarrantyLabelLabel";
            this.WarrantyLabelLabel.Size = new System.Drawing.Size(99, 13);
            this.WarrantyLabelLabel.TabIndex = 4;
            this.WarrantyLabelLabel.Text = "Warranty Label:";
            // 
            // MaxCAStoreTitleLengthText
            // 
            this.MaxCAStoreTitleLengthText.Location = new System.Drawing.Point(214, 84);
            this.MaxCAStoreTitleLengthText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxCAStoreTitleLengthText.MaxLength = 3;
            this.MaxCAStoreTitleLengthText.Name = "MaxCAStoreTitleLengthText";
            this.MaxCAStoreTitleLengthText.Size = new System.Drawing.Size(88, 21);
            this.MaxCAStoreTitleLengthText.TabIndex = 3;
            // 
            // MaxCAStoreTitleLengthLabel
            // 
            this.MaxCAStoreTitleLengthLabel.AutoSize = true;
            this.MaxCAStoreTitleLengthLabel.Location = new System.Drawing.Point(212, 68);
            this.MaxCAStoreTitleLengthLabel.Name = "MaxCAStoreTitleLengthLabel";
            this.MaxCAStoreTitleLengthLabel.Size = new System.Drawing.Size(134, 13);
            this.MaxCAStoreTitleLengthLabel.TabIndex = 2;
            this.MaxCAStoreTitleLengthLabel.Text = "CA Store Title Length:";
            // 
            // MaxTitleLengthText
            // 
            this.MaxTitleLengthText.Location = new System.Drawing.Point(5, 84);
            this.MaxTitleLengthText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaxTitleLengthText.MaxLength = 3;
            this.MaxTitleLengthText.Name = "MaxTitleLengthText";
            this.MaxTitleLengthText.Size = new System.Drawing.Size(88, 21);
            this.MaxTitleLengthText.TabIndex = 1;
            // 
            // MaxTitleLengthLabel
            // 
            this.MaxTitleLengthLabel.AutoSize = true;
            this.MaxTitleLengthLabel.Location = new System.Drawing.Point(3, 68);
            this.MaxTitleLengthLabel.Name = "MaxTitleLengthLabel";
            this.MaxTitleLengthLabel.Size = new System.Drawing.Size(124, 13);
            this.MaxTitleLengthLabel.TabIndex = 0;
            this.MaxTitleLengthLabel.Text = "Auction Title Length:";
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(369, 464);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(66, 19);
            this.CloseButton.TabIndex = 12;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // fromFileCheckBox
            // 
            this.fromFileCheckBox.AutoSize = true;
            this.fromFileCheckBox.Checked = true;
            this.fromFileCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.fromFileCheckBox.Location = new System.Drawing.Point(8, 20);
            this.fromFileCheckBox.Name = "fromFileCheckBox";
            this.fromFileCheckBox.Size = new System.Drawing.Size(147, 17);
            this.fromFileCheckBox.TabIndex = 1;
            this.fromFileCheckBox.Text = "Extract data from file";
            this.fromFileCheckBox.UseVisualStyleBackColor = true;
            this.fromFileCheckBox.CheckedChanged += new System.EventHandler(this.fromFileCheckBox_CheckedChanged);
            // 
            // selectedFileText
            // 
            this.selectedFileText.Location = new System.Drawing.Point(7, 43);
            this.selectedFileText.Name = "selectedFileText";
            this.selectedFileText.ReadOnly = true;
            this.selectedFileText.Size = new System.Drawing.Size(362, 21);
            this.selectedFileText.TabIndex = 2;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Location = new System.Drawing.Point(374, 41);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(27, 23);
            this.selectFileButton.TabIndex = 3;
            this.selectFileButton.Text = "...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // fileDialog
            // 
            this.fileDialog.FileName = "openFileDialog1";
            // 
            // wholeFileRadio
            // 
            this.wholeFileRadio.AutoSize = true;
            this.wholeFileRadio.Checked = true;
            this.wholeFileRadio.Location = new System.Drawing.Point(8, 143);
            this.wholeFileRadio.Name = "wholeFileRadio";
            this.wholeFileRadio.Size = new System.Drawing.Size(81, 17);
            this.wholeFileRadio.TabIndex = 6;
            this.wholeFileRadio.TabStop = true;
            this.wholeFileRadio.Text = "Whole file";
            this.wholeFileRadio.UseVisualStyleBackColor = true;
            this.wholeFileRadio.CheckedChanged += new System.EventHandler(this.wholeFileRadio_CheckedChanged);
            // 
            // skusRadio
            // 
            this.skusRadio.AutoSize = true;
            this.skusRadio.Location = new System.Drawing.Point(8, 166);
            this.skusRadio.Name = "skusRadio";
            this.skusRadio.Size = new System.Drawing.Size(70, 17);
            this.skusRadio.TabIndex = 7;
            this.skusRadio.Text = "SKU(s):";
            this.skusRadio.UseVisualStyleBackColor = true;
            this.skusRadio.CheckedChanged += new System.EventHandler(this.skusRadio_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SKURadioButton);
            this.groupBox1.Controls.Add(this.UPCRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(8, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(403, 67);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // EMGExtractor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(452, 492);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.EMGExtractorTabControl);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EMGExtractor";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMG Extractor";
            this.EMGExtractorTabControl.ResumeLayout(false);
            this.EMGExtractorTab.ResumeLayout(false);
            this.EMGExtractorTab.PerformLayout();
            this.SettingsTab.ResumeLayout(false);
            this.SettingsTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TabControl EMGExtractorTabControl;
        private System.Windows.Forms.TabPage EMGExtractorTab;
        private System.Windows.Forms.TabPage SettingsTab;
        private System.Windows.Forms.RadioButton UPCRadioButton;
        private System.Windows.Forms.RadioButton SKURadioButton;
        private System.Windows.Forms.RichTextBox SKURichTextBox;
        private System.Windows.Forms.Label MaxTitleLengthLabel;
        private System.Windows.Forms.TextBox MaxTitleLengthText;
        private System.Windows.Forms.Label MaxCAStoreTitleLengthLabel;
        private System.Windows.Forms.TextBox DefaultWarrantyValueText;
        private System.Windows.Forms.TextBox WarrantyLabelText;
        private System.Windows.Forms.Label DefaultWarrantyValueLabel;
        private System.Windows.Forms.Label WarrantyLabelLabel;
        private System.Windows.Forms.TextBox MaxCAStoreTitleLengthText;
        private System.Windows.Forms.Label MaxClassificationLengthLabel;
        private System.Windows.Forms.TextBox MaxClassificationLengthText;
        private System.Windows.Forms.TextBox DCCodeText;
        private System.Windows.Forms.Label DCCodeLabel;
        private System.Windows.Forms.TextBox WarehouseLocationText;
        private System.Windows.Forms.TextBox SupplierCodeText;
        private System.Windows.Forms.Label WarehouseLoctionLabel;
        private System.Windows.Forms.Label SupplierCodeLabel;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.TextBox FilePrefixText;
        private System.Windows.Forms.Label FilePrefixLabel;
        private System.Windows.Forms.TextBox URLText;
        private System.Windows.Forms.Label URLLabel;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label SelectFolderLabel;
        private System.Windows.Forms.Button SelectFolderButton;
        private System.Windows.Forms.TextBox SelectFolderText;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.TextBox selectedFileText;
        private System.Windows.Forms.CheckBox fromFileCheckBox;
        private System.Windows.Forms.OpenFileDialog fileDialog;
        private System.Windows.Forms.RadioButton skusRadio;
        private System.Windows.Forms.RadioButton wholeFileRadio;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}