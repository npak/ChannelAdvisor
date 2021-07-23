using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.ComponentModel;
using System.Net;
using System.Globalization;
using HtmlAgilityPack;
using System.Windows.Forms;
using System.Threading;
using log4net;
    
namespace ChannelAdvisor
{
    public class BenchmarkService :IVendorService
    {
        public readonly ILog log = LogManager.GetLogger(typeof(BenchmarkService));
        public Vendor VendorInfo { get; set; }
        public static int ThreadBenchmarkRunning;
        private Thread threadBenchmark;
        string _instepSiteHTML = "";
        private List<Benchmark> benchmarkList = new List<Benchmark>();
        /// <summary>
        /// constructor
        /// </summary>
        public BenchmarkService()
        {
            VendorInfo = new DAL().GetVendor((int)VendorName.Benchmark);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Benchmark"));

           
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForBenchmark(false);
        }//end method

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForBenchmark(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForBenchmark(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Read files and create list of SKUs and their Price

            Interlocked.Exchange(ref ThreadBenchmarkRunning, 1);   
            StartBenchmarkListThread();
               
            //GetBenchmarkList();
            while (System.Threading.Interlocked.CompareExchange(ref ThreadBenchmarkRunning, 0, 0) != 0)
            {
                System.Threading.Thread.Sleep(30000);
            }
            //Save html file to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveHTMLFileAsVendorFile(_instepSiteHTML, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the Generic Price List and create dto
            for (int x = 0; x < benchmarkList.Count; x++)
            {
                Inventory invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, benchmarkList[x].Item.Trim());
                invDTO.Price = null;
                invDTO.RetailPrice = null;
                invDTO.MAP = 0;
                invDTO.Description = benchmarkList[x].Description.Trim();
                if ((benchmarkList[x].Status.Trim().Replace("-","").Replace(" ","").ToLower() == "instock") || (benchmarkList[x].Status.ToLower() == "now available") || (benchmarkList[x].Status.ToLower() == "available now!"))
                    invDTO.Qty = 100;
                else if (IsValidDateTimeTest(benchmarkList[x].Status))
                    invDTO.Qty = 0;
                //if sku exists update or add
                AddInventoryOrUpdate(lstEMGInventory, invDTO);
                
            }//end for each

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstEMGInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// This method accepts an inventory DTO, checks whether the SKU is already
        /// present in the InventoryDTOlist. If present it updates qty 
        /// and if not found then adds it.
        /// </summary>
        /// <param name="lstEMGInventory"></param>
        /// <param name="invDTO"></param>
        private void AddInventoryOrUpdate(BindingList<Inventory> lstEMGInventory, Inventory invDTO)
        {
            //loop list and check if SKU exists
            for (int x = 0; x < lstEMGInventory.Count; x++)
            {
                if (lstEMGInventory[x].SKU == invDTO.SKU)
                {
                    lstEMGInventory[x].Qty += invDTO.Qty;
                    return;
                }//end if
            }//end for

            lstEMGInventory.Add(invDTO);
        }//end method


        /// <summary>
        /// Method to scrape pacific Cycle and get the list
        /// </summary>
        /// <returns></returns>

        private void StartBenchmarkListThread()
        {
            threadBenchmark = new Thread(GetBenchmarkList);
            threadBenchmark.SetApartmentState(ApartmentState.STA);
            threadBenchmark.Start();
            
        }

        private void StopBenchmarkListThread()
        {
            // kill thread
            if ((threadBenchmark!=null) && (threadBenchmark.IsAlive))
            {
                string s;
                try
                {
                    threadBenchmark.Abort();
                    threadBenchmark.Join();
                }
                catch (System.Threading.ThreadAbortException ex)
                {
                    return;
                }
                
            }
        }

        private void GetBenchmarkList()
        {
            string url;
            string username;
            string password;
            try
            {
                new DAL().GetBenchmarkInfo(out url, out username, out password);

                // create a webBrowser object
                WebBrowser webBrowser1 = new WebBrowser();
                webBrowser1.Navigate(url);
                while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                    System.Windows.Forms.Application.DoEvents();
                System.Threading.Thread.Sleep(5000);

                if (webBrowser1.DocumentTitle != "Stock Status Report")
                {
                    //find elemrnts and set values
                    System.Windows.Forms.HtmlDocument doc = webBrowser1.Document;
                    HtmlElement elem1 = doc.GetElementById("username");
                    elem1.InnerText = username;
                    HtmlElement elem2 = doc.GetElementById("passwd");
                    elem2.InnerText = password;
                    //click
                    foreach (HtmlElement he in doc.GetElementsByTagName("input"))
                    {
                        if (he.GetAttribute("value").Equals("Login"))
                        {
                            he.InvokeMember("click");
                            break;
                        }
                    }

                    // wait  while the page is loading
                    while ((webBrowser1.ReadyState != WebBrowserReadyState.Complete) || (webBrowser1.DocumentTitle != "Stock Status Report"))
                        System.Windows.Forms.Application.DoEvents();
                }
                else
                {
                    webBrowser1.Refresh();
                    // wait  while the page is loading
                    while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
                        System.Windows.Forms.Application.DoEvents();
                }
                //get page content
                _instepSiteHTML = webBrowser1.DocumentText;


                Benchmark benchmark;

                //parce page content
                HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
                htmldoc.LoadHtml(_instepSiteHTML);
                var nodes = htmldoc.DocumentNode.SelectNodes("//table[@class='factory2nds']/tbody/tr");
                if (nodes != null)
                {
                    HtmlNodeCollection cols;
                    bool b = false;
                    foreach (HtmlNode node in nodes)
                    {
                        if (b)
                        {
                            cols = node.SelectNodes(".//td");
                            benchmark = new Benchmark();

                            benchmark.Item = cols[0].InnerText.Trim();
                            benchmark.Description = cols[1].InnerText.Replace("&nbsp;", " ").Replace(",", ";").Trim();
                            benchmark.Status = cols[2].InnerText.Trim();
                            benchmarkList.Add(benchmark);
                            //foreach (HtmlNode td in cols)
                            //    result += td.InnerText.Replace("&nbsp;", " ").Trim() + ";";
                            //result += "\n";
                        }
                        else b = true;
                    }
                }
            }
            catch (Exception ex)
            { log.Debug("LoadThread error: " + ex.Message); }
            finally
            {
                Interlocked.Exchange(ref ThreadBenchmarkRunning, 0);
                StopBenchmarkListThread();
            }
            //return benchmarkList;
            
        }//end method

        public bool IsValidDateTimeTest(string dateTime)
        {
            var date_regex = @"^(([1-9])|(0[1-9])|(1[0-2]))\/(([1-9])|(0[1-9])|(1\d)|(2\d)|(3[01]))\/(\d\d)$";
            if (System.Text.RegularExpressions.Regex.IsMatch(dateTime, date_regex))
                return true;
            else
                return false;
        }
       
    }//end class

}//end namespace
