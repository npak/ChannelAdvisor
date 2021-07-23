using System;
using System.Data;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// Represents EMG Extractor settings
    /// </summary>
    public class EMGExtractorSettings
    {
        int mID;
        public int ID { get { return mID; } }

        public string URLToLoadXML;

        public string SupplierCode;

        public string WarehouseLocation;

        public string DCCode;

        public int AuctionTitleMaxLength;

        public int CAStoreTitleMaxLength;

        public int ClassificationMaxLength;

        public string WarrantyLabel;

        public string WarrantyDefaultValue;

        public string OutputFilePrefix;

        public EMGExtractorSettings(DataRow aRow)
        {
            mID = (int)aRow["ID"];
            URLToLoadXML = aRow["URLToLoadXML"].ToString();
            SupplierCode = aRow["SupplierCode"] == DBNull.Value ? "" : aRow["SupplierCode"].ToString();
            WarehouseLocation = aRow["WarehouseLocation"] == DBNull.Value ? "" : aRow["WarehouseLocation"].ToString();
            DCCode = aRow["DCCode"] == DBNull.Value ? "" : aRow["DCCode"].ToString();
            AuctionTitleMaxLength = (int)aRow["AuctionTitleMaxLength"];
            CAStoreTitleMaxLength = (int)aRow["CAStoreTitleMaxLength"];
            ClassificationMaxLength = (int)aRow["ClassificationMaxLength"];
            WarrantyLabel = aRow["WarrantyLabel"].ToString();
            WarrantyDefaultValue = aRow["WarrantyDefaultValue"].ToString();
            OutputFilePrefix = aRow["OutputFilePrefix"] == DBNull.Value ? "" : aRow["OutputFilePrefix"].ToString();
        }

        public bool Update()
        {
            DAL lDAL = new DAL();
            return lDAL.UpdateEMGExtractorSettings(this);
        }

        public static EMGExtractorSettings Load()
        {
            DAL lDAL = new DAL();
            return new EMGExtractorSettings(lDAL.LoadEMGExtractorSettings());
        }
    }
}
