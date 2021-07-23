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
    public partial class PreviewAndUpdate : Form
    {
        private InventoryUpdateServiceDTO invUpdSrvcDTO = null;
        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        string csvfolder = "";
        string csvfilename = "";
        string csvIsftp = "0";
        //for AZ
        string csvfilenameWithRenamedFields = "";


        public PreviewAndUpdate()
        {
            InitializeComponent();
            dgInventory.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Click event to get inventory from EMG
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetInventory_Click(object sender, EventArgs e)
        {
            //Check if profile is selected
            if (cmbProfile.SelectedIndex < 0)
            {
                Util.ShowMessage("Please select a Profile to update.");
                return;
            }//end if

            if (IsLoadingCache())
            {
                Util.ShowMessage("Cache is reloading. Please try again late.");
                return;
            }

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
                btnGetInventory.Text = "Get Inventory";
            }

        }//end method

        /// <summary>
        /// Method to display inventory
        /// </summary>
        /// 

        private void DisplayInventory()
        {
            //Clear the CA errors grid
            dgCAErrors.DataSource = null;

            string vendorName = cmbVendors.Text;
            int vendorId = Convert.ToInt32(cmbVendors.SelectedValue);
            int profileId = Convert.ToInt32(cmbProfile.SelectedValue);
            //InventoryUpdateServiceDTO invUpdSrvcDTO = null;
            WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();
            dialogWithWork.ShowWithWork(() =>
            {
                dialogWithWork.ShowMessage(string.Format("Fetching data from vendor {0}, please wait...", vendorName));
                invUpdSrvcDTO = VendorServiceFactory.GetVendorService(vendorId).GetInventoryListForPreviewAndUpdate(profileId);
            });

            if (invUpdSrvcDTO != null)
            {
                if (invUpdSrvcDTO.WithoutResult)
                    Util.ShowMessage(invUpdSrvcDTO.SuccesMessage);
                else
                {
                    //dgInventory.DataMember = "EMGInventoryDTO";
                    dgInventory.DataSource = null;
                    dgInventory.DataSource = invUpdSrvcDTO.InventoryDTO;


                    BindingSource _gridSource = new BindingSource();
                    _gridSource.DataSource = invUpdSrvcDTO.ErrorLogDTO;
                    dgErrors.DataSource = _gridSource;

                    SetColumnWidths();
                }
            }
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void SetColumnWidths()
        {

            //dgInventory.Columns[4].HeaderText = "Calc. Price";
            //dgInventory.Columns[5].HeaderText = "Markup %";



            dgInventory.Columns["UPC"].Width = 120; //UPC
            dgInventory.Columns["SKU"].Width = 120; //SKU
            dgInventory.Columns["QTY"].Width = 70; //QTY
            dgInventory.Columns["Price"].Width = 70; //Price
            dgInventory.Columns["MarkupPercentage"].Width = 70; //Markup
            dgInventory.Columns["MAP"].Width = 70; //MAP
            dgInventory.Columns["DomesticShipping"].Width = 70; //domestic shipping
            dgInventory.Columns["MarkupPrice"].Width = 70; //Markup Price
            dgInventory.Columns["RetailPrice"].Width = 70; //Retail Price
            dgInventory.Columns["Description"].Width = 480; //Description

            dgInventory.Columns["UPC"].DisplayIndex = 0;
            dgInventory.Columns["SKU"].DisplayIndex = 1;
            dgInventory.Columns["QTY"].DisplayIndex = 2;
            dgInventory.Columns["Price"].DisplayIndex = 3;
            dgInventory.Columns["MarkupPercentage"].DisplayIndex = 4;
            dgInventory.Columns["MAP"].DisplayIndex = 5;
            dgInventory.Columns["DomesticShipping"].DisplayIndex = 6;
            dgInventory.Columns["MarkupPrice"].DisplayIndex = 7;
            dgInventory.Columns["RetailPrice"].DisplayIndex = 8;
            dgInventory.Columns["Description"].DisplayIndex = 9;
            //dgInventory.Columns["LinnworksStockItemId"].DisplayIndex =-1; 

            dgInventory.Columns["MarkupPercentage"].HeaderText = "Markup %";
            dgInventory.Columns["MarkupPrice"].HeaderText = "Calc. Price";
            dgInventory.Columns["RetailPrice"].HeaderText = "Retail Price";

            dgInventory.Columns["UPC"].ReadOnly = true;
            dgInventory.Columns["SKU"].ReadOnly = true;
            dgInventory.Columns["QTY"].ReadOnly = false;
            dgInventory.Columns["Price"].ReadOnly = true;
            dgInventory.Columns["MarkupPercentage"].ReadOnly = true;
            dgInventory.Columns["MAP"].ReadOnly = true;
            dgInventory.Columns["MarkupPrice"].ReadOnly = false;
            dgInventory.Columns["RetailPrice"].ReadOnly = false;
            dgInventory.Columns["Description"].ReadOnly = true;

            DataGridViewCellStyle numberCell = new DataGridViewCellStyle();
            numberCell.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgInventory.Columns["QTY"].DefaultCellStyle = numberCell;
            dgInventory.Columns["Price"].DefaultCellStyle = numberCell;
            dgInventory.Columns["MarkupPercentage"].DefaultCellStyle = numberCell;
            dgInventory.Columns["MAP"].DefaultCellStyle = numberCell;
            dgInventory.Columns["MarkupPrice"].DefaultCellStyle = numberCell;
            dgInventory.Columns["RetailPrice"].DefaultCellStyle = numberCell;
            dgInventory.Columns["DomesticShipping"].DefaultCellStyle = numberCell;

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
            Util.PopulateVendors(cmbVendors);
            Util.PopulateProfiles(cmbProfile);

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

        /// <summary>
        /// 
        /// </summary>
        private void FormatCAErrorGrid()
        {
            dgCAErrors.Columns[0].Width = 400;

            dgCAErrors.Columns[1].Visible = false;
        }//end method

        /// <summary>
        /// Method to Update Channel Advisor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCA_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgInventory.Rows.Count == 0)
                {
                    Util.ShowMessage("Inventory list is empty.");
                    return;
                }
                if (IsLoadingCache())
                {
                    Util.ShowMessage("Cache is reloading. Please try again late.");
                    return;
                }
                Settings();

                System.Diagnostics.Debug.WriteLine("Updating CA");
                List<ErrorLog> errorLogList = new List<ErrorLog>();
                WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();
                int vendorId = Convert.ToInt32(cmbVendors.SelectedValue);

                UpdateToFTP uftp = new UpdateToFTP(vendorId, Convert.ToInt32(cmbProfile.SelectedValue), invUpdSrvcDTO);
                //errorLogList = uftp.ExportCSV();
                errorLogList = uftp.ExportCSV();

                if (errorLogList.Count == 0)
                    Util.ShowMessage("Export csv file to Ftp was completed");
                else
                    Util.ShowMessage("Error when export csv file.");

                BindingSource _gridSource = new BindingSource();
                _gridSource.DataSource = errorLogList;
                dgCAErrors.DataSource = _gridSource;

                FormatCAErrorGrid();

            }
            catch (ArgumentOutOfRangeException ex)
            {
                Util.ShowMessage("Data updating in Linnworks unexpectedly didn't return the error log list.");
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }
            finally
            {
                btnUpdateCA.Text = "Update Again";
            }

        }//end event


        /// <summary>
        /// Method that will loop the grid and create InventoryDTO list
        /// </summary>
        /// <returns></returns>
        private BindingList<Inventory> CreateListForCAUpdate()
        {
            BindingList<Inventory> inventoryDTOList = new BindingList<Inventory>();

            foreach (DataGridViewRow dRow in dgInventory.Rows)
            {
                var boundItem = (Inventory)dRow.DataBoundItem;
                //if (boundItem.LinnworksStockItemId == Guid.Empty)
                //    continue;
                inventoryDTOList.Add(boundItem);
            }//end foreach

            return inventoryDTOList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyLocalErrors_Click(object sender, EventArgs e)
        {
            Util.CopyGridToClipBoard(dgErrors, "Errors");
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyCAErrors_Click(object sender, EventArgs e)
        {
            Util.CopyGridToClipBoard(dgCAErrors, "CAErrors");
        }

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

        private void Settings()
        {
            DAL dal = new DAL();
            csvIsftp = "0";
            switch (cmbVendors.SelectedValue.ToString())
            {
                case "1":
                    csvfolder = dal.GetSettingValue("EMG_CSVFolder");
                    csvfilename = dal.GetSettingValue("EMG_CSVFIle");
                    csvIsftp = dal.GetSettingValue("EMG_CSVIsFTP") == "1" ? "1" : "0";
                    break;

                case "3":
                    csvfolder = dal.GetSettingValue("PCycle_CSVFolder");
                    csvfilename = dal.GetSettingValue("PCycle_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PCycle_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "4":
                    csvfolder = dal.GetSettingValue("DUpAmerica_CSVFolder");
                    csvfilename = dal.GetSettingValue("DUpAmerica_CSVFIle");
                    csvIsftp = dal.GetSettingValue("DUpAmerica_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "5":
                    csvfolder = dal.GetSettingValue("PTime_CSVFolder");
                    csvfilename = dal.GetSettingValue("PTime_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PTime_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "6":
                    csvfolder = dal.GetSettingValue("Sumdex_CSVFolder");
                    csvfilename = dal.GetSettingValue("Sumdex_CSVFile");
                    csvIsftp = dal.GetSettingValue("Sumdex_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "7":
                    csvfolder = dal.GetSettingValue("CWR_CSVFolder");
                    csvfilename = dal.GetSettingValue("CWR_CSVFIle");
                    csvIsftp = dal.GetSettingValue("CWR_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "10":
                    csvfolder = dal.GetSettingValue("KwikTek_CSVFolder");
                    csvfilename = dal.GetSettingValue("KwikTek_CSVFIle");
                    csvIsftp = dal.GetSettingValue("KwikTek_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "11":
                    csvfolder = dal.GetSettingValue("Rockline_CSVFolder");
                    csvfilename = dal.GetSettingValue("Rockline_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Rockline_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "12":
                    csvfolder = dal.GetSettingValue("Morris_CSVFolder");
                    csvfilename = dal.GetSettingValue("Morris_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Morris_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "13":
                    csvfolder = dal.GetSettingValue("Petra_CSVFolder");
                    csvfilename = dal.GetSettingValue("Petra_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Petra_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "14":
                    csvfolder = dal.GetSettingValue("MorrisComplete_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisComplete_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisComplete_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "15":
                    csvfolder = dal.GetSettingValue("Benchmark_CSVFolder");
                    csvfilename = dal.GetSettingValue("Benchmark_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Benchmark_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "16":
                    csvfolder = dal.GetSettingValue("MorrisNightly_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisNightly_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisNightly_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "17":
                    csvfolder = dal.GetSettingValue("MorrisChanges_CSVFolder");
                    csvfilename = dal.GetSettingValue("MorrisChanges_CSVFIle");
                    csvIsftp = dal.GetSettingValue("MorrisChanges_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "18":
                    csvfolder = dal.GetSettingValue("AZ_CSVFolder");
                    csvfilename = dal.GetSettingValue("AZ_CSVFIle");
                    csvfilenameWithRenamedFields = dal.GetSettingValue("AZ_CSVFIleImage");
                    csvIsftp = dal.GetSettingValue("AZ_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "20":
                    csvfolder = dal.GetSettingValue("GreenSupply_CSVFolder");
                    csvfilename = dal.GetSettingValue("GreenSupply_CSVFIle");
                    csvIsftp = dal.GetSettingValue("GreenSupply_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "21":
                    csvfolder = dal.GetSettingValue("Viking_CSVFolder");
                    csvfilename = dal.GetSettingValue("Viking_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Viking_CSVIsFTP") == "1" ? "1" : "0";
                    break;

                case "22":
                    csvfolder = dal.GetSettingValue("NearlyNatural_CSVFolder");
                    csvfilename = dal.GetSettingValue("NearlyNatural_CSVFIle");
                    csvIsftp = dal.GetSettingValue("NearlyNatural_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "23":
                    csvfolder = dal.GetSettingValue("Moteng_CSVFolder");
                    //csvfilenameConverted = dal.GetSettingValue("Moteng_CSVFIleConverted");
                    csvfilename = dal.GetSettingValue("Moteng_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Moteng_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "27":
                    csvfolder = dal.GetSettingValue("Seawide_CSVFolder");
                    csvfilename = dal.GetSettingValue("Seawide_CSVFIle");
                    csvIsftp = dal.GetSettingValue("Seawide_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "28":
                    csvfolder = dal.GetSettingValue("TWH_CSVFolder");
                    csvfilename = dal.GetSettingValue("TWH_CSVFIle");
                    csvIsftp = dal.GetSettingValue("TWH_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "53":
                    csvfolder = dal.GetSettingValue("PetGear_CSVFolder");
                    csvfilename = dal.GetSettingValue("PetGear_CSVFIle");
                    csvIsftp = dal.GetSettingValue("PetGear_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                case "61":
                    csvfolder = dal.GetSettingValue("OceanStar_CSVFolder");
                    csvfilename = dal.GetSettingValue("OceanStar_CSVFIle");
                    csvIsftp = dal.GetSettingValue("OceanStar_CSVIsFTP") == "1" ? "1" : "0";
                    break;
                default:
                    //Other Vendors
                    csvfolder = dal.GetSettingValue("OtherVendors_CSVFolder");
                    csvfilename = dal.GetSettingValue("OtherVendors_CSVFIle");
                    csvIsftp = dal.GetSettingValue("OtherVendors_CSVIsFTP") == "1" ? "1" : "0";

                    break;
            }
        }//end method

        //
        private bool IsLoadingCache()
        {
            string path = Path.Combine(Application.StartupPath, "inventoryCache");
            //string pathtemp = path + "temp";
            //string  messagefile = Path.Combine(pathtemp, "message.txt");
            //if (File.Exists(messagefile))

            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.GetFiles().Length == 0)
                return true;
            else
                return false;

        }

        private void cmbVendors_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void button1_Click(object sender, EventArgs e)
        //{

        //    UpdateLinnworksForVendor();
        //    //string ss = "\"<!-- span.ebay \t{} -->\r\neq6wDZgvq9A&amp;hl=en\" />\r\n<p><span style=\"font-family: Arial;\">&nbsp; ";
        //    //ss = ss.Replace("\r\n", "\"\r\n\"");
        //    //textBox1.Text = ss;
        //}

        //private void UpdateLinnworksForVendor()
        //{
        //    List<int> profiles = new List<int>();
        //    profiles.Add(1);
        //    //log.Debug("UpdateLinnworksForVendor:");
        //    UpdateToFTP uftp = new UpdateToFTP(18);
        //    csvIsftp = uftp.Settings();

        //    //Dictionary to maintain CA error log for each profile
        //    Dictionary<int, List<ErrorLog>> profileErrorLogs
        //            = new Dictionary<int, List<ErrorLog>>();

        //    //Dictionary to store CA files
        //    Dictionary<int, string> profileCAFiles = new Dictionary<int, string>();


        //    InventoryUpdateService invService = new InventoryUpdateService();
        //    var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();

        //    InventoryUpdateServiceDTO invUpdateServiceDTO = null;
        //    //Call Sevice based on vendor
        //    int _vendorID = 18;
        //    // if vendor is AZ then we need get dttable(ccsv file data)
        //    DataTable azdt = new DataTable();
        //    string skurefix = "";
        //    if (_vendorID == 18)
        //    {
        //        AZService az = new AZService();
        //        skurefix = az.VendorInfo.SkuPrefix;
        //        invUpdateServiceDTO = az.GetInventoryListForService();
        //        azdt = az.dtProducts;
        //        //                VendorServiceFactory.GetVendorService(_vendorID).GetInventoryListForService();
        //    }
        //    else
        //        invUpdateServiceDTO = VendorServiceFactory.GetVendorService(_vendorID).GetInventoryListForService();

        //    if (invUpdateServiceDTO == null)
        //        return;

        //    //Get files to delete
        //    List<ErrorLog> profileErrors = new List<ErrorLog>();

        //    // if cache is not updating
        //    for (int x = 0; x < profiles.Count; x++)
        //    {
        //        //Update the markup prices
        //        invService.UpdateMarkupPrice(_vendorID,
        //                                        profiles[x],
        //                                        invUpdateServiceDTO);

        //        uftp.invUpdSrvcDTO = invUpdateServiceDTO;
        //        uftp.ProfileID = profiles[x];

        //        // if vendor is AZ
        //        if (_vendorID == 18)
        //            profileErrors = uftp.ExportAZCSV(azdt, skurefix);
        //        else
        //            profileErrors = uftp.ExportCSV();

        //        profileErrorLogs.Add(profiles[x], profileErrors);

        //        //create file
        //        string caFile = CAUtil.CreateCAFile(_vendorID, profiles[x], invUpdateServiceDTO.InventoryDTO);
        //        profileCAFiles.Add(profiles[x], caFile);

        //    }//end for

        //    //Save Logs to database

        //}//end method

    }//end form
}//end namespace
