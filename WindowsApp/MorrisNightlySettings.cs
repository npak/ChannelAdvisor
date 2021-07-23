using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class MorrisNightlySettings : Form
    {
        private Vendor Morris { get; set; }

        public MorrisNightlySettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Morris = dal.GetVendor((int)VendorName.MorrisNightly);

            commonSettings.VendorInfo = Morris;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MorrisSettings_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }//end event


        /// <summary>
        /// 
        /// </summary>
        private void DisplayInfo()
        {
            string url = "";
           // string url_count = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            DAL dal = new DAL();

            dal.GetMorrisNightlySettings(out url, out dropshipfee,out csvfolder,out csvfilename, out csvIsftp);
            txtURL.Text = url;
            //txtUrlCount.Text = url_count;
            txtDropShipFee.Text = dropshipfee;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false; 
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                bool isSuccess = new DAL().SaveMorrisNightlySettings(txtURL.Text.Trim(), txtDropShipFee.Text.Trim(),txtFolderName.Text,txtFileName.Text,rbFTP.Checked);

                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }//end isSucess if
            }
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            if (!commonSettings.ValidateInput())
                return false;

            if (txtURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid URL");
                txtURL.Focus();
                return false;
            }
            if (txtFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a Folder name");
                txtFolderName.Focus();
                return false;
            }

            if (txtFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name");
                txtFileName.Focus();
                return false;
            }
            // from 0 to 99.99
            if (!string.IsNullOrWhiteSpace(txtDropShipFee.Text))
            {
                Regex regex = new Regex(@"(^\d{1,2}$)|(^\d{0,2}[.]\d{1,2}$)");  //(@"\d+");
                Match match = regex.Match(txtDropShipFee.Text);
                if (!match.Success)
                {
                    Util.ShowMessage("Please enter a vadid Drop Ship Fee.");
                    return false;
                }
            }

            return true;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //private void txtFileName_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void label6_Click(object sender, EventArgs e)
        //{

        //}

        //private void txtFolderName_TextChanged(object sender, EventArgs e)
        //{

        //}

        //private void label8_Click(object sender, EventArgs e)
        //{

        //}

        //private void label4_Click(object sender, EventArgs e)
        //{

        //}

        //private void groupBox1_Enter(object sender, EventArgs e)
        //{

        //}
    }
}
