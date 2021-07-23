using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace ChannelAdvisor.Extractors
{
    public class CWRExtractor
    {
        private const string shortFieldsList = "upc,sku";
        private const string fullFieldsList = "ti,upc,sku,wt,mfgn,fdesc,price,list,imgpath,cat,mfg,wa,map";
        private const int maxSkusPerRequest = 50;

        private Vendor vendorInfo = Vendor.Load((int)VendorName.CWR);

        private IList<string> skus;
        private bool isAllSkus;
        private string outputFolder;
        private CWRExtractorSettings settings = CWRExtractorSettings.Load();

        public CWRExtractor(IList<string> skus, bool isAllSkus, string outputFolder)
        {
            this.skus = skus;
            this.isAllSkus = isAllSkus;
            this.outputFolder = outputFolder;
        }

        public void Extract()
        {
            OrderedDictionary skuUPC = GetSkusUPCs();

            bool finished = false;
            if (!isAllSkus)
            {
                // files indexer
                int i = 0;
                while ((i * settings.NumberOfProducts < skuUPC.Keys.Count) && (!finished))
                {
                    i++;
                    IList<XmlDocument> docs = new List<XmlDocument>();
                    // requests indexer
                    int k = 0;
                    while ((k * maxSkusPerRequest < i * settings.NumberOfProducts) && (!finished))
                    {
                        StringBuilder skusList = new StringBuilder();
                        // skus indexer
                        int j = (i - 1) * settings.NumberOfProducts + k * maxSkusPerRequest; ;
                        while ((j < (i - 1) * settings.NumberOfProducts + k * maxSkusPerRequest + maxSkusPerRequest) 
                            && (j < skus.Count))
                        {
                            if (skus[(i - 1) * settings.NumberOfProducts + j].Length > 5)
                            // UPC specified
                            {
                                skusList.Append(string.Format("{0},",
                                    GetKeyByValue(skuUPC, skus[(i - 1) * settings.NumberOfProducts + j])));
                            }
                            else
                            // SKU specified
                            {
                                skusList.Append(string.Format("{0},",
                                    skus[(i - 1) * settings.NumberOfProducts + j]));
                            }
                            j++;
                        }
                        if (j >= skus.Count) finished = true;
                        docs.Add(GetInventories(skusList.ToString()));
                        k++;
                    }

                    ExportToFiles(docs, i);
                    docs.Clear();
                }
            }
            else
            {
                // files indexer
                int i = 0;
                while ((i * settings.NumberOfProducts < skuUPC.Keys.Count) && (!finished))
                {
                    i++;
                    IList<XmlDocument> docs = new List<XmlDocument>();
                    // requests indexer
                    int k = 0;
                    while ((k * maxSkusPerRequest < settings.NumberOfProducts) && (!finished))
                    {
                        StringBuilder skusList = new StringBuilder();
                        // skus indexer
                        int j = (i - 1) * settings.NumberOfProducts + k * maxSkusPerRequest;
                        while ((j < (i - 1) * settings.NumberOfProducts + k * maxSkusPerRequest + maxSkusPerRequest) 
                            && (j < skuUPC.Keys.Count))
                        {
                            skusList.Append(string.Format("{0},", GetKeyByIndex(skuUPC, j)));
                            j++;
                        }
                        if (j >= skuUPC.Keys.Count) finished = true;
                        docs.Add(GetInventories(skusList.ToString()));
                        k++;
                    }

                    ExportToFiles(docs, i);
                    docs.Clear();
                }
            }

            
        }

        private void ExportToFiles(IList<XmlDocument> docs, int index)
        {
            IList<CWRExportRow> rows = new List<CWRExportRow>();
            rows.Add(CWRExportRow.GetHeaderRow());
            IList<CWRExportShippingRow> shippingRows = new List<CWRExportShippingRow>();
            shippingRows.Add(CWRExportShippingRow.GetHeaderRow());

            foreach (XmlDocument doc in docs)
            {
                XmlNodeList nodes = doc.SelectNodes("//product");
                foreach (XmlNode node in nodes)
                {
                    CWRExportRow row = new CWRExportRow();

                    string auctionTitle = node.SelectSingleNode("description").Attributes.GetNamedItem("title") != null ?
                        node.SelectSingleNode("description").Attributes.GetNamedItem("title").Value : string.Empty;
                    row.AuctionTitle = auctionTitle.Length > settings.AuctionTitleMaxLength ?
                        auctionTitle.Substring(0, settings.AuctionTitleMaxLength) : auctionTitle;
                    string upc = node.Attributes.GetNamedItem("upc") != null ? node.Attributes.GetNamedItem("upc").Value : string.Empty;
                    row.InventoryNumber = string.IsNullOrEmpty(upc) ?
                        (string.Format("{0}{1}", vendorInfo.SkuPrefix,
                        node.Attributes.GetNamedItem("cwr_sku") != null ? node.Attributes.GetNamedItem("cwr_sku").Value : string.Empty))
                        : upc;
                    row.Weight = node.SelectSingleNode("packages").Attributes.GetNamedItem("total_weight") != null ?
                        node.SelectSingleNode("packages").Attributes.GetNamedItem("total_weight").Value : string.Empty;
                    row.UPC = upc;
                    row.MPN = node.Attributes.GetNamedItem("mfg_part_number") != null ? 
                        node.Attributes.GetNamedItem("mfg_part_number").Value : string.Empty;
                    row.Description = node.SelectSingleNode("description").Attributes.GetNamedItem("full") != null ?
                        node.SelectSingleNode("description").Attributes.GetNamedItem("full").Value.Replace("\r\n", " ").Replace("\n", " ") : 
                        string.Empty;
                    row.SellerCost = node.SelectSingleNode("price").Attributes.GetNamedItem("your_price") != null ?
                        node.SelectSingleNode("price").Attributes.GetNamedItem("your_price").Value : 
                        string.Empty;
                    row.RetailPrice = node.SelectSingleNode("price").Attributes.GetNamedItem("list") != null ? 
                        node.SelectSingleNode("price").Attributes.GetNamedItem("list").Value : string.Empty;
                    if (node.SelectSingleNode("image") != null)
                        row.PictureURL = node.SelectSingleNode("image").Attributes.GetNamedItem("path") != null ?
                            node.SelectSingleNode("image").Attributes.GetNamedItem("path").Value : string.Empty;
                    row.SupplierCode = settings.SupplierCode;
                    row.WarehouseLocation = settings.WarehouseLocation;
                    row.DCCode = settings.DCCode;
                    row.ChannelAdvisorStoreTitle = auctionTitle.Length > settings.CAStoreTitleMaxLength ?
                        auctionTitle.Substring(0, settings.CAStoreTitleMaxLength) : auctionTitle;
                    row.ChannelAdvisorStoreDescription = node.SelectSingleNode("description").Attributes.GetNamedItem("full") != null ?
                        node.SelectSingleNode("description").Attributes.GetNamedItem("full").Value.Replace("\r\n", " ").Replace("\n", " ") : 
                        string.Empty;
                    string classification = string.Empty;
                    if (node.SelectNodes("categories/category").Count > 0)
                        classification = node.SelectNodes("categories/category")[0].Attributes.GetNamedItem("title") != null ?
                            node.SelectNodes("categories/category")[0].Attributes.GetNamedItem("title").Value : string.Empty;
                    row.Classification = classification.Length > settings.ClassificationMaxLength ?
                        classification.Substring(0, settings.ClassificationMaxLength) : classification;
                    row.Label = vendorInfo.Label;
                    row.Attribute1Name = "Brand";
                    row.Attribute1Value = node.SelectSingleNode("manufacturer").Attributes.GetNamedItem("name") != null ?
                        node.SelectSingleNode("manufacturer").Attributes.GetNamedItem("name").Value : string.Empty;
                    row.Attribute2Name = "Manufacturer Warranty";
                    string warranty = "N/A";
                    int years;
                    if (node.SelectSingleNode("warranty") != null)
                    {
                        if (node.SelectSingleNode("warranty").Attributes.GetNamedItem("years") != null)
                            if (int.TryParse(node.SelectSingleNode("warranty").Attributes.GetNamedItem("years").Value, out years))
                                switch (years)
                                {
                                    case 0:
                                        break;
                                    case 1:
                                        warranty = "1 year";
                                        break;
                                    default:
                                        warranty = string.Format("{0} years", years);
                                        break;
                                }
                    }
                    row.Attribute2Value = warranty;
                    row.Attribute3Name = "MAP";
                    row.Attribute3Value = node.SelectSingleNode("price").Attributes.GetNamedItem("map") != null ?
                        node.SelectSingleNode("price").Attributes.GetNamedItem("map").Value : string.Empty;
                    row.Attribute4Name = "cwr part number";
                    row.Attribute4Value = node.Attributes.GetNamedItem("cwr_sku") != null ? 
                        node.Attributes.GetNamedItem("cwr_sku").Value : string.Empty;
                    rows.Add(row);

                    CWRExportShippingRow shippingRow = new CWRExportShippingRow();
                    shippingRow.SKU = row.InventoryNumber;
                    shippingRow.Classification = row.Classification;
                    shippingRow.WarehouseLocation = settings.WarehouseLocation;
                    shippingRow.Weight = row.Weight;
                    shippingRows.Add(shippingRow);
                }
            }

            CWRExporter.ExportCWRDataToTextFile(rows, string.Format("{0}{1}{2}_{3}.txt",
                outputFolder, settings.OutputFilePrefix, DateTime.Now.ToString("MMddyyyy"), index));
            CWRExportShippingRow.ExportCWRShippingData(string.Format("{0}{1}{2}shipping_{3}.txt",
                outputFolder, settings.OutputFilePrefix, DateTime.Now.ToString("MMddyyyy"), index), shippingRows);
        }

        private OrderedDictionary GetSkusUPCs()
        {
            XmlDocument xml = ProcessRequest(shortFieldsList, string.Empty);
            OrderedDictionary result = new OrderedDictionary();
            XmlNodeList nodes = xml.SelectNodes("//product");

            foreach (XmlNode node in nodes)
            {
                result.Add(
                    node.Attributes.GetNamedItem("cwr_sku").Value,
                    node.Attributes.GetNamedItem("upc") == null ? string.Empty : node.Attributes.GetNamedItem("upc").Value);
            }

            return result;
        }

        private XmlDocument GetInventories(string skus)
        {
            return ProcessRequest(fullFieldsList, skus);
        }

        private XmlDocument ProcessRequest(string fields, string skus)
        {
            //System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);

            DAL dal = new DAL();
            PostSubmitter post = new PostSubmitter();
            post.Url = dal.GetSettingValue("CWR_URL");
            post.PostItems.Add("fields", fields);
            post.PostItems.Add("format", dal.GetSettingValue("CWR_Delim"));
            post.PostItems.Add("id", dal.GetSettingValue("CWR_ID"));
            post.PostItems.Add("time", dal.GetSettingValue("CWR_Time"));
            post.PostItems.Add("version", "2");
            if (!string.IsNullOrEmpty(skus))
                post.PostItems.Add("skus", skus);

            post.Type = PostSubmitter.PostTypeEnum.Get;
            string result = post.Post();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(new StringReader(result));

            return xmlDoc;
        }

        private string GetKeyByValue(OrderedDictionary dict, string value)
        {
            foreach(string key in dict.Keys)
            {
                if (value.Equals(dict[key]))
                    return key;
            }

            return string.Empty;
        }

        private string GetKeyByIndex(OrderedDictionary dict, int index)
        {
            int i = 0;
            foreach (DictionaryEntry entry in dict)
            {
                if (i == index) return entry.Key.ToString();
                i++;
            }

            return string.Empty;
        }
    }
}
