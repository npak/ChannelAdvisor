namespace ChannelAdvisor
{
    partial class WynitBasicData
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
            this.OutputFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.OutputFolderLabel = new System.Windows.Forms.Label();
            this.OutputFolderText = new System.Windows.Forms.TextBox();
            this.OutputFolderButton = new System.Windows.Forms.Button();
            this.FilePrefixLabel = new System.Windows.Forms.Label();
            this.FilePrefixText = new System.Windows.Forms.TextBox();
            this.SaveButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.waitLabel = new System.Windows.Forms.Label();
            this.MaxUPCLengthLabel = new System.Windows.Forms.Label();
            this.MaxUPCLengthText = new System.Windows.Forms.TextBox();
            this.SupplierCodeLabel = new System.Windows.Forms.Label();
            this.SupplierCodeText = new System.Windows.Forms.TextBox();
            this.WarehouseLocationLabel = new System.Windows.Forms.Label();
            this.WarehouseLocationText = new System.Windows.Forms.TextBox();
            this.DCCodeLabel = new System.Windows.Forms.Label();
            this.DCCodeText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // OutputFolderDialog
            // 
            this.OutputFolderDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // OutputFolderLabel
            // 
            this.OutputFolderLabel.AutoSize = true;
            this.OutputFolderLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OutputFolderLabel.Location = new System.Drawing.Point(55, 39);
            this.OutputFolderLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.OutputFolderLabel.Name = "OutputFolderLabel";
            this.OutputFolderLabel.Size = new System.Drawing.Size(105, 16);
            this.OutputFolderLabel.TabIndex = 0;
            this.OutputFolderLabel.Text = "Output Folder:";
            // 
            // OutputFolderText
            // 
            this.OutputFolderText.Location = new System.Drawing.Point(167, 36);
            this.OutputFolderText.Name = "OutputFolderText";
            this.OutputFolderText.ReadOnly = true;
            this.OutputFolderText.Size = new System.Drawing.Size(294, 23);
            this.OutputFolderText.TabIndex = 1;
            // 
            // OutputFolderButton
            // 
            this.OutputFolderButton.Location = new System.Drawing.Point(467, 33);
            this.OutputFolderButton.Name = "OutputFolderButton";
            this.OutputFolderButton.Size = new System.Drawing.Size(35, 28);
            this.OutputFolderButton.TabIndex = 2;
            this.OutputFolderButton.Text = "...";
            this.OutputFolderButton.UseVisualStyleBackColor = true;
            this.OutputFolderButton.Click += new System.EventHandler(this.OutputFolderButton_Click);
            // 
            // FilePrefixLabel
            // 
            this.FilePrefixLabel.AutoSize = true;
            this.FilePrefixLabel.Location = new System.Drawing.Point(44, 98);
            this.FilePrefixLabel.Name = "FilePrefixLabel";
            this.FilePrefixLabel.Size = new System.Drawing.Size(117, 16);
            this.FilePrefixLabel.TabIndex = 3;
            this.FilePrefixLabel.Text = "File name prefix:";
            // 
            // FilePrefixText
            // 
            this.FilePrefixText.Location = new System.Drawing.Point(167, 95);
            this.FilePrefixText.Name = "FilePrefixText";
            this.FilePrefixText.Size = new System.Drawing.Size(335, 23);
            this.FilePrefixText.TabIndex = 4;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(189, 305);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(81, 30);
            this.SaveButton.TabIndex = 5;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(276, 305);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(81, 30);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // waitLabel
            // 
            this.waitLabel.AutoSize = true;
            this.waitLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.waitLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.waitLabel.Location = new System.Drawing.Point(232, 273);
            this.waitLabel.Name = "waitLabel";
            this.waitLabel.Size = new System.Drawing.Size(82, 16);
            this.waitLabel.TabIndex = 7;
            this.waitLabel.Text = "Please wait";
            // 
            // MaxUPCLengthLabel
            // 
            this.MaxUPCLengthLabel.AutoSize = true;
            this.MaxUPCLengthLabel.Location = new System.Drawing.Point(39, 157);
            this.MaxUPCLengthLabel.Name = "MaxUPCLengthLabel";
            this.MaxUPCLengthLabel.Size = new System.Drawing.Size(121, 16);
            this.MaxUPCLengthLabel.TabIndex = 8;
            this.MaxUPCLengthLabel.Text = "Max UPC Length:";
            // 
            // MaxUPCLengthText
            // 
            this.MaxUPCLengthText.Location = new System.Drawing.Point(167, 154);
            this.MaxUPCLengthText.MaxLength = 2;
            this.MaxUPCLengthText.Name = "MaxUPCLengthText";
            this.MaxUPCLengthText.Size = new System.Drawing.Size(51, 23);
            this.MaxUPCLengthText.TabIndex = 9;
            // 
            // SupplierCodeLabel
            // 
            this.SupplierCodeLabel.AutoSize = true;
            this.SupplierCodeLabel.Location = new System.Drawing.Point(292, 157);
            this.SupplierCodeLabel.Name = "SupplierCodeLabel";
            this.SupplierCodeLabel.Size = new System.Drawing.Size(104, 16);
            this.SupplierCodeLabel.TabIndex = 10;
            this.SupplierCodeLabel.Text = "Supplier Code:";
            // 
            // SupplierCodeText
            // 
            this.SupplierCodeText.Location = new System.Drawing.Point(402, 154);
            this.SupplierCodeText.Name = "SupplierCodeText";
            this.SupplierCodeText.Size = new System.Drawing.Size(100, 23);
            this.SupplierCodeText.TabIndex = 11;
            // 
            // WarehouseLocationLabel
            // 
            this.WarehouseLocationLabel.AutoSize = true;
            this.WarehouseLocationLabel.Location = new System.Drawing.Point(13, 213);
            this.WarehouseLocationLabel.Name = "WarehouseLocationLabel";
            this.WarehouseLocationLabel.Size = new System.Drawing.Size(148, 16);
            this.WarehouseLocationLabel.TabIndex = 12;
            this.WarehouseLocationLabel.Text = "Warehouse Location:";
            // 
            // WarehouseLocationText
            // 
            this.WarehouseLocationText.Location = new System.Drawing.Point(167, 210);
            this.WarehouseLocationText.Name = "WarehouseLocationText";
            this.WarehouseLocationText.Size = new System.Drawing.Size(100, 23);
            this.WarehouseLocationText.TabIndex = 13;
            // 
            // DCCodeLabel
            // 
            this.DCCodeLabel.AutoSize = true;
            this.DCCodeLabel.Location = new System.Drawing.Point(326, 213);
            this.DCCodeLabel.Name = "DCCodeLabel";
            this.DCCodeLabel.Size = new System.Drawing.Size(70, 16);
            this.DCCodeLabel.TabIndex = 14;
            this.DCCodeLabel.Text = "DC Code:";
            // 
            // DCCodeText
            // 
            this.DCCodeText.Location = new System.Drawing.Point(402, 210);
            this.DCCodeText.Name = "DCCodeText";
            this.DCCodeText.Size = new System.Drawing.Size(100, 23);
            this.DCCodeText.TabIndex = 15;
            // 
            // WynitBasicData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(547, 356);
            this.Controls.Add(this.DCCodeText);
            this.Controls.Add(this.DCCodeLabel);
            this.Controls.Add(this.WarehouseLocationText);
            this.Controls.Add(this.WarehouseLocationLabel);
            this.Controls.Add(this.SupplierCodeText);
            this.Controls.Add(this.SupplierCodeLabel);
            this.Controls.Add(this.MaxUPCLengthText);
            this.Controls.Add(this.MaxUPCLengthLabel);
            this.Controls.Add(this.waitLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.FilePrefixText);
            this.Controls.Add(this.FilePrefixLabel);
            this.Controls.Add(this.OutputFolderButton);
            this.Controls.Add(this.OutputFolderText);
            this.Controls.Add(this.OutputFolderLabel);
            this.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WynitBasicData";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wynit basic data";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog OutputFolderDialog;
        private System.Windows.Forms.Label OutputFolderLabel;
        private System.Windows.Forms.TextBox OutputFolderText;
        private System.Windows.Forms.Button OutputFolderButton;
        private System.Windows.Forms.Label FilePrefixLabel;
        private System.Windows.Forms.TextBox FilePrefixText;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label waitLabel;
        private System.Windows.Forms.Label MaxUPCLengthLabel;
        private System.Windows.Forms.TextBox MaxUPCLengthText;
        private System.Windows.Forms.Label SupplierCodeLabel;
        private System.Windows.Forms.TextBox SupplierCodeText;
        private System.Windows.Forms.Label WarehouseLocationLabel;
        private System.Windows.Forms.TextBox WarehouseLocationText;
        private System.Windows.Forms.Label DCCodeLabel;
        private System.Windows.Forms.TextBox DCCodeText;
    }
}