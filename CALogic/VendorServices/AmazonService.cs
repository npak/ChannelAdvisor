/*******************************************************************************
 * Copyright 2009-2017 Amazon Services. All Rights Reserved.
 * Licensed under the Apache License, Version 2.0 (the "License"); 
 *
 * You may not use this file except in compliance with the License. 
 * You may obtain a copy of the License at: http://aws.amazon.com/apache2.0
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the 
 * specific language governing permissions and limitations under the License.
 *******************************************************************************
 * Marketplace Web Service Products
 * API Version: 2011-10-01
 * Library Version: 2017-03-22
 * Generated: Wed Mar 22 23:24:36 UTC 2017
 */

using MarketplaceWebServiceProducts.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using ChannelAdvisor.Objects;
using System.Net;
using System.IO.Compression;
using System.Text;
using System.Configuration;
using log4net;
using ChannelAdvisor;

////using System.IO;
////using System.Net;
//using System.Xml.XPath;
//using System.Xml;
//using log4net;
//using System.Threading;

namespace MarketplaceWebServiceProducts
{

    /// <summary>
    /// Runnable sample code to demonstrate usage of the C# client.
    ///
    /// To use, import the client source as a console application,
    /// and mark this class as the startup object. Then, replace
    /// parameters below with sensible values and run.
    /// </summary>
    public class AmazonMarketplaceWebServiceProducts
    {
        private List<MwsResult> _otputList = new List<MwsResult>();
        public List<MwsResult> OutputList
        {
                get
                {
                    return this._otputList;
                }
                set
                {
                    this._otputList = value;
                }
        }

        public readonly ILog log = LogManager.GetLogger(typeof(AmazonMarketplaceWebServiceProducts));

        // ftp 
        private string localFolder;
        private string inputFilename;

        string inputFilePath = "";
        string outputFilePath = "";

        string _sellerId = "A3HN5BTMBU8KF9";
        string _mwsAuthToken = "example";
        string _marketplaceId = "";
        string _idType = "";

        private readonly MarketplaceWebServiceProducts client;

        public AmazonMarketplaceWebServiceProducts(MarketplaceWebServiceProducts client)
        {
            this.client = client;
        }

        public AmazonMarketplaceWebServiceProducts(string marketplaceId,string type)
        {
            _marketplaceId = marketplaceId;
            _idType = type; 
            InitProperties();
        }

