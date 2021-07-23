namespace ChannelAdvisor
{
    partial class MWSSearch
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
            this.cmbSite = new System.Windows.Forms.ComboBox();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.grvAmazonResult = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.cIDType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cASIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBinding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cBrand = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cItemPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cManufacturer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cNumberOfItems = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPartNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cProductGroup = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cProductTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cProductTypeSubcategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cSmallImageURL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cVariationChildsString = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grvAmazonResult)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSite
            // 
            this.cmbSite.FormattingEnabled = true;
            this.cmbSite.Items.AddRange(new object[] {
            "Amazon US"});
            this.cmbSite.Location = new System.Drawing.Point(22, 13);
            this.cmbSite.Name = "cmbSite";
            this.cmbSite.Size = new System.Drawing.Size(121, 21);
            this.cmbSite.TabIndex = 0;
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Location = new System.Drawing.Point(226, 12);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(121, 21);
            this.cmbType.TabIndex = 1;
            // 
            // btSearch
            // 
            this.btSearch.Location = new System.Drawing.Point(516, 10);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(75, 23);
            this.btSearch.TabIndex = 2;
            this.btSearch.Text = "Search";
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // grvAmazonResult
            // 
            this.grvAmazonResult.AllowUserToAddRows = false;
            this.grvAmazonResult.AllowUserToDeleteRows = false;
            this.grvAmazonResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grvAmazonResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvAmazonResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cIDType,
            this.cASIN,
            this.cBinding,
            this.cBrand,
            this.cColor,
            this.cItemPartNumber,
            this.cManufacturer,
            this.cModel,
            this.cNumberOfItems,
            this.Column7,
            this.cPartNumber,
            this.cSize,
            this.cTitle,
            this.cProductGroup,
            this.cProductTypeName,
            this.cProductTypeSubcategory,
            this.cSmallImageURL,
            this.cVariationChildsString,
            this.cPrice});
            this.grvAmazonResult.Location = new System.Drawing.Point(1, 55);
            this.grvAmazonResult.Name = "grvAmazonResult";
            this.grvAmazonResult.ReadOnly = true;
            this.grvAmazonResult.Size = new System.Drawing.Size(946, 307);
            this.grvAmazonResult.TabIndex = 3;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(845, 371);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 4;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cIDType
            // 
            this.cIDType.DataPropertyName = "UPC";
            this.cIDType.HeaderText = "UPC";
            this.cIDType.Name = "cIDType";
            // 
            // cASIN
            // 
            this.cASIN.DataPropertyName = "ASIN";
            this.cASIN.HeaderText = "ASIN";
            this.cASIN.Name = "cASIN";
            // 
            // cBinding
            // 
            this.cBinding.DataPropertyName = "Binding";
            this.cBinding.HeaderText = "Binding";
            this.cBinding.Name = "cBinding";
            // 
            // cBrand
            // 
            this.cBrand.DataPropertyName = "Brand";
            this.cBrand.HeaderText = "Brand";
            this.cBrand.Name = "cBrand";
            // 
            // cColor
            // 
            this.cColor.DataPropertyName = "Color";
            this.cColor.HeaderText = "Color";
            this.cColor.Name = "cColor";
            // 
            // cItemPartNumber
            // 
            this.cItemPartNumber.DataPropertyName = "ItemPartNumber";
            this.cItemPartNumber.HeaderText = "ItemPartNumber";
            this.cItemPartNumber.Name = "cItemPartNumber";
            // 
            // cManufacturer
            // 
            this.cManufacturer.DataPropertyName = "Manufacturer";
            this.cManufacturer.HeaderText = "Manufacturer";
            this.cManufacturer.Name = "cManufacturer";
            // 
            // cModel
            // 
            this.cModel.DataPropertyName = "Model";
            this.cModel.HeaderText = "Model";
            this.cModel.Name = "cModel";
            // 
            // cNumberOfItems
            // 
            this.cNumberOfItems.DataPropertyName = "NumberOfItems";
            this.cNumberOfItems.HeaderText = "NumberOfItems";
            this.cNumberOfItems.Name = "cNumberOfItems";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "PackageQuantity";
            this.Column7.HeaderText = "PackageQuantity";
            this.Column7.Name = "Column7";
            // 
            // cPartNumber
            // 
            this.cPartNumber.DataPropertyName = "PartNumber";
            this.cPartNumber.HeaderText = "PartNumber";
            this.cPartNumber.Name = "cPartNumber";
            // 
            // cSize
            // 
            this.cSize.DataPropertyName = "Size";
            this.cSize.HeaderText = "Size";
            this.cSize.Name = "cSize";
            // 
            // cTitle
            // 
            this.cTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.cTitle.DataPropertyName = "Title";
            this.cTitle.HeaderText = "Title";
            this.cTitle.Name = "cTitle";
            // 
            // cProductGroup
            // 
            this.cProductGroup.DataPropertyName = "ProductGroup";
            this.cProductGroup.HeaderText = "ProductGroup";
            this.cProductGroup.Name = "cProductGroup";
            // 
            // cProductTypeName
            // 
            this.cProductTypeName.DataPropertyName = "ProductTypeName";
            this.cProductTypeName.HeaderText = "ProductTypeName";
            this.cProductTypeName.Name = "cProductTypeName";
            // 
            // cProductTypeSubcategory
            // 
            this.cProductTypeSubcategory.DataPropertyName = "ProductTypeSubcategory";
            this.cProductTypeSubcategory.HeaderText = "ProductTypeSubcategory";
            this.cProductTypeSubcategory.Name = "cProductTypeSubcategory";
            // 
            // cSmallImageURL
            // 
            this.cSmallImageURL.DataPropertyName = "SmallImageURL";
            this.cSmallImageURL.HeaderText = "SmallImageURL";
            this.cSmallImageURL.Name = "cSmallImageURL";
            // 
            // cVariationChildsString
            // 
            this.cVariationChildsString.DataPropertyName = "VariationChildsString";
            this.cVariationChildsString.HeaderText = "VariationChilds";
            this.cVariationChildsString.Name = "cVariationChildsString";
            // 
            // cPrice
            // 
            this.cPrice.DataPropertyName = "Price";
            this.cPrice.HeaderText = "Price";
            this.cPrice.Name = "cPrice";
            // 
            // MWSSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 403);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grvAmazonResult);
            this.Controls.Add(this.btSearch);
            this.Controls.Add(this.cmbType);
            this.Controls.Add(this.cmbSite);
            this.Name = "MWSSearch";
            this.Text = "MWSSearch";
            this.Load += new System.EventHandler(this.MWSSearch_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvAmazonResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSite;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.DataGridView grvAmazonResult;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn cIDType;
        private System.Windows.Forms.DataGridViewTextBoxColumn cASIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBinding;
        private System.Windows.Forms.DataGridViewTextBoxColumn cBrand;
        private System.Windows.Forms.DataGridViewTextBoxColumn cColor;
        private System.Windows.Forms.DataGridViewTextBoxColumn cItemPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn cManufacturer;
        private System.Windows.Forms.DataGridViewTextBoxColumn cModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn cNumberOfItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPartNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn cTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn cProductGroup;
        private System.Windows.Forms.DataGridViewTextBoxColumn cProductTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cProductTypeSubcategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn cSmallImageURL;
        private System.Windows.Forms.DataGridViewTextBoxColumn cVariationChildsString;
        private System.Windows.Forms.DataGridViewTextBoxColumn cPrice;
    }
}