using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.Configuration;


namespace ChannelAdvisor
{
    public partial class PreviewCache : Form
    {

        private List<StockItem> _inventorylist = new List<StockItem>();
        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        string csvfolder = "";
        string csvfilename = "";
        string csvIsftp = "0";


        public PreviewCache()
        {
            InitializeComponent();
            dgInventory.AutoGenerateColumns = false;
            DisplayInventory();
        }

        /// <summary>
        /// Click event to get inventory from EMG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetInventory_Click(object sender, EventArgs e)
        {
            try
            {
                btnGetInventory.Text = "Processing..";
                btnGetInventory.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                //Get and display inventory in grid
                DisplayInventory();
            }
            catch (Exception ex)
            {                
                Util.ShowMessage(ex.Message);
            }//end try            
            finally
            {
                this.Cursor = Cursors.Default;
                btnGetInventory.Enabled = true;
                btnGetInventory.Text = "Refresh Cache";
            }
            
        }//end method

        /// <summary>
        /// Method to display inventory
        /// </summary>
        /// 

        private void DisplayInventory()
        {
            InventoryUpdateService obj = new InventoryUpdateService();
            
            var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();
            
           

            //dgInventory.DataMember = "EMGInventoryDTO";
            dgInventory.DataSource = null;
            _inventorylist = obj.GetCache();
            dgInventory.DataSource = Funcs.ToDataTable(_inventorylist);
            //_inventorylist;


            //BindingSource _gridSource = new BindingSource();
            //_gridSource.DataSource = invUpdSrvcDTO.ErrorLogDTO;
            // Added new menu item Seeting=>review Cache
            // Morris is not work for me now. Dont know why.
            // but Morris Nightly works. attachment is morris nightly

            //dgErrors.DataSource = _gridSource;

            SetColumnWidths();

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void SetColumnWidths()
        {
            
            //dgInventory.Columns[4].HeaderText = "Calc. Price";
            //dgInventory.Columns[5].HeaderText = "Markup %";
            


           // dgInventory.Columns["UPC"].Width = 120; //UPC
            dgInventory.Columns["SKU"].Width = 140; //SKU
            //dgInventory.Columns["QTY"].Width = 70; //QTY
            //dgInventory.Columns["Price"].Width = 70; //Price
            //dgInventory.Columns["MarkupPercentage"].Width = 70; //Markup
            //dgInventory.Columns["MAP"].Width = 70; //MAP
            dgInventory.Columns["DomesticShipping"].Width = 70; //domestic shipping
           // dgInventory.Columns["MarkupPrice"].Width = 70; //Markup Price
            //dgInventory.Columns["RetailPrice"].Width = 70; //Retail Price
            dgInventory.Columns["Title"].Width = 580; //Description

            //dgInventory.Columns["UPC"].DisplayIndex = 0; 
            //dgInventory.Columns["SKU"].DisplayIndex = 1; 
            //dgInventory.Columns["QTY"].DisplayIndex = 2; 
            //dgInventory.Columns["Price"].DisplayIndex = 3; 
            //dgInventory.Columns["MarkupPercentage"].DisplayIndex = 4;
            //dgInventory.Columns["MAP"].DisplayIndex = 5;
            //dgInventory.Columns["DomesticShipping"].DisplayIndex = 6;
            //dgInventory.Columns["MarkupPrice"].DisplayIndex = 7;
            //dgInventory.Columns["RetailPrice"].DisplayIndex = 8; 
            //dgInventory.Columns["Description"].DisplayIndex = 9;
            //dgInventory.Columns["LinnworksStockItemId"].DisplayIndex =-1; 
            
            //dgInventory.Columns["MarkupPercentage"].HeaderText = "Markup %";
            //dgInventory.Columns["MarkupPrice"].HeaderText = "Calc. Price";
            //dgInventory.Columns["RetailPrice"].HeaderText = "Retail Price";

            //dgInventory.Columns["UPC"].ReadOnly = true;
            //dgInventory.Columns["SKU"].ReadOnly = true;
            //dgInventory.Columns["QTY"].ReadOnly = false;
            //dgInventory.Columns["Price"].ReadOnly = true;
            //dgInventory.Columns["MarkupPercentage"].ReadOnly = true;
            //dgInventory.Columns["MAP"].ReadOnly = true;
            //dgInventory.Columns["MarkupPrice"].ReadOnly = false;
            //dgInventory.Columns["RetailPrice"].ReadOnly = false;
            //dgInventory.Columns["Description"].ReadOnly = true;

            DataGridViewCellStyle numberCell = new DataGridViewCellStyle();
            numberCell.Alignment=DataGridViewContentAlignment.MiddleRight;

            //dgInventory.Columns["QTY"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["Price"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["MarkupPercentage"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["MAP"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["MarkupPrice"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["RetailPrice"].DefaultCellStyle = numberCell;
            //dgInventory.Columns["DomesticShipping"].DefaultCellStyle = numberCell;

            //dgInventory.Columns["UPC"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["SKU"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["QTY"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["Price"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["MarkupPercentage"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["MAP"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["MarkupPrice"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["RetailPrice"].SortMode = DataGridViewColumnSortMode.Automatic;
            //dgInventory.Columns["Description"].SortMode = DataGridViewColumnSortMode.Automatic;

            dgInventory.ColumnHeadersHeight = 40;
        }//end method

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewAndUpdate_Load(object sender, EventArgs e)
        {
    
            SetColumnWidths();
      
        }//end event

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgInventory_EditingControlShowing(Object sender, System.Windows.Forms.DataGridViewEditingControlShowingEventArgs e)
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
            if (((TextBox)sender).Text == String.Empty && e.KeyChar == (char)46)
            {
                e.Handled = true;
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

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }//end event

        private ListSortDirection sortDirection = ListSortDirection.Ascending;
        private int columnIndex = -1;

        private void dgInventory_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == columnIndex)
                sortDirection = sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            else
                sortDirection = ListSortDirection.Ascending;

