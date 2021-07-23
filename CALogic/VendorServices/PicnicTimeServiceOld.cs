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


    public class PicnicTimeServiceOld:IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";//"http://www.picnictime.com/dealers/memberLogin.php";
        private string _excelUrl = "";//"http://www.picnictime.com/dealers/";
        private string _username = "";//"BAR1139";
        private string _password = "";//"63151";
        private int _qtyForAvailable;// = 100;
        private int _qtyForDate;// = -50;

        private string _localFolder = "";
        private string _localFile = "PicnicTime.xlsx";
        private string _excelConStringName = "ExcelConStringIMEXwithoutHeader";

        ///// <summary>
        ///// 
        ///// </summary>
        //public PicnicTimeServiceOld()
        //{
        //    string textQtyForAvailable = "";
        //    string textQtyForDate = "";

        //    new DAL().GetPicnicTimeInfo(out _url,
        //                                out _excelUrl,
        //                                out _username,
        //                                out _password,
        //                                out textQtyForAvailable,
        //                                out textQtyForDate);

        //    _qtyForAvailable = Int32.Parse(textQtyForAvailable);
        //    _qtyForDate = Int32.Parse(textQtyForDate);

        //    _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";

        //    VendorInfo = new DAL().GetVendor((int)VendorName.PicnicTime);
        //    if (VendorInfo == null)
        //        throw new Exception(string.Format("Cannot load data for Picnic Time"));
        //}//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForPicnicTime(false);
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForPicnicTime(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPicnicTime(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Download excel and return datatables
            DownloadExcelFile();
            DataTable dtProducts = GetPicnicTimeProductTable();
            DataTable dtDiscontinued = GetDiscontinuedTable();

            //Save excelfile to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveExcelFileAsVendorFile(_localFolder + _localFile, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the datarows and create dto's
            for(int x=10; x<dtProducts.Rows.Count; x++)
            {
                if (dtProducts.Rows[x][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtProducts.Rows[x][0].ToString()))
                    {
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, dtProducts.Rows[x][0].ToString());
                        invDTO.Qty = GetCorrectQty(dtProducts.Rows[x][3].ToString().Trim());
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtProducts.Rows[x][2].ToString() +
                                               " - " + dtProducts.Rows[x][1].ToString();

                        lstEMGInventory.Add(invDTO);
                    }//end string empty check
                }//end null check

            }//end for each

            // Loop through discontinued items
            for (int i = 2; i < dtDiscontinued.Rows.Count; i++)
            {
                if (dtDiscontinued.Rows[i][0] != null)
                {
                    if (!String.IsNullOrEmpty(dtDiscontinued.Rows[i][0].ToString()))
                    {
                        
                        Inventory invDTO = new Inventory();
                        invDTO.UPC = "";
                        string discontinuedSku = dtDiscontinued.Rows[i][0].ToString();
                        if(discontinuedSku.EndsWith("-000-0"))
                            discontinuedSku = discontinuedSku.Substring(0, discontinuedSku.Length - "-000-0".Length);
                        invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, discontinuedSku);//.Replace("-","");
                        invDTO.Qty = -5000;
                        invDTO.Price = null;
                        invDTO.RetailPrice = null;
                        invDTO.MAP = 0;
                        invDTO.Description = dtDiscontinued.Rows[i][2].ToString() +
                                               " - " + dtDiscontinued.Rows[i][1].ToString();

                        lstEMGInventory.Add(invDTO);
                    }
                }
            }

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ETA"></param>
        /// <returns></returns>
        private int GetCorrectQty(string ETA)
        {
            int qty;

            if ((ETA.ToLower() == "available") || (ETA.ToLower().Contains("now") && ETA.ToLower().Contains("available")))
            {
                qty = _qtyForAvailable;    
            }
            else
            {
                qty = _qtyForDate;
            }
            return qty;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetPicnicTimeExcelFileURL()
        {
            PostSubmitter post = new PostSubmitter();
            post.Url = _url;

            post.PostItems.Add("username", _username);
            post.PostItems.Add("passwd", _password);
            post.PostItems.Add("submit", "Log In");

            post.Type = PostSubmitter.PostTypeEnum.Post;

            //try to post 3 time because of request timeout issue
            int count = 0;
            string result = "";

            while (count < 3)
            {
                try
                {
                    result = post.Post();
                    count = 4;
                }
                catch(Exception ex)
                {
                    count++;
                    if (count >= 3) throw ex;
                }
            }//end while
                        
            //Convert it to xml
            var bodyNode = GetBodyNode(result);

            //get url
            string excelFileURL = ExtractProductExcelFileURL(bodyNode);

            return excelFileURL;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private string ExtractProductExcelFileURL(HtmlAgilityPack.HtmlNode bodyNode)
        {
            XPathNavigator nav = bodyNode.CreateNavigator();
            XPathNodeIterator nodes = nav.Select("//a");

            string xlsPath = "";
            while (nodes.MoveNext())
            {
                string href = nodes.Current.GetAttribute("href", "");
                if (href.Length > 28 && href.Substring(0, 28) == "docs/stock_status_picnictime")
                {
                    xlsPath = href;
                    
                }
            }//end while

            xlsPath = _excelUrl + xlsPath;
            return xlsPath;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private HtmlAgilityPack.HtmlNode GetBodyNode(string result)
        {
            HtmlAgilityPack.HtmlDocument htmlDoc = new HtmlAgilityPack.HtmlDocument();

            // There are various options, set as needed
            htmlDoc.OptionFixNestedTags = true;
            htmlDoc.LoadHtml(result);  
            if (htmlDoc.DocumentNode != null)
            {
               return htmlDoc.DocumentNode.SelectSingleNode("//body");
                
            }
            return null;
            //Sgml.SgmlReader sgmlReader = new SgmlReader();
            //sgmlReader.DocType = "HTML";
            //StringReader s = new StringReader(result);

            //sgmlReader.InputStream = s;

            //XmlDocument doc = new XmlDocument();
            //doc.PreserveWhitespace = true;
            //doc.XmlResolver = null;
            //doc.Load(sgmlReader);

            //return doc;
        }//end method
        

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetPicnicTimeProductTable()
        {
            //Download file first
            //DownloadExcelFile();

            //get excel connection string
            //string excelConString = System.Configuration.ConfigurationSettings.AppSettings["ExcelConStringIMEX"];
            //IT-Dimension: 2012.06.11 - text cell parsing fix
            string excelConString = System.Configuration.ConfigurationManager.AppSettings[_excelConStringName];

            //Get datatable
            // create a connection to your excel file
            //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + _localFile));//_localFolder

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [IN STOCK ITEMS$]", con);
            
            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;

        }//end method

        private DataTable GetDiscontinuedTable()
        {
            //get excel connection string
            string excelConString = System.Configuration.ConfigurationManager.AppSettings["ExcelConStringIMEX"];

            //Get datatable
            // create a connection to your excel file
            //OleDbConnection con = new OleDbConnection(String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", "Temp\\DressUpAmerica.xls"));
            OleDbConnection con = new OleDbConnection(String.Format(excelConString, _localFolder + _localFile));//_localFolder

            // create a new blank datatable
            DataTable dtSheets = new DataTable();

            // create a data adapter to select everything from the worksheet you want
            OleDbDataAdapter da = new OleDbDataAdapter("Select * from [Discontinued$]", con);

            // use the data adapter to fill the datatable
            da.Fill(dtSheets);

            return dtSheets;
        }

        /// <summary>
        /// Method to access Dress Up America website and download 
        /// </summary>
        private void DownloadExcelFile()
        {
            string excelFileURL = GetPicnicTimeExcelFileURL();
            
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(excelFileURL);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();
            Stream responseStream = res.GetResponseStream();

            string saveTo = _localFolder + _localFile;//"PicnicTime.xls";
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

    }//end class
}//end namespace
