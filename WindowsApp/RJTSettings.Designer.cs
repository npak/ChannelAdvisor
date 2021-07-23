namespace ChannelAdvisor
{
    partial class RJTSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.fileFolderLabel = new System.Windows.Forms.Label();
            this.folderText = new System.Windows.Forms.TextBox();
            this.filesFolderButton = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.commonVendorSettings = new ChannelAdvisor.CommonVendorSettings();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 259);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "RJT information";
            // 
            // fileFolderLabel
            // 
            this.fileFolderLabel.AutoSize = true;
            this.fileFolderLabel.Location = new System.Drawing.Point(12, 291);
            this.fileFolderLabel.Name = "fileFolderLabel";
            this.fileFolderLabel.Size = new System.Drawing.Size(101, 14);
            this.fileFolderLabel.TabIndex = 2;
            this.fileFolderLabel.Text = "Folder to scan:";
            // 
            // folderText
            // 
            this.folderText.Location = new System.Drawing.Point(124, 288);
            this.folderText.Name = "folderText";
            this.folderText.Size = new System.Drawing.Size(402, 22);
            this.folderText.TabIndex = 3;
            // 
            // filesFolderButton
            // 
            this.filesFolderButton.Location = new System.Drawing.Point(532, 286);
            this.filesFolderButton.Name = "filesFolderButton";
            this.filesFolderButton.Size = new System.Drawing.Size(30, 23);
            this.filesFolderButton.TabIndex = 4;
            this.filesFolderButton.Text = "...";
            this.filesFolderButton.UseVisualStyleBackColor = true;
            this.filesFolderButton.Click += new System.EventHandler(this.filesFolderButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(211, 336);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(293, 336);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 6;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // commonVendorSettings
            // 
            this.commonVendorSettings.IsNameEditable = false;
            this.commonVendorSettings.Location = new System.Drawing.Point(13, 13);
            this.commonVendorSettings.Name = "commonVendorSettings";
            this.commonVendorSettings.Size = new System.Drawing.Size(550, 230);
            this.commonVendorSettings.TabIndex = 0;
            this.commonVendorSettings.VendorInfo = null;
            // 
            // RJTSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 374);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.filesFolderButton);
            this.Controls.Add(this.folderText);
            this.Controls.Add(this.fileFolderLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.commonVendorSettings);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RJTSettings";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RJT Settings";
            this.Load += new System.EventHandler(this.RJTSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CommonVendorSettings commonVendorSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label fileFolderLabel;
        private System.Windows.Forms.TextBox folderText;
        private System.Windows.Forms.Button filesFolderButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;

    }
}