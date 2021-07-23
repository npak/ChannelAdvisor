using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.IO;

namespace ChannelAdvisor
{
    public class MorrisNightlyService : IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _url = "";//"https://shop.cwrelectronics.com/feeds/productdownload.php";
        private string _fields = "";//"upc,sku,qty,price,list,map,mrp,sdesc";
        private string _delim = "";//"xml";
        private string _id = "";//"MPB_MzU4NzEzMzU4NzEzMzQ0";
        private string _time = "";//"1174276800";
        private string _urlCount = "";
        private string _dropshipfree = "";
        string csvfolder = "";
        string csvfilename = "";
        string csvIsftp = "";
        /// <summary>
        /// 
        /// </summary>
        public MorrisNightlyService()
        {
            new DAL().GetMorrisNightlySettings(out _url, out _dropshipfree, out csvfolder, out csvfilename, out csvIsftp);

            VendorInfo = new DAL().GetVendor((int)VendorName.MorrisNightly);

            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Morris Nightly"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForMorris(false);
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
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForMorris(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForMorris(bool isWinForm)
        {
            //string UDT = "";
            string date = "";
            string type = "";
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            InventoryUpdateService invService = new InventoryUpdateService();

            XmlDocument xmlInventory = new XmlDocument();
            
            BindingList<Inventory> lstMorrisNightlyInventory = new BindingList<Inventory>();
            XmlNodeList nodes;
            Inventory invDTO;
            

            // get count
            // get common information
                xmlInventory = GetInventoryXML(1); 
                if (xmlInventory == null)
                {
                    return null; ;
                }
                XmlNode rootnodes = xmlInventory.SelectSingleNode("//AvailableBatch"); // get all <AvailableBatch> nodes
                int XMLFileCount = 0;
                //nodes = new List<MorrisExportRow>();
                foreach (XmlNode xmlnode in rootnodes.ChildNodes)
                {
                    if (xmlnode.Name == "of")
                    {
                        XMLFileCount = Convert.ToInt32(xmlnode.InnerText);
                        break;
                    }
                }

                XMLFileCount++;

            //test
            //    XMLFileCount = 2;

            // loop in url count
            for (int i = 1; i < XMLFileCount; i++)
            {
                if (WaitDialogWithWork.Current != null)
                    WaitDialogWithWork.Current.ShowMessage("Fetching data from " + _url.Replace("001", i.ToString("000")) + ", please wait...");

                xmlInventory = GetInventoryXML(i); 
                
                if (!isWinForm)
                {
                    System.Diagnostics.Debug.WriteLine("Trying to save vendor file for CWR...");
                    //Save xml file to vendor folder and assign to DTO
                    string vendorFile = CAUtil.SaveXMLAsVendorFile(xmlInventory, (int)VendorName.MorrisNightly);
                    invUpdateSrcvDTO.VendorFile = vendorFile;
                }

                nodes = xmlInventory.SelectNodes("//Available"); // get all <Available> nodes
                //float fee = 0;
                //if (!string.IsNullOrWhiteSpace(_dropshipfree))
                //    fee = Convert.ToSingle(_dropshipfree); //float.Parse(_dropshipfree);

                //loop through the xml nodes and create dto
                foreach (XmlNode node in nodes)
                {
                    invDTO = new Inventory();
                    //invDTO.Price =fee; //float.Parse(_dropshipfree);

                    //This is confusing, but we use their "Part" column B as our sku instead of column A..
                    invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, node["Part"].InnerXml);

                    //check for blank
                    if (node["Qty"].InnerXml.Trim() != "")
                    {
                        invDTO.Qty = Convert.ToInt32(node["Qty"].InnerXml);
                    }
                    else
                    {
                        invDTO.Qty = 0;
                    }
                    //XmlNode detail = node.SelectSingleNode("Detail");
                    //if (detail["Price"] == null)
                    //    invDTO.Price = null;
                    //else
                    //{
                    //    invDTO.Price = invDTO.Price + float.Parse(detail["Price"].InnerText.Trim());
                    //    invDTO.Price = (float)(Math.Round((double)invDTO.Price, 2));
                    //}

                    invDTO.RetailPrice = null;
                    invDTO.MAP = 0;

                    lstMorrisNightlyInventory.Add(invDTO);
                }//end for each
            }

            //Create InventoryDTO list with valid skus and add duplicate and blank skus to Error Log
            invService.CreateInventoryDTOListOfValidSKUs(VendorInfo.ID,
                                                            lstMorrisNightlyInventory,
                                                            invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        private XmlDocument GetInventoryXML(int i)
        {
            try
            {
                XmlDocument xd = new XmlDocument();
                xd.Load(_url.Replace("001", i.ToString("000")));
                //"http://morris.morriscostumes.com/out/available_batchnynyy_001.xml");
                return xd;
            }
            catch
            {
                return null;
            }
          
            
        }//end method

    }
}//end namespace
