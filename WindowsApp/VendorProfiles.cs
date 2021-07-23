using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using log4net;
using log4net.Config;

namespace ChannelAdvisor
{
    public partial class VendorProfiles : Form
    {
        DAL dal = new DAL();

        //public readonly ILog log = LogManager.GetLogger(typeof(Program));

        public VendorProfiles()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Profiles_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    int x = 0;
            //    int y = 5 / x;
            //}
            //catch (Exception ex)
            //{
            //    log.Error("Error in Profile", ex);
            //}

            PopulateProfiles();

            Util.PopulateVendors(cmbVendors);
        }

        /// <summary>
        /// 
        /// </summary>
        private void PopulateProfiles()
        {
            dgProfiles.DataSource = dal.GetProfiles().Tables[0];
        }//end method

        /// <summary>
        /// Selected index change event of Vendor dropdown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindGrid((int)(((DataRowView)cmbVendors.SelectedValue)["ID"]));

            DataTable dtProfiles = dal.GetProfilesForVendor((int)cmbVendors.SelectedValue).Tables[0];

            //loop the datatable and check the datagridview
            foreach (DataRow dr in dtProfiles.Rows)
            {
                //loop the grid
                for (int x = 0; x < dgProfiles.Rows.Count; x++)
                {
                    if (dgProfiles.Rows[x].Cells["ID"].Value.ToString() == dr["ProfileID"].ToString())
                    {
                        dgProfiles.Rows[x].Cells["Update"].Value = true;
                        x = dgProfiles.Rows.Count;
                    }//end if
                }//end for
                
            }//end foreach

        }//end event

        

        /// <summary>
        /// Save click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (ValidateGrid())
            //{
            //    bool isSaved = new DAL().SaveProfiles((int)(((DataRowView)cmbVendors.SelectedValue)["ID"]),
            //                            (DataTable)dgProfiles.DataSource);
            //}//end validate grid if

            bool isSaved = dal.SaveProfilesToUpdateForVendor((int)cmbVendors.SelectedValue, GetSelectedProfiles());
            if (isSaved)
            {
                Util.ShowMessage("Record Saved!");
            }
            else
            {
                Util.ShowMessage("Record could not be saved!");
            }//end if

        }//end method


        /// <summary>
        /// Method that retreives the checked profiles
        /// </summary>
        /// <returns></returns>
        private List<int> GetSelectedProfiles()
        {
            List<int> profiles = new List<int>();

            //loop the grid
            foreach (DataGridViewRow dRow in dgProfiles.Rows)
            {
                if (dRow.Cells["Update"].Value != null && (bool)dRow.Cells["Update"].Value == true)
                {
                    //add to list if checked
                    profiles.Add((int)dRow.Cells["ID"].Value);
                }//end if
            }//end foreach

            return profiles;
        }//end method

        /// <summary>
        /// Close click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end method


    }//end class

}//end namespace
