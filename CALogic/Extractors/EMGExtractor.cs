using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;

namespace ChannelAdvisor.Extractors
{
    public class EMGExtractor
    {
        private Vendor vendorInfo { get; set; }

        public EMGExtractorSettings Settings { get; set; }
        public IList<string> SKUList { get; set; }
        public bool IsSkuUsed { get; set; }
        public string FileName { get; set; }
        public string FolderName { get; set; }
        public bool IsWholeFile { get; set; }

        public EMGExtractor(EMGExtractorSettings settings, List<string> SKUs, bool isSKUUsed, string fileName, 
            string folderName, bool isWholeFile)
        {
            Settings = settings;
            SKUList = SKUs;
            IsSkuUsed = isSKUUsed;
            FileName = fileName;
            FolderName = folderName;
            IsWholeFile = isWholeFile;

            vendorInfo = Vendor.Load((int)VendorName.EMG);
        }

        public void Extract()
        {
            string sheetName = ExcelUtils.GetSheetNameWithSubstring(FileName, "ExcelConStringIMEX", "Entire List");
            DataTable table = ExcelUtils.ReadExcelSheet(FileName, sheetName, "ExcelConStringIMEX");

            IList<EMGExportRow> result = new List<EMGExportRow>();
            result.Add(EMGExportRow.GetHeaderRow());

            foreach (DataRow row in table.Rows)
            {
                string upc = row[6].ToString();
                if (upc.Length == 11) upc = "0" + upc;
                if (upc.Length == 13) upc = "0" + upc;

                string sku = IsSkuUsed ? row[2].ToString() : upc;
                if (!string.IsNullOrEmpty(sku) && (IsWholeFile || (SKUList.IndexOf(sku) > -1)))
                {
                    EMGExportRow exportRow = new EMGExportRow();
                    string supDesc = row[5].ToString();

                    exportRow.AuctionTitle = (supDesc.Length > Settings.AuctionTitleMaxLength ?
                        supDesc.Substring(0, Settings.AuctionTitleMaxLength) : supDesc).Replace("\n", " ");
                    exportRow.InventoryNumber = sku;
                    exportRow.Weight = row[18].ToString();
                    exportRow.UPC = upc;
                    exportRow.MPN = row[2].ToString();

                    List<string> lDescriptions = new List<string>();
                    for (int i = 20; i <= 24; i++)
                        if (row[i].ToString().Length > 0)
                            lDescriptions.Add(HttpUtility.HtmlEncode(row[i].ToString()));
                    string IntroParagraph = HttpUtility.HtmlEncode(row[19].ToString()).Replace("#", "");
                    exportRow.Description = GetHtml(IntroParagraph, lDescriptions);

                    exportRow.SupplierCode = Settings.SupplierCode;
                    exportRow.WarehouseLocation = Settings.WarehouseLocation;
                    exportRow.DCCode = Settings.DCCode;
                    exportRow.ChannelAdvisorStoreTitle = (supDesc.Length > Settings.CAStoreTitleMaxLength ?
                        supDesc.Substring(0, Settings.CAStoreTitleMaxLength) : supDesc).Replace("\n", " ");
                    exportRow.ChannelAdvisorStoreDescription = GetHtml(IntroParagraph, lDescriptions);
                    exportRow.Classification = row[4].ToString().Length > Settings.ClassificationMaxLength ?
                        row[4].ToString().Substring(0, Settings.ClassificationMaxLength) : row[4].ToString();
                    exportRow.Label = vendorInfo.Label;
                    exportRow.Attribute1Name = "Brand";
                    exportRow.Attribute1Value = row[1].ToString();
                    exportRow.Attribute2Name = "Last Update";
                    exportRow.Attribute2Value = row[12].ToString();
                    exportRow.Attribute3Name = Settings.WarrantyLabel;
                    if (row[26].ToString().Length > 0)
                    {
                        exportRow.Attribute3Value = row[26].ToString();
                        exportRow.Warranty = row[26].ToString();
                    }
                    else
                    {
                        exportRow.Attribute3Value = Settings.WarrantyDefaultValue;
                        exportRow.Warranty = Settings.WarrantyDefaultValue;
                    }
                    exportRow.Attribute5Name = "L";
                    exportRow.Attribute5Value = row[15].ToString(); ;
                    exportRow.Attribute6Name = "W";
                    exportRow.Attribute6Value = row[16].ToString();
                    exportRow.Attribute7Name = "H";
                    exportRow.Attribute7Value = row[17].ToString();

                    result.Add(exportRow);
                }
            }

            string fileName = string.Format("{0}{1}{2}.txt", FolderName, Settings.OutputFilePrefix, DateTime.Now.ToString("MMddyyyy"));
            EMGExporter.ExportEMGDataToTextFile(result, fileName);

            // Exporting to shipping file
            IList<EMGExportShippingRow> shippingRows = new List<EMGExportShippingRow>();
            shippingRows.Add(EMGExportShippingRow.GetHeaderRow());
            for(int i=1; i<result.Count; i++)
            {
                EMGExportShippingRow shippingRow = new EMGExportShippingRow();
                shippingRow.SKU = result[i].InventoryNumber;
                shippingRow.Classification = result[i].Classification;
                shippingRow.WarehouseLocation = Settings.WarehouseLocation;
                shippingRow.Weight = result[i].Weight;
                shippingRows.Add(shippingRow);
            }
            string shippingFileName = string.Format("{0}{1}{2}shipping.txt", FolderName, Settings.OutputFilePrefix, DateTime.Now.ToString("MMddyyyy"));
            EMGExportShippingRow.ExportEMGShippingData(shippingFileName, shippingRows);
        }

        private string GetHtml(string aIntroParagraph, List<string> aDescriptions)
        {
            StringBuilder lSB = new StringBuilder();
            lSB.AppendLine("\"<!DOCTYPE HTML PUBLIC \"\"-//W3C//DTD HTML 3.2//EN\"\">");
            lSB.AppendLine("<html>");
            lSB.AppendLine("<head>");
            lSB.AppendLine("</head>");
            lSB.AppendLine("<body bgcolor=\"\"#ffffff\"\">");
            lSB.AppendLine("<p style=\"\"font-family: Arial;\"\"><span ");
            lSB.AppendLine("style=\"\"FONT-SIZE: 12pt;\"\"><font");
            lSB.AppendLine(string.Format(" size=\"\"2\"\">{0}</font></span></p>", aIntroParagraph));
            lSB.AppendLine("<p style=\"\"font-family: Arial;\"\"><font size=\"\"2\"\"><span style=\"\"font-weight:bold;\"\">Features:</span></font> </p>");
            lSB.AppendLine("<ul style=\"\"font-family: Arial;\"\">");

            foreach (string lDescr in aDescriptions)
            {
                lSB.AppendLine("<li><font");
                lSB.AppendLine(string.Format(" size=\"\"2\"\">{0}</font></li>", HttpUtility.HtmlEncode(lDescr)));
            }

            lSB.Append("</ul>");
            lSB.AppendLine("</body>");
            lSB.Append("</html>\"");

            return lSB.ToString();
        }
    }
}
