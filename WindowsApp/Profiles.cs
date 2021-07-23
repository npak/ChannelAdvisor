using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class Profiles : Form
    {
        DAL dal = new DAL();

        public Profiles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profiles_Load(object sender, EventArgs e)
        {
            BindGrid();
        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid()
        {
            dgProfiles.DataSource = dal.GetProfiles().Tables[0];
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
                bool isSaved = false;

                try
                {
                    isSaved = dal.SaveProfiles((DataTable)dgProfiles.DataSource);
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
                
                if (isSaved)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("Record could not be saved!");
                }
            }//end if
        }//end method

        private bool ValidateGrid()
        {
            int x = 1;
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgProfiles.Rows)
            {
                if (dv.IsNewRow) return true;

                if (dv.Cells["Profile"].Value.ToString() == "")
                {
                    Util.ShowMessage("Please enter value for Profile column in row: " + x.ToString());
                    return false;
                }

                if (dv.Cells["ProfileAPIKey"].Value.ToString() == "")
                {
                    Util.ShowMessage("Please enter value for Profile API Key column in row: " + x.ToString());
                    return false;
                }

                x++;
            }//end foreach

            return true;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgProfiles.SelectedRows.Count > 0)
            {
                if (!dgProfiles.SelectedRows[0].IsNewRow)
                {
                    dgProfiles.Rows.Remove(dgProfiles.SelectedRows[0]);
                    dgProfiles.EndEdit();
                }
            }
        }//end method

    }//end class

}//end namespace
