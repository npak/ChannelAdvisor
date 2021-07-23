using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class Frequency : Form
    {
        DAL dal = new DAL();

        public Frequency()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgFrequencyTimes_EditingControlShowing(Object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control != null)
            {
                TextBox tb = (TextBox)e.Control;

                tb.KeyPress += new KeyPressEventHandler(tb_KeyPress);
            }//end if
        }//end event handler

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_KeyPress(Object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            //---if textbox is empty and user pressed a decimal char---
            if (((TextBox)sender).Text == String.Empty && e.KeyChar == (char)58)
            {
                e.Handled = true;
                return;
            }

            //--if textbox already has a decimal point---
            if (((TextBox)sender).Text.Contains(Convert.ToString(((char)58))) && e.KeyChar == (char)58)
            {
                e.Handled = true;
                return;
            }

            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar == (char)58))
            {
                e.Handled = true;
            }

        }//end event


        /// <summary>
        /// Form load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frequency_Load(object sender, EventArgs e)
        {
            PopulateProfiles();
            Util.PopulateVendorsWith24(cmbVendors);
           
        }//end form load

        /// <summary>
        /// Populate profiles in both grid and combo
        /// </summary>
        private void PopulateProfiles()
        {
            dgProfiles.DataSource = dal.GetProfiles().Tables[0];

            Util.PopulateProfiles(cmbProfileToUpdate);
        }//end method

        /// <summary>
        /// Vendor selection change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindData((int)cmbVendors.SelectedValue);
            PopulateSingleProfileFrequency((int)cmbVendors.SelectedValue);

            PopulateMultiProfileFrequency((int)cmbVendors.SelectedValue);
        }//end event

        #region Display

        /// <summary>
        /// Method to display data for single profile frequency
        /// </summary>
        /// <param name="vendorID"></param>
        private void PopulateSingleProfileFrequency(int vendorID)
        {
            DataTable dtFreqWeekdays = dal.GetSingleProfileFreqWeekDays(vendorID).Tables[0];
            DataTable dtFreqTime = dal.GetSingleProfileFreqTimes(vendorID).Tables[0];
            DataTable dtFreqProfile = dal.GetSingleProfileFreqProfiles(vendorID).Tables[0];

            //Display weekdays
            ClearWeekDays(lstWeekDays);
            SelectWeekDays(lstWeekDays, dtFreqWeekdays);
            
            //Display frequency times
            PopulateTimes(dgFrequencyTimes, dtFreqTime);

            if (dtFreqProfile.Rows.Count > 0)
            {
                cmbProfileToUpdate.SelectedValue = dtFreqProfile.Rows[0][0];
            }//end if

        }//end method

        /// <summary>
        /// Method to uncheck all values in listview
        /// </summary>
        /// <param name="lst"></param>
        private void ClearWeekDays(ListView lst)
        {
            //loop through weekdays
            for (int x = 0; x < lst.Items.Count; x++)
            {
                lst.Items[x].Checked = false;
            }//end for
        }//end method

        /// <summary>
        /// Method to display data for multi profile frequency
        /// </summary>
        /// <param name="vendorID"></param>
        private void PopulateMultiProfileFrequency(int vendorID)
        {
            DataTable dtFreqWeekdays = dal.GetMultiProfileFreqWeekDays(vendorID).Tables[0];
            DataTable dtFreqTime = dal.GetMultiProfileFreqTimes(vendorID).Tables[0];
            DataTable dtFreqProfiles = dal.GetMultiProfileFreqProfiles(vendorID).Tables[0];

            //Display weekdays
            ClearWeekDays(lstMultiWeekdays);
            SelectWeekDays(lstMultiWeekdays, dtFreqWeekdays);

            //Display frequency times
            PopulateTimes(dgMultiFrequencyTimes, dtFreqTime);

            //Check selected profiles
            ClearProfileGrid();
            SelectProfilesForMultiFrequency(dtFreqProfiles);

        }//end method

        /// <summary>
        /// 
        /// </summary>
        private void ClearProfileGrid()
        {
            //loop the grid
            for (int x = 0; x < dgProfiles.Rows.Count; x++)
            {
                dgProfiles.Rows[x].Cells["Update"].Value = false;
            }//end for
        }//end method

        /// <summary>
        /// Method to check selected profiles to update
        /// </summary>
        /// <param name="dtProfiles"></param>
        private void SelectProfilesForMultiFrequency(DataTable dtProfiles)
        {
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

        }//end method

        /// <summary>
        /// Method to populate times
        /// </summary>
        /// <param name="dgTimes"></param>
        /// <param name="dtTimes"></param>
        private void PopulateTimes(DataGridView dgTimes, DataTable dtTimes)
        {
            dgTimes.DataSource = null;
            CreateColumns(dgTimes);
            dgTimes.DataSource = dtTimes;

        }//end method

        /// <summary>
        /// Method to select weekdays in grid
        /// </summary>
        /// <param name="dg"></param>
        /// <param name="dt"></param>
        private void SelectWeekDays(ListView lst, DataTable dt)
        {
            string weekDay = "";

            //loop through weekdays
            for (int x = 0; x < lst.Items.Count; x++)
            {
                weekDay = lst.Items[x].Text;

                DataRow[] dr = dt.Select("WeekDay='" + weekDay + "'");
                if (dr.GetLength(0) > 0)
                {
                    lst.Items[x].Checked = Convert.ToBoolean(dr[0]["IsEnabled"]);
                }//end if

            }//end for

        }//end method

        #endregion

        /// <summary>
        /// Method to bind data
        /// </summary>
        private void BindData(int vendorID)
        {
            
            string weekDay = "";
            //Bind Frequency WeekDays
            DataTable dtFrequencyWeekDays = dal.GetFrequencyWeekDays(vendorID).Tables[0];

            //loop through weekdays
            for (int x = 0; x < lstWeekDays.Items.Count; x++)
            {
                weekDay = lstWeekDays.Items[x].Text;

                DataRow[] dr = dtFrequencyWeekDays.Select("WeekDay='" + weekDay + "'");
                if (dr.GetLength(0) > 0)
                {
                    lstWeekDays.Items[x].Checked = Convert.ToBoolean(dr[0]["IsEnabled"]);
                }//end if

            }//end for

            //BindGrid(vendorID);
        }


        /// <summary>
        /// Close click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end event

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
        /// Save click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateFrequencyTimeGrids())
            {
                DAL dal = new DAL();

                //bool isSaved = dal.SaveFrequency((int)cmbVendors.SelectedValue, GetWeekDays(), (DataTable)dgFrequencyTimes.DataSource);

                bool isSaved = dal.SaveFrequency((int)cmbVendors.SelectedValue, GetWeekDays(lstWeekDays), (DataTable)dgFrequencyTimes.DataSource, (int)cmbProfileToUpdate.SelectedValue, GetWeekDays(lstMultiWeekdays), (DataTable)dgMultiFrequencyTimes.DataSource, GetSelectedProfiles());


                if (isSaved)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("Record could not be saved!");
                }

            }//end if

        }//end event


        /// <summary>
        /// Method to get weekdays
        /// </summary>
        /// <returns></returns>
        private List<WeekDayDTO> GetWeekDays(ListView lstFreqWeekDays)
        {
            List<WeekDayDTO> lstWeekDayDTO = new List<WeekDayDTO>();
            //loop through the list
            foreach (ListViewItem lstItem in lstFreqWeekDays.Items)
            {
                WeekDayDTO weekDay = new WeekDayDTO();
                weekDay.WeekDay = lstItem.Text;
                weekDay.IsEnabled = lstItem.Checked;

                lstWeekDayDTO.Add(weekDay);
            }//end for

            return lstWeekDayDTO;
        }//end method

        /// <summary>
        /// Method to create columns
        /// </summary>
        private void CreateColumns(DataGridView dgTimes)
        {
            DataGridViewTextBoxColumn colTime = new DataGridViewTextBoxColumn();
            colTime.DataPropertyName = "Time";
            colTime.HeaderText = "Time";
            colTime.Width = 70;
            
            dgTimes.Columns.Add(colTime);

        }//end method

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="vendorID"></param>
        //private void BindGrid(int vendorID)
        //{
        //    CreateColumns();
        //    dgFrequencyTimes.DataSource = new DAL().GetFrequencyTimes(vendorID).Tables[0];
        //}//end method

        /// <summary>
        /// Method to validate both the frequency time grids
        /// </summary>
        /// <returns></returns>
        private bool ValidateFrequencyTimeGrids()
        {
            bool isSingleFreqValid = false;
            bool isMultiFreqValid = false;

            isSingleFreqValid = ValidateGrid(dgFrequencyTimes);
            isMultiFreqValid = ValidateGrid(dgMultiFrequencyTimes);

            //if both are false
            if (isSingleFreqValid == false || isMultiFreqValid == false)
            {
                return false;
            }
            else
            {
                return true;
            }//end if
        }//end method

        /// <summary>
        /// Validate whether all the fields have been entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid(DataGridView dgView)
        {
            int x = 1;
            string colName = "";
            string text="";
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgView.Rows)
            {
                if (dv.IsNewRow) return true;
                for (int x1 = 0; x1 < dv.Cells.Count; x1++)
                {
                    if (x1 == 0) colName = "Time";
                    if (x1 == 1) colName = "AMPM";

                    text= dv.Cells[x1].Value.ToString();

                    if (text == "")
                    {
                        Util.ShowMessage("Please enter a value for row " + x.ToString() + " of column '" + colName + "' for " + (dgView.Name == "dgFrequencyTimes"?"Single Profile Frequency": "Multi Profile Frequency"));
                        return false;
                    }//end if

                    //Check time with regular expression
                    if (x1 == 0)
                    {
                        if (!Util.ValidateTime(text))
                        {
                            Util.ShowMessage("Please enter a valid time for row " + x.ToString() + " of column '" + colName + "' for " + (dgView.Name == "dgFrequencyTimes" ? "Single Profile Frequency" : "Multi Profile Frequency"));
                            return false;
                        }
                    }//end if

                }//end for each

                x++;
            }//end foreach

            return true;
        }//end method

        /// <summary>
        /// Delete button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgFrequencyTimes.SelectedRows.Count > 0)
            {
                if (!dgFrequencyTimes.SelectedRows[0].IsNewRow)
                {
                    dgFrequencyTimes.Rows.Remove(dgFrequencyTimes.SelectedRows[0]);
                    dgFrequencyTimes.EndEdit();
                }
            }
        }//end event

        /// <summary>
        /// Delete button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMultiDelete_Click(object sender, EventArgs e)
        {
            if (dgMultiFrequencyTimes.SelectedRows.Count > 0)
            {
                if (!dgMultiFrequencyTimes.SelectedRows[0].IsNewRow)
                {
                    dgMultiFrequencyTimes.Rows.Remove(dgMultiFrequencyTimes.SelectedRows[0]);
                    dgMultiFrequencyTimes.EndEdit();
                }
            }
        }//end event



    }//end class

}//end namespace
