using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.ComponentModel;
using System.Net;
using log4net;
using log4net.Config;

namespace ChannelAdvisor
{
    public class PacificCycleService:IVendorService
    {
        public readonly ILog log = LogManager.GetLogger(typeof(PacificCycleService));
        public Vendor VendorInfo { get; set; }

        //private string _url;
        //private string _username;
        //private string _password;
        //private int _qtyFor100;       
      

        int _instepQtyFor100;
        int _inlineToysQtyFor100;

        string _instepSiteHTML = "";

        /// <summary>
        /// constructor
        /// </summary>
        public PacificCycleService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            VendorInfo = new DAL().GetVendor((int)VendorName.PacificCycle);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Pacific Cycle"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForPacificCycle(false);
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
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForPacificCycle(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPacificCycle(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Read files and create list of SKUs and their Price
            List<PacificCycle> pacificCycleList = GetPacificCycleList();
            // Append list with data for Inline & Toys
            AppendWithInlineToys(pacificCycleList);

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
            for (int x = 0; x < pacificCycleList.Count; x++)
            {
                Inventory invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, pacificCycleList[x].Material.Trim());
                invDTO.Qty = pacificCycleList[x].ATP;
                invDTO.Price = null;
                invDTO.RetailPrice = null;
                invDTO.MAP = 0;
                invDTO.Description = pacificCycleList[x].Description.Trim();

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
        /// Method that accepts a qty string and returns correct qty
        /// for e.g. 100+ returns 100
        /// </summary>
        /// <param name="qtyText"></param>
        /// <returns></returns>
        private int GetCorrectQty(string qtyText, int qtyFor100)
        {
            int qty;

            if (qtyText == "100+")
            {
                qty = qtyFor100;//100;
            }
            else
            {
                qty = Int32.Parse(qtyText);
            }

            return qty;
        }//end method

        /// <summary>
        /// Method to scrape pacific Cycle and get the list
        /// </summary>
        /// <returns></returns>
        private List<PacificCycle> GetPacificCycleList()
        {
            string url;
            string username;
            string password;
            string textQtyFor100;

            new DAL().GetPacificCycleInfo(out url, out username, out password, out textQtyFor100);
            _instepQtyFor100 = Int32.Parse(textQtyFor100);

            log.Error("URL: " + url + "; username: " + username + "; password: " + password);
            //Get html as string
            _instepSiteHTML = GetWebsiteHTML(url, username, password);

            //Get XML document
            XmlDocument xmlDoc = CAUtil.CreateXMLDocFromHTML(_instepSiteHTML);

            List<PacificCycle> pacificCycleList = new List<PacificCycle>();

            //loop nodes and create Pacific Cycle List
            XmlNodeList entryElements = xmlDoc.SelectNodes("//tr");

            foreach (XmlElement element in entryElements)
            {
                XmlNodeList childNodes = element.SelectNodes("td");

                if (childNodes.Count > 0)
                {
                    PacificCycle pacificCycle = new PacificCycle();
                    pacificCycle.Material = childNodes[0].InnerText.Trim();
                    pacificCycle.Description = childNodes[1].InnerText.Trim();
                    pacificCycle.Plant = childNodes[2].InnerText.Trim();
                    pacificCycle.ATP = GetCorrectQty(childNodes[3].InnerText.Trim(), _instepQtyFor100);
                    pacificCycle.ATPDate = childNodes[4].InnerText.Trim();
                    pacificCycle.Size = childNodes[5].InnerText.Trim();
                    pacificCycle.Type = childNodes[6].InnerText.Trim();
                    pacificCycle.Brand = childNodes[7].InnerText.Trim();
                    //pacificCycle.ModelYear = childNodes[8].InnerText.Trim();

                    pacificCycleList.Add(pacificCycle);
                }//end if
            }//end foreach

            return pacificCycleList;
        }//end method

        private void AppendWithInlineToys(List<PacificCycle> list)
        {
            string url;
            string username;
            string password;
            string textQtyFor100;

            new DAL().GetPacificCycleInlineToysInfo(out url, out username, out password, out textQtyFor100);
            _inlineToysQtyFor100 = Int32.Parse(textQtyFor100);

            //Get html as string
            string siteHtml = GetWebsiteHTML(url, username, password);

            //Get XML document
            XmlDocument xmlDoc = CAUtil.CreateXMLDocFromHTML(siteHtml);

            //loop nodes and create Pacific Cycle List
            XmlNodeList entryElements = xmlDoc.SelectNodes("//tr");

            foreach (XmlElement element in entryElements)
            {
                XmlNodeList childNodes = element.SelectNodes("td");

                if (childNodes.Count > 0)
                {
                    PacificCycle pacificCycle = new PacificCycle();
                    pacificCycle.Material = childNodes[0].InnerText.Trim();
                    pacificCycle.Description = childNodes[1].InnerText.Trim();
                    pacificCycle.Plant = childNodes[2].InnerText.Trim();
                    pacificCycle.ATP = GetCorrectQty(childNodes[3].InnerText.Trim(), _inlineToysQtyFor100);
                    pacificCycle.ATPDate = childNodes[4].InnerText.Trim();
                    pacificCycle.Size = childNodes[5].InnerText.Trim();
                    pacificCycle.Type = childNodes[6].InnerText.Trim();
                    pacificCycle.Brand = childNodes[7].InnerText.Trim();
                    pacificCycle.ModelYear = "";

                    list.Add(pacificCycle);
                }
            }
        }

        /// <summary>
        /// Method to login to the website and get output as string 
        /// </summary>
        /// <returns></returns>
        private string GetWebsiteHTML(string url, string username, string password)
        {
            //string url = _url;//"http://dealers.pacific-cycle.com/instep/instep_inv.htm";

            //string userID = _username;//@"pacific-cycle\instepdealers";
            //string password = _password;//"stroller2";

            //create authentication string
            string base64EncodedAuthorizationString = username + ":" + password;
            byte[] binaryData = new Byte[base64EncodedAuthorizationString.Length];
            binaryData = Encoding.UTF8.GetBytes(base64EncodedAuthorizationString);
            base64EncodedAuthorizationString = Convert.ToBase64String(binaryData);
            base64EncodedAuthorizationString = "Basic " +
            base64EncodedAuthorizationString;


            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Headers.Add("Authorization", base64EncodedAuthorizationString);
            req.Method = "GET";

            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            string result = "";
            using (Stream responseStream = res.GetResponseStream())
            {
                using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    result = readStream.ReadToEnd();
                }
            }

            return result;
        }//end method

    }//end class

}//end namespace
