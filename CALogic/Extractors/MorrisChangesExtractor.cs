using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Linq;

namespace ChannelAdvisor.Extractors
{
    public class MorrisChangesExtractor

    {
        //private const string shortFieldsList = "upc,sku";
        //private const string fullFieldsList = "ti,upc,sku,wt,mfgn,fdesc,price,list,imgpath,cat,mfg,wa,map";
        //private const int maxSkusPerRequest = 50;
        public Vendor VendorInfo { get; set; }
        private XmlNodeList allnodes;

        private List<MorrisChangesExportRow> morrisList { get; set; }

        private Vendor vendorInfo = Vendor.Load((int)VendorName.MorrisChanges);

        private IList<string> skus;
        private bool isAllSkus;
        private string outputFolder;
        private MorrisChangesExtractorSettings settings = MorrisChangesExtractorSettings.Load();

        public MorrisChangesExtractor(IList<string> skus, bool isAllSkus, string outputFolder)
        {
            this.skus = skus;
            this.isAllSkus = isAllSkus;
            this.outputFolder = outputFolder;

            VendorInfo = new DAL().GetVendor((int)VendorName.MorrisChanges);
        }

        public void Extract()
        {
            GetMorrisExportList();
            if (!isAllSkus)
                GetMorrisSkusExportlis();

            int i = 1;
            List<MorrisChangesExportRow> doc;
            while ((i * settings.NumberOfProducts < this.morrisList.Count))
            {
               doc = new List<MorrisChangesExportRow>();

                for (int k = 0; k < settings.NumberOfProducts; k++ )
                    doc.Add(morrisList[(i - 1) * settings.NumberOfProducts + k]);
                ExportToFiles(doc, i);
                i++;
            }

            int del = morrisList.Count - (i - 1) * settings.NumberOfProducts;
            if (del >0)
            {
                doc = new List<MorrisChangesExportRow>();
                for (int k = 0; k < del; k++)
                    doc.Add(morrisList[(i - 1) * settings.NumberOfProducts + k]);
                ExportToFiles(doc, i);
            }
            
        }

        private void ExportToFiles(List<MorrisChangesExportRow> doc, int index)
        {
            string filePath = string.Format("{0}{1}{2}_{3}.csv",outputFolder, settings.OutputFilePrefix, DateTime.Now.ToString("MMddyyyy"), index);
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
           
            StringBuilder sb = new StringBuilder();

            doc.Insert(0,MorrisChangesExportRow.GetHeaderRow());
            foreach (MorrisChangesExportRow morr in doc)
            {
                sb.Append(morr.Sku).Append(',');
                sb.Append(morr.Part).Append(',');
                //sb.Append(morr.Gtin).Append(',');
                sb.Append(String.Format("\"{0}\"", morr.Gtin)).Append(',');
                sb.Append(morr.On_Sale).Append(',');
                sb.Append(morr.Qty).Append(',');
                sb.Append(morr.Time).Append(',');
                if (morr.Details != null)
                {
                    sb.Append(morr.Details.Desc).Append(',');
                    sb.Append(morr.Details.Weight).Append(',');
                    sb.Append(morr.Details.Length).Append(',');
                    sb.Append(morr.Details.Width).Append(',');
                    sb.Append(morr.Details.Height).Append(',');
                    sb.Append(morr.Details.Cubes).Append(',');
                    sb.Append(morr.Details.Domestic_Air_Dim_Weight).Append(',');
                    sb.Append(morr.Details.Domestic_Gnd_Dim_Weight).Append(',');
                    sb.Append(morr.Details.Intl_Dim_Weight).Append(',');
                    sb.Append(morr.Details.Price).Append(',');
                    if (morr.Details.Relateds.Count > 0)
                    {
                        foreach (var rel in morr.Details.Relateds)
                        {
                            sb.Append(rel.Sku).Append(',');
                            sb.Append(rel.Part).Append(',');
                            sb.Append(rel.Desc).Append(',');
                        }
                    }
                }
                sb.Length--; // Remove last ";"
                sb.AppendLine();
            }
            
            File.WriteAllText(filePath, sb.ToString());
           
        }
        
