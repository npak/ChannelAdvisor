using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PetraSettings : Form
    {
        private Vendor Petra { get; set; }

        public PetraSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Petra = dal.GetVendor((int)VendorName.Petra);

            commonSettings.VendorInfo = Petra;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PetraSettings_Load(object sender, EventArgs e)
        {
            DisplayPetraInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPetraInfo()
        {
            string ftpUrl = "";
            string ftpFileName = ""; 
            string username = ""; 
            string password = ""; 
            //string qtyForAvailable = "";
            //string qtyForDate = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            string csvfilenameToServer = "";

            DAL dal = new DAL();
            dal.GetPetraInfo(out ftpUrl,
                                        out ftpFileName,
                                        out username,
                                        out password,
                                        out csvfolder, out csvfilename, out csvIsftp, out dropshipfee, out csvfilenameToServer);

            txtFtpURL.Text = ftpUrl;
            txtFtpFileName.Text = ftpFileName;
            txtUsername.Text = username;
            txtPassword.Text = password;
            txtDropShipFee.Text = dropshipfee;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false;
            txtFileNameOnServer.Text = csvfilenameToServer;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            if (txtFtpURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid URL");
                txtFtpURL.Focus();
                return false;
            }

            if (txtFtpFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a ftp file name");
                txtFtpFileName.Focus();
                return false;
            }

            if (txtUsername.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid username");
                txtUsername.Focus();
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter the password");
                txtPassword.Focus();
                return false;
            }  

            if ( txtFolderName.Text.Trim() == "")
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


            if  (txtFileNameOnServer.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name to drop on server.");
                txtFileNameOnServer.Focus();
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
                    = new DAL().SavePetraSettings(txtFtpURL.Text.Trim(),
                                                        txtFtpFileName.Text.Trim(),
                                                        txtUsername.Text.Trim(),
                                                        txtPassword.Text.Trim(),
                                                        txtFolderName.Text, txtFileName.Text, rbFTP.Checked, txtDropShipFee.Text, txtFileNameOnServer.Text);

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

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
