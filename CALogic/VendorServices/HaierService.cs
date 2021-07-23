using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;

namespace ChannelAdvisor
{
    public class HaierService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _scanFolder = "";

        string _filePath = ""; //path to excel file

        /// <summary>
        /// 
        /// </summary>
        public HaierService()
        {
            //load settings
            _scanFolder = new DAL().GetHaierFolder();

            VendorInfo = new DAL().GetVendor((int)VendorName.Haier);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Haier"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForHaier(false);
        }//end method


        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForHaier(true);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForHaier(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatble
            DataTable dtProducts = GetHaierProductTable(invUpdateSrcvDTO);

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_filePath, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstHaierInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's. Start from 1st row
            for (int x = 0; x < dtProducts.Rows.Count; x++)
            {
                Inventory invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString().Trim());

                invDTO.Qty = 0;
                if ((dtProducts.Rows[x][1] != null) && (!string.IsNullOrEmpty(dtProducts.Rows[x][1].ToString().Trim())))
                    invDTO.Qty += int.Parse(dtProducts.Rows[x][1].ToString().Trim());
                if ((dtProducts.Rows[x][2] != null) && (!string.IsNullOrEmpty(dtProducts.Rows[x][2].ToString().Trim())))
                    invDTO.Qty += int.Parse(dtProducts.Rows[x][2].ToString().Trim());
                if ((dtProducts.Rows[x][3] != null) && (!string.IsNullOrEmpty(dtProducts.Rows[x][3].ToString().Trim())))
                    invDTO.Qty += int.Parse(dtProducts.Rows[x][3].ToString().Trim());

                invDTO.Price = null;
                invDTO.RetailPrice = null;
                invDTO.MAP = 0;
                invDTO.Description = "";
                lstHaierInventory.Add(invDTO);

            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstHaierInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetHaierProductTable(InventoryUpdateServiceDTO invUpdateSrcvDTO)
        {
            //Get file path
            //string filePath = GetExcelFilePath();
            _filePath = GetExcelFilePath();

            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEX"];

            //extract data from excel into datatable
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _filePath));

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]", con);

            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            //add file to to-delete list
            invUpdateSrcvDTO.ToDeleteFiles.Add(_filePath);

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

    }
}
