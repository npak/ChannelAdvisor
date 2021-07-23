using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class MorrisNightlyExtractorSettings
    {
        public string FileUrl { get; set; }

        public int NumberOfProducts { get; set; }

        public string OutputFilePrefix { get; set; }


        public static MorrisNightlyExtractorSettings Load()
        {
            MorrisNightlyExtractorSettings settings = new MorrisNightlyExtractorSettings();
            DAL dal = new DAL();
            settings.FileUrl = dal.GetSettingValue("MorrisNightly_FileUrl");
            settings.NumberOfProducts = int.Parse(dal.GetSettingValue("MorrisNightly_NumberOfProducts"));
            settings.OutputFilePrefix = dal.GetSettingValue("MorrisNightly_FilePrefix");
            return settings;
        }
    }
}
