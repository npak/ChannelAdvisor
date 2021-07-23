using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace ChannelAdvisor
{
    public partial class DuplicateSKUs : Form
    {
        //private variables
        private bool isDataBinding = true; //variable to specify that cell value changed was called during data binding

        /// <summary>
        /// Default constructor
        /// </summary>
        public DuplicateSKUs()
        {
            InitializeComponent();
        }//end method

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DuplicateSKUs_Load(object sender, EventArgs e)
        {
            Util.PopulateVendors(cmbVendors);
        }//end method

        /// <summary>
        /// 
        /// </summary>
        private void BindGrid(int vendorID)
        {
            isDataBinding = true;
            //bind
            dgSKUs.DataSource = new DAL().GetDuplicateSKUs(vendorID).Tables[0];


            isDataBinding = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid((int)cmbVendors.SelectedValue);
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgSKUs.SelectedRows.Count > 0)
            {
                if (!dgSKUs.SelectedRows[0].IsNewRow)
                {
                    dgSKUs.Rows.Remove(dgSKUs.SelectedRows[0]);
                    dgSKUs.EndEdit();
                }
            }
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
        /// Change the new SKU value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgSKUs_CellValueChanged(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 0 || e.ColumnIndex == 1) && !isDataBinding)
            {
                string upc = dgSKUs.Rows[e.RowIndex].Cells[0].Value.ToString();
                string sku = dgSKUs.Rows[e.RowIndex].Cells[1].Value.ToString();

                if ((!String.IsNullOrEmpty(upc)) && (!String.IsNullOrEmpty(sku)))
                {
                    dgSKUs.Rows[e.RowIndex].Cells[2].Value = upc + "-" + sku;
                }
                else if ((!String.IsNullOrEmpty(upc)) && String.IsNullOrEmpty(sku))
                {
                    dgSKUs.Rows[e.RowIndex].Cells[2].Value = upc;
                }//end if

            }//end if
        }

        /// <summary>
        /// Save click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateGrid())
                {
                    bool isSaved = new DAL().SaveDuplicateSKUs((int)cmbVendors.SelectedValue,
                                            (DataTable)dgSKUs.DataSource);
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
            
        }//end method


        /// <summary>
        /// Validate whether all the fields have been entered
        /// </summary>
        /// <returns></returns>
        private bool ValidateGrid()
        {
            int x = 1;
            //loop through datagrid and check whether any cell is blank
            foreach (DataGridViewRow dv in dgSKUs.Rows)
            {
                if (dv.IsNewRow) return true;
                for (int x1 = 0; x1 < dv.Cells.Count; x1++)
                {
                    if (dv.Cells[x1].Value.ToString() == "" && (x1 != 1 && x1 !=0))
                    {
                        string colName = "";
                        if (x1 == 0) colName = "UPC";
                        //if (x1 == 1) colName = "SKU";
                        if (x1 == 2) colName = "New SKU";

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
        private void btnExport_Click(object sender, EventArgs e)
        {
            //Select folder to export
            string extension = "CSV file (*.csv)|*.csv";
            string title = "Where do you want to save the Duplicate SKU's csv file?";
            string fileToSave = CAUtil.GetSaveFileDialogFileName(extension, title);

            if (!@String.IsNullOrEmpty(fileToSave))
            {
                DuplicateSKU[] duplicateSKUArray = GetDuplicateSKUArray();

                CAUtil.ExportDuplicateSKUs(fileToSave, duplicateSKUArray);
            }//end if
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DuplicateSKU[] GetDuplicateSKUArray()
        {
            DataTable dt = (DataTable)dgSKUs.DataSource;

            DuplicateSKU[] duplicateSKUArray = new DuplicateSKU[dt.Rows.Count+1];

            //create header
            duplicateSKUArray[0] = DuplicateSKU.GetHeaderClass();

            int x = 1;
            //loop and create array
            foreach (DataRow dr in dt.Rows)
            {
                DuplicateSKU duplicateSKU = new DuplicateSKU();
                if (dr["UPC"] == null)
                {
                    duplicateSKU.UPC = "";
                }
                else
                {
                    //Append single quote at the end
                    duplicateSKU.UPC = dr["UPC"].ToString() + "'";
                }
                duplicateSKU.SKU = dr["SKU"].ToString();
                duplicateSKU.NewSKU = dr["NewSKU"].ToString();

                duplicateSKUArray[x] = duplicateSKU;
                x++;
            }//end for each

            return duplicateSKUArray;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {
            string extension = "CSV file (*.csv)|*.csv";
            string title = "Select the Duplicate SKU's csv file?";

            string csvFile = CAUtil.GetOpenFileDialogFileName(extension, title);

            if(!@String.IsNullOrEmpty(csvFile))
            {
                DuplicateSKU[] duplicateSKUArray = CAUtil.ImportDuplicateSKUs(csvFile);

                DataTable dt = (DataTable)dgSKUs.DataSource;
                dt.Clear();

                //import from array
                for (int x = 0; x < duplicateSKUArray.GetLength(0); x++)
                {
                    DataRow dr = dt.NewRow();
                    dr["UPC"] = duplicateSKUArray[x].UPC.Replace("'","");//replace single quotes
                    dr["SKU"] = duplicateSKUArray[x].SKU;
                    dr["NewSKU"] = duplicateSKUArray[x].NewSKU;

                    dt.Rows.Add(dr);
                }//end for

                //bind grid
                isDataBinding = true;
                dgSKUs.DataSource = dt;
                isDataBinding = false;
            }//end if
        }

    }//end class

}//end namespace
