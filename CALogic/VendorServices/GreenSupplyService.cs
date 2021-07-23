using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Linq;
using System.Net;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data;
using Sgml;

namespace ChannelAdvisor
{


    public class GreenSupplyService:IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;      

        private string _url = "";//"http://www.picnictime.com/dealers/memberLogin.php";
        private string _dropshipfee = "";

        private string _localFolder = "";
        private string _localFileName = "GreenSupply.csv";

        /// <summary>
        /// 
        /// </summary>
        public GreenSupplyService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            new DAL().GetGreenSupplyUrl(out _url, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.GreenSupply);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Green Supply"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForGreenSupply(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForGreenSupply(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForGreenSupply(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            List<ErrorLog> errorList = new List<ErrorLog>();
            DataTable dtProducts =GetGreenSupplyProductTable(ref errorList);
            invUpdateSrcvDTO.ErrorLogDTO = errorList;
            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);

            for(int x=0; x<dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";    
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString());
                        invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x][3]);

                        if (dtProducts.Rows[x][8] == null)
                            invDTO.Price = null;
                        else
                        {
             //               test = dtProducts.Rows[x]["Price"].ToString();
                            invDTO.Price = fee + float.Parse(dtProducts.Rows[x][8].ToString());
                            invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                        }
                        invDTO.MAP = float.Parse(dtProducts.Rows[x][9].ToString());
                        invDTO.RetailPrice = null;
                        
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


        private void DownloadCsvFile(string url, string fileName)
        {
            // Create a request using a URL that can receive a post. 
            request = (HttpWebRequest)HttpWebRequest.Create(url);

            // Set the Method property of the request to GET.
            request.Method = "GET";
            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Get the response.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        using (StreamWriter writer = new StreamWriter(fileName, false))
                        {
                            writer.Write(reader.ReadToEnd());
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    responseStream.Close();
                }
                response.Close();
            }
        }

  
        public DataTable GetGreenSupplyProductTable(ref List<ErrorLog> errorlist)
        {
            // upcomment after
            string saveTo = _localFolder + _localFileName;//"GreenSupply.xls";
            DownloadCsvFile(_url,saveTo);
            DataTable dtProducts = new DataTable();
            var connString = string.Format(
                @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                Path.GetDirectoryName(saveTo)
            );

            using (var conn = new OleDbConnection(connString))
            {
                conn.Open();
                var query = "SELECT * FROM [" + Path.GetFileName(saveTo) + "]";
                using (var adapter = new OleDbDataAdapter(query, conn))
                {
                    // use the data adapter to fill the datatable
                    adapter.Fill(dtProducts);
                }
            }

            return dtProducts;
        }
  
  
    }//end class
}//end namespace