        public List<MwsResult> GetData()
        {
            // TODO: Set the below configuration variables before attempting to run

            // Developer AWS access key
            string accessKey = "AKIAIVUPPT4KIQ2CLN2A";

            // Developer AWS secret key
            string secretKey = "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";

            // The client application name
            string appName = "CSharpSampleCode";

            // The client application version
            string appVersion = "1.0";

            // The endpoint for region service and version (see developer guide)
            // ex: https://mws.amazonservices.com
            string serviceURL = "https://mws.amazonservices.com";

            // Create a configuration object
            MarketplaceWebServiceProductsConfig config = new MarketplaceWebServiceProductsConfig();
            config.ServiceURL = serviceURL;

            // Set other client connection configurations here if needed
            // Create the client itself
            MarketplaceWebServiceProducts client = new MarketplaceWebServiceProductsClient(appName, appVersion, accessKey, secretKey, config);

            AmazonMarketplaceWebServiceProducts sample = new AmazonMarketplaceWebServiceProducts(client);
            sample._marketplaceId = this._marketplaceId;
            sample._idType = this._idType;
 
            try
            {
                // get ftp date
                string filepath = GetFTPData();

                // parce text file with UPCs
                Encoding encoding = System.Text.Encoding.GetEncoding("UTF-8");
                StreamReader sr = new StreamReader(filepath, encoding);

                // read headr 'UPC'
                string strheader = sr.ReadLine();

                IdListType idList = new IdListType();

                // declare variables 
                List<MwsResult> outputList = new List<MwsResult>();
                MwsResult newItem;
                
                string VariationChildString = "";
                string newSalesRankString ="";
                
                string attributeSets;
                string relationships;
                string salesRankings;

                GetMatchingProductForIdResponse response;
                List<GetMatchingProductForIdResult> listOfResults;

                XmlDocument doc = new XmlDocument();
                XmlNamespaceManager nsmgr;
                XmlNode attribute;
                XmlNodeList variationChildNodes;
                XmlNodeList salesRankNodes;

                 int cnt = 0;
                string temp = "";
                List<string> listOfUPC = new List<string>(); 

                // create list of  UPC
                while (!sr.EndOfStream )
                {
                    temp = sr.ReadLine();
                    if (temp.Length > 0)
                    {
                        listOfUPC.Add(temp);
                    }
                }

                // remove dublicates
                listOfUPC = listOfUPC.Distinct().ToList();

                for (int i = 0; i < listOfUPC.Count; i += 5)
                {
                    //cnt = 0;
                    idList = new IdListType();
                    for (int j = 0; j < 5; j++)
                    {
                        cnt = i + j;
                        if (cnt < listOfUPC.Count)
                            idList.Id.Add(listOfUPC[cnt]);
                        else
                            break;
                    }

                    if (idList.Id.Count > 0)
                    {
                        System.Threading.Thread.Sleep(1000);  
                        response = sample.InvokeGetMatchingProductForId(idList);

                        // get list of results by upc 
                        listOfResults = response.GetMatchingProductForIdResult;

                        foreach (GetMatchingProductForIdResult result in listOfResults)
                        {
                            // check error
                            if (result.status == "Success")
                            {
                                foreach (Product product in result.Products.Product)
                                {
                                    newItem = new MwsResult();
                                    newItem.UPC = result.Id;
                                    newItem.ASIN = product.Identifiers.MarketplaceASIN.ASIN;
                                    // get Product attributed
                                    attributeSets = product.AttributeSets.ToXML();

                                    doc.LoadXml(attributeSets);

                                    // create a new name manager
                                    nsmgr = new XmlNamespaceManager(doc.NameTable);
                                    nsmgr.AddNamespace("ns2", "http://mws.amazonservices.com/schema/Products/2011-10-01/default.xsd");

                                    //get Brand
                                    attribute = doc.SelectSingleNode("//ns2:Brand", nsmgr);
                                    if (attribute != null)
                                        newItem.Brand = attribute.InnerText;
                                    //get Color
                                    attribute = doc.SelectSingleNode("//ns2:Color", nsmgr);
                                    if (attribute != null)
                                        newItem.Color = attribute.InnerText;
                                    //get ItemPartNumber
                                    attribute = doc.SelectSingleNode("//ns2:ItemPartNumber", nsmgr);
                                    if (attribute != null)
                                        newItem.ItemPartNumber = attribute.InnerText;
                                    //get Manufacturer
                                    attribute = doc.SelectSingleNode("//ns2:Manufacturer", nsmgr);
                                    if (attribute != null)
                                        newItem.Manufacturer = attribute.InnerText;

                                    //get Model
                                    attribute = doc.SelectSingleNode("//ns2:Model", nsmgr);
                                    if (attribute != null)
                                        newItem.Model = attribute.InnerText;
                                    //get NumberOfItems
                                    attribute = doc.SelectSingleNode("//ns2:NumberOfItems", nsmgr);
                                    if (attribute != null)
                                        newItem.NumberOfItems = attribute.InnerText;
                                    //get PackageQuantity
                                    attribute = doc.SelectSingleNode("//ns2:PackageQuantity", nsmgr);
                                    if (attribute != null)
                                        newItem.PackageQuantity = attribute.InnerText;
                                    //get PartNumber
                                    attribute = doc.SelectSingleNode("//ns2:PartNumber", nsmgr);
                                    if (attribute != null)
                                        newItem.PartNumber = attribute.InnerText;

                                    //get Size
                                    attribute = doc.SelectSingleNode("//ns2:Size", nsmgr);
                                    if (attribute != null)
                                        newItem.Size = attribute.InnerText;
                                    // get Title
                                    attribute = doc.SelectSingleNode("//ns2:Title", nsmgr);
                                    if (attribute != null)
                                        newItem.Title = attribute.InnerText;

                                    //get Binding
                                    attribute = doc.SelectSingleNode("//ns2:Binding", nsmgr);
                                    if (attribute != null)
                                        newItem.Binding = attribute.InnerText;
                                    //get ProductGroup
                                    attribute = doc.SelectSingleNode("//ns2:ProductGroup", nsmgr);
                                    if (attribute != null)
                                        newItem.ProductGroup = attribute.InnerText;
                                    //get ProductTypeName
                                    attribute = doc.SelectSingleNode("//ns2:ProductTypeName", nsmgr);
                                    if (attribute != null)
                                        newItem.ProductTypeName = attribute.InnerText;

                                    //get ProductTypeSubcategory
                                    attribute = doc.SelectSingleNode("//ns2:ProductTypeSubcategory", nsmgr);
                                    if (attribute != null)
                                        newItem.ProductTypeSubcategory = attribute.InnerText;

                                    // get SmallImage URL
                                    attribute = doc.SelectSingleNode("//ns2:SmallImage/ns2:URL", nsmgr);
                                    if (attribute != null)
                                        newItem.SmallImageURL = attribute.InnerText;

                                    // get price
                                    attribute = doc.SelectSingleNode("//ns2:ListPrice/ns2:Amount", nsmgr);
                                    if (attribute != null)
                                        newItem.Price = attribute.InnerText;

                                    // Parse Variation Child

                                    relationships = product.Relationships.ToXML();

                                    ////test
                                    //doc.Load("E:\\papa\\2-15-15\\ChannelAdvisorSources - work\\output\\XMLFile1.xml");
                                    doc.LoadXml(relationships);

                                    variationChildNodes = doc.GetElementsByTagName("ns2:VariationChild");
                                    if (variationChildNodes.Count > 0)
                                    {
                                        foreach (XmlNode node in variationChildNodes)
                                        {
                                            VariationChildString = "";
                                            attribute = node.SelectSingleNode("//ns2:Color", nsmgr);
                                            if (attribute != null)
                                                VariationChildString += "Color:" + attribute.InnerText + ",";

                                            attribute = node.SelectSingleNode("//ns2:Model", nsmgr);
                                            if (attribute != null)
                                                VariationChildString += "Model:" + attribute.InnerText + ",";

                                            attribute = node.SelectSingleNode("//ns2:PackageQuantity", nsmgr);
                                            if (attribute != null)
                                                VariationChildString += "PackageQuantity:" + attribute.InnerText + ",";

                                            attribute = node.SelectSingleNode("//ns2:Size", nsmgr);
                                            if (attribute != null)
                                                VariationChildString += "Size:" + attribute.InnerText + ",";

                                            // remove the last ',' 
                                            if (VariationChildString.Length > 0)
                                                newItem.VariationChildsString += VariationChildString.Substring(0, VariationChildString.Length - 1) + ";";
                                        }

                                        // remove the last ';'
                                        if (newItem.VariationChildsString.Length > 0)
                                            newItem.VariationChildsString = newItem.VariationChildsString.Substring(0, newItem.VariationChildsString.Length - 1);
                                    }

                                    // hide for now
                                    //// SalesRankings 

                                    //salesRankings = product.SalesRankings.ToXML();

                                    //////test
                                    ////doc.Load("E:\\papa\\2-15-15\\ChannelAdvisorSources - work\\output\\XMLFile1.xml");
                                    //doc.LoadXml(salesRankings);

                                    //salesRankNodes = doc.GetElementsByTagName("SalesRank");
                                    //if (salesRankNodes.Count > 0)
                                    //{
                                    //    foreach (XmlNode node in salesRankNodes)
                                    //    {

                                    //        newSalesRankString = "";

                                    //        attribute = node.FirstChild;  //.SelectSingleNode("//SalesRank/ProductCategoryId");
                                    //        if (attribute != null)
                                    //            newSalesRankString += "ProductCategoryId:" + attribute.InnerText + ",";

                                    //        attribute = node.LastChild; //  .SelectSingleNode("//Rank");
                                    //        if (attribute != null)
                                    //            newSalesRankString += "Rank:" + attribute.InnerText + ";";

                                    //        if (newSalesRankString.Length > 0)
                                    //            newItem.SalesRankingsString += newSalesRankString;
                                    //    }
                                    //    // remove the last ';'
                                    //    if (newSalesRankString.Length > 0)
                                    //        newItem.SalesRankingsString = newItem.SalesRankingsString.Substring(0, newItem.SalesRankingsString.Length - 1);

                                    //}


                                    // add new item into output list
                                    outputList.Add(newItem);

                                }
                            }
                        }
                    }
                //test b = false;
                }
                this.OutputList = outputList; 
                return outputList;
            }
            catch (MarketplaceWebServiceProductsException ex)
            {
                ResponseHeaderMetadata rhmd = ex.ResponseHeaderMetadata;
                Console.WriteLine("Service Exception:");
                if (rhmd != null)
                {
                    log.Error("RequestId: " + rhmd.RequestId);
                }
                log.Error("Message: " + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Convert list of objects to text file with tab delimited and Upload on FTp
        /// </summary>
        /// <param name="list"> List of object (MwsResult type)</param>
        /// <param name="csvName">name of the file</param>
        /// <returns>is succes bool </returns>
        public bool UploadAmazonFile()
        {
            try
            {
                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();
         //       ASIN, Brand, Color, ItemPartNumber, Manufacturer,                                             Model, NumberOfItems, PackageQuantity,                                                                                                      PartNumber, Size, Title, Binding, ProductGroup, ProductTypeName, ProductTypeSubcategory
                var sb = new StringBuilder();
                var delimiter = "\t";
                sb.AppendLine("UPC" + delimiter + "ASIN" + delimiter + "Brand" + delimiter + "Color" + delimiter + "ItemPartNumber" + delimiter + "Manufacturer" + delimiter + "Model" + delimiter + "NumberOfItems" + delimiter + "PackageQuantity" + delimiter + "PartNumber" + delimiter + "Size" + delimiter + "Title" + delimiter + "Binding" + delimiter + "ProductGroup" + delimiter + "ProductTypeName" + delimiter + "ProductTypeSubcategory" + delimiter + "SmallImageURL" + delimiter + "VariationChildsString" );

                foreach (var data in this.OutputList)
                {
                    sb.AppendLine(data.UPC + delimiter + data.ASIN + delimiter + data.Brand + delimiter + data.Color + delimiter + data.ItemPartNumber + delimiter + data.Manufacturer + delimiter + data.Model + delimiter + data.NumberOfItems + delimiter + data.PackageQuantity + delimiter + data.PartNumber + delimiter + data.Size + delimiter + data.Title + delimiter + data.Binding + delimiter + data.ProductGroup + delimiter + data.ProductTypeName + delimiter + data.ProductTypeSubcategory + delimiter + data.SmallImageURL + delimiter + data.VariationChildsString );
                }

                byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
                WebRequest request = WebRequest.Create("ftp://" + ftpAddress + this.outputFilePath); // "AZ_UpdatedColumns.csv");
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(username, password);
                Stream reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();
                return true;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                return false;
            }
        }

        public GetMatchingProductForIdResponse InvokeGetMatchingProductForId(IdListType idList)
        {
            // Create a request.
            GetMatchingProductForIdRequest request = new GetMatchingProductForIdRequest();
            request.SellerId = _sellerId;
            request.MWSAuthToken = _mwsAuthToken;
            request.MarketplaceId = _marketplaceId;
            request.IdType = _idType;

            request.IdList = idList;
            return this.client.GetMatchingProductForId(request);
        }

        ///// <summary>
        ///// Method to Read csv file from 
        ///// </summary>
        private string GetFTPData()
        {
            string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
            string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
            string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

            var location = System.Reflection.Assembly.GetEntryAssembly().Location;
            localFolder = Path.GetDirectoryName(location) + "\\Temp";

            FTP ftp = new FTP("ftp://" + ftpAddress , username, password, true);

            // download csv file
            ftp.DownloadFile(inputFilePath,  inputFilename, localFolder); //(string fileNameFrom, string fileNameTo, string folderPath)

            return string.Format(@"{0}{1}", localFolder + "\\" , inputFilename);
        }

        ///// <summary>
        ///// Convert list of objects to csv and Upload on FTp
        ///// </summary>
        ///// <param name="list"> List of object (RateToOutput type)</param>
        ///// <param name="csvName">name of csv file</param>
        ///// <returns>string in Json format</returns>
        //public bool UploadRateCsvFile(List<RateToOutput> list, string outputFileName)
        //{
        //    try
        //    {
        //        string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
        //        string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
        //        string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

        //        var sb = new StringBuilder();
        //        sb.AppendLine("SKU,PurchasePrice ,Existing Domestic Shipping,USPS Priority Mail,Priority Mail Insurance,USPS Parcel Select, Parcel Select Insurance, USPS First Class, First Class Insurance,UPS Ground,Ground insurance,UPS Next Day Air Saver,Next Day Air Saver Insurance,Best Service,Best Rate");

        //        foreach (var data in list)
        //        {
        //            sb.AppendLine(data.sku + "," + data.purchasePrice + "," + data.existingDomesticShiping + "," + data.USPSPriorityMail + "," + data.PriorityMailinsurance + "," + data.USPSParcelSelect + "," + data.ParcelSelectinsurance + "," + data.USPSFirstClassMail + "," + data.FirstClassMailinsurance + "," + data.UPSGround + "," + data.Groundinsurance + "," + data.UPSNextDayAirSaver + "," + data.NextDayAirSaverinsurance + "," + data.BestService + "," + data.BestRate);
        //        }

        //        byte[] buffer = Encoding.Default.GetBytes(sb.ToString());
        //        WebRequest request = WebRequest.Create("ftp://" + ftpAddress + outputFileName); // "AZ_UpdatedColumns.csv");
        //        request.Method = WebRequestMethods.Ftp.UploadFile;
        //        request.Credentials = new NetworkCredential(username, password);
        //        Stream reqStream = request.GetRequestStream();
        //        reqStream.Write(buffer, 0, buffer.Length);
        //        reqStream.Close();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message);
        //        return false;
        //    }
        //}

        /// <summary>
        /// Read setting value of from/to postal codes from DB
        /// </summary>
        private bool InitProperties()
        {
            new DAL().GetAmazonMWSInfo(out inputFilePath, out outputFilePath);

            if (string.IsNullOrWhiteSpace(inputFilePath) || string.IsNullOrWhiteSpace(outputFilePath))
                return false;

            int ind = inputFilePath.LastIndexOf("/");
            inputFilename = inputFilePath.Substring(ind + 1);

            return true;

        }//end method 

    }


}
