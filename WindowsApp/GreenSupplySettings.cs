using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class GreenSupplySettings : Form
    {
        private Vendor GreenSupply { get; set; }

        public GreenSupplySettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            GreenSupply = dal.GetVendor((int)VendorName.GreenSupply);

            commonSettings.VendorInfo = GreenSupply;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GreenSupplySettings_Load(object sender, EventArgs e)
        {
            DisplayGreenSupplyInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayGreenSupplyInfo()
        {
            string url = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";

            DAL dal = new DAL();
            dal.GetGreenSupplyInfo(out url, out csvfolder, out csvfilename, out csvIsftp, out dropshipfee);

            txtURL.Text = url;
            txtFolderName.Text = csvfolder;
            txtFileName.Text = csvfilename;
            rbFTP.Checked = csvIsftp == "1" ? true : false;
            txtDropShipFee.Text = dropshipfee;
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

            if (txtDropShipFee.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a DropShipFee value");
                txtDropShipFee.Focus();
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
                    = new DAL().SaveGreenSupplySettings(txtURL.Text.Trim(),
                                                        txtFolderName.Text.Trim(), txtFileName.Text.Trim(), rbFTP.Checked,txtDropShipFee.Text.Trim());

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
