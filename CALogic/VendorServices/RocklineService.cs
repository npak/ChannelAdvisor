using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Data;
using System.Data.OleDb;
using System.ComponentModel;


namespace ChannelAdvisor
{
    public class RocklineService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";
        public string FTPServer { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        private string _localFolder = "";
        private string _localFileName = "Inventory.csv";

        /// <summary>
        /// Default constructor
        /// </summary>
        public RocklineService()
        {
            DAL dal = new DAL();
            //Get URL
            dal.GetRockLineInfo(out _url);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
           // _localFolder += SettingsConstant.RocklineFile_Folder_Name; // "Rockline\\Downloads\\";
            VendorInfo = dal.GetVendor((int)VendorName.Rockline);
            
            // get ftp
            FTPServer = dal.GetSettingValue("Rockline_FTPServer");

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Rockline"));
        }//end constructor


        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForRockline(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForRockline(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForRockline(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatble
            DataTable dtProducts = GetRocklineProductTable();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();
            Inventory invDTO;
            //loop through the Generic Price List and create dto
            for (int i = 0; i < dtProducts.Rows.Count; i++)
            {
                if (dtProducts.Rows[i][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[i][0].ToString()))
                    {
                        invDTO = new Inventory();
                        invDTO.UPC = dtProducts.Rows[i][2].ToString();
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[i][1].ToString());
                        invDTO.Qty = Int32.Parse(dtProducts.Rows[i][3].ToString());
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtProducts.Rows[i][0].ToString();

                        lstEMGInventory.Add(invDTO);
                    }//end string empty check 0Product Name,1Product Custom SKU,2UPC,3Qty
                }//end null check

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
        private DataTable GetRocklineProductTableOLD()
        {
            //Download file first
            GetInventoryExcel();
            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEXwithoutHeader"];

            //Get datatable
            // create a connection to your excel file
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + "Inventory.csv"));

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from Inventory.csv", con);

            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;

        }//end method

        public DataTable GetRocklineProductTable()
        {
            //Download file first
            GetInventoryExcel();

            StreamReader sr = new StreamReader(_localFolder + _localFileName);
            string[] headers = sr.ReadLine().Split(',');
            DataTable dt = new DataTable();
            foreach (string header in headers)
            {
                dt.Columns.Add(header);
            }
            while (!sr.EndOfStream)
            {
                string[] rows = sr.ReadLine().Split(',');
                DataRow dr = dt.NewRow();
                if (rows.Length > headers.Length)
                {
                    int delta = rows.Length - headers.Length;
                    for (int i = 1; i <= delta; i++)
                        rows[0] += ","+rows[i];
                    for (int k = 1; k < headers.Length; k++)
                        rows[k] = rows[k + delta];
                }
                for (int i = 0; i < headers.Length; i++)
                {
                    dr[i] = rows[i];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        } 

        private string GetInventoryExcel()
        {
            FTP ftp = new FTP(FTPServer, Login, Password);
            string filename = "Inventory.csv"; 
            string saveTo = _localFolder;
            // download inventory file
            ftp.DownloadFile("Inventory.csv",filename, saveTo);

            return string.Format(@"{0}{1}", saveTo, filename);
        }

    }
}//end namespace
