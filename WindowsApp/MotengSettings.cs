using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class MotengSettings : Form
    {
        private Vendor Moteng { get; set; }

        public MotengSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Moteng = dal.GetVendor((int)VendorName.Moteng);

            commonSettings.VendorInfo = Moteng;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MotengSettings_Load(object sender, EventArgs e)
        {
            DisplayMotengInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayMotengInfo()
        {
            string url = "";
            string dropshipfee = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvProdfilename = "";
            string csvPricefilename = "";
            string csvQtyfilename = "";
            string csvIsftp = "";
            string username = "";
            string password = "";
            string csvfilenameConverted = "";

            DAL dal = new DAL();
            dal.GetMotengInfo(out url,out csvfolder, out csvfilename, out csvProdfilename, out csvPricefilename, out csvQtyfilename, out csvIsftp, out dropshipfee, out username, out password, out csvfilenameConverted);

            txtURL.Text = url;
            txtDropShipFee.Text = dropshipfee;
            txtFoldername.Text = csvfolder;
            txtCsvFilename.Text = csvfilename;
            txtProdFileName.Text = csvProdfilename;        
            txtPriceFile.Text = csvPricefilename;
            txtQtyFile.Text = csvQtyfilename;
            txtFilenameConverted.Text = csvfilenameConverted;
            txtUsername.Text = username;
            txtPassword.Text = password; 
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

            if (txtFoldername.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid output folder  name");
                txtFoldername.Focus();
                return false;
            }

            if (txtCsvFilename.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid output csv file name");
                txtCsvFilename.Focus();
                return false;
            }

            if (txtProdFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a File name");
                txtProdFileName.Focus();
                return false;
            }


            if (txtPriceFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a Price File name");
                txtPriceFile.Focus();
                return false;
            }


            if (txtQtyFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a Qty File name");
                txtQtyFile.Focus();
                return false;
            }
            if (txtUsername.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter User name");
                txtUsername.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Password");
                txtPassword.Focus();
                return false;
            }

            if (txtFilenameConverted.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a converted File name");
                txtFilenameConverted.Focus();
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
                    = new DAL().SaveMotengSettings(txtFoldername.Text.Trim(), txtCsvFilename.Text.Trim(), txtFilenameConverted.Text, txtURL.Text.Trim(), txtUsername.Text.Trim(), txtPassword.Text.Trim(),
                                                    txtProdFileName.Text.Trim(), txtPriceFile.Text.Trim(), txtQtyFile.Text.Trim(), rbFTP.Checked, txtDropShipFee.Text.Trim());

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
