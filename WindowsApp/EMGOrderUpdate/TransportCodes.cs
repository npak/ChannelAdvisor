using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class TransportCodes : Form
    {
        public TransportCodes()
        {
            InitializeComponent();
        }



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
        private void BindGrid()
        {
            //bind
            dgTransportCodes.DataSource = new DAL().GetTransportCodes().Tables[0];
           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TransportCodes_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateGrid())
                {
                    bool isSaved = new DAL().SaveTransportCodes((DataTable)dgTransportCodes.DataSource);
                        
                    if (isSaved)
                    {
                        Util.ShowMessage("Record Saved!");
                    }
                    else
                    {
                        Util.ShowMessage("Record could not be saved!");
                    }
                }//end if
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }
        }//end event


        /// <summary>
        /// Validate whether all the fields have been entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid()
        {
            int x = 1;
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgTransportCodes.Rows)
            {
                if (dv.IsNewRow) return true;
                for (int x1 = 0; x1 < dv.Cells.Count; x1++)
                {
                    if (dv.Cells[x1].Value.ToString().Trim() == "" && (x1!= 3 && x1!= 4))
                    {
                        string colName = "";
                        if (x1 == 0) colName = "StoneEdge Ship Code";
                        if (x1 == 1) colName = "EMG Carrier ID";
                        if (x1 == 2) colName = "EMG Route Code";

                        Util.ShowMessage("Please enter value for row " + x.ToString() + " of column '" + colName + "'");

                        return false;
                    }//end if
                }//end for each

                x++;
            }//end foreach

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgTransportCodes.SelectedRows.Count > 0)
            {
                if (!dgTransportCodes.SelectedRows[0].IsNewRow)
                {
                    dgTransportCodes.Rows.Remove(dgTransportCodes.SelectedRows[0]);
                    dgTransportCodes.EndEdit();
                }
            }
        }

        private void dgTransportCodes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }



    }//end class

}//end namespace
