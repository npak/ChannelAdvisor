using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PetraOrderSettings : Form
    {
        private Vendor Petra { get; set; }

        public PetraOrderSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Petra = dal.GetVendor((int)VendorName.PetraOrderModule);

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
            string foldername = "";
            string archivename = "";

            DAL dal = new DAL();
            dal.GetPetraOrderInfo(out orderFtpurl, out orderUsername, out orderPassword, out foldername, out archivename);

            txtOrderFtp.Text = orderFtpurl;
            txtOrderFtpUsername.Text = orderUsername;
            txtOrderFtpPassword.Text = orderPassword;
            txtFolderName.Text = foldername;
            txtArchiveName.Text = archivename;
        }


        /// <summary>
        /// 
        /// </summary>
        private bool ValidateForm()
        {

            if ( txtFolderName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a Folder name");
                txtFolderName.Focus();
                return false;
            }


            if (txtArchiveName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a archive name");
                txtArchiveName.Focus();
                return false;
            }


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

                bool isSuccess
                    = new DAL().SavePetraOrderSettings(txtOrderFtp.Text, txtOrderFtpUsername.Text, txtOrderFtpPassword.Text, txtFolderName.Text, txtArchiveName.Text);

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
