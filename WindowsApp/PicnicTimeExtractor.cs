using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PicnicTimeExtractor : Form
    {
        // Dictionary of variations, store Item as string and Parent Export Row
        private Dictionary<string, PicnicTimeExportRow> mVariations = new Dictionary<string, PicnicTimeExportRow>();
        // Store data from Map Policy excel file
        private DataTable mPolicyTable;

        private Vendor vendorInfo;

        public PicnicTimeExtractor()
        {
            InitializeComponent();

            OutputFolderText.Text = Properties.Settings.Default.PicnicTimeExtractor_OutputFolder;
            vendorInfo = new DAL().GetVendor((int)VendorName.PicnicTime);
        }

        /// <summary>
        /// Validate data on the main tab
        /// </summary>
        /// <returns>True if all data valid, otherwise false</returns>
        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(ProductDataFileText.Text))
            {
                Util.ShowMessage("Please select file with product data");
                return false;
            }
            if (string.IsNullOrEmpty(MAPPolicyFileText.Text))
            {
                Util.ShowMessage("Please select file with MAP policy data");
                return false;
            }
            if (string.IsNullOrEmpty(OutputFolderText.Text))
            {
                Util.ShowMessage("Please select output folder");
                return false;
            }

            return true;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                try
                {
                    // Disabled controls
                    TabPages.Enabled = false;
                    CloseButton.Enabled = false;

                    // Read products excel file
                    DataTable lProductTable = ReadProductFile(ProductDataFileText.Text);
                    // Read MAP plicy excel file (for Title field only)
                    mPolicyTable = ReadExcelSheet(MAPPolicyFileText.Text, "Master$");

                    SavePicnicTimeExportRows(lProductTable);

                    Properties.Settings.Default.PicnicTimeExtractor_OutputFolder = OutputFolderText.Text;
                    Properties.Settings.Default.Save();
                    Util.ShowMessage("Operation completed successfully");
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
                finally
                {
                    // Enabled controls
                    TabPages.Enabled = true;
                    CloseButton.Enabled = true;
                }
            }
        }

        /// <summary>
        /// Creates list of rows for exporting
        /// </summary>
        /// <param name="aProductTable">Product table</param>
        private void SavePicnicTimeExportRows(DataTable aProductTable)
        {
            List<PicnicTimeExportRow> lResultList = new List<PicnicTimeExportRow>();
            lResultList.Add(PicnicTimeExportRow.CreateHeader());

            IList<PicnicTimePricingExportRow> pricingList = new List<PicnicTimePricingExportRow>();
            pricingList.Add(PicnicTimePricingExportRow.GetHeaderRow());

            IList<PicnicTimeShippingExportRow> shippingList = new List<PicnicTimeShippingExportRow>();
            shippingList.Add(PicnicTimeShippingExportRow.GetHeaderRow());

            // Get list of entered SKUs if necessary
            IList<string> selectedSKUs = new List<string>();
            if (scrapeSKUs.Checked)
            {
                for (int i = 0; i < SKUsList.Lines.GetLength(0); i++)
                {
                    if (string.IsNullOrEmpty(SKUsList.Lines[i])) continue;
                    string sku = SKUsList.Lines[i];
                    sku = sku.Replace("Error for SKU:", "");
                    sku = sku.Replace("'The specified Sku does not exist!'", "");
                    selectedSKUs.Add(sku.Trim());
                }
            }

            foreach (DataRow lRow in aProductTable.Rows)
            {
                // Check SKU
                if (scrapeSKUs.Checked)
                {
                    string sku = string.Format("{0}{1}-{2}", vendorInfo.SkuPrefix,
                        lRow[1].ToString(), lRow[2].ToString());
                    if (!selectedSKUs.Contains(sku)) continue;
                }

                if (!string.IsNullOrEmpty(lRow["Item"].ToString()))
                {
                    // Product file
                    PicnicTimeExportRow lExportRow = new PicnicTimeExportRow();
                    // Get all rows with the same Item from product table and description for this item from MAP Policy table
                    string lItem = lRow["Item"].ToString();
                    DataRow[] lRowsWithCurrentItem = aProductTable.Select("Item = '" + lItem + "'");
                    DataRow[] lPolicyRow = mPolicyTable.Select("[SKU#] = '" + lItem + "'");
                    //string lPolicyDescription = lPolicyRow.Length > 0 ? lPolicyRow[0][3].ToString() : "";

                    // Exporting product file
                    // Check if current Item has variations
                    if (lRowsWithCurrentItem.Length > 1)
                    {// Has variations
                        PicnicTimeExportRow lParentRow;
                        if (mVariations.ContainsKey(lRow["Item"].ToString()))
                            lParentRow = mVariations[lRow["Item"].ToString()];
                        else
                        {
                            lParentRow = GetParentExportRow(lRow);
                            mVariations.Add(lRow["Item"].ToString(), lParentRow);
                            lResultList.Add(lParentRow);
                        }
                        lExportRow = GetExportRow(lRow, lParentRow);
                    }
                    else
                    {// Without variations
                        lExportRow = GetExportRow(lRow, null);
                    }

                    lResultList.Add(lExportRow);

                    // Pricing file
                    pricingList.Add(GetPricingRow(lRow, lPolicyRow.Length > 0 ? lPolicyRow[0] : null));

                    // Shipping file
                    shippingList.Add(GetShippingRow(lRow, lPolicyRow.Length > 0 ? lPolicyRow[0] : null));
                }
            }

            // Export product file
            string lFileName = string.Format("{0}{1}.txt", OutputFolderText.Text, DateTime.Now.ToString("MMddyyyy"));
            PicnicTimeExporter.ExportPicnicTimeData(lFileName, lResultList);
            // Export pricing file
            string pricingFileName = string.Format("{0}{1}pricingsheet.txt", OutputFolderText.Text, DateTime.Now.ToString("MMddyyyy"));
            PicnicTimePricingExportRow.ExportPicnicTimePricingData(pricingFileName, pricingList);
            // Export shipping file
            string shippingFileName = string.Format("{0}{1}shipping.txt", OutputFolderText.Text, DateTime.Now.ToString("MMddyyyy"));
            PicnicTimeShippingExportRow.ExportPicnicTimeShippingData(shippingFileName, shippingList);
        }

        private PicnicTimePricingExportRow GetPricingRow(DataRow productRow, DataRow policyRow)
        {
            PicnicTimePricingExportRow row = new PicnicTimePricingExportRow();
            row.SKU = string.Format("{0}{1}-{2}", vendorInfo.SkuPrefix,
                       productRow[1].ToString(), productRow[2].ToString());
            float lWholesalePrice;
            if (float.TryParse(productRow[9].ToString(), out lWholesalePrice))
                row.SellerCost = (lWholesalePrice +
                        float.Parse(Properties.Settings.Default.PicnicTimeExtractor_SellectCostAddition, new CultureInfo("en-US"))).ToString();
            else
                row.SellerCost = "";
            float msrp;
            if (float.TryParse(productRow[10].ToString(), out msrp))
                row.RetailPrice = msrp.ToString();
            else
                row.RetailPrice = "";
            float map;
            if (float.TryParse(productRow[11].ToString(), out map))
                row.MAP = map.ToString();
            else
                row.MAP = "";

            return row;
        }

        private PicnicTimeShippingExportRow GetShippingRow(DataRow productRow, DataRow policyRow)
        {
            PicnicTimeShippingExportRow row = new PicnicTimeShippingExportRow();
            row.SKU = string.Format("{0}{1}-{2}", vendorInfo.SkuPrefix,
                       productRow[1].ToString(), productRow[2].ToString());
            row.Classification = "Picnic Time";
            row.WarehouseLocation = Properties.Settings.Default.PicnicTimeExtractor_WarehouseLocation;
            row.Weight = productRow[31].ToString();

            return row;
        }

        /// <summary>
        /// Create export row
        /// </summary>
        /// <param name="aDataRow">DataRow for current Item</param>
        /// <param name="aPolicyDescription">Description from MAP Ploicy table</param>
        /// <param name="aParentRow">Parent row, or null for items without variations</param>
        /// <returns></returns>
        private PicnicTimeExportRow GetExportRow(DataRow aDataRow, PicnicTimeExportRow aParentRow)
        {
            PicnicTimeExportRow lExportRow = new PicnicTimeExportRow();

            //
            string lTitle = string.Format("{0} {1}", aDataRow[3].ToString(), aDataRow[7].ToString().Replace("\n", " "));
            lExportRow.AuctionTitle = lTitle.Length > Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength ?
                lTitle.Substring(0, Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength) : lTitle;
            //
            lExportRow.InventoryNumber = string.Format("{0}{1}-{2}", vendorInfo.SkuPrefix,
                        aDataRow[1].ToString(), aDataRow[2].ToString());
            lExportRow.Weight = aDataRow[31].ToString();
            lExportRow.UPC = aDataRow[8].ToString();
            lExportRow.MPN = string.Format("{0}-{1}", aDataRow[1].ToString(), aDataRow[2].ToString());
            //
            float lWholesalePrice;
            if (float.TryParse(aDataRow[9].ToString(), out lWholesalePrice))
                lExportRow.SellerCost = (lWholesalePrice +
                        float.Parse(Properties.Settings.Default.PicnicTimeExtractor_SellectCostAddition, new CultureInfo("en-US"))).ToString();
            else
                lExportRow.SellerCost = "";
            //
            float lMSRP;
            lExportRow.RetailPrice = float.TryParse(aDataRow[10].ToString(), out lMSRP) ? lMSRP.ToString() : "";
            // Get picture file name, substitute first symbol with its upper variant
            //string lLowerImageFileName = aDataRow[5].ToString();
            //if (lLowerImageFileName.Length > 1)
            //{
            //    lExportRow.PictureURLs = GetImageURL(lLowerImageFileName);
            //}
            //else
                lExportRow.PictureURLs = "";
            //
            lExportRow.SupplierCode = Properties.Settings.Default.PicnicTimeExtractor_SupplierCode;
            lExportRow.WarehouseLocation = Properties.Settings.Default.PicnicTimeExtractor_WarehouseLocation;
            //
            string lDescription = CreateDescription(aDataRow, false);
            lExportRow.Description = lDescription;
            //
            if (aParentRow == null) // Without variations
            {  
                lExportRow.RelationshipName = "";
                lExportRow.VariationParentSKU = "";
                lExportRow.Attribute4Name = "";
                lExportRow.Attribute4Value = "";
            }
            else // For variations
            {
                lExportRow.RelationshipName = "Picnic Time Colors";
                lExportRow.VariationParentSKU = aParentRow.InventoryNumber;
                lExportRow.Attribute4Name = "Color";
                lExportRow.Attribute4Value = aDataRow[7].ToString();
            }
            lExportRow.CAStoreDescription = lDescription;
            lExportRow.DCCode = Properties.Settings.Default.PicnicTimeExtractor_DCCode;
            lExportRow.CAStoreTitle = lTitle.Length > Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength ?
                lTitle.Substring(0, Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength) : lTitle;
            lExportRow.Classification = "Picnic Time";
            lExportRow.Attribute1Name = "Brand";
            lExportRow.Attribute1Value = "Picnic Time";
            lExportRow.Attribute2Name = "Manufacturer Warranty";
            lExportRow.Attribute2Value = "Limited Lifetime";
            lExportRow.Attribute3Name = "MAP";
            //
            float lMAP;
            lExportRow.Attribute3Value = float.TryParse(aDataRow[10].ToString(), out lMAP) ? lMAP.ToString() : "";

            return lExportRow;
        }

        /// <summary>
        /// Create parent export row for cpecified child row
        /// </summary>
        /// <param name="aChildRow">First child row</param>
        /// <param name="aPolicyDescription">Description from MAP Plicy table</param>
        /// <returns></returns>
        private PicnicTimeExportRow GetParentExportRow(DataRow aChildRow)
        {
            PicnicTimeExportRow lParentExportRow = new PicnicTimeExportRow();
            //
            string lTitle = string.Format("{0}", aChildRow[3].ToString().Replace("\n", " "));
            lParentExportRow.AuctionTitle = lTitle.Length > Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength ?
                lTitle.Substring(0, Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength) : lTitle;
            //
            lParentExportRow.InventoryNumber = string.Format("{0}{1}-Parent", vendorInfo.SkuPrefix,
                        aChildRow[1].ToString());
            lParentExportRow.Weight = "";
            lParentExportRow.UPC = "";
            lParentExportRow.MPN = string.Format("{0}-Parent", aChildRow[1].ToString());
            //
            string lDescription = CreateDescription(aChildRow, true);
            lParentExportRow.Description = lDescription;
            //
            lParentExportRow.SellerCost = "";
            lParentExportRow.RetailPrice = "";
            //
            //string lLowerImageFileName = aChildRow[5].ToString();
            //if (lLowerImageFileName.Length > 1)
            //{
            //    lParentExportRow.PictureURLs = GetImageURL(lLowerImageFileName);
            //}
            //else
                lParentExportRow.PictureURLs = string.Empty;
            //
            lParentExportRow.SupplierCode = Properties.Settings.Default.PicnicTimeExtractor_SupplierCode;
            lParentExportRow.WarehouseLocation = Properties.Settings.Default.PicnicTimeExtractor_WarehouseLocation;
            lParentExportRow.RelationshipName = "Picnic Time Colors";
            lParentExportRow.VariationParentSKU = "Parent";
            lParentExportRow.DCCode = Properties.Settings.Default.PicnicTimeExtractor_DCCode;
            lParentExportRow.CAStoreTitle = lTitle.Length > Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength ?
                lTitle.Substring(0, Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength) : lTitle;
            lParentExportRow.CAStoreDescription = lDescription;
            lParentExportRow.Classification = "Picnic Time";
            lParentExportRow.Attribute1Name = "Brand";
            lParentExportRow.Attribute1Value = "Picnic Time";
            lParentExportRow.Attribute2Name = "Manufacturer Warranty";
            lParentExportRow.Attribute2Value = "Limited Lifetime";
            lParentExportRow.Attribute3Name = "";
            lParentExportRow.Attribute3Value = "";
            lParentExportRow.Attribute4Name = "";
            lParentExportRow.Attribute4Value = "";

            return lParentExportRow;
        }

        /// <summary>
        /// Create image URL
        /// </summary>
        /// <param name="aOriginalPictureName">Picture name from product excel file</param>
        /// <returns>Image URL</returns>
        private string GetImageURL(string aOriginalPictureName)
        {
            string[] lWords = aOriginalPictureName.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);

            StringBuilder lUpperFileName = new StringBuilder();
            for (int i = 0; i < lWords.Length; i++)
            {
                if (i > 0)
                    lUpperFileName.Append("_");
                lUpperFileName.Append(lWords[i][0].ToString().ToUpper());
                lUpperFileName.Append(lWords[i].Substring(1));
            }

            return Properties.Settings.Default.PicnicTimeExtractor_ImageURLs + lUpperFileName.ToString();
        }

        /// <summary>
        /// Create HTML file of description for specified row
        /// </summary>
        /// <param name="aRow">Row which require description</param>
        /// <param name="aIsParent">If true indecates that current row is parent, otherwise it is not parent (used for color description)</param>
        /// <returns>HTML text of description</returns>
        private string CreateDescription(DataRow aRow, bool aIsParent)
        {
            StringBuilder lSB = new StringBuilder();
            lSB.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\">");
            lSB.Append("<html>");
            lSB.Append("<head>");
            lSB.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
            lSB.Append("<title></title>");
            lSB.Append("</head>");
            lSB.Append("<body bgcolor=\"#ffffff\">");
            lSB.Append(string.Format("<p>{0}</p>", aRow[4].ToString()));
            lSB.Append("<p><span style=\"FONT-SIZE: 12pt; FONT-FAMILY: 'Times New Roman';\">");
            lSB.Append(string.Format("Approximate Dimensions: {0}</span></p>", aRow[12].ToString()));
            //if (!aIsParent)
            //{
            //    lSB.Append("<p><span style=\"FONT-SIZE: 12pt; FONT-FAMILY: 'Times New Roman'\">");
            //    lSB.Append("</span></p>");
            //}
            
            lSB.Append("<p><span style=\"FONT-SIZE: 12pt; FONT-FAMILY: 'Times New Roman'\">");
            lSB.Append(" Images are for informational purposes and may or may not represent this specific color. ");
            lSB.Append("Only the&nbsp;description determines the&nbsp;color to be shipped, not the&nbsp;");
            lSB.Append("image. Each listing is for 1 main item and related components.</span></p>");
            lSB.Append("</body></html>");

            return lSB.ToString().Replace("\n", " ");
        }

        /// <summary>
        /// Reads product file for picnic time extractor
        /// </summary>
        /// <param name="fileName">Name of excel file</param>
        /// <returns>Table with whole data from first sheet of excel file</returns>
        private DataTable ReadProductFile(string fileName)
        {
            string lConnectionString = string.Format(ConfigurationManager.AppSettings["Excel2007ConString"].ToString(), fileName);
            string sheetName = string.Empty;

            using (OleDbConnection lConn = new OleDbConnection(lConnectionString))
            {
                lConn.Open();
                DataTable tables = lConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if ((tables != null) && (tables.Rows.Count > 0))
                {
                    sheetName = tables.Rows[0]["TABLE_NAME"].ToString();
                    sheetName = sheetName.Substring(1, sheetName.Length - 2);
                }
                lConn.Close();
            }

            return ReadExcelSheet(fileName, sheetName);
        }

        /// <summary>
        /// Read all data from specified spreadsheet in specified excel file
        /// </summary>
        /// <param name="aExcelFileName">Excel file name</param>
        /// <param name="aSheetName">Spreadsheet name</param>
        /// <returns>Table with whole data from spreadsheet</returns>
        private DataTable ReadExcelSheet(string aExcelFileName, string aSheetName)
        {
            string lConnectionString = string.Format(ConfigurationManager.AppSettings["Excel2007ConString"].ToString(), aExcelFileName);
            DataTable lTable = new DataTable();

            using (OleDbConnection lConn = new OleDbConnection(lConnectionString))
            {
                lConn.Open();
                OleDbDataAdapter lAdapter = new OleDbDataAdapter(string.Format("select * from [{0}]", aSheetName), lConn);
                lAdapter.Fill(lTable);
                lConn.Close();
            }

            return lTable;
        }

        #region Settings
        private bool ValidateSettings()
        {
            // Validate auction title length
            int lAuctionTitleLength;
            if (!int.TryParse(AuctionTitleText.Text, out lAuctionTitleLength))
            {
                Util.ShowMessage("Incorrect value for Auction Title Length");
                return false;
            }
            if ((lAuctionTitleLength <= 0) || (lAuctionTitleLength > int.MaxValue))
            {
                Util.ShowMessage("Incorrect value for Auction Title Length");
                return false;
            }

            // Validate CA store title length
            int lCAStoreTitleLength;
            if (!int.TryParse(CAStoreTitleLengthText.Text, out lCAStoreTitleLength))
            {
                Util.ShowMessage("Incorrect value for CA Store Title Length");
                return false;
            }
            if ((lCAStoreTitleLength <= 0) || (lCAStoreTitleLength > int.MaxValue))
            {
                Util.ShowMessage("Incorrect value for Auction Title Length");
                return false;
            }

            // Validate seller cost addition
            float lSellerCostAddition;
            if (!float.TryParse(SellerCostAdditionText.Text, NumberStyles.Float, new CultureInfo("en-US"), out lSellerCostAddition))
            {
                Util.ShowMessage("Incorrect value for Seller Cost Addition");
                return false;
            }
            if ((lSellerCostAddition <= 0) || (lSellerCostAddition > float.MaxValue))
            {
                Util.ShowMessage("Incorrect value for Seller Cost Addition");
                return false;
            }

            if (!FileAndDirectoryUtils.ValidateFileNamePrefix(FilePrefixText.Text))
            {
                Util.ShowMessage("Incorrect value for File Name Prefix");
                return false;
            }

            return true;
        }

        private void LoadSettings()
        {
            AuctionTitleText.Text = Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength.ToString();
            CAStoreTitleLengthText.Text = Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength.ToString();
            ImageURLsText.Text = Properties.Settings.Default.PicnicTimeExtractor_ImageURLs;
            SellerCostAdditionText.Text = Properties.Settings.Default.PicnicTimeExtractor_SellectCostAddition;
            SupplierCodeText.Text = Properties.Settings.Default.PicnicTimeExtractor_SupplierCode;
            WarehouseLocationText.Text = Properties.Settings.Default.PicnicTimeExtractor_WarehouseLocation;
            DCCodeText.Text = Properties.Settings.Default.PicnicTimeExtractor_DCCode;
            FilePrefixText.Text = Properties.Settings.Default.PicnicTimeExtractor_FilePrefix;
        }

        private void SaveSettings()
        {
            if (ValidateSettings())
            {
                Properties.Settings.Default.PicnicTimeExtractor_AuctionTitleLength = int.Parse(AuctionTitleText.Text);
                Properties.Settings.Default.PicnicTimeExtractor_CAStoreTitleLength = int.Parse(CAStoreTitleLengthText.Text);
                Properties.Settings.Default.PicnicTimeExtractor_ImageURLs = ImageURLsText.Text;
                Properties.Settings.Default.PicnicTimeExtractor_SellectCostAddition = SellerCostAdditionText.Text;
                Properties.Settings.Default.PicnicTimeExtractor_SupplierCode = SupplierCodeText.Text;
                Properties.Settings.Default.PicnicTimeExtractor_WarehouseLocation = WarehouseLocationText.Text;
                Properties.Settings.Default.PicnicTimeExtractor_DCCode = DCCodeText.Text;
                Properties.Settings.Default.PicnicTimeExtractor_FilePrefix = FilePrefixText.Text;

                Properties.Settings.Default.Save();
                Util.ShowMessage("Settings saved");
            }
        }
        #endregion

        #region Dialogs
        private void ProductDataFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ProductDataFileText.Text = openFileDialog1.FileName;
            }
        }

        private void MAPPolicyFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MAPPolicyFileText.Text = openFileDialog1.FileName;
            }
        }

        private void OutputFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string lOutputFolder = folderBrowserDialog1.SelectedPath;

                if (lOutputFolder.Substring(lOutputFolder.Length - 1, 1) != @"\")
                    lOutputFolder += @"\";

                OutputFolderText.Text = lOutputFolder;
            }
        }
        #endregion

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void SettingsTabPage_Enter(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void scrapeAll_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                SKUsList.Enabled = false;
        }

        private void scrapeSKUs_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                SKUsList.Enabled = true;
        }
    }
}
