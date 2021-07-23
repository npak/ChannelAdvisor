using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class RJTSettings : Form
    {
        private Vendor RJT { get; set; }

        public RJTSettings()
        {
            InitializeComponent();

            RJT = Vendor.Load((int)VendorName.RJT);

            commonVendorSettings.VendorInfo = RJT;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void filesFolderButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                folderText.Text = folderBrowserDialog1.SelectedPath;

                if (folderText.Text.Substring(folderText.Text.Length - 2, 1) != "\\")
                    folderText.Text += "\\";
            }
        }

        private void RJTSettings_Load(object sender, EventArgs e)
        {
            DisplayRJTInfo();
        }

        private void DisplayRJTInfo()
        {
            DAL dal = new DAL();
            folderText.Text = dal.GetSettingValue("RJT_Folder");
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //check if fields have been entered
            if (folderText.Text.Trim() == "")
            {
                Util.ShowMessage("Please select folder to scan RJT files");
                return;
            }

            if (!commonVendorSettings.SaveVendorCommonSettings())
            {
                Util.ShowMessage("Vendor common settings could not be saved");
                return;
            }

            if (new DAL().UpdateSetting("RJT_Folder", folderText.Text))
            {
                Util.ShowMessage("Record Saved!");
            }
            else
            {
                Util.ShowMessage("The record could not be saved!");
            }
        }
    }
}
