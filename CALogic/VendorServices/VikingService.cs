using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Net;
using System.ComponentModel;
using System.Data;
using log4net;

namespace ChannelAdvisor
{


    public class VikingService:IVendorService
    {
        public readonly ILog log = LogManager.GetLogger(typeof(VikingService));
       
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;      

        private string _url = "";
        private string _dropshipfee = "";

        private string _localFolder = "";
        private string _localFileName = "Viking.csv";

        /// <summary>
        /// 
        /// </summary>
        public VikingService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            new DAL().GetVikingUrl(out _url, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.Viking);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Viking"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForViking(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForViking(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForViking(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            List<ErrorLog> errorList = new List<ErrorLog>();
            DataTable dtProducts =GetVikingProductTable(ref errorList);
            invUpdateSrcvDTO.ErrorLogDTO = errorList;
            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveScvFileAsVendorFile(_localFolder + _localFileName, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();
            try
            {
                //loop through the datarows and create dto's
                float fee = 0;
                if (!string.IsNullOrWhiteSpace(_dropshipfee))
                    fee = Convert.ToSingle(_dropshipfee);

                for (int x = 0; x < dtProducts.Rows.Count; x++)
                {
                    if (x>0)
                    {
                        if (!String.IsNullOrEmpty(dtProducts.Rows[x][1].ToString()))
                        {
                            Inventory invDTO = new Inventory();
                            invDTO.UPC = "";
                            invDTO.SKU = VendorInfo.SkuPrefix + dtProducts.Rows[x][1].ToString();
                            if (  dtProducts.Rows[x][3] != DBNull.Value)
                                invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x][3]);

                            if (dtProducts.Rows[x][4] != DBNull.Value)
                            {
                                //         value != DBNull.Value       test = dtProducts.Rows[x]["Price"].ToString();
                                invDTO.Price = float.Parse(dtProducts.Rows[x][4].ToString());
                                if (invDTO.Price <= 20)
                                {
                                    invDTO.Price += fee;
                                    //invDTO.DomesticShipping = (decimal)fee; 
                                }
                                invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                            }

                            invDTO.RetailPrice = null;
                            if (dtProducts.Rows[x][5] != DBNull.Value)
                                invDTO.MAP = float.Parse(dtProducts.Rows[x][5].ToString());
                            lstEMGInventory.Add(invDTO);
                        }//end string empty check
                    }//end null check

                }//end for each

            }
            catch (Exception ex)
            {
                log.Debug("Error CreateInvUpdateServiceDTOForViking: " + ex.Message);
            }
            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        private void DownloadCsvFile(string url, string fileName)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

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

  
        public DataTable GetVikingProductTable(ref List<ErrorLog> errorlist)
        {
            DataTable dtProducts = new DataTable();
            try
            {
                string saveTo = _localFolder + _localFileName;
                DownloadCsvFile(_url, saveTo);

               
                dtProducts = ExcelUtils.ReadExcelSheet(saveTo.Replace("\\"+ _localFileName, ""), _localFileName, "CsvConStringWithHeader");

            }
            catch (Exception ex)
            {
                log.Debug("Error GetVikingProductTable: " + ex.Message);
            }

            return dtProducts;
        }

        public DataTable GetVikingProductTableOLD(ref List<ErrorLog> errorlist)
        {
            DataTable dt = new DataTable();

            try
            {
                // upcomment after
                string saveTo = _localFolder + _localFileName;
                DownloadCsvFile(_url, saveTo);

                //StreamReader sr = new StreamReader("E:\\papa\\2-15-15\\ChannelAdvisorSources\\output\\prodlist.csv");
                StreamReader sr = new StreamReader(_localFolder + _localFileName);
                // skip two ffirst lines
                //sr.ReadLine();
                //sr.ReadLine();

                // 0 v_products_model, 1 v_products_upc, 2 v_products_map, 3 v_products_price_cost, 4 v_products_name_1,
                // 5 v_products_description_1, 6 v_products_image, 7 v_products_price, 8 v_products_quantity, 9 v_products_weight, ...
                // sku -0
                //price- 7
                // quantity - 8


                // read headers
                string[] headers = sr.ReadLine().Split(',');

                dt.Columns.Add("sku");
                dt.Columns.Add("price");
                dt.Columns.Add("quantity");
                dt.Columns.Add("map");
                int cnt = 0;

                //string[] stringSeparators = new string[] { "\",\"" };
                string[] stringSeparators = new string[] { "," };

                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(stringSeparators, StringSplitOptions.None);
                    DataRow dr = dt.NewRow();
                    //if (rows.Count() != headers.Length)
                    //{
                    //    cnt++;

                    //    ErrorLog err = new ErrorLog(0, "Viking csv record with SKU:" + rows[0] + " is not properly formated: ");
                    //    errorlist.Add(err);
                    //    continue;
                    //}

                    dr["sku"] = rows[0].Replace("\"", "");
                    dr["price"] = rows[7].Replace("\"", "");
                    dr["quantity"] = rows[8].Replace("\"", "");
                    dr["map"] = rows[2].Replace("\"", "");
                    dt.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                log.Debug("Error GetVikingProductTable: " + ex.Message);
            }

            return dt;
        }

    }//end class
}//end namespace
