using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class EMGFrequency : Form
    {
        DAL dal = new DAL();

        public EMGFrequency()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbServiceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cmbServiceType.Items[cmbServiceType.SelectedIndex].ToString());
            if (cmbServiceType.Items[cmbServiceType.SelectedIndex].ToString() == "EMG Order Update")
            {
                PopulateFrequency(1);
            }
            else
            {
                PopulateFrequency(2);
            }
            
        }//end event

        /// <summary>
        /// Method to display data for frequency
        /// </summary>
        /// <param name="vendorID"></param>
        private void PopulateFrequency(int serviceType)
        {
            DataTable dtFreqWeekdays = dal.GetEMGFrequencyWeekDays(serviceType).Tables[0];
            DataTable dtFreqTime = dal.GetEMGFrequencyTimes(serviceType).Tables[0];
            

            //Display weekdays
            ClearWeekDays(lstWeekDays);
            SelectWeekDays(lstWeekDays, dtFreqWeekdays);

            //Display frequency times
            PopulateTimes(dgFrequencyTimes, dtFreqTime);


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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EMGFrequency_Load(object sender, EventArgs e)
        {
            cmbServiceType.SelectedIndex = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateGrid(dgFrequencyTimes))
            {
                DAL dal = new DAL();

                int serviceType = cmbServiceType.Items[cmbServiceType.SelectedIndex].ToString() == "EMG Order Update" ? 1 : 2;
                

                bool isSaved
                    = dal.SaveEMGFrequency(serviceType, 
                                            GetWeekDays(lstWeekDays), 
                                            (DataTable)dgFrequencyTimes.DataSource);


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
        /// Validate whether all the fields have been entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid(DataGridView dgView)
        {
            int x = 1;
            string colName = "";
            string text = "";
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgView.Rows)
            {
                if (dv.IsNewRow) return true;
                for (int x1 = 0; x1 < dv.Cells.Count; x1++)
                {
                    if (x1 == 0) colName = "Time";
                    if (x1 == 1) colName = "AMPM";

                    text = dv.Cells[x1].Value.ToString();

                    if (text == "")
                    {
                        Util.ShowMessage("Please enter a value for row " + x.ToString() + " of column '" + colName + "' for Time");
                        return false;
                    }//end if

                    //Check time with regular expression
                    if (x1 == 0)
                    {
                        if (!Util.ValidateTime(text))
                        {
                            Util.ShowMessage("Please enter a valid time for row " + x.ToString() + " of column '" + colName + "' for Time");
                            return false;
                        }
                    }//end if

                }//end for each

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
        }

        /// <summary>
        /// 
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



    }//end class

}//end namespace
