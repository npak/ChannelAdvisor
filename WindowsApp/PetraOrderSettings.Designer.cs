namespace ChannelAdvisor
{
    partial class PetraOrderSettings
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtOrderFtp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtOrderFtpPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtOrderFtpUsername = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtArchiveName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(290, 188);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(105, 26);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(177, 188);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 26);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtOrderFtp
            // 
            this.txtOrderFtp.Location = new System.Drawing.Point(118, 44);
            this.txtOrderFtp.MaxLength = 200;
            this.txtOrderFtp.Name = "txtOrderFtp";
            this.txtOrderFtp.Size = new System.Drawing.Size(339, 22);
            this.txtOrderFtp.TabIndex = 76;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(23, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 14);
            this.label11.TabIndex = 77;
            this.label11.Text = "Ftp Url:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(238, 16);
            this.label12.TabIndex = 75;
            this.label12.Text = "Petra Order Module Information";
            // 
            // txtOrderFtpPassword
            // 
            this.txtOrderFtpPassword.Location = new System.Drawing.Point(365, 74);
            this.txtOrderFtpPassword.MaxLength = 100;
            this.txtOrderFtpPassword.Name = "txtOrderFtpPassword";
            this.txtOrderFtpPassword.Size = new System.Drawing.Size(152, 22);
            this.txtOrderFtpPassword.TabIndex = 79;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(285, 77);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 14);
            this.label13.TabIndex = 81;
            this.label13.Text = "Password:";
            // 
            // txtOrderFtpUsername
            // 
            this.txtOrderFtpUsername.Location = new System.Drawing.Point(102, 74);
            this.txtOrderFtpUsername.MaxLength = 100;
            this.txtOrderFtpUsername.Name = "txtOrderFtpUsername";
            this.txtOrderFtpUsername.Size = new System.Drawing.Size(155, 22);
            this.txtOrderFtpUsername.TabIndex = 78;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(22, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(76, 14);
            this.label14.TabIndex = 80;
            this.label14.Text = "Username:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 14);
            this.label9.TabIndex = 67;
            this.label9.Text = "Folder name (in):";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(156, 115);
            this.txtFolderName.MaxLength = 200;
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(361, 22);
            this.txtFolderName.TabIndex = 66;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 151);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(125, 14);
            this.label8.TabIndex = 69;
            this.label8.Text = "Folder name (out):";
            // 
            // txtArchiveName
            // 
            this.txtArchiveName.Location = new System.Drawing.Point(157, 145);
            this.txtArchiveName.MaxLength = 200;
            this.txtArchiveName.Name = "txtArchiveName";
            this.txtArchiveName.Size = new System.Drawing.Size(360, 22);
            this.txtArchiveName.TabIndex = 68;
            // 
            // PetraOrderSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 221);
            this.Controls.Add(this.txtOrderFtpPassword);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtOrderFtpUsername);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtOrderFtp);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtArchiveName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PetraOrderSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petra Order Module Settings";
            this.Load += new System.EventHandler(this.PetraSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtOrderFtp;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtOrderFtpPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtOrderFtpUsername;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtArchiveName;
    }
}