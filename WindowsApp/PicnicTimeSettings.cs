using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PicnicTimeSettings : Form
    {
        private Vendor PicnicTime { get; set; }

        public PicnicTimeSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            PicnicTime = dal.GetVendor((int)VendorName.PicnicTime);

            commonSettings.VendorInfo = PicnicTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicnicTimeSettings_Load(object sender, EventArgs e)
        {
            DisplayPicnicTimeInfo();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayPicnicTimeInfo()
        {
            string csvurl = "";
            string excelUrl = "";
            string isexcelfile = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";
            string dropshipfee = "";
            DAL dal = new DAL();
            dal.GetPicnicTimeInfo(out csvurl,
                                        out excelUrl,
                                        out isexcelfile,
                                        out csvfolder, out csvfilename, out csvIsftp, out dropshipfee);

            txtCsvURL.Text = csvurl;
            txtExcelURL.Text = excelUrl;
            rbCsv.Checked = isexcelfile == "0" ? true : false;
            rbExcel.Checked = !rbCsv.Checked;
            
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
            if (txtCsvURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid URL");
                txtCsvURL.Focus();
                return false;
            }
            if (txtExcelURL.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Excel File URL");
                txtExcelURL.Focus();
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
            //dropshipfee
            if (txtDropShipFee.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter drop ship fee.");
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

                string isexcelfile = "0";
                if (rbExcel.Checked)
                    isexcelfile = "1";
                bool isSuccess
                    = new DAL().SavePicnicTimeSettings(txtCsvURL.Text.Trim(),
                                                        txtExcelURL.Text.Trim(),
                                                        isexcelfile, txtFolderName.Text, txtFileName.Text, rbFTP.Checked,txtDropShipFee.Text);

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
