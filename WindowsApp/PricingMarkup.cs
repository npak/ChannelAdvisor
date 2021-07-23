using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class PricingMarkup : Form
    {
        public PricingMarkup()
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgPricingMarkup_EditingControlShowing(Object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
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
            if (((TextBox)sender).Text == String.Empty && e.KeyChar ==  (char)46)
            {
                e.Handled= true;
                return;
            }

            //--if textbox already has a decimal point---
            if (((TextBox)sender).Text.Contains(Convert.ToString(((char)46))) && e.KeyChar == (char)46)
            {
                e.Handled = true;
                return;
            }

            if (!(Char.IsDigit(e.KeyChar) || Char.IsControl(e.KeyChar) || e.KeyChar == (char)46))
            {
                e.Handled = true;
            }

        }//end event

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PricingMarkup_Load(object sender, EventArgs e)
        {
            Util.PopulateVendors(cmbVendors);
            Util.PopulateProfiles(cmbProfile);
            //BindGrid();
        }//end event


        /// <summary>
        /// 
        /// </summary>
        private void BindGrid(int vendorID, int profileID)
        {
            //bind
            //To be changed
            dgPricingMarkup.DataSource = new DAL().GetPricingMarkup(vendorID, profileID).Tables[0];

            dgPricingMarkup.Columns[0].HeaderText = "From";
            dgPricingMarkup.Columns[1].HeaderText = "To";
            dgPricingMarkup.Columns[2].HeaderText = "Markup";

            
            dgPricingMarkup.AllowUserToDeleteRows = true;
        }

        /// <summary>
        /// Button to delete rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgPricingMarkup.SelectedRows.Count > 0)
            {
                if (!dgPricingMarkup.SelectedRows[0].IsNewRow)
                {
                    dgPricingMarkup.Rows.Remove(dgPricingMarkup.SelectedRows[0]);
                    dgPricingMarkup.EndEdit();
                }
            }
        }//end event



        /// <summary>
        /// Change event of vendor combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProfile.SelectedIndex >= 0)
            {
                BindGrid((int)cmbVendors.SelectedValue, (int)cmbProfile.SelectedValue);
            }//end if
        }//end method

        /// <summary>
        /// Save Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateGrid())
            {
                bool isSaved = new DAL().SavePricingMarkup((int)cmbVendors.SelectedValue, 
                                                            (int)cmbProfile.SelectedValue,
                                                            (DataTable)dgPricingMarkup.DataSource);
                if (isSaved)
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("Record could not be saved!");
                }
            }//end if
        }//end Save event

        /// <summary>
        /// Validate whether all the fields have been entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid()
        {
            int x =1;
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgPricingMarkup.Rows)
            {
                if (dv.IsNewRow) return true;
                for(int x1=0; x1<dv.Cells.Count; x1++)
                {
                    if (dv.Cells[x1].Value.ToString() == "")
                    {
                        string colName = "";
                        if (x1 == 0) colName = "From";
                        if (x1 == 1) colName = "To";
                        if (x1 == 2) colName = "Markup";

                        Util.ShowMessage("Please enter value for row " + x.ToString() + " of column '" + colName + "'");

                        return false;
                    }//end if
                }//end for each

                x++;
            }//end foreach

            return true;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end method


        /// <summary>
        /// Change event of profile combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbVendors.SelectedIndex >= 0)
            {
                BindGrid((int)cmbVendors.SelectedValue, (int)cmbProfile.SelectedValue);
            }//end if
        }//end method
        

    }//end class

}//end namespace
