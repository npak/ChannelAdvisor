using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace ChannelAdvisor
{
    /// <summary>
    /// Constant class
    /// </summary>
    public static class SettingsConstant
    {
        public const string Auto_Update_Disable = "Auto_Update_Disable";
        public const string Max_SKU_Update = "Max_SKU_Update";
        public const string Inventory_Negative_QTY = "Inventory_Negative_QTY";
        public const string Inventory_Negative_QTY_Check = "Inventory_Negative_QTY_Check";

        public const float Default_Markup_Percentage = 125;

        public const string CAFiles_Folder_Name = "CAFiles";
        public const string VendorFile_Folder_Name = "VendorFiles";
        public const string Cache_Update_Interval = "Cache_Update_Interval";

        //Amazon MWS api
        public const string mwsInputFileName = "mwsInputFileName";
        public const string mwsOutputFileName = "mwsOutputFileName";

        //Moriss XML Creator
        public const string morrisXMLInputFileName = "morrisXMLInputFileName";
        public const string morrisXMLProcessedFileName = "morrisXMLProcessedFileName";
        public const string morrisXMLReadyFileName = "morrisXMLReadyFileName";
        public const string morrisXMLErrorFolderName = "morrisXMLErrorsFolder";

        //shipstation api
        public const string ssGetRate_FromZip = "ccGetRate_FromZip";
        public const string ssGetRate_ToZip = "ccGetRate_ToZip";
        public const string ssAPIServices = "ssAPIServices";

        public const string ssUPSInsurance = "ssUPSInsurance";
        public const string ssStampsInsurance = "ssStampsInsurance";
        public const string ssInputFile = "ssInputFile";
        public const string ssOutputFile = "ssOutputFile";
        public const string ssRequireSignature = "ssRequireSignature";

        public const string ssUSPSPriorityMail = "ssUSPSPriorityMail";
        public const string ssUSPS1ClassMail = "ssUSPS1ClassMail";
        public const string ssUSPSParcelSelect = "ssUSPSParcelSelect";
        public const string ssUSPSMarkup1 = "ssUSPSMarkup";
        public const string ssUSPSMarkupParcel = "ssUSPSMarkupParcel";
        public const string ssUSPSMarkupPriority = "ssUSPSMarkupPriority";
        //private string weightunit = "pound";
        //private string dimunit = "inches";
        //private string country = "\"US\"";
        //public string confirmation = "delivery";
        //public string residential = "false";
        //public string shipingFolder = "/Shipping/";

    }

    /// <summary>
    /// Web Service Constants
    /// </summary>
    [Obsolete("Use enum CAServiceType instead")]
    public static class WebServiceConstants
    {
        public const int Inventory_Service = 1;
        public const int Marketplace_Ad_Service = 2;
        public const int Order_Service = 3;
        public const int Cart_Service = 4;
        public const int Shipping_Service = 5;
        public const int Tax_Service = 6;
        public const int Store_Service = 7;
    }

    public class WeekDayDTO
    {
        public string WeekDay;
        public bool IsEnabled;
    }
}//end Namespace
