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
//using Microsoft.VisualBasic.FileIO;

namespace ChannelAdvisor
{

    public class AZService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;

        private string _url = "";
        private string _dropshipfee = "";

        private string _localFolder = "";
        //private string _localFileName = "AZ.csv";
        private string path;

        public DataTable dtProducts { get; set; }
        //= new DataTable();
        /// <summary>
        /// 
        /// </summary>
        public AZService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            string timeName = DateTime.Now.Hour.ToString("00") + DateTime.Now.Minute.ToString("00") + DateTime.Now.Second.ToString("00");
            path = System.Windows.Forms.Application.StartupPath + "\\Temp\\tempAZ" + timeName + ".csv";
            new DAL().GetAZInfo(out _url, out _dropshipfee);

            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.AZ);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for AZ"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForAZ(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForAZ(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForAZ(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            List<ErrorLog> errorList = new List<ErrorLog>();
            //download csv file
            DownloadCsvFile();

            //read csv file to table
            dtProducts = GetAZProductTable();

            invUpdateSrcvDTO.ErrorLogDTO = errorList;
            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveScvFileAsVendorFile(path, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

                //delete temp file
                if (File.Exists(path))
                    File.Delete(path);

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            float fee = 0;
            if (!string.IsNullOrWhiteSpace(_dropshipfee))
                fee = Convert.ToSingle(_dropshipfee);

            for (int x = 0; x < dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString());
                        invDTO.Qty = Convert.ToInt32(dtProducts.Rows[x][13]);//GetCorrectQty(dtProducts.Rows[x][3].ToString().Trim());
                        if (dtProducts.Rows[x][12] == null)
                            invDTO.Price = null;
                        else
                        {
                            invDTO.Price = fee + float.Parse(dtProducts.Rows[x][12].ToString());
                            invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                        }
                        invDTO.RetailPrice = null;

                        if (!String.IsNullOrEmpty(dtProducts.Rows[x][1].ToString()))
                            invDTO.Category = dtProducts.Rows[x][1].ToString();
                        if (!String.IsNullOrEmpty(dtProducts.Rows[x][2].ToString()))
                            invDTO.Description = dtProducts.Rows[x][2].ToString();

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
        /// Method to access AZ website and download  csv file
        /// </summary>
        private void DownloadCsvFile()
        {

            // Create a request using a URL that can receive a post. 
            request = (HttpWebRequest)HttpWebRequest.Create(_url);

            // Set the Method property of the request to Get.
            request.Method = "Get";

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);

            // save csv file
            SaveFile(sr.ReadToEnd());

            sr.Close();
            sr = null;
            response.Close(); ;

        }//end method

        private void UploadOriginalFile()
        {
            UpdateToFTP uftp = new UpdateToFTP((int)VendorName.AZ);
            uftp.UploadOriginalFile(path, GetCsvFileName(_url));

        }

        public DataTable GetAZProductTable()
        {
            DataTable dtProducts = new DataTable();
            try
            {
                var connString = string.Format(
                    @"Provider=Microsoft.Jet.OleDb.4.0; Data Source={0};Extended Properties=""Text;HDR=YES;FMT=Delimited""",
                    Path.GetDirectoryName(path)
                );

                using (var conn = new OleDbConnection(connString))
                {
                    conn.Open();
                    var query = "SELECT * FROM [" + Path.GetFileName(path) + "]";
                    using (var adapter = new OleDbDataAdapter(query, conn))
                    {
                        // use the data adapter to fill the datatable
                        adapter.Fill(dtProducts);
                    }
                }

            }
            catch (Exception ex)
            {
            }
           
            return dtProducts;

        }

        private void SaveFile(string readCsv)
        {
            string imgFrom = "Category,\"Item Name\",images,images,images,images,images,images,images,images";
            string imgTo = "Categorycsv,\"Item Name\",image1,image2,image3,image4,image5,image6,image7,image8";
            readCsv = readCsv.Replace(imgFrom, imgTo);

            // Create a file to write to
            File.WriteAllText(path, readCsv);
        }

        private string ReadWebScv(string url)
        {
            request = (HttpWebRequest)HttpWebRequest.Create(_url);

            // Set the Method property of the request to Get.
            request.Method = "Get";

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

            System.Text.Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
            StreamReader sr = new StreamReader(response.GetResponseStream(), encoding);

            string readScv = sr.ReadToEnd();
            sr.Close();
            sr = null;

            response.Close();
            return readScv;
        }

        //test
        private string GetCsvFileName(string str)
        {
            int ind = str.LastIndexOf("/");
            return str.Substring(ind + 1);
        }

    }//end class
}//end namespace
