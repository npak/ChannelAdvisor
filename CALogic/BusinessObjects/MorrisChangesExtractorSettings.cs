using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChannelAdvisor
{
    public class MorrisChangesExtractorSettings
    {
        public string FileUrl { get; set; }

        public int NumberOfProducts { get; set; }

        public string OutputFilePrefix { get; set; }


        public static MorrisChangesExtractorSettings Load()
        {
            MorrisChangesExtractorSettings settings = new MorrisChangesExtractorSettings();
            DAL dal = new DAL();
            settings.FileUrl = dal.GetSettingValue("MorrisChanges_FileUrl");
            settings.NumberOfProducts = int.Parse(dal.GetSettingValue("MorrisChanges_NumberOfProducts"));
            settings.OutputFilePrefix = dal.GetSettingValue("MorrisChanges_FilePrefix");
            return settings;
        }
    }
}
