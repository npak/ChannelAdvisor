namespace ChannelAdvisor
{
    partial class PetraSettings
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
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFtpURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbAPI = new System.Windows.Forms.RadioButton();
            this.rbFTP = new System.Windows.Forms.RadioButton();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFolderName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDropShipFee = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFileNameOnServer = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.commonSettings = new ChannelAdvisor.CommonVendorSettings();
            this.txtFtpFileName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(295, 533);
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
            this.btnSave.Location = new System.Drawing.Point(182, 533);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(105, 26);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(355, 342);
            this.txtPassword.MaxLength = 100;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(177, 22);
            this.txtPassword.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(275, 345);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 14);
            this.label4.TabIndex = 32;
            this.label4.Text = "Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(92, 342);
            this.txtUsername.MaxLength = 100;
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(174, 22);
            this.txtUsername.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 345);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 14);
            this.label3.TabIndex = 31;
            this.label3.Text = "Username:";
            // 
            // txtFtpURL
            // 
            this.txtFtpURL.Location = new System.Drawing.Point(79, 271);
            this.txtFtpURL.MaxLength = 500;
            this.txtFtpURL.Name = "txtFtpURL";
            this.txtFtpURL.Size = new System.Drawing.Size(451, 22);
            this.txtFtpURL.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 274);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 14);
            this.label2.TabIndex = 30;
            this.label2.Text = "Ftp URL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 246);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 16);
            this.label1.TabIndex = 29;
            this.label1.Text = "Petra Information";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbAPI);
            this.groupBox1.Controls.Add(this.rbFTP);
            this.groupBox1.Location = new System.Drawing.Point(391, 418);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(153, 74);
            this.groupBox1.TabIndex = 70;
            this.groupBox1.TabStop = false;
            // 
            // rbAPI
            // 
            this.rbAPI.AutoSize = true;
            this.rbAPI.Checked = true;
            this.rbAPI.Location = new System.Drawing.Point(6, 21);
            this.rbAPI.Name = "rbAPI";
            this.rbAPI.Size = new System.Drawing.Size(115, 18);
            this.rbAPI.TabIndex = 62;
            this.rbAPI.TabStop = true;
            this.rbAPI.Text = "Update by API";
            this.rbAPI.UseVisualStyleBackColor = true;
            // 
            // rbFTP
            // 
            this.rbFTP.AutoSize = true;
            this.rbFTP.Location = new System.Drawing.Point(6, 45);
            this.rbFTP.Name = "rbFTP";
            this.rbFTP.Size = new System.Drawing.Size(116, 18);
            this.rbFTP.TabIndex = 63;
            this.rbFTP.Text = "Update by FTP";
            this.rbFTP.UseVisualStyleBackColor = true;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(98, 465);
            this.txtFileName.MaxLength = 200;
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(244, 22);
            this.txtFileName.TabIndex = 68;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 471);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 14);
            this.label8.TabIndex = 69;
            this.label8.Text = "File name:";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(112, 435);
            this.txtFolderName.MaxLength = 200;
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(228, 22);
            this.txtFolderName.TabIndex = 66;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 440);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 14);
            this.label9.TabIndex = 67;
            this.label9.Text = "Folder name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(13, 412);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 16);
            this.label10.TabIndex = 65;
            this.label10.Text = "Petra CSV Information";
            // 
            // txtDropShipFee
            // 
            this.txtDropShipFee.Location = new System.Drawing.Point(123, 377);
            this.txtDropShipFee.MaxLength = 20;
            this.txtDropShipFee.Name = "txtDropShipFee";
            this.txtDropShipFee.Size = new System.Drawing.Size(72, 22);
            this.txtDropShipFee.TabIndex = 71;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 380);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 14);
            this.label5.TabIndex = 72;
            this.label5.Text = "Drop Ship Fee:";
            // 
            // txtFileNameOnServer
            // 
            this.txtFileNameOnServer.Location = new System.Drawing.Point(209, 499);
            this.txtFileNameOnServer.MaxLength = 200;
            this.txtFileNameOnServer.Name = "txtFileNameOnServer";
            this.txtFileNameOnServer.Size = new System.Drawing.Size(244, 22);
            this.txtFileNameOnServer.TabIndex = 73;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 502);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(186, 14);
            this.label7.TabIndex = 74;
            this.label7.Text = "File name to drop on server:";
            // 
            // commonSettings
            // 
            this.commonSettings.IsNameEditable = false;
            this.commonSettings.Location = new System.Drawing.Point(8, 10);
            this.commonSettings.Name = "commonSettings";
            this.commonSettings.Size = new System.Drawing.Size(550, 230);
            this.commonSettings.TabIndex = 38;
            this.commonSettings.VendorInfo = null;
            // 
            // txtFtpFileName
            // 
            this.txtFtpFileName.Location = new System.Drawing.Point(120, 308);
            this.txtFtpFileName.MaxLength = 500;
            this.txtFtpFileName.Name = "txtFtpFileName";
            this.txtFtpFileName.Size = new System.Drawing.Size(409, 22);
            this.txtFtpFileName.TabIndex = 75;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 14);
            this.label6.TabIndex = 76;
            this.label6.Text = "Ftp File Name:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // PetraSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 571);
            this.Controls.Add(this.txtFtpFileName);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtFileNameOnServer);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDropShipFee);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtFolderName);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.commonSettings);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFtpURL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PetraSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Petra Settings";
            this.Load += new System.EventHandler(this.PetraSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFtpURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private CommonVendorSettings commonSettings;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbAPI;
        private System.Windows.Forms.RadioButton rbFTP;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtFolderName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDropShipFee;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtFileNameOnServer;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFtpFileName;
        private System.Windows.Forms.Label label6;
    }
}