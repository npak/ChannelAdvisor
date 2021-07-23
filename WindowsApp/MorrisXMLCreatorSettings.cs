using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ChannelAdvisor
{
    public partial class MorrisXMLCreatorSettings : Form
    {
        DAL dal = new DAL();

        public MorrisXMLCreatorSettings()
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
            string processedFile = "";
            string readyToUloadFile = "";
            string errorFolderName = "";

            dal.GetMorrisXMLCreatorInfo(out inputFileName, out processedFile, out readyToUloadFile, out errorFolderName);
            txtInputFile.Text = inputFileName;
            txtrocessedFile.Text = processedFile;
            txtReadyToUload.Text = readyToUloadFile;
            txtErrorFolder.Text = errorFolderName;
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
                bool isSuccess = dal.SaveMorrisXMLCreatorSettings(txtInputFile.Text.Trim(),txtrocessedFile.Text.Trim(), txtReadyToUload.Text.Trim(), txtErrorFolder.Text.Trim());

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

            if (txtrocessedFile.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Processed File Name");
                txtrocessedFile.Focus();
                return false;
            }

            if (txtReadyToUload.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Ready to Upload File Name");
                txtReadyToUload.Focus();
                return false;
            }


            if (txtErrorFolder.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter a valid Error Flder Name");
                txtErrorFolder.Focus();
                return false;
            }
            return true;
        }

    }//end class

}//end namespace
