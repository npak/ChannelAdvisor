using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class CWRExtractor : Form
    {
        private Vendor vendorInfo;

        public CWRExtractor()
        {
            InitializeComponent();

            OutputFolderText.Text = new DAL().GetSettingValue("CWRExtractor_OutputFolder");
        }

        private bool ValidateData()
        {
            if (string.IsNullOrEmpty(OutputFolderText.Text))
            {
                Util.ShowMessage("Please select output folder");
                return false;
            }

            if ((!scrapeAll.Checked) && (string.IsNullOrEmpty(SKUsList.Text.Trim())))
            {
                Util.ShowMessage("Please specify list of SKUs to extract");
                return false;
            }

            return true;
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

            // Validate number of products
            int numberOfProducts;
            if (!int.TryParse(countOfProductsText.Text, out numberOfProducts))
            {
                Util.ShowMessage("Incorrect value for Number of products per spreadsheet");
                return false;
            }
            if ((numberOfProducts <= 0) || (numberOfProducts > int.MaxValue))
            {
                Util.ShowMessage("Incorrect value for Number of products per spreadsheet");
                return false;
            }

            // Validate classification length
            int classificationLength;
            if (!int.TryParse(classificationLengthText.Text, out classificationLength))
            {
                Util.ShowMessage("Incorrect value for Classification Length");
                return false;
            }
            if ((classificationLength <= 0) || (classificationLength > int.MaxValue))
            {
                Util.ShowMessage("Incorrect value for Classification Length");
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
            DAL dal = new DAL();
            AuctionTitleText.Text = dal.GetSettingValue("CWRExtractor_AuctionTitleLength");
            CAStoreTitleLengthText.Text = dal.GetSettingValue("CWRExtractor_CAStoreTitleLength");
            countOfProductsText.Text = dal.GetSettingValue("CWRExtractor_NumberOfProducts");
            classificationLengthText.Text = dal.GetSettingValue("CWRExtractor_ClassificationLength");
            SupplierCodeText.Text = dal.GetSettingValue("CWRExtractor_SupplierCode");
            WarehouseLocationText.Text = dal.GetSettingValue("CWRExtractor_WarehouseLocation");
            DCCodeText.Text = dal.GetSettingValue("CWRExtractor_DCCode");
            FilePrefixText.Text = dal.GetSettingValue("CWRExtractor_FilePrefix");
        }

        private void SaveSettings()
        {
            if (ValidateSettings())
            {
                DAL dal = new DAL();
                dal.UpdateSetting("CWRExtractor_AuctionTitleLength", AuctionTitleText.Text);
                dal.UpdateSetting("CWRExtractor_CAStoreTitleLength", CAStoreTitleLengthText.Text);
                dal.UpdateSetting("CWRExtractor_NumberOfProducts", countOfProductsText.Text);
                dal.UpdateSetting("CWRExtractor_ClassificationLength", classificationLengthText.Text);
                dal.UpdateSetting("CWRExtractor_SupplierCode", SupplierCodeText.Text);
                dal.UpdateSetting("CWRExtractor_WarehouseLocation", WarehouseLocationText.Text);
                dal.UpdateSetting("CWRExtractor_DCCode", DCCodeText.Text);
                dal.UpdateSetting("CWRExtractor_FilePrefix", FilePrefixText.Text);

                Util.ShowMessage("Settings saved");
            }
        }
        #endregion

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void scrapeAll_CheckedChanged(object sender, EventArgs e)
        {
            if (scrapeAll.Checked)
                SKUsList.Enabled = false;
            else
                SKUsList.Enabled = true;
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

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (ValidateData())
            {
                try
                {
                    // Disabled controls
                    tabPages.Enabled = false;
                    closeButton.Enabled = false;

                    Extractors.CWRExtractor extractor = new ChannelAdvisor.Extractors.CWRExtractor(GetEnteredValues(), 
                        scrapeAll.Checked, OutputFolderText.Text);
                    extractor.Extract();

                    new DAL().UpdateSetting("CWRExtractor_OutputFolder", OutputFolderText.Text);
                    Util.ShowMessage("Operation completed successfully");
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
                finally
                {
                    // Enabled controls
                    tabPages.Enabled = true;
                    closeButton.Enabled = true;
                }
            }
        }

        private IList<string> GetEnteredValues()
        {
            List<string> lResult = new List<string>();

            foreach (string lValue in SKUsList.Lines)
                if (!string.IsNullOrEmpty(lValue))
                {
                    if (!lValue.Contains("SKU"))
                        lResult.Add(StringUtils.RemoveNonNumericCharacters(lValue.Trim()));
                    else
                    {
                        // For error messages, like: Error for SKU: SCF30013 'The specified Sku does not exist!'
                        string[] lStrings = lValue.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 0; i < lStrings.Length; i++)
                        {
                            if ((lStrings[i].Contains("SKU")) && (i < lStrings.Length - 1))
                            {
                                lResult.Add(StringUtils.RemoveNonNumericCharacters(lStrings[i + 1].Trim()));
                                break;
                            }
                        }
                    }
                }
            return lResult;
        }
    }
}
