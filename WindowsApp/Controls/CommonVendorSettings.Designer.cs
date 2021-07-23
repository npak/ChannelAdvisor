namespace ChannelAdvisor
{
    partial class CommonVendorSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.settingsGroup = new System.Windows.Forms.GroupBox();
            this.fileArchiveButton = new System.Windows.Forms.Button();
            this.folderButton = new System.Windows.Forms.Button();
            this.fileArchiveText = new System.Windows.Forms.TextBox();
            this.folderText = new System.Windows.Forms.TextBox();
            this.outOfStockQuantityText = new ChannelAdvisor.NullableTextBox();
            this.outOfStockQuantityLabel = new System.Windows.Forms.Label();
            this.outOfStockThresholdText = new ChannelAdvisor.NullableTextBox();
            this.outOfStockThresholdLabel = new System.Windows.Forms.Label();
            this.prefixName = new System.Windows.Forms.TextBox();
            this.prefixLabel = new System.Windows.Forms.Label();
            this.setOutOfStock = new System.Windows.Forms.CheckBox();
            this.fileArchiveLabel = new System.Windows.Forms.Label();
            this.folderLabel = new System.Windows.Forms.Label();
            this.nameText = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.commonSettingsErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.commonSettingsFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.TextBox();
            this.settingsGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commonSettingsErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // settingsGroup
            // 
            this.settingsGroup.Controls.Add(this.labelText);
            this.settingsGroup.Controls.Add(this.label);
            this.settingsGroup.Controls.Add(this.fileArchiveButton);
            this.settingsGroup.Controls.Add(this.folderButton);
            this.settingsGroup.Controls.Add(this.fileArchiveText);
            this.settingsGroup.Controls.Add(this.folderText);
            this.settingsGroup.Controls.Add(this.outOfStockQuantityText);
            this.settingsGroup.Controls.Add(this.outOfStockQuantityLabel);
            this.settingsGroup.Controls.Add(this.outOfStockThresholdText);
            this.settingsGroup.Controls.Add(this.outOfStockThresholdLabel);
            this.settingsGroup.Controls.Add(this.prefixName);
            this.settingsGroup.Controls.Add(this.prefixLabel);
            this.settingsGroup.Controls.Add(this.setOutOfStock);
            this.settingsGroup.Controls.Add(this.fileArchiveLabel);
            this.settingsGroup.Controls.Add(this.folderLabel);
            this.settingsGroup.Controls.Add(this.nameText);
            this.settingsGroup.Controls.Add(this.nameLabel);
            this.settingsGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.settingsGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingsGroup.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.settingsGroup.Location = new System.Drawing.Point(0, 0);
            this.settingsGroup.Name = "settingsGroup";
            this.settingsGroup.Size = new System.Drawing.Size(550, 230);
            this.settingsGroup.TabIndex = 0;
            this.settingsGroup.TabStop = false;
            this.settingsGroup.Text = "Common vendor settings";
            // 
            // fileArchiveButton
            // 
            this.fileArchiveButton.Location = new System.Drawing.Point(511, 89);
            this.fileArchiveButton.Name = "fileArchiveButton";
            this.fileArchiveButton.Size = new System.Drawing.Size(30, 23);
            this.fileArchiveButton.TabIndex = 5;
            this.fileArchiveButton.Text = "...";
            this.fileArchiveButton.UseVisualStyleBackColor = true;
            this.fileArchiveButton.Click += new System.EventHandler(this.fileArchiveButton_Click);
            // 
            // folderButton
            // 
            this.folderButton.Location = new System.Drawing.Point(511, 57);
            this.folderButton.Name = "folderButton";
            this.folderButton.Size = new System.Drawing.Size(31, 23);
            this.folderButton.TabIndex = 3;
            this.folderButton.Text = "...";
            this.folderButton.UseVisualStyleBackColor = true;
            this.folderButton.Click += new System.EventHandler(this.folderButton_Click);
            // 
            // fileArchiveText
            // 
            this.fileArchiveText.Location = new System.Drawing.Point(108, 90);
            this.fileArchiveText.Name = "fileArchiveText";
            this.fileArchiveText.ReadOnly = true;
            this.fileArchiveText.Size = new System.Drawing.Size(398, 22);
            this.fileArchiveText.TabIndex = 4;
            // 
            // folderText
            // 
            this.folderText.Location = new System.Drawing.Point(108, 58);
            this.folderText.Name = "folderText";
            this.folderText.ReadOnly = true;
            this.folderText.Size = new System.Drawing.Size(398, 22);
            this.folderText.TabIndex = 2;
            // 
            // outOfStockQuantityText
            // 
            this.outOfStockQuantityText.Location = new System.Drawing.Point(439, 190);
            this.outOfStockQuantityText.Name = "outOfStockQuantityText";
            this.outOfStockQuantityText.Size = new System.Drawing.Size(80, 22);
            this.outOfStockQuantityText.TabIndex = 10;
            // 
            // outOfStockQuantityLabel
            // 
            this.outOfStockQuantityLabel.AutoSize = true;
            this.outOfStockQuantityLabel.Location = new System.Drawing.Point(289, 193);
            this.outOfStockQuantityLabel.Name = "outOfStockQuantityLabel";
            this.outOfStockQuantityLabel.Size = new System.Drawing.Size(144, 14);
            this.outOfStockQuantityLabel.TabIndex = 9;
            this.outOfStockQuantityLabel.Text = "Out of stock quantity:";
            // 
            // outOfStockThresholdText
            // 
            this.outOfStockThresholdText.Location = new System.Drawing.Point(166, 190);
            this.outOfStockThresholdText.Name = "outOfStockThresholdText";
            this.outOfStockThresholdText.Size = new System.Drawing.Size(80, 22);
            this.outOfStockThresholdText.TabIndex = 9;
            // 
            // outOfStockThresholdLabel
            // 
            this.outOfStockThresholdLabel.AutoSize = true;
            this.outOfStockThresholdLabel.Location = new System.Drawing.Point(6, 193);
            this.outOfStockThresholdLabel.Name = "outOfStockThresholdLabel";
            this.outOfStockThresholdLabel.Size = new System.Drawing.Size(154, 14);
            this.outOfStockThresholdLabel.TabIndex = 7;
            this.outOfStockThresholdLabel.Text = "Out of stock Threshold:";
            // 
            // prefixName
            // 
            this.prefixName.Location = new System.Drawing.Point(88, 158);
            this.prefixName.MaxLength = 10;
            this.prefixName.Name = "prefixName";
            this.prefixName.Size = new System.Drawing.Size(158, 22);
            this.prefixName.TabIndex = 7;
            // 
            // prefixLabel
            // 
            this.prefixLabel.AutoSize = true;
            this.prefixLabel.Location = new System.Drawing.Point(6, 161);
            this.prefixLabel.Name = "prefixLabel";
            this.prefixLabel.Size = new System.Drawing.Size(76, 14);
            this.prefixLabel.TabIndex = 5;
            this.prefixLabel.Text = "SKU prefix:";
            // 
            // setOutOfStock
            // 
            this.setOutOfStock.AutoSize = true;
            this.setOutOfStock.Location = new System.Drawing.Point(9, 125);
            this.setOutOfStock.Name = "setOutOfStock";
            this.setOutOfStock.Size = new System.Drawing.Size(276, 18);
            this.setOutOfStock.TabIndex = 6;
            this.setOutOfStock.Text = "Set out of stock for not presented SKUs";
            this.setOutOfStock.UseVisualStyleBackColor = true;
            // 
            // fileArchiveLabel
            // 
            this.fileArchiveLabel.AutoSize = true;
            this.fileArchiveLabel.Location = new System.Drawing.Point(6, 93);
            this.fileArchiveLabel.Name = "fileArchiveLabel";
            this.fileArchiveLabel.Size = new System.Drawing.Size(82, 14);
            this.fileArchiveLabel.TabIndex = 3;
            this.fileArchiveLabel.Text = "File archive:";
            // 
            // folderLabel
            // 
            this.folderLabel.AutoSize = true;
            this.folderLabel.Location = new System.Drawing.Point(6, 61);
            this.folderLabel.Name = "folderLabel";
            this.folderLabel.Size = new System.Drawing.Size(51, 14);
            this.folderLabel.TabIndex = 2;
            this.folderLabel.Text = "Folder:";
            // 
            // nameText
            // 
            this.nameText.Enabled = false;
            this.nameText.Location = new System.Drawing.Point(108, 26);
            this.nameText.MaxLength = 50;
            this.nameText.Name = "nameText";
            this.nameText.Size = new System.Drawing.Size(411, 22);
            this.nameText.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(6, 29);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(96, 14);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Vendor name:";
            // 
            // commonSettingsErrorProvider
            // 
            this.commonSettingsErrorProvider.ContainerControl = this;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(289, 162);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(46, 14);
            this.label.TabIndex = 10;
            this.label.Text = "Label:";
            // 
            // labelText
            // 
            this.labelText.Location = new System.Drawing.Point(341, 159);
            this.labelText.MaxLength = 50;
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(178, 22);
            this.labelText.TabIndex = 8;
            // 
            // CommonVendorSettings
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.settingsGroup);
            this.Name = "CommonVendorSettings";
            this.Size = new System.Drawing.Size(550, 230);
            this.settingsGroup.ResumeLayout(false);
            this.settingsGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.commonSettingsErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox nameText;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label fileArchiveLabel;
        private System.Windows.Forms.Label folderLabel;
        private System.Windows.Forms.Label outOfStockThresholdLabel;
        private System.Windows.Forms.TextBox prefixName;
        private System.Windows.Forms.Label prefixLabel;
        private System.Windows.Forms.CheckBox setOutOfStock;
        private System.Windows.Forms.Label outOfStockQuantityLabel;
        private ChannelAdvisor.NullableTextBox outOfStockThresholdText;
        private ChannelAdvisor.NullableTextBox outOfStockQuantityText;
        private System.Windows.Forms.GroupBox settingsGroup;
        private System.Windows.Forms.Button fileArchiveButton;
        private System.Windows.Forms.Button folderButton;
        private System.Windows.Forms.TextBox fileArchiveText;
        private System.Windows.Forms.TextBox folderText;
        private System.Windows.Forms.ErrorProvider commonSettingsErrorProvider;
        private System.Windows.Forms.FolderBrowserDialog commonSettingsFolderDialog;
        private System.Windows.Forms.TextBox labelText;
        private System.Windows.Forms.Label label;
    }
}
