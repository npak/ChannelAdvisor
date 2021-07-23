using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;
using System.IO;


namespace ChannelAdvisor
{
    /// <summary>
    /// 
    /// </summary>
    public class SumdexService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _scanFolder = "";//@"D:\Freelance\Channel Advisor\ChannelAdvisor\ScanFiles\Sumdex\";

        string _filePath = ""; //path to excel file

        /// <summary>
        /// 
        /// </summary>
        public SumdexService()
        {
            //load settings
            _scanFolder = new DAL().GetSumdexFolder();

            VendorInfo = new DAL().GetVendor((int)VendorName.Sumdex);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Sumdex"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForSumdex(false);
        }//end method


        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForSumdex(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForSumdex(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatble
            DataTable dtProducts = GetSumdexProductTable(invUpdateSrcvDTO);

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_filePath, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's. Start from 1st row
            for (int x = 4; x < dtProducts.Rows.Count; x++)
            {
                //if first column is blank then exit loop
                if (dtProducts.Rows[x][0] == null || string.IsNullOrEmpty(dtProducts.Rows[x][0].ToString().Trim()))
                {
                    continue;
                }
                else
                {
                    Inventory invDTO = new Inventory();
                    invDTO.UPC = "";
                    invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString().Trim());

                    if ((dtProducts.Rows[x][3] != null) && (string.Compare(dtProducts.Rows[x][3].ToString(), "Discontinued", true) == 0))
                        invDTO.Qty = -5000;
                    else
                    {
                        //check if qty is blank
                        if (dtProducts.Rows[x][2] == null || string.IsNullOrEmpty(dtProducts.Rows[x][2].ToString().Trim()))
                            invDTO.Qty = 0;
                        else
                            invDTO.Qty = Int32.Parse(dtProducts.Rows[x][2].ToString().Trim());
                    }

                    invDTO.Price = null;
                    invDTO.RetailPrice = null;
                    invDTO.MAP = 0;
                    invDTO.Description = "";
                    lstEMGInventory.Add(invDTO);
                }//end if

            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetSumdexProductTable(InventoryUpdateServiceDTO invUpdateSrcvDTO)
        {
            //Get file path
            //string filePath = GetExcelFilePath();
            _filePath = GetExcelFilePath();

            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConString"];

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            //extract data from excel into datatable
            using (OleDbConnection con = new OleDbConnection(String.Format(excelConString, _filePath)))
            {
                con.Open();

                string tableName = con.GetSchema("Tables").Rows[0]["TABLE_NAME"].ToString();

                // create a data adapter to select everything from the worksheet you want
                //OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sumdex$]", con);
                OleDbDataAdapter da = new OleDbDataAdapter(string.Format("select * from [{0}]", tableName), con);

                // use the data adapter to fill the datatable
                da.Fill(dtSheets);

                //add file to to-delete list
                invUpdateSrcvDTO.ToDeleteFiles.Add(_filePath);
            }

            return dtSheets;
        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetExcelFilePath()
        {
            //Get list of files in directory
            string[] files = Directory.GetFiles(_scanFolder, "*.xls");

            //return first file
            if (files.GetLength(0) > 0)
            {
                return files[0];
            }

            return "";
        }//end method

    }//end class

}//end namespace
