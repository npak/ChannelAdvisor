namespace ChannelAdvisor
{
    partial class TransportCodes
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgTransportCodes = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CarrierID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransportID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Carrier = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShippingMethod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgTransportCodes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(828, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(36, 30);
            this.btnDelete.TabIndex = 31;
            this.btnDelete.Text = "-";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(441, 348);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 30);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgTransportCodes
            // 
            this.dgTransportCodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTransportCodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Code,
            this.CarrierID,
            this.TransportID,
            this.Carrier,
            this.ShippingMethod});
            this.dgTransportCodes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgTransportCodes.Location = new System.Drawing.Point(12, 12);
            this.dgTransportCodes.Name = "dgTransportCodes";
            this.dgTransportCodes.Size = new System.Drawing.Size(810, 327);
            this.dgTransportCodes.TabIndex = 28;
            this.dgTransportCodes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTransportCodes_CellContentClick);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(354, 348);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(81, 30);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "StoneEdge Ship Code";
            this.Code.Name = "Code";
            // 
            // CarrierID
            // 
            this.CarrierID.DataPropertyName = "CarrierID";
            this.CarrierID.HeaderText = "EMG Carrier ID";
            this.CarrierID.Name = "CarrierID";
            this.CarrierID.Width = 150;
            // 
            // TransportID
            // 
            this.TransportID.DataPropertyName = "TransportID";
            this.TransportID.HeaderText = "EMG Route Code";
            this.TransportID.Name = "TransportID";
            this.TransportID.Width = 120;
            // 
            // Carrier
            // 
            this.Carrier.DataPropertyName = "Carrier";
            this.Carrier.HeaderText = "Carrier";
            this.Carrier.Name = "Carrier";
            this.Carrier.Width = 150;
            // 
            // ShippingMethod
            // 
            this.ShippingMethod.DataPropertyName = "ShippingMethod";
            this.ShippingMethod.HeaderText = "Shipping Method";
            this.ShippingMethod.Name = "ShippingMethod";
            this.ShippingMethod.Width = 220;
            // 
            // TransportCodes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 387);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgTransportCodes);
            this.Controls.Add(this.btnSave);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TransportCodes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transport Codes";
            this.Load += new System.EventHandler(this.TransportCodes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgTransportCodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgTransportCodes;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn CarrierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransportID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Carrier;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShippingMethod;
    }
}