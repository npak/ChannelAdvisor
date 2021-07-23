using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class MorrisCompleteExtractorSettings
    {
        //MorrisExtractor_FileUrl
        public string FileUrl { get; set; }

        public int NumberOfProducts { get; set; }

        public string OutputFilePrefix { get; set; }


        public static MorrisCompleteExtractorSettings Load()
        {
            MorrisCompleteExtractorSettings settings = new MorrisCompleteExtractorSettings();
            DAL dal = new DAL();
            settings.FileUrl = dal.GetSettingValue("MorrisCompleteExtractor_FileUrl");
            settings.NumberOfProducts = int.Parse(dal.GetSettingValue("MorrisCompleteExtractor_NumberOfProducts"));
            settings.OutputFilePrefix = dal.GetSettingValue("MorrisCompleteExtractor_FilePrefix");
            return settings;
        }
    }
}