        public void GetMorrisExportList()
        {
            XmlDocument xmlDoc = new XmlDocument();
           // xmlDoc.Load("E:\\papa\\2-15-15\\ChannelAdvisorSources\\WindowsApp\\XMLFile1.xml");
            xmlDoc.Load(settings.FileUrl);
         
            // read root elements
            XmlNode rootnodes = xmlDoc.SelectSingleNode("//AvailableBatch"); // get all <AvailableBatch> nodes
            int XMLFileCount = 0;
            morrisList = new List<MorrisChangesExportRow>();
            foreach(XmlNode xmlnode in rootnodes.ChildNodes )
            {
                if (xmlnode.Name == "of")
                {
                    XMLFileCount = Convert.ToInt32(xmlnode.InnerText);
                    break;
                }
            }
            
            REadMorrisXML(xmlDoc);
            
            for (int i = 2; i < XMLFileCount + 1; i++)
            {
                if (WaitDialogWithWork.Current != null)
                    WaitDialogWithWork.Current.ShowMessage("Fetching data from "+settings.FileUrl.Replace("001",i.ToString("000"))+", please wait..." );

                xmlDoc.Load(settings.FileUrl.Replace("001",i.ToString("000")));
                REadMorrisXML(xmlDoc);
            }

        }
        public void REadMorrisXML(XmlDocument xmlDoc)
        {
            MorrisChangesExportRow morrisRow;
            XmlNode detail;
            MorrisChangesExportRow.Detail morrisdet;

                XmlNodeList nodes = xmlDoc.SelectNodes("//Available"); // get all <Available> nodes

                foreach (XmlNode node in nodes)
                {
                    morrisRow = new MorrisChangesExportRow();
                    morrisRow.Sku = node["Part"] == null ? string.Empty : vendorInfo.SkuPrefix+ node["Part"].InnerText;
                    morrisRow.Part = node["Part"] == null ? string.Empty : node["Part"].InnerText;
                    morrisRow.Gtin = node["Gtin"] == null ? string.Empty : node["Gtin"].InnerText;
                    morrisRow.On_Sale = node["On_Sale"] == null ? string.Empty : node["On_Sale"].InnerText;
                    morrisRow.Qty = node["Qty"] == null ? string.Empty : node["Qty"].InnerText; ;
                    morrisRow.Time = node["Time"] == null ? string.Empty : node["Time"].InnerText;
                    // get deetail node
                    detail = node.SelectSingleNode("Detail");
                    if (detail != null)
                    {
                        morrisdet = new MorrisChangesExportRow.Detail();
                        morrisdet.Desc = detail["Desc"] == null ? string.Empty : detail["Desc"].InnerText;
                        morrisdet.Weight = detail["Weight"] == null ? string.Empty : detail["Weight"].InnerText;
                        morrisdet.Length = detail["Length"] == null ? string.Empty : detail["Length"].InnerText;
                        morrisdet.Width = detail["Width"] == null ? string.Empty : detail["Width"].InnerText;
                        morrisdet.Height = detail["Height"] == null ? string.Empty : detail["Height"].InnerText;
                        morrisdet.Cubes = detail["Cubes"] == null ? string.Empty : detail["Cubes"].InnerText;
                        morrisdet.Domestic_Air_Dim_Weight = detail["Domestic_Air_Dim_Weight"] == null ? string.Empty : detail["Domestic_Air_Dim_Weight"].InnerText;
                        morrisdet.Intl_Dim_Weight = detail["Intl_Dim_Weight"] == null ? string.Empty : detail["Intl_Dim_Weight"].InnerText;
                        if (detail["Domestic_Gnd_Dim_Weight"] != null)
                            morrisdet.Domestic_Gnd_Dim_Weight = detail["Domestic_Gnd_Dim_Weight"].InnerText;
                        morrisdet.Price = detail["Price"] == null ? string.Empty : detail["Price"].InnerText;
                        morrisRow.Details = morrisdet;
                        if (detail["Related"] != null)
                        {
                            XmlNodeList rels = detail.SelectNodes("Related");
                            MorrisChangesExportRow.Related newrel;
                            List<MorrisChangesExportRow.Related> rellist = new List<MorrisChangesExportRow.Related>();

                            foreach (XmlNode rel in rels)
                            {
                                newrel = new MorrisChangesExportRow.Related();
                                newrel.Sku = rel["Sku"].InnerText;
                                newrel.Part = rel["Part"].InnerText;
                                newrel.Desc = rel["Desc"].InnerText;
                                rellist.Add(newrel);
                            }
                            morrisRow.Details.Relateds = rellist;
                        }
                    }
                    morrisList.Add(morrisRow);
                }
            
        }

