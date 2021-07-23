using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.ComponentModel;
using System.Net;
using System.Linq;
using ChannelAdvisor.VendorServices;
using HtmlAgilityPack;

namespace ChannelAdvisor
{
    public class PetGearService:IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private Int32 inStockValue = 0;
        string _instepSiteHTML = "";

        /// <summary>
        /// constructor
        /// </summary>
        public PetGearService()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            DAL dal = new DAL();
            VendorInfo = dal.GetVendor((int)VendorName.PetGear);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Pet Gear"));
            string ss =dal.GetSettingValue("PetGear_InStockValue");
            inStockValue = string.IsNullOrWhiteSpace(ss) ? 15: Convert.ToInt32(ss);
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForPetGear(false);
        }//end method

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summar
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            //Create Inventory Update Service DTO
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForPetGear(true);

            ////Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForPetGear(bool isWinForm)
        {
            //check cache to read linnworks catalog
            List<StockItem> cache_catalog;
            InventoryCache.LoadCaches(out cache_catalog);

            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            //Read files and create list of SKUs and their Price
            List<PetGear> petGearList1 = GetPetGear();

            //Get html as string
            string _priceHtml = GetWebsiteHTML("http://www.petgearinc.com/Price_List.asp");
            List<PetGear> lstPrice = ParceHTML(_priceHtml);

            // get left outer join result
            List<PetGear> petGearList = (from l1 in petGearList1
                                             join l2 in lstPrice on l1.ItemNumber  equals l2.ItemNumber into gj
                      from l2_left in gj.DefaultIfEmpty()
                      select new PetGear
                      {
                          ItemNumber = l1.ItemNumber,
                          ItemNumberLeft = l2_left == null ? String.Empty : l2_left.ItemNumber,
                          Price = l2_left == null ? null : l2_left.Price,
                          Description = l1.Description,
                          DiscontinuedOn = l1.DiscontinuedOn,
                          LastUpdate = l1.LastUpdate,
                          EstRestockDate = l1.EstRestockDate
                      }).ToList<PetGear>();

            // get left outer join result
            List<PetGear> priceListToAdd = (from l1 in lstPrice
                                            join l2 in petGearList on l1.ItemNumber equals l2.ItemNumber into gj
                                            from l2_left in gj.DefaultIfEmpty()
                                            where l2_left == null
                                            select new PetGear
                                            {
                                                ItemNumber = l1.ItemNumber,
                                                ItemNumberLeft = l2_left == null ? String.Empty : l2_left.ItemNumber,
                                                Price = l1.Price
                                            }).ToList<PetGear>();


            //Save html file to archive
            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveHTMLFileAsVendorFile(_instepSiteHTML, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }

            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>(); 

            InventoryUpdateService invService = new InventoryUpdateService();
            Inventory invDTO;
            //loop through the Generic Price List and create dto
            for (int x = 0; x < petGearList.Count; x++)
            {
                invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, petGearList[x].ItemNumber.Trim());

                //invDTO.Qty = petGearList[x].;
                invDTO.Price = petGearList[x].Price == null ? null : petGearList[x].Price;
                invDTO.RetailPrice = null;
                invDTO.MAP = 0;
                invDTO.Description = petGearList[x].Description.Trim();

                if (petGearList[x].DiscontinuedOn.ToLower().Contains("out of stock") || petGearList[x].DiscontinuedOn.ToLower().Contains("discontinued"))
                    invDTO.Qty = 0;
                else if (petGearList[x].DiscontinuedOn.ToLower().Contains("in stock"))
                {
                    if (IsExist(cache_catalog, invDTO.SKU) && string.IsNullOrEmpty(petGearList[x].ItemNumberLeft))
                        invDTO.Qty = 0;
                    else
                        invDTO.Qty = IsExist(cache_catalog, invDTO.SKU) ? inStockValue : 100;
                }
                //if sku exists update or add
                AddInventoryOrUpdate(lstEMGInventory, invDTO);
            }
                //loop through the  Price List that doesn't exists in cache
                for (int x = 0; x < priceListToAdd.Count; x++)
                {
                    invDTO = new Inventory();
                    invDTO.UPC = "";
                    invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, priceListToAdd[x].ItemNumber.Trim());
                    invDTO.Price = priceListToAdd[x].Price;
                    invDTO.Qty = IsExist(cache_catalog, invDTO.SKU) ? inStockValue : 100;

                    //if sku exists update or add
                    AddInventoryOrUpdate(lstEMGInventory, invDTO);
                }
            
            //  loop cache

            //filter cache where sku is not in feed list
            List<StockItem> filteredCache = (from l1 in cache_catalog
                                      join l2 in lstEMGInventory on l1.SKU equals l2.SKU into gj
                                      from l2_left in gj.DefaultIfEmpty()
                                             where l2_left == null && l1.SKU.StartsWith("pgi-") && l1.Category.ToLower()  == "pet gear"
                                             select new StockItem
                                      {
                                          SKU = l1.SKU,
                                          Title = l1.Title,
                                          Category = l1.Category
                                      }).ToList<StockItem>();

            for (int x = 0; x < filteredCache.Count; x++)
            {
                invDTO = new Inventory();
                invDTO.UPC = "";
                invDTO.SKU = filteredCache[x].SKU;

                invDTO.MAP = 0;
                invDTO.Description = filteredCache[x].Title;
                //# set quantity =0 because the item is not in the Price List.
                invDTO.Qty = 0;  //inStockValue;
                lstEMGInventory.Add(invDTO);

            }//end for each
