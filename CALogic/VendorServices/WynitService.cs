using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Globalization;

namespace ChannelAdvisor
{
    public class WynitService:IVendorService
    {
        public Vendor VendorInfo { get; set; }

        private string _ftpServer;
        private string _ftpUserName;
        private string _ftpPassword;
        private string _localFolder;

        /// <summary>
        /// Constructor
        /// </summary>
        public WynitService()
        {
            //Get wynit settings from database
            //_ftpServer = "ftp://ftp.wynit.info/";
            //_ftpUserName = "bargdelllc";
            //_ftpPassword = "3tgl23h";
            new DAL().GetWynitInfo(out _ftpServer, out _ftpUserName, out _ftpPassword);
            _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\","") + "\\Temp\\"; //"C:\\";

            VendorInfo = new DAL().GetVendor((int)VendorName.Wynit);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Wynit"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return CreateInvUpdateServiceDTOForWynit(false);
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
            InventoryUpdateServiceDTO invUpdateSrcvDTO = CreateInvUpdateServiceDTOForWynit(true);

            //Update Pricing Markups
            new InventoryUpdateService().UpdateMarkupPrice(VendorInfo.ID, profileID, invUpdateSrcvDTO);

            return invUpdateSrcvDTO;

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private InventoryUpdateServiceDTO CreateInvUpdateServiceDTOForWynit(bool isWinForm)
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            XmlDocument xmlInventory = new XmlDocument();
            //xmlInventory.Load("C:\\426445-832.xml"); //for debug only
            xmlInventory = GetInventoryXML(isWinForm); //To Un-Comment

            if (!isWinForm)
            {
                //Save xml file to vendor folder and assign to DTO
                string vendorFile = CAUtil.SaveXMLAsVendorFile(xmlInventory, VendorInfo.ID);
                invUpdateSrcvDTO.VendorFile = vendorFile;
            }


            BindingList<Inventory> lstEMGInventory = new BindingList<Inventory>();

            XmlNodeList nodes = xmlInventory.SelectNodes("inventory/data");

            InventoryUpdateService invService = new InventoryUpdateService();

            //loop through the xml nodes and create dto
            var ci = CultureInfo.InvariantCulture;            
            foreach (XmlNode node in nodes)
            {
                Inventory invDTO = new Inventory();
                invDTO.UPC = node["upc"].InnerXml;
                invDTO.SKU = string.Format("{0}{1}", VendorInfo.SkuPrefix, node["partnum"].InnerXml);
                invDTO.Qty = Convert.ToInt32(node["qtyavail"].InnerXml);
                invDTO.Price = float.Parse(node["cost"].InnerXml, ci);
                invDTO.RetailPrice = float.Parse(node["msrp"].InnerXml, ci);
                invDTO.Description = node["partdescript"].InnerXml;

                lstEMGInventory.Add(invDTO);
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
        /// <param name="isWinForm"></param>
        /// <returns></returns>
        public XmlDocument GetInventoryXML(bool isWinForm)
        {
            FTP ftp = new FTP(_ftpServer, _ftpUserName, _ftpPassword);

            //Get list of files
            List<string> files = ftp.GetFiles();

            //download file
            if(files.Count>0)
            {
                ftp.DownloadFile(files[0], _localFolder);    
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Inventory file for Wynit not found on FTP server");
            }//end if

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_localFolder + files[0]);
            // Add SKU prefix
            XmlNodeList lNodeList = xmlDoc.SelectNodes("inventory/data/partnum");
            foreach (XmlNode lNode in lNodeList)
                lNode.InnerXml = lNode.InnerXml;

            return xmlDoc;

        }//end method

        /// <summary>
        /// Create tab delimeted text file for Wynit inventary data
        /// </summary>
        /// <param name="aFolderName">Folder for saving output file</param>
        /// <param name="aFileNamePrefix">Prefix of file name for output file</param>
        public void GetWynitBasicData(string aFolderName, string aFileNamePrefix, int aMaxUPCLength, string aSupplierCode,
                string aWarehouseLocation, string aDCCode)
        {
            string lFileName = aFolderName + aFileNamePrefix + DateTime.Now.ToString("MMddyyyy") + ".txt";
            XmlDocument lXmlInventory = GetInventoryXML(true);

            XmlNodeList lNodes = lXmlInventory.SelectNodes("inventory/data");
            List<WynitExportRow> lList = new List<WynitExportRow>();
            lList.Add(WynitExportRow.CreateHeader());

            foreach (XmlNode lNode in lNodes)
            {
                WynitExportRow lRow = new WynitExportRow();
                lRow.InventoryNumber = VendorInfo.SkuPrefix + lNode["partnum"].InnerXml;
                lRow.MPN = lNode["stdpartnum"].InnerXml;
                lRow.ShortDesription = lNode["partdescript"].InnerXml;
                string lUPC = StringUtils.RemoveNonNumericCharacters(lNode["upc"].InnerXml);
                while (lUPC.Length < 12) lUPC = "0" + lUPC;
                if (lUPC.Length == 13) lUPC = "0" + lUPC;
                lRow.UPC = lUPC.Length > aMaxUPCLength ? lUPC.Substring(0, aMaxUPCLength) : lUPC;
                lRow.RetailPrice = lNode["msrp"].InnerXml;
                lRow.Weight = lNode["weight"].InnerXml;
                lRow.SupplierCode = aSupplierCode;
                lRow.WarehouseLocation = aWarehouseLocation;
                lRow.DCCode = aDCCode;

                lList.Add(lRow);
            }

            WynitExporter.ExportWynitDataToTextFile(lList, lFileName);
        }

    }//end class

}//end namespace
