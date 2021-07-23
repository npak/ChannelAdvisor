namespace ChannelAdvisor
{
    partial class ShipstationSettings
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFromZip = new System.Windows.Forms.TextBox();
            this.txtToZip = new System.Windows.Forms.TextBox();
            this.txtUSPInsurance = new System.Windows.Forms.TextBox();
            this.txtstamps = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.txtInputFile = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt1ClassMailCutoff = new System.Windows.Forms.TextBox();
            this.txtPriorityMailCutoff = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtParcelSelectCutoff = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtUSPSMarkup1cl = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUSPSMarkupParcel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtUSPSMarkupPriority = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtRequireSignature = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnClose.Location = new System.Drawing.Point(386, 398);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(299, 398);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "From Postal Code";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "To Postal Code";
            // 
            // txtFromZip
            // 
            this.txtFromZip.Location = new System.Drawing.Point(147, 13);
            this.txtFromZip.Name = "txtFromZip";
            this.txtFromZip.Size = new System.Drawing.Size(100, 23);
            this.txtFromZip.TabIndex = 7;
            // 
            // txtToZip
            // 
            this.txtToZip.Location = new System.Drawing.Point(147, 42);
            this.txtToZip.Name = "txtToZip";
            this.txtToZip.Size = new System.Drawing.Size(100, 23);
            this.txtToZip.TabIndex = 8;
            // 
            // txtUSPInsurance
            // 
            this.txtUSPInsurance.Location = new System.Drawing.Point(401, 41);
            this.txtUSPInsurance.Name = "txtUSPInsurance";
            this.txtUSPInsurance.Size = new System.Drawing.Size(100, 23);
            this.txtUSPInsurance.TabIndex = 12;
            // 
            // txtstamps
            // 
            this.txtstamps.Location = new System.Drawing.Point(401, 12);
            this.txtstamps.Name = "txtstamps";
            this.txtstamps.Size = new System.Drawing.Size(100, 23);
            this.txtstamps.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(266, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "UPS Insurance";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(266, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 9;
            this.label4.Text = "USPS Insurance";
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(148, 361);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(353, 23);
            this.txtOutputFile.TabIndex = 16;
            // 
            // txtInputFile
            // 
            this.txtInputFile.Location = new System.Drawing.Point(148, 332);
            this.txtInputFile.Name = "txtInputFile";
            this.txtInputFile.Size = new System.Drawing.Size(353, 23);
            this.txtInputFile.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 364);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(118, 16);
            this.label7.TabIndex = 14;
            this.label7.Text = "Output file name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 335);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 16);
            this.label8.TabIndex = 13;
            this.label8.Text = "Input file name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 308);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 16);
            this.label5.TabIndex = 17;
            this.label5.Text = "Shipping Folder:";
            // 
            // txt1ClassMailCutoff
            // 
            this.txt1ClassMailCutoff.Location = new System.Drawing.Point(232, 139);
            this.txt1ClassMailCutoff.Name = "txt1ClassMailCutoff";
            this.txt1ClassMailCutoff.Size = new System.Drawing.Size(100, 23);
            this.txt1ClassMailCutoff.TabIndex = 21;
            // 
            // txtPriorityMailCutoff
            // 
            this.txtPriorityMailCutoff.Location = new System.Drawing.Point(232, 110);
            this.txtPriorityMailCutoff.Name = "txtPriorityMailCutoff";
            this.txtPriorityMailCutoff.Size = new System.Drawing.Size(100, 23);
            this.txtPriorityMailCutoff.TabIndex = 20;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 142);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 16);
            this.label10.TabIndex = 19;
            this.label10.Text = "USPS First Class Mail Level";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 113);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(163, 16);
            this.label11.TabIndex = 18;
            this.label11.Text = "USPS Priority Mail Level";
            // 
            // txtParcelSelectCutoff
            // 
            this.txtParcelSelectCutoff.Location = new System.Drawing.Point(232, 169);
            this.txtParcelSelectCutoff.Name = "txtParcelSelectCutoff";
            this.txtParcelSelectCutoff.Size = new System.Drawing.Size(100, 23);
            this.txtParcelSelectCutoff.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 172);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(174, 16);
            this.label6.TabIndex = 26;
            this.label6.Text = "USPS Parcel Select Level";
            // 
            // txtUSPSMarkup1cl
            // 
            this.txtUSPSMarkup1cl.Location = new System.Drawing.Point(232, 221);
            this.txtUSPSMarkup1cl.Name = "txtUSPSMarkup1cl";
            this.txtUSPSMarkup1cl.Size = new System.Drawing.Size(100, 23);
            this.txtUSPSMarkup1cl.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 224);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 16);
            this.label9.TabIndex = 28;
            this.label9.Text = "First Class Mail";
            // 
            // txtUSPSMarkupParcel
            // 
            this.txtUSPSMarkupParcel.Location = new System.Drawing.Point(232, 250);
            this.txtUSPSMarkupParcel.Name = "txtUSPSMarkupParcel";
            this.txtUSPSMarkupParcel.Size = new System.Drawing.Size(100, 23);
            this.txtUSPSMarkupParcel.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 253);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(151, 16);
            this.label12.TabIndex = 30;
            this.label12.Text = "Parcel Select Ground ";
            // 
            // txtUSPSMarkupPriority
            // 
            this.txtUSPSMarkupPriority.Location = new System.Drawing.Point(232, 279);
            this.txtUSPSMarkupPriority.Name = "txtUSPSMarkupPriority";
            this.txtUSPSMarkupPriority.Size = new System.Drawing.Size(100, 23);
            this.txtUSPSMarkupPriority.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 279);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(84, 16);
            this.label13.TabIndex = 32;
            this.label13.Text = "Priority Mail";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(12, 199);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(142, 16);
            this.label14.TabIndex = 34;
            this.label14.Text = "USPS Markup(%) :";
            // 
            // txtRequireSignature
            // 
            this.txtRequireSignature.Location = new System.Drawing.Point(232, 80);
            this.txtRequireSignature.Name = "txtRequireSignature";
            this.txtRequireSignature.Size = new System.Drawing.Size(100, 23);
            this.txtRequireSignature.TabIndex = 36;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 83);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(143, 16);
            this.label15.TabIndex = 35;
            this.label15.Text = "Require Signature at";
            // 
            // ShipstationSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 440);
            this.Controls.Add(this.txtRequireSignature);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txtUSPSMarkupPriority);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txtUSPSMarkupParcel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txtUSPSMarkup1cl);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtParcelSelectCutoff);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt1ClassMailCutoff);
            this.Controls.Add(this.txtPriorityMailCutoff);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtOutputFile);
            this.Controls.Add(this.txtInputFile);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtUSPInsurance);
            this.Controls.Add(this.txtstamps);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtToZip);
            this.Controls.Add(this.txtFromZip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShipstationSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipstation Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtFromZip;
        private System.Windows.Forms.TextBox txtToZip;
        private System.Windows.Forms.TextBox txtUSPInsurance;
        private System.Windows.Forms.TextBox txtstamps;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.TextBox txtInputFile;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt1ClassMailCutoff;
        private System.Windows.Forms.TextBox txtPriorityMailCutoff;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtParcelSelectCutoff;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtUSPSMarkup1cl;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtUSPSMarkupParcel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtUSPSMarkupPriority;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtRequireSignature;
        private System.Windows.Forms.Label label15;
    }
}