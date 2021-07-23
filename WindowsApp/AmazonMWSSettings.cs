using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ChannelAdvisor
{
    public partial class AmazonMWSSettings : Form
    {
        DAL dal = new DAL();

        public AmazonMWSSettings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            DisplayValues();
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayValues()
        {
            string inputFileName = "";
            string outputFileName = "";

            dal.GetAmazonMWSInfo(out inputFileName, out outputFileName);
            txtInputFile.Text = inputFileName;
            txtOutputFile.Text = outputFileName;
            
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                bool isSuccess = dal.SaveAmazonMWSSettings (txtInputFile.Text.Trim(),txtOutputFile.Text.Trim());

                if (isSuccess)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("There was some problem while trying to save the record. The record could not be saved.");
                }

            }
        }//end method

        private bool isValid()
        {
            
            if (txtInputFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Input File Name");
                txtInputFile.Focus();
                return false;
            }

            if (txtOutputFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Output File Name");
                txtOutputFile.Focus();
                return false;
            }
           
            return true;
        }

    }//end class

}//end namespace
