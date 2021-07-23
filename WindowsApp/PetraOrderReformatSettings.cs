using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PetraOrderReformatSettings : Form
    {
        private Vendor PetraOrderReformat { get; set; }

        public PetraOrderReformatSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            PetraOrderReformat = dal.GetVendor((int)VendorName.PetraOrderReformat);

            //commonSettings.VendorInfo = Petra;
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
            string orderFtpurl = "";
            string orderUsername = "";
            string orderPassword = "";
            string inFolderName = "";
            string outFolderName = "";
            string archiveFolderName = "";

            DAL dal = new DAL();
            dal.GetPetraOrderReformatInfo(out orderFtpurl, out orderUsername, out orderPassword, out inFolderName, out outFolderName, out archiveFolderName);
            txtOrderFtp.Text = orderFtpurl;
            txtOrderFtpUsername.Text = orderUsername;
            txtOrderFtpPassword.Text = orderPassword;
            txtInFolderName.Text = inFolderName;
            txtOutFolderName.Text = outFolderName;
            txtArchivename.Text = archiveFolderName; 
        }


        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {
            //
            if (txtOrderFtp.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid FTP URL");
                txtOrderFtp.Focus();
                return false;
            }
            if (txtOrderFtpUsername.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid FTP username");
                txtOrderFtpUsername.Focus();
                return false;
            }
            if (txtOrderFtpPassword.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter FTP password");
                txtOrderFtpPassword.Focus();
                return false;
            }

            if (txtInFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a In Folder name");
                txtInFolderName.Focus();
                return false;
            }

            if (txtOutFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Out Folder Name");
                txtOutFolderName.Focus();
                return false;
            }

            if (txtArchivename.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid FTP URL");
                txtArchivename.Focus();
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

                bool isSuccess = new DAL().SavePetraOrderReformatSettings(txtOrderFtp.Text, txtOrderFtpUsername.Text, txtOrderFtpPassword.Text, txtInFolderName.Text, txtOutFolderName.Text,txtArchivename.Text);

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
