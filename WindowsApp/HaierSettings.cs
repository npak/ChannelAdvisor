using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class HaierSettings : Form
    {
        private Vendor Haier { get; set; }

        public HaierSettings()
        {
            InitializeComponent();
            DAL dal = new DAL();
            Haier = dal.GetVendor((int)VendorName.Haier);

            commonSettings.VendorInfo = Haier;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void HaierSettings_Load(object sender, EventArgs e)
        {
            DAL dal = new DAL();

            folderTextBox.Text = dal.GetHaierFolder();
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();

            browse.Description = "Please select a folder to scan files";
            browse.ShowNewFolderButton = true;
            browse.RootFolder = Environment.SpecialFolder.MyComputer;
            browse.SelectedPath = Environment.SpecialFolder.MyComputer.ToString();

            if (browse.ShowDialog() == DialogResult.OK)
            {
                folderTextBox.Text = browse.SelectedPath;

                if (folderTextBox.Text.Substring(folderTextBox.Text.Length - 2, 1) != "\\")
                    folderTextBox.Text += "\\";

            }//end if
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (folderTextBox.Text.Trim() == "")
            {
                Util.ShowMessage("Please select folder to scan Haier files");
                return;
            }

            if (!commonSettings.SaveVendorCommonSettings())
            {
                Util.ShowMessage("Vendor common settings could not be saved");
                return;
            }

            //save
            bool isSuccess = new DAL().SaveHaierSettings(folderTextBox.Text);

            if (isSuccess)
            {
                Util.ShowMessage("Record Saved!");
            }
            else
            {
                Util.ShowMessage("The record could not be saved!");
            }//end isSucess if
        }
    }
}
