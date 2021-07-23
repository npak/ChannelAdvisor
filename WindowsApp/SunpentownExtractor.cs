using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class SunpentownExtractor : Form
    {
        public SunpentownExtractor()
        {
            InitializeComponent();

            outputFolderText.Text = Properties.Settings.Default.SunpentownExtractor_OutputFolder;
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(excelFileText.Text))
            {
                Util.ShowMessage("Please select file with product data");
                return false;
            }
            if (string.IsNullOrEmpty(outputFolderText.Text))
            {
                Util.ShowMessage("Please select output folder");
                return false;
            }

            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                try
                {
                    // Disabled controls
                    sunpentownExtractorTabControl.Enabled = false;
                    closeButton.Enabled = false;

                    // Read excel file
                    DataTable lProductTable = ReadExcelSheet(excelFileText.Text);
                    SaveFile(GetExportRows(lProductTable));

                    Properties.Settings.Default.SunpentownExtractor_OutputFolder = outputFolderText.Text;
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
                    sunpentownExtractorTabControl.Enabled = true;
                    closeButton.Enabled = true;
                }
            }
        }

        private DataTable ReadExcelSheet(string aExcelFileName)
        {
            string lConnectionString = string.Format(ConfigurationManager.AppSettings["ExcelConStringIMEX"].ToString(), aExcelFileName);
            DataTable lTable = new DataTable();

            try
            {
                using (OleDbConnection lConn = new OleDbConnection(lConnectionString))
                {
                    lConn.Open();
                    OleDbDataAdapter lAdapter = new OleDbDataAdapter("select * from [Pricing$]", lConn);
                    lAdapter.Fill(lTable);
                    lConn.Close();
                }
            }
            catch
            {
                throw new Exception(string.Format("Cannot read {0} file", aExcelFileName));
            }

            return lTable;
        }

        private List<SunpentownExportRow> GetExportRows(DataTable table)
        {
            List<SunpentownExportRow> result = new List<SunpentownExportRow>();
            result.Add(SunpentownExportRow.CreateHeader());

            string lClassification = "";
            for (int i = 2; i < table.Rows.Count; i++)
            {
                if (!string.IsNullOrEmpty(table.Rows[i][0].ToString()))
                    lClassification = table.Rows[i][0].ToString().Replace("\n", " ");

                string lTitle = table.Rows[i][2].ToString();
                string lDescription = ExtractDescription(table.Rows[i][13].ToString());

                SunpentownExportRow exportRow = new SunpentownExportRow();
                exportRow.AuctionTitle = 
                    lTitle.Length > Properties.Settings.Default.SunpentownExtractor_AuctionTitleLength ? 
                    lTitle.Substring(0, Properties.Settings.Default.SunpentownExtractor_AuctionTitleLength) : 
                    lTitle;
                exportRow.InventoryNumber = table.Rows[i][7].ToString();
                exportRow.Weight = table.Rows[i][11].ToString();
                exportRow.UPC = table.Rows[i][7].ToString();
                exportRow.MPN = table.Rows[i][1].ToString();
                exportRow.Description = lDescription;
                exportRow.SellerCost = table.Rows[i][3].ToString();
                exportRow.RetailPrice = table.Rows[i][4].ToString();
                exportRow.PictureURLs = string.Format("{0}{1}.jpg",
                    Properties.Settings.Default.SunpentownExtractor_ImageURLs, table.Rows[i][1].ToString());
                exportRow.SupplierCode = Properties.Settings.Default.SunpentownExtractor_SupplierCode;
                exportRow.WarehouseLocation = Properties.Settings.Default.SunpentownExtractor_WarehouseLocation;
                exportRow.DCCode = Properties.Settings.Default.SunpentownExtractor_DCCode;
                exportRow.CAStoreTitle =
                    lTitle.Length > Properties.Settings.Default.SunpentownExtractor_CAStoreTitleLength ?
                    lTitle.Substring(0, Properties.Settings.Default.SunpentownExtractor_CAStoreTitleLength) :
                    lTitle;
                exportRow.CAStoreDescription = lDescription;
                exportRow.Classification = lClassification;
                exportRow.Attribute1Name = "Brand";
                exportRow.Attribute1Value = "SPT";
                exportRow.Attribute2Name = "Manufacturer Warranty";
                exportRow.Attribute2Value = "One-year parts and labor warranty";
                exportRow.Attribute3Name = "Domestic Shipping";
                exportRow.Attribute3Value = table.Rows[i][5].ToString();
                exportRow.Attribute4Name = "Domestic Shipping Additional";
                exportRow.Attribute4Value = table.Rows[i][5].ToString();
                exportRow.Attribute5Name = "MAP";
                exportRow.Attribute5Value = table.Rows[i][6].ToString();
                exportRow.Attribute6Name = "Size";
                exportRow.Attribute6Value = ExtractSize(lTitle);
                exportRow.ShipZoneName = "Domestic";
                exportRow.ShipCarrierCode = "Standard";
                exportRow.ShipClassCode = "Ground";
                exportRow.ShipRateFirstItem = table.Rows[i][5].ToString();
                exportRow.ShipRateAdditionalItem = table.Rows[i][5].ToString();

                result.Add(exportRow);
            }

            return result;
        }

        private string ExtractDescription(string descr)
        {
            string[] strings = descr.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            return GetHtml(strings);
        }

        private string GetHtml(string[] strings)
        {
            StringBuilder lSB = new StringBuilder();
            lSB.AppendLine("\"<!DOCTYPE HTML PUBLIC \"\"-//W3C//DTD HTML 3.2//EN\"\">");
            lSB.AppendLine("<html>");
            lSB.AppendLine("<head>");
            lSB.AppendLine("</head>");
            lSB.AppendLine("<body bgcolor=\"\"#ffffff\"\">");

            bool isFirst = true;
            foreach (string str in strings)
            {
                if (str[0].Equals('-'))
                {
                    if (isFirst)
                    {
                        lSB.AppendLine("<ul style=\"\"font-family: Arial;\"\">");
                        isFirst = false;
                    }
                    lSB.AppendLine("<li><font");
                    lSB.AppendLine(string.Format(" size=\"\"2\"\">{0}</font></li>", HttpUtility.HtmlEncode(str.Substring(1))));
                }
                else
                {
                    if (!isFirst)
                    {
                        lSB.Append("</ul>");
                        isFirst = true;
                    }
                    
                    lSB.Append("<p style=\"\"font-family: Arial;\"\"><span ");
                    lSB.AppendLine("style=\"\"FONT-SIZE: 12pt;\"\"><font");
                    lSB.AppendLine(string.Format(" size=\"\"2\"\">{0}</font></span></p>", HttpUtility.HtmlEncode(str)));
                }
            }
            if (!isFirst)
                lSB.Append("</ul>");
            lSB.AppendLine("</body>");
            lSB.Append("</html>\"");

            return lSB.ToString();
        }

        private string ExtractSize(string title)
        {
            string[] regExValues = new string[] { @"\d+,\s*\d+ BTU", @"\d+ BTU",
                @"\d+-pint", @"\d+-cups", @"\d+-bottle", 
                @"\d+W", @"\d+\.\d+L", @"\d+L", @"\d+\.\d+ cu\.ft"};
            foreach (string regEx in regExValues)
            {
                string result = GetSizeByRegEx(title, new Regex(regEx));
                if (!string.IsNullOrEmpty(result)) return result;
            }

            return string.Empty;
        }

        private string GetSizeByRegEx(string title, Regex r)
        {
            Match m = r.Match(title);
            if (m.Success)
            {
                return m.Value;
            }
            else
                return string.Empty;
        }

        private void SaveFile(List<SunpentownExportRow> rows)
        {
            string lFileName = string.Format("{0}{1}.txt", outputFolderText.Text, DateTime.Now.ToString("MMddyyyy"));
            SunpentownExporter.ExportSunpentownData(lFileName, rows);
        }

        #region Dialogs
        private void excelFileButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                excelFileText.Text = openFileDialog1.FileName;
            }
        }

        private void outputFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string lOutputFolder = folderBrowserDialog1.SelectedPath;

                if (lOutputFolder.Substring(lOutputFolder.Length - 1, 1) != @"\")
                    lOutputFolder += @"\";

                outputFolderText.Text = lOutputFolder;
            }
        }
        #endregion

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Settings
        private bool ValidateSettings()
        {
            // Validate auction title length
            int lAuctionTitleLength;
            if (!int.TryParse(auctionTitleLengthText.Text, out lAuctionTitleLength))
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

            if (!FileAndDirectoryUtils.ValidateFileNamePrefix(filePrefixText.Text))
            {
                Util.ShowMessage("Incorrect value for File Name Prefix");
                return false;
            }

            return true;
        }

        private void LoadSettings()
        {
            auctionTitleLengthText.Text = Properties.Settings.Default.SunpentownExtractor_AuctionTitleLength.ToString();
            CAStoreTitleLengthText.Text = Properties.Settings.Default.SunpentownExtractor_CAStoreTitleLength.ToString();
            imageURLsText.Text = Properties.Settings.Default.SunpentownExtractor_ImageURLs;
            supplierCode.Text = Properties.Settings.Default.SunpentownExtractor_SupplierCode;
            warehouseLocation.Text = Properties.Settings.Default.SunpentownExtractor_WarehouseLocation;
            DCCodeText.Text = Properties.Settings.Default.SunpentownExtractor_DCCode;
            filePrefixText.Text = Properties.Settings.Default.SunpentownExtractor_FilePrefix;
        }

        private void SaveSettings()
        {
            if (ValidateSettings())
            {
                Properties.Settings.Default.SunpentownExtractor_AuctionTitleLength = int.Parse(auctionTitleLengthText.Text);
                Properties.Settings.Default.SunpentownExtractor_CAStoreTitleLength = int.Parse(CAStoreTitleLengthText.Text);
                Properties.Settings.Default.SunpentownExtractor_ImageURLs = imageURLsText.Text;
                Properties.Settings.Default.SunpentownExtractor_SupplierCode = supplierCode.Text;
                Properties.Settings.Default.SunpentownExtractor_WarehouseLocation = warehouseLocation.Text;
                Properties.Settings.Default.SunpentownExtractor_DCCode = DCCodeText.Text;
                Properties.Settings.Default.SunpentownExtractor_FilePrefix = filePrefixText.Text;

                Properties.Settings.Default.Save();
                Util.ShowMessage("Settings saved");
            }
        }
        #endregion

        private void settingsTab_Enter(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }
    }
}
