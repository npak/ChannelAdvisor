using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class NearlyNaturalSettings : Form
    {
        private Vendor NearlyNatural { get; set; }

        public NearlyNaturalSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            NearlyNatural = dal.GetVendor((int)VendorName.NearlyNatural);

            commonSettings.VendorInfo = NearlyNatural;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NearlyNaturalSettings_Load(object sender, EventArgs e)
        {
            DisplayNearlyNaturalInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayNearlyNaturalInfo()
        {
            string url = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            string csvfilenameOriginal = "";

            DAL dal = new DAL();
            dal.GetNearlyNaturalInfo(out url, out csvfolder, out csvfilename, out csvIsftp, out dropshipfee, out csvfilenameOriginal);

            txtURL.Text = url;
            txtDropShipFee.Text = dropshipfee;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            txtFilenamOriginal.Text = csvfilenameOriginal;
            rbFTP.Checked = csvIsftp == "1" ? true : false;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
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

            if (txtFilenamOriginal.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name (with renamed fields)");
                txtFilenamOriginal.Focus();
                return false;
            }
            return true;
        }

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

                bool isSuccess
                    = new DAL().SaveNearlyNaturalSettings(txtFilenamOriginal.Text, txtURL.Text.Trim(),
                                                        txtFolderName.Text.Trim(), txtFileName.Text.Trim(), rbFTP.Checked, txtDropShipFee.Text.Trim());

                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("The record could not be saved!");
                }//end isSucess if

            }//end if
        }//end event

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
