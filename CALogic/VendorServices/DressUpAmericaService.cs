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
    public class DressUpAmericaService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";

        private string _localFolder = "";
        private string _localFileName = "DressUpAmerica.xls";

        /// <summary>
        /// Default constructor
        /// </summary>
        public DressUpAmericaService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            //Get URLF
            new DAL().GetDressUpAmericaInfo(out _url);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.DressUpAmerica);

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for DressUpAmerica"));
        }//end constructor


        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForDressUpAmerica(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForDressUpAmerica(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForDressUpAmerica(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatble
            DataTable dtProducts = GetDressUpAmericaProductTable();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the Generic Price List and create dto
            //foreach(DataRow dr in dtProducts.Rows) 
            bool isNumeric = true;
            int num = 0;
            for (int i = 1; i < dtProducts.Rows.Count; i++)
            {
                if (dtProducts.Rows[i][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[i][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = dtProducts.Rows[i][1].ToString();
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[i][0].ToString());
                        isNumeric = int.TryParse(dtProducts.Rows[i][3].ToString(), out num);
                        if (isNumeric)
                            invDTO.Qty = num;
                        else
                            invDTO.Qty = 0;
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtProducts.Rows[i][2].ToString();

                        lstEMGInventory.Add(invDTO);
                    }//end string empty check
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
        private DataTable GetDressUpAmericaProductTable()
        {
            //Download file first
            DownloadExcelFile();

            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEXwithoutHeader"];

            //Get datatable
            // create a connection to your excel file
            //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + "DressUpAmerica.xls"));

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [All products$]", con);

            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;

        }//end method

        /// <summary>
        /// Method to access Dress Up America website and download 
        /// </summary>
        private void DownloadExcelFile()
        {
            //string sHome = "http://www.dressupamerica.com/files/availabilitystatus.xls";
            string sHome = _url;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(sHome);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream responseStream = res.GetResponseStream();

            string saveTo = _localFolder + _localFileName;//"DressUpAmerica.xls";
            //// create a write stream
            FileStream writeStream = new FileStream(saveTo, FileMode.Create, FileAccess.Write);
            //// write to the stream
            ReadWriteStream(responseStream, writeStream);


        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="readStream"></param>
        /// <param name="writeStream"></param>
        private void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            //int Length = 256;
            int Length = 1256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }//end method

    }
}//end namespace
