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
    public partial class MorrisCompleteExtractor : Form
    {
       // private Vendor vendorInfo;

        public MorrisCompleteExtractor()
        {
            InitializeComponent();

            OutputFolderText.Text = new DAL().GetSettingValue("MorrisExtractor_OutputFolder");
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
            txtExtractFileUrl.Text = dal.GetSettingValue("MorrisCompleteExtractor_FileUrl");
            countOfProductsText.Text = dal.GetSettingValue("MorrisCompleteExtractor_NumberOfProducts");
            FilePrefixText.Text = dal.GetSettingValue("MorrisCompleteExtractor_FilePrefix");
        }

        private void SaveSettings()
        {
            if (ValidateSettings())
            {
                DAL dal = new DAL();
                dal.UpdateSetting("MorrisCompleteExtractor_FileUrl", txtExtractFileUrl.Text);
                dal.UpdateSetting("MorrisCompleteExtractor_NumberOfProducts", countOfProductsText.Text);
                dal.UpdateSetting("MorrisCompleteExtractor_FilePrefix", FilePrefixText.Text);

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
                   // lblMessage.Text = "Converting csv files. Please wait...";
                    tabPages.Enabled = false;
                    closeButton.Enabled = false;
                    DAL dal = new DAL();
                    string xmlUrl = dal.GetSettingValue("MorrisCompleteExtractor_FileUrl");
                    Extractors.MorrisCompleteExtractor extractor = new Extractors.MorrisCompleteExtractor(GetEnteredValues(), 
                        scrapeAll.Checked, OutputFolderText.Text);
                    WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();
                    dialogWithWork.ShowWithWork(() =>
                    {
                        dialogWithWork.ShowMessage(string.Format("Fetching data from " + xmlUrl + ", please wait..."));
                        extractor.Extract();
                    });

                    new DAL().UpdateSetting("MorrisCompleteExtractor_OutputFolder", OutputFolderText.Text);
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