        public void GetMorrisSkusExportlis()
        {
            var m = (from morr in morrisList
                    join scu in this.skus
                    on morr.Sku equals scu
                    select morr).ToList();
            this.morrisList = m.ToList<MorrisChangesExportRow>();
        }

        //public List<MorrisChangesExportRow> GetMorrisErxoptListLinqTest()
        //{
        //    //"http://morris.morriscostumes.com/out/190821/available_batchyynyy_001.xml"
        //    // E:\\papa\\2-15-15\\ChannelAdvisorSources\\WindowsApp\\XMLFile1.xml settings.FileUrl

        //    System.Xml.Linq.XElement xmlDoc = System.Xml.Linq.XElement.Load("E:\\papa\\2-15-15\\ChannelAdvisorSources\\WindowsApp\\XMLFile1.xml");
        //    var morris =
        //    from morr in xmlDoc.Descendants("Available")
        //    select new MorrisChangesExportRow
        //    {
        //        Sku = morr.Element("Sku").Value,
        //        Part = morr.Element("Part").Value,
        //        Gtin = morr.Element("Gtin").Value,
        //        On_Sale = morr.Element("On_Sale").Value,
        //        Qty = morr.Element("Qty").Value,
        //        Time = morr.Element("Time").Value,
        //        file_no = morr.Element("file_no").Value,
        //        of = morr.Element("of").Value,
        //        Count = morr.Element("Count").Value,
        //        file_count = morr.Element("file_count").Value,
        //        //Details = new MorrisExportRow.Detail(from det in morr.Descendants("Detail"))
        //        Details = new MorrisChangesExportRow.Detail()
        //        {
        //            Desc = morr.Element("Detail").Element("Desc").Value,
        //            Weight = morr.Element("Detail").Element("Desc").Value,
        //            Length = morr.Element("Detail").Element("Desc").Value,
        //            Width = morr.Element("Detail").Element("Desc").Value,
        //            Height = morr.Element("Detail").Element("Desc").Value,
        //            Cubes = morr.Element("Detail").Element("Desc").Value,
        //            Domestic_Air_Dim_Weight = morr.Element("Detail").Element("Desc").Value,
        //            Intl_Dim_Weight = morr.Element("Detail").Element("Desc").Value,
        //            Domestic_Gnd_Dim_Weight = morr.Element("Detail").Element("Desc").Value,
        //            Price = morr.Element("Detail").Element("Desc").Value,
        //            Relateds = morr.Element("Detail").Descendants("Related") != null ?
        //                        new List<MorrisChangesExportRow.Related>(from rel in morr.Element("Detail").Descendants("Related")
        //                                                                 select new MorrisChangesExportRow.Related
        //                                                          {
        //                                                              Sku = rel.Element("Sku").Value,
        //                                                              Part = rel.Element("Part").Value,
        //                                                              Desc = rel.Element("Desc").Value
        //                                                          }) : new List<MorrisChangesExportRow.Related>()


        //            //Phone = new List<PhoneContact>(from phn in cust.Descendants("phone")
        //            //            select new PhoneContact
        //            //            {
        //            //               Type = phn.Element("type").Value,
        //            //               Number = phn.Element("no").Value
        //            //            })                                                                 
        //        }
        //    };
        //    string ss = "";
        //    foreach (MorrisChangesExportRow r in morris)
        //        ss = r.Sku;
        //    return morris.ToList();

        //}


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
