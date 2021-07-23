using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class TWHSettings : Form
    {
        private Vendor TWH { get; set; }

        public TWHSettings()
        {
            InitializeComponent();
            DAL dal = new DAL();
            TWH = dal.GetVendor((int)VendorName.TWH);
            commonSettings.VendorInfo = TWH;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeawideSettings_Load(object sender, EventArgs e)
        {
            DisplayInfo();
        }//end event


        /// <summary>
        /// 
        /// </summary>
        private void DisplayInfo()
        {
            string url = "";
            string user = "";
            string password = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";

            DAL dal = new DAL();

            dal.GetTWHSettings(out url,
                                    out user,
                                    out password, out csvfolder, out csvfilename, out csvIsftp, out dropshipfee);
       
            txtURL.Text = url;
            txtassword.Text = password;
            txtUser.Text = user;
            txtDropShipFee.Text = dropshipfee;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false; 
        }//end method

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (!commonSettings.SaveVendorCommonSettings())
                {
                    Util.ShowMessage("Vendor common settings could not be saved");
                    return;
                }

                bool isSuccess = new DAL().SaveTWHSettings(txtURL.Text.Trim(),
                                                            txtUser.Text.Trim(), 
                                                            txtassword.Text.Trim(),
                                                            txtFolderName.Text, txtFileName.Text, rbFTP.Checked, txtDropShipFee.Text.Trim());

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
            
            if (txtUser.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter User name");
                txtUser.Focus();
                return false;
            }
            if (txtassword.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Password");
                txtassword.Focus();
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

    }
}