///
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
        /// Method to scrape pet gear and get the list
        /// </summary>
        /// <returns></returns>
        private List<PetGear> GetPetGear()
        {
            string url;       

            new DAL().GetPetGearInfo(out url);
            
            //Get html as string
            _instepSiteHTML = GetWebsiteHTML(url);
            //Get XML document
            XmlDocument xmlDoc = CAUtil.CreateXMLDocFromHTML(_instepSiteHTML.Replace("<b>","").Replace("</b>",""));
            List<PetGear> petgearList = new List<PetGear>();   

            //loop nodes and create Pacific Cycle List
            XmlNodeList entryElements = xmlDoc.SelectNodes("//tr");
            PetGear petgear;
            foreach (XmlElement element in entryElements)
            {
                XmlNodeList childNodes = element.SelectNodes("td");

                if (childNodes.Count > 0 && childNodes[0].InnerText.Trim() != "Item Number")
                {
                    petgear = new PetGear();
                    petgear.ItemNumber= childNodes[0].InnerText.Trim();
                    petgear.Description = childNodes[1].InnerText.Trim();
                    petgear.LastUpdate = childNodes[2].InnerText.Trim();
                    petgear.EstRestockDate = childNodes[3].InnerText.Trim();
                    petgear.DiscontinuedOn = childNodes[4].InnerText.Trim();
                    petgearList.Add(petgear);
                }//end if
            }//end foreach

            return petgearList;
        }//end method

   
        /// <summary>
        /// Method to login to the website and get output as string 
        /// </summary>
        /// <returns></returns>
        private string GetWebsiteHTML(string url)
        {
            //string url = _url;//"http://dealers.pacific-cycle.com/instep/instep_inv.htm";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //req.Headers.Add("Authorization", base64EncodedAuthorizationString);
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

        /// <summary>
        /// Method to parce HTML
        /// </summary>
        /// <returns></returns>
        private List<PetGear> ParceHTML(string urlSource)
        {
            string result = "";

            HtmlAgilityPack.HtmlDocument htmldoc = new HtmlAgilityPack.HtmlDocument();
            htmldoc.LoadHtml(urlSource);
            var trnodes = htmldoc.DocumentNode.SelectNodes("//table[@style='width: 200px']/tr");
            string sku;
            string price;
            List<PetGear> priceList = new List<PetGear>();
            PetGear priceNode;
            if (trnodes != null)
            {
                HtmlNodeCollection cols;
                Int32  count = 0;
                foreach (HtmlNode node in trnodes)
                {
                    if (count==2)
                    {
                        cols = node.SelectNodes(".//td");
                        priceNode  = new ChannelAdvisor.PetGear();

                        priceNode.ItemNumber  = cols[0].InnerText.Trim();
                        priceNode.Price  = Convert.ToSingle(cols[1].InnerText.Trim().Replace("$", "") );
                        priceList.Add(priceNode);
                        //foreach (HtmlNode td in cols)
                        //    result += td.InnerText.Replace("&nbsp;", " ").Trim() + ";";
                        //result += "\n";
                    }
                    else count++;
                }
            }
            return priceList;
        }//end method

        /// <summary>
        /// Method to check if string is in date format?
        /// </summary>
        /// <returns>true/false</returns>

        private bool IsDateFormat(string str)
        {
            //var date_regex = @"^(([1-9])|(0[1-9])|(1[0-2]))\/(([1-9])|(0[1-9])|(1\d)|(2\d)|(3[01]))\/(\d\d)$";
            var date_regex = @"^(([1-9])|(0[1-9])|(1[0-2]))\/(([1-9])|(0[1-9])|(1\d)|(2\d)|(3[01]))\/((\d\d)||((19|20)\d\d))$";
           
            str = str.Replace(".", "/").Replace("-", "/");
            if (System.Text.RegularExpressions.Regex.IsMatch(str, date_regex))
                return true;//MessageBox.Show("Yse");
            else
                return false; //MessageBox.Show("No"); //s = "no";

        }

        /// <summary>
        /// Method to check if exist SKU in the cache
        /// </summary>
        /// <returns>true/false</returns>
        /// 
        public Boolean IsExist(List<StockItem> list,string sku)
        {
            var count = (from item in list
                         where item.SKU == sku
                         select item).Count();
            if (count > 0)
                return true;
            else
                return false;
        }

        public Boolean IsExistInPriceList(List<PetGear> list, string sku)
        {
            var count = (from item in list
                         where item.ItemNumberLeft  == sku
                         select item).Count();
            if (count > 0)
                return true;
            else
                return false;
        }

    }//end classp

}//end namespace
