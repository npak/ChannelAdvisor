namespace ChannelAdvisor
{
    partial class RunShipstation
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
            this.button1 = new System.Windows.Forms.Button();
            this.caDataGridView1 = new ChannelAdvisor.CADataGridView();
            this.SKU = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PuchasePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cshipmentCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUSPSPriorityMailPackage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cinsuranceCost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUSPSParcelSelectGroundPackage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cParcelSelectinsurance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c1ClassMailinsurance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cUPSGround = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cGroundinsurance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNextDayAirSaverinsurance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.caDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(753, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // caDataGridView1
            // 
            this.caDataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.caDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.caDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SKU,
            this.PuchasePrice,
            this.cshipmentCost,
            this.cUSPSPriorityMailPackage,
            this.cinsuranceCost,
            this.cUSPSParcelSelectGroundPackage,
            this.cParcelSelectinsurance,
            this.cColumn1,
            this.c1ClassMailinsurance,
            this.cUPSGround,
            this.cGroundinsurance,
            this.Column1,
            this.cNextDayAirSaverinsurance,
            this.Column3,
            this.Column4});
            this.caDataGridView1.Location = new System.Drawing.Point(12, 12);
            this.caDataGridView1.Name = "caDataGridView1";
            this.caDataGridView1.Size = new System.Drawing.Size(1005, 396);
            this.caDataGridView1.TabIndex = 2;
            // 
            // SKU
            // 
            this.SKU.DataPropertyName = "sku";
            this.SKU.HeaderText = "SKU";
            this.SKU.Name = "SKU";
            // 
            // PuchasePrice
            // 
            this.PuchasePrice.DataPropertyName = "PurchasePrice";
            this.PuchasePrice.HeaderText = "Puchase Price";
            this.PuchasePrice.Name = "PuchasePrice";
            this.PuchasePrice.Width = 80;
            // 
            // cshipmentCost
            // 
            this.cshipmentCost.DataPropertyName = "existingDomesticShiping";
            this.cshipmentCost.HeaderText = "Existing Domestic Shipping";
            this.cshipmentCost.Name = "cshipmentCost";
            this.cshipmentCost.Width = 90;
            // 
            // cUSPSPriorityMailPackage
            // 
            this.cUSPSPriorityMailPackage.DataPropertyName = "USPSPriorityMail";
            this.cUSPSPriorityMailPackage.HeaderText = "USPS Priority Mail";
            this.cUSPSPriorityMailPackage.Name = "cUSPSPriorityMailPackage";
            this.cUSPSPriorityMailPackage.Width = 90;
            // 
            // cinsuranceCost
            // 
            this.cinsuranceCost.DataPropertyName = "PriorityMailinsurance";
            this.cinsuranceCost.HeaderText = "Priority Mail insurance";
            this.cinsuranceCost.Name = "cinsuranceCost";
            this.cinsuranceCost.Width = 80;
            // 
            // cUSPSParcelSelectGroundPackage
            // 
            this.cUSPSParcelSelectGroundPackage.DataPropertyName = "USPSParcelSelect";
            this.cUSPSParcelSelectGroundPackage.HeaderText = "USPS Parcel Select";
            this.cUSPSParcelSelectGroundPackage.Name = "cUSPSParcelSelectGroundPackage";
            this.cUSPSParcelSelectGroundPackage.Width = 90;
            // 
            // cParcelSelectinsurance
            // 
            this.cParcelSelectinsurance.DataPropertyName = "ParcelSelectinsurance";
            this.cParcelSelectinsurance.HeaderText = "Parcel Select insurance";
            this.cParcelSelectinsurance.Name = "cParcelSelectinsurance";
            this.cParcelSelectinsurance.Width = 80;
            // 
            // cColumn1
            // 
            this.cColumn1.DataPropertyName = "USPSFirstClassMail";
            this.cColumn1.HeaderText = "USPS First Class Mail";
            this.cColumn1.Name = "cColumn1";
            this.cColumn1.Width = 90;
            // 
            // c1ClassMailinsurance
            // 
            this.c1ClassMailinsurance.DataPropertyName = "FirstClassMailinsurance";
            this.c1ClassMailinsurance.HeaderText = "1th Class Mail insurance ";
            this.c1ClassMailinsurance.Name = "c1ClassMailinsurance";
            this.c1ClassMailinsurance.Width = 80;
            // 
            // cUPSGround
            // 
            this.cUPSGround.DataPropertyName = "UPSGround";
            this.cUPSGround.HeaderText = "UPS Ground";
            this.cUPSGround.Name = "cUPSGround";
            this.cUPSGround.Width = 90;
            // 
            // cGroundinsurance
            // 
            this.cGroundinsurance.DataPropertyName = "Groundinsurance";
            this.cGroundinsurance.HeaderText = "Ground insurance";
            this.cGroundinsurance.Name = "cGroundinsurance";
            this.cGroundinsurance.Width = 80;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "UPSNextDayAirSaver";
            this.Column1.HeaderText = "UPS Next Day Air Saver";
            this.Column1.Name = "Column1";
            // 
            // cNextDayAirSaverinsurance
            // 
            this.cNextDayAirSaverinsurance.DataPropertyName = "NextDayAirSaverinsurance";
            this.cNextDayAirSaverinsurance.HeaderText = "Next Day Air Saver insurance";
            this.cNextDayAirSaverinsurance.Name = "cNextDayAirSaverinsurance";
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "BestService";
            this.Column3.HeaderText = "Best Service";
            this.Column3.Name = "Column3";
            this.Column3.Width = 180;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "BestRate";
            this.Column4.HeaderText = "Best Rate";
            this.Column4.Name = "Column4";
            // 
            // RunShipstation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 447);
            this.Controls.Add(this.caDataGridView1);
            this.Controls.Add(this.button1);
            this.Name = "RunShipstation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Shipping Rates by API ";
            this.Load += new System.EventHandler(this.Shipstation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.caDataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private CADataGridView caDataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SKU;
        private System.Windows.Forms.DataGridViewTextBoxColumn PuchasePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn cshipmentCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUSPSPriorityMailPackage;
        private System.Windows.Forms.DataGridViewTextBoxColumn cinsuranceCost;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUSPSParcelSelectGroundPackage;
        private System.Windows.Forms.DataGridViewTextBoxColumn cParcelSelectinsurance;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn c1ClassMailinsurance;
        private System.Windows.Forms.DataGridViewTextBoxColumn cUPSGround;
        private System.Windows.Forms.DataGridViewTextBoxColumn cGroundinsurance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNextDayAirSaverinsurance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}

