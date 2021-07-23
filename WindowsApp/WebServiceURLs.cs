using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class WebServiceURLs : Form
    {
        public WebServiceURLs()
        {
            InitializeComponent();
        }

        

        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WebServiceURLs_Load(object sender, EventArgs e)
        {
            BindGrid();

        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            dgServices.DataSource = new DAL().GetWebServices().Tables[0];
            dgServices.AllowUserToDeleteRows = false;
            dgServices.AllowUserToAddRows = false;

            dgServices.Columns[2].HeaderText = "Service URL";
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid()
        {
            int x = 1;

            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgServices.Rows)
            {
                if (dv.Cells[2].Value.ToString().Trim() == "")
                {
                    Util.ShowMessage("Please enter the URL for " + dv.Cells[1].Value.ToString());
                    return false;
                }//end if
                x++;
            }//end foreach

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
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateGrid())
            {
                bool isSaved = new DAL().UpdateServiceURLs((DataTable)dgServices.DataSource);

                if (isSaved)
                {
                    Util.ShowMessage("Records Saved!");
                }
                else
                {
                    Util.ShowMessage("There was some problem while trying to save the records. Records could not be saved.");
                }//end if
            }//end if
        }//end method

    }//end class
}//end namespace
