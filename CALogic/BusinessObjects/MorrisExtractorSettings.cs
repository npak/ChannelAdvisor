using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class MorrisExtractorSettings
    {
        //MorrisExtractor_FileUrl
        public string FileUrl { get; set; }

        public int NumberOfProducts { get; set; }

        public string OutputFilePrefix { get; set; }


        public static MorrisExtractorSettings Load()
        {
            MorrisExtractorSettings settings = new MorrisExtractorSettings();
            DAL dal = new DAL();
            settings.FileUrl = dal.GetSettingValue("MorrisExtractor_FileUrl");
            settings.NumberOfProducts = int.Parse(dal.GetSettingValue("MorrisExtractor_NumberOfProducts"));
            settings.OutputFilePrefix = dal.GetSettingValue("MorrisExtractor_FilePrefix");
            return settings;
        }
    }
}
