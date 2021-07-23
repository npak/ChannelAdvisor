namespace ChannelAdvisor
{
    partial class EMGOrderUpdateSettings
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
            this.txtShipToID = new System.Windows.Forms.TextBox();
            this.txtSendOrderURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGetOrderStatusURL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtUOMCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.txtCSVFile = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSelectDB = new System.Windows.Forms.Button();
            this.txtStoneEdgeDBPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "EMG Order Update Information";
            // 
            // txtShipToID
            // 
            this.txtShipToID.Location = new System.Drawing.Point(15, 162);
            this.txtShipToID.Name = "txtShipToID";
            this.txtShipToID.Size = new System.Drawing.Size(175, 23);
            this.txtShipToID.TabIndex = 3;
            // 
            // txtSendOrderURL
            // 
            this.txtSendOrderURL.Location = new System.Drawing.Point(15, 57);
            this.txtSendOrderURL.Name = "txtSendOrderURL";
            this.txtSendOrderURL.Size = new System.Drawing.Size(534, 23);
            this.txtSendOrderURL.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 16);
            this.label3.TabIndex = 13;
            this.label3.Text = "Ship To ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Send Order URL:";
            // 
            // txtGetOrderStatusURL
            // 
            this.txtGetOrderStatusURL.Location = new System.Drawing.Point(15, 109);
            this.txtGetOrderStatusURL.Name = "txtGetOrderStatusURL";
            this.txtGetOrderStatusURL.Size = new System.Drawing.Size(534, 23);
            this.txtGetOrderStatusURL.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "Get Order Status URL:";
            // 
            // txtUOMCode
            // 
            this.txtUOMCode.Location = new System.Drawing.Point(196, 162);
            this.txtUOMCode.Name = "txtUOMCode";
            this.txtUOMCode.Size = new System.Drawing.Size(175, 23);
            this.txtUOMCode.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(193, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 16);
            this.label5.TabIndex = 18;
            this.label5.Text = "UOM Code:";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(525, 212);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(37, 30);
            this.btnSelectFile.TabIndex = 6;
            this.btnSelectFile.Text = "...";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // txtCSVFile
            // 
            this.txtCSVFile.Location = new System.Drawing.Point(15, 216);
            this.txtCSVFile.Name = "txtCSVFile";
            this.txtCSVFile.ReadOnly = true;
            this.txtCSVFile.Size = new System.Drawing.Size(504, 23);
            this.txtCSVFile.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 197);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "CSV File Location:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(294, 310);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 10;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(207, 310);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSelectDB
            // 
            this.btnSelectDB.Location = new System.Drawing.Point(525, 265);
            this.btnSelectDB.Name = "btnSelectDB";
            this.btnSelectDB.Size = new System.Drawing.Size(37, 30);
            this.btnSelectDB.TabIndex = 8;
            this.btnSelectDB.Text = "...";
            this.btnSelectDB.UseVisualStyleBackColor = true;
            this.btnSelectDB.Click += new System.EventHandler(this.btnSelectDB_Click);
            // 
            // txtStoneEdgeDBPath
            // 
            this.txtStoneEdgeDBPath.Location = new System.Drawing.Point(15, 269);
            this.txtStoneEdgeDBPath.Name = "txtStoneEdgeDBPath";
            this.txtStoneEdgeDBPath.ReadOnly = true;
            this.txtStoneEdgeDBPath.Size = new System.Drawing.Size(504, 23);
            this.txtStoneEdgeDBPath.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 250);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(187, 16);
            this.label7.TabIndex = 23;
            this.label7.Text = "StoneEdge Database Path:";
            // 
            // EMGOrderUpdateSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 352);
            this.Controls.Add(this.btnSelectDB);
            this.Controls.Add(this.txtStoneEdgeDBPath);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.txtCSVFile);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtUOMCode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtGetOrderStatusURL);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtShipToID);
            this.Controls.Add(this.txtSendOrderURL);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EMGOrderUpdateSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EMG Order Update Settings";
            this.Load += new System.EventHandler(this.EMGOrderUpdateSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtShipToID;
        private System.Windows.Forms.TextBox txtSendOrderURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtGetOrderStatusURL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtUOMCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox txtCSVFile;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSelectDB;
        private System.Windows.Forms.TextBox txtStoneEdgeDBPath;
        private System.Windows.Forms.Label label7;
    }
}