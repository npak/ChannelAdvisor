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
    public partial class SeawideSettings : Form
    {
        private Vendor Seawide { get; set; }

        public SeawideSettings()
        {
            InitializeComponent();

            DAL dal = new DAL();
            Seawide = dal.GetVendor((int)VendorName.Seawide);
             
            commonSettings.VendorInfo = Seawide;
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
            string ftpfilename = "";

            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";

            DAL dal = new DAL();

            dal.GetSeawideSettings(out url,
                                    out user,
                                    out password, out ftpfilename, out csvfolder, out csvfilename, out csvIsftp);
       
            txtURL.Text = url;
            txtassword.Text = password;
            txtUser.Text = user;
            txtFtpFileName.Text = ftpfilename;

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

                bool isSuccess = new DAL().SaveSeawideSettings(txtURL.Text.Trim(),
                                                            txtUser.Text.Trim(), 
                                                            txtassword.Text.Trim(),
                                                            txtFtpFileName.Text.Trim(),
                                                            txtFolderName.Text, txtFileName.Text, rbFTP.Checked);

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

            if (txtFtpFileName.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a ftp File name");
                txtFtpFileName.Focus();
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

        private string GetInventoryExcel()
        {
            string url = "";
            string user = "";
            string password = "";
            string ftpfilename = "";
            string csvfolder = "";
            string csvfilename = "";
            string csvIsftp = "";

            DAL dal = new DAL();

            dal.GetSeawideSettings(out url,
                                    out user,
                                    out password, out ftpfilename, out csvfolder, out csvfilename, out csvIsftp);
            FTP ftp = new FTP(url, user, password, true);
            // Get list of files
            IList<string> files = ftp.GetFiles();

            try
            {
                // Get inventory excel file
                string filename = files.Single(f => f.Contains("SeawideFeedBAR2670XX") && f.EndsWith(".csv"));
                // download inventory file
                string LocalFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

                ftp.DownloadFile(filename, LocalFolder);

                return string.Format(@"{0}{1}", LocalFolder, filename);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetInventoryExcel();
        }
    }
}
