namespace ChannelAdvisor
{
    partial class Settings
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
            this.chkDisableAutoUpdate = new System.Windows.Forms.CheckBox();
            this.txtMaxSkusToUpdate = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNegativeQtyCheck = new System.Windows.Forms.TextBox();
            this.txtNegativeQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxSun = new System.Windows.Forms.CheckBox();
            this.checkBoxMon = new System.Windows.Forms.CheckBox();
            this.checkBoxTue = new System.Windows.Forms.CheckBox();
            this.checkBoxWed = new System.Windows.Forms.CheckBox();
            this.checkBoxThu = new System.Windows.Forms.CheckBox();
            this.checkBoxFri = new System.Windows.Forms.CheckBox();
            this.checkBoxSat = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSun = new System.Windows.Forms.MaskedTextBox();
            this.txtMon = new System.Windows.Forms.MaskedTextBox();
            this.txtTue = new System.Windows.Forms.MaskedTextBox();
            this.txtWed = new System.Windows.Forms.MaskedTextBox();
            this.txtThu = new System.Windows.Forms.MaskedTextBox();
            this.txtFri = new System.Windows.Forms.MaskedTextBox();
            this.txtSat = new System.Windows.Forms.MaskedTextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkDisableAutoUpdate
            // 
            this.chkDisableAutoUpdate.AutoSize = true;
            this.chkDisableAutoUpdate.Location = new System.Drawing.Point(12, 12);
            this.chkDisableAutoUpdate.Name = "chkDisableAutoUpdate";
            this.chkDisableAutoUpdate.Size = new System.Drawing.Size(161, 20);
            this.chkDisableAutoUpdate.TabIndex = 1;
            this.chkDisableAutoUpdate.Text = "Disable Auto Update";
            this.chkDisableAutoUpdate.UseVisualStyleBackColor = true;
            // 
            // txtMaxSkusToUpdate
            // 
            this.txtMaxSkusToUpdate.Location = new System.Drawing.Point(321, 36);
            this.txtMaxSkusToUpdate.MaxLength = 5;
            this.txtMaxSkusToUpdate.Name = "txtMaxSkusToUpdate";
            this.txtMaxSkusToUpdate.Size = new System.Drawing.Size(57, 23);
            this.txtMaxSkusToUpdate.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(306, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Max No of SKU\'s to Update At A Single Time:";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(234, 181);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(147, 181);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNegativeQtyCheck
            // 
            this.txtNegativeQtyCheck.Location = new System.Drawing.Point(397, 66);
            this.txtNegativeQtyCheck.MaxLength = 5;
            this.txtNegativeQtyCheck.Name = "txtNegativeQtyCheck";
            this.txtNegativeQtyCheck.Size = new System.Drawing.Size(41, 23);
            this.txtNegativeQtyCheck.TabIndex = 21;
            // 
            // txtNegativeQty
            // 
            this.txtNegativeQty.Location = new System.Drawing.Point(112, 66);
            this.txtNegativeQty.MaxLength = 5;
            this.txtNegativeQty.Name = "txtNegativeQty";
            this.txtNegativeQty.Size = new System.Drawing.Size(41, 23);
            this.txtNegativeQty.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Send Qty As";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(156, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "To CA If Qty Is Equal Or Less Than";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(97, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 22);
            this.label3.TabIndex = 25;
            this.label3.Text = "-";
            // 
            // checkBoxSun
            // 
            this.checkBoxSun.AutoSize = true;
            this.checkBoxSun.Location = new System.Drawing.Point(4, 23);
            this.checkBoxSun.Name = "checkBoxSun";
            this.checkBoxSun.Size = new System.Drawing.Size(52, 20);
            this.checkBoxSun.TabIndex = 29;
            this.checkBoxSun.Text = "Sun";
            this.checkBoxSun.UseVisualStyleBackColor = true;
            this.checkBoxSun.CheckedChanged += new System.EventHandler(this.checkBoxSun_CheckedChanged);
            // 
            // checkBoxMon
            // 
            this.checkBoxMon.AutoSize = true;
            this.checkBoxMon.Location = new System.Drawing.Point(63, 23);
            this.checkBoxMon.Name = "checkBoxMon";
            this.checkBoxMon.Size = new System.Drawing.Size(54, 20);
            this.checkBoxMon.TabIndex = 30;
            this.checkBoxMon.Text = "Mon";
            this.checkBoxMon.UseVisualStyleBackColor = true;
            this.checkBoxMon.CheckedChanged += new System.EventHandler(this.checkBoxMon_CheckedChanged);
            // 
            // checkBoxTue
            // 
            this.checkBoxTue.AutoSize = true;
            this.checkBoxTue.Location = new System.Drawing.Point(125, 23);
            this.checkBoxTue.Name = "checkBoxTue";
            this.checkBoxTue.Size = new System.Drawing.Size(51, 20);
            this.checkBoxTue.TabIndex = 31;
            this.checkBoxTue.Text = "Tue";
            this.checkBoxTue.UseVisualStyleBackColor = true;
            this.checkBoxTue.CheckedChanged += new System.EventHandler(this.checkBoxTue_CheckedChanged);
            // 
            // checkBoxWed
            // 
            this.checkBoxWed.AutoSize = true;
            this.checkBoxWed.Location = new System.Drawing.Point(188, 22);
            this.checkBoxWed.Name = "checkBoxWed";
            this.checkBoxWed.Size = new System.Drawing.Size(55, 20);
            this.checkBoxWed.TabIndex = 32;
            this.checkBoxWed.Text = "Wed";
            this.checkBoxWed.UseVisualStyleBackColor = true;
            this.checkBoxWed.CheckedChanged += new System.EventHandler(this.checkBoxWed_CheckedChanged);
            // 
            // checkBoxThu
            // 
            this.checkBoxThu.AutoSize = true;
            this.checkBoxThu.Location = new System.Drawing.Point(256, 22);
            this.checkBoxThu.Name = "checkBoxThu";
            this.checkBoxThu.Size = new System.Drawing.Size(52, 20);
            this.checkBoxThu.TabIndex = 33;
            this.checkBoxThu.Text = "Thu";
            this.checkBoxThu.UseVisualStyleBackColor = true;
            this.checkBoxThu.CheckedChanged += new System.EventHandler(this.checkBoxThu_CheckedChanged);
            // 
            // checkBoxFri
            // 
            this.checkBoxFri.AutoSize = true;
            this.checkBoxFri.Location = new System.Drawing.Point(321, 22);
            this.checkBoxFri.Name = "checkBoxFri";
            this.checkBoxFri.Size = new System.Drawing.Size(43, 20);
            this.checkBoxFri.TabIndex = 34;
            this.checkBoxFri.Text = "Fri";
            this.checkBoxFri.UseVisualStyleBackColor = true;
            this.checkBoxFri.CheckedChanged += new System.EventHandler(this.checkBoxFri_CheckedChanged);
            // 
            // checkBoxSat
            // 
            this.checkBoxSat.AutoSize = true;
            this.checkBoxSat.Location = new System.Drawing.Point(382, 22);
            this.checkBoxSat.Name = "checkBoxSat";
            this.checkBoxSat.Size = new System.Drawing.Size(50, 20);
            this.checkBoxSat.TabIndex = 35;
            this.checkBoxSat.Text = "Sat";
            this.checkBoxSat.UseVisualStyleBackColor = true;
            this.checkBoxSat.CheckedChanged += new System.EventHandler(this.checkBoxSat_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSat);
            this.groupBox1.Controls.Add(this.txtFri);
            this.groupBox1.Controls.Add(this.txtThu);
            this.groupBox1.Controls.Add(this.txtWed);
            this.groupBox1.Controls.Add(this.txtTue);
            this.groupBox1.Controls.Add(this.txtMon);
            this.groupBox1.Controls.Add(this.txtSun);
            this.groupBox1.Controls.Add(this.checkBoxThu);
            this.groupBox1.Controls.Add(this.checkBoxSat);
            this.groupBox1.Controls.Add(this.checkBoxSun);
            this.groupBox1.Controls.Add(this.checkBoxFri);
            this.groupBox1.Controls.Add(this.checkBoxMon);
            this.groupBox1.Controls.Add(this.checkBoxTue);
            this.groupBox1.Controls.Add(this.checkBoxWed);
            this.groupBox1.Location = new System.Drawing.Point(7, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(451, 80);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cache reload schedule ";
            // 
            // txtSun
            // 
            this.txtSun.Enabled = false;
            this.txtSun.Location = new System.Drawing.Point(4, 50);
            this.txtSun.Mask = "00:00";
            this.txtSun.Name = "txtSun";
            this.txtSun.Size = new System.Drawing.Size(49, 23);
            this.txtSun.TabIndex = 36;
            this.txtSun.ValidatingType = typeof(System.DateTime);
            // 
            // txtMon
            // 
            this.txtMon.Enabled = false;
            this.txtMon.Location = new System.Drawing.Point(63, 50);
            this.txtMon.Mask = "00:00";
            this.txtMon.Name = "txtMon";
            this.txtMon.Size = new System.Drawing.Size(49, 23);
            this.txtMon.TabIndex = 37;
            this.txtMon.ValidatingType = typeof(System.DateTime);
            // 
            // txtTue
            // 
            this.txtTue.Enabled = false;
            this.txtTue.Location = new System.Drawing.Point(125, 50);
            this.txtTue.Mask = "00:00";
            this.txtTue.Name = "txtTue";
            this.txtTue.Size = new System.Drawing.Size(49, 23);
            this.txtTue.TabIndex = 38;
            this.txtTue.ValidatingType = typeof(System.DateTime);
            // 
            // txtWed
            // 
            this.txtWed.Enabled = false;
            this.txtWed.Location = new System.Drawing.Point(188, 48);
            this.txtWed.Mask = "00:00";
            this.txtWed.Name = "txtWed";
            this.txtWed.Size = new System.Drawing.Size(49, 23);
            this.txtWed.TabIndex = 39;
            this.txtWed.ValidatingType = typeof(System.DateTime);
            // 
            // txtThu
            // 
            this.txtThu.Enabled = false;
            this.txtThu.Location = new System.Drawing.Point(256, 48);
            this.txtThu.Mask = "00:00";
            this.txtThu.Name = "txtThu";
            this.txtThu.Size = new System.Drawing.Size(49, 23);
            this.txtThu.TabIndex = 40;
            this.txtThu.ValidatingType = typeof(System.DateTime);
            // 
            // txtFri
            // 
            this.txtFri.Enabled = false;
            this.txtFri.Location = new System.Drawing.Point(321, 49);
            this.txtFri.Mask = "00:00";
            this.txtFri.Name = "txtFri";
            this.txtFri.Size = new System.Drawing.Size(49, 23);
            this.txtFri.TabIndex = 41;
            this.txtFri.ValidatingType = typeof(System.DateTime);
            // 
            // txtSat
            // 
            this.txtSat.Enabled = false;
            this.txtSat.Location = new System.Drawing.Point(382, 48);
            this.txtSat.Mask = "00:00";
            this.txtSat.Name = "txtSat";
            this.txtSat.Size = new System.Drawing.Size(49, 23);
            this.txtSat.TabIndex = 42;
            this.txtSat.ValidatingType = typeof(System.DateTime);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 218);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNegativeQty);
            this.Controls.Add(this.txtNegativeQtyCheck);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtMaxSkusToUpdate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkDisableAutoUpdate);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkDisableAutoUpdate;
        private System.Windows.Forms.TextBox txtMaxSkusToUpdate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtNegativeQtyCheck;
        private System.Windows.Forms.TextBox txtNegativeQty;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxSun;
        private System.Windows.Forms.CheckBox checkBoxMon;
        private System.Windows.Forms.CheckBox checkBoxTue;
        private System.Windows.Forms.CheckBox checkBoxWed;
        private System.Windows.Forms.CheckBox checkBoxThu;
        private System.Windows.Forms.CheckBox checkBoxFri;
        private System.Windows.Forms.CheckBox checkBoxSat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox txtSat;
        private System.Windows.Forms.MaskedTextBox txtFri;
        private System.Windows.Forms.MaskedTextBox txtThu;
        private System.Windows.Forms.MaskedTextBox txtWed;
        private System.Windows.Forms.MaskedTextBox txtTue;
        private System.Windows.Forms.MaskedTextBox txtMon;
        private System.Windows.Forms.MaskedTextBox txtSun;
    }
}