            columnIndex = e.ColumnIndex;
            dgInventory.Sort(dgInventory.Columns[e.ColumnIndex], sortDirection);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                btSearch.Text = "Searching...";
                btSearch.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                if (textBox1.Text.Trim().Length > 0)
                    Search(); //Filter();
                else
                    DisplayInventory();
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }//end try            
            finally
            {
                this.Cursor = Cursors.Default;
                btSearch.Enabled = true;
                btSearch.Text = "Search";
            }
            

        }

  
        private void Search()
        {
            if (dgInventory.DataSource == null)
                DisplayInventory();
            if (dgInventory.FirstDisplayedScrollingRowIndex>0)
                dgInventory.FirstDisplayedScrollingRowIndex = 0;
            for (int i = 0; i < dgInventory.RowCount; i++)
            {
                dgInventory.Rows[i].Selected = false;
                //for (int j = 0; j < dgInventory.ColumnCount; j++)
                if (dgInventory.Rows[i].Cells[0].Value != null)
                    if (dgInventory.Rows[i].Cells[0].Value.ToString().Contains(textBox1.Text))
                    {

                        dgInventory.Rows[i].Selected = true;
                        dgInventory.FirstDisplayedScrollingRowIndex=i; 
                        break;
                    }
                
            }

            if (dgInventory.FirstDisplayedScrollingRowIndex <1)            
            {
                dgInventory.DataSource = null;
                Util.ShowMessage("Could not find item in cache with SKU: " + textBox1.Text);
            }
        }

        private void Filter()
        {
            //WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();
            //dialogWithWork.ShowWithWork(() =>
            //{ });
            //    dialogWithWork.ShowMessage(string.Format("Searching Sku: " + textBox1.Text + " in cache please wait..."));

                var filt = from l in _inventorylist
                           where l.SKU == textBox1.Text
                           select l;
                if (filt.Count() > 0)
                    dgInventory.DataSource = filt.ToList();
                else
                {
                    dgInventory.DataSource = null;
                    Util.ShowMessage("Could not find item in cache with SKU: " + textBox1.Text);

                }

        
           
           
        }



        // delete
        private void dgInventory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView == null)
            {
                var ex = new InvalidOperationException("This event is for a DataGridView type senders only.");
                ex.Data.Add("Sender type", sender.GetType().Name);
                throw ex;
            }

            foreach (DataGridViewColumn column in dataGridView.Columns)
                column.SortMode = DataGridViewColumnSortMode.Automatic;
        }
    }//end 

    static class Funcs
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
    }
}//end namespace
