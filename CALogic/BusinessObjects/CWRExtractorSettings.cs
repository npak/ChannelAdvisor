using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class CWRExtractorSettings
    {
        public int AuctionTitleMaxLength { get; set; }

        public int CAStoreTitleMaxLength { get; set; }

        public int NumberOfProducts { get; set; }

        public int ClassificationMaxLength { get; set; }

        public string SupplierCode { get; set; }

        public string WarehouseLocation { get; set; }

        public string DCCode { get; set; }

        public string OutputFilePrefix { get; set; }

        public static CWRExtractorSettings Load()
        {
            CWRExtractorSettings settings = new CWRExtractorSettings();
            DAL dal = new DAL();

            settings.AuctionTitleMaxLength = int.Parse(dal.GetSettingValue("CWRExtractor_AuctionTitleLength"));
            settings.CAStoreTitleMaxLength = int.Parse(dal.GetSettingValue("CWRExtractor_CAStoreTitleLength"));
            settings.NumberOfProducts = int.Parse(dal.GetSettingValue("CWRExtractor_NumberOfProducts"));
            settings.ClassificationMaxLength = int.Parse(dal.GetSettingValue("CWRExtractor_ClassificationLength"));
            settings.SupplierCode = dal.GetSettingValue("CWRExtractor_SupplierCode");
            settings.WarehouseLocation = dal.GetSettingValue("CWRExtractor_WarehouseLocation");
            settings.DCCode = dal.GetSettingValue("CWRExtractor_DCCode");
            settings.OutputFilePrefix = dal.GetSettingValue("CWRExtractor_FilePrefix");
            return settings;
        }
    }
}
