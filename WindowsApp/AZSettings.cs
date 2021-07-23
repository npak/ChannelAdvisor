using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class AZSettings : Form
    {
        private Vendor AZ { get; set; }

        public AZSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            AZ = dal.GetVendor((int)VendorName.AZ);

            commonSettings.VendorInfo = AZ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AZSettings_Load(object sender, EventArgs e)
        {
            DisplayAZInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayAZInfo()
        {
            string url = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            string csvfilenameWithImage = "";

            DAL dal = new DAL();
            dal.GetAZInfo(out url, out csvfolder, out csvfilename, out csvIsftp, out dropshipfee, out csvfilenameWithImage);

            txtURL.Text = url;
            txtDropShipFee.Text = dropshipfee;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            txtFilenamWithImage.Text = csvfilenameWithImage;
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

            if (txtFilenamWithImage.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name (with renamed fields)");
                txtFilenamWithImage.Focus();
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
                    = new DAL().SaveAZSettings(txtFilenamWithImage.Text, txtURL.Text.Trim(),
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
