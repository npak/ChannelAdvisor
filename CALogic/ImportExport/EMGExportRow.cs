using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class EMGExportRow
    {
        #region Fields
        public string AuctionTitle;

        public string InventoryNumber;

        public string Weight;

        public string UPC;

        public string MPN;

        public string Description;

        public string SupplierCode;

        public string WarehouseLocation;

        public string DCCode;

        public string ChannelAdvisorStoreTitle;

        public string ChannelAdvisorStoreDescription;

        public string Classification;

        public string Warranty;

        public string Label;

        public string Attribute1Name;

        public string Attribute1Value;

        public string Attribute2Name;

        public string Attribute2Value;

        public string Attribute3Name;

        public string Attribute3Value;

        public string Attribute4Name;

        public string Attribute4Value;

        public string Attribute5Name;

        public string Attribute5Value;

        public string Attribute6Name;

        public string Attribute6Value;

        public string Attribute7Name;

        public string Attribute7Value;
        #endregion

        public static EMGExportRow GetHeaderRow()
        {
            EMGExportRow lExportRow = new EMGExportRow();
            lExportRow.AuctionTitle = "Auction Title";
            lExportRow.InventoryNumber = "Inventory Number";
            lExportRow.Weight = "Weight";
            lExportRow.UPC = "UPC";
            lExportRow.MPN = "MPN";
            lExportRow.Description = "Description";
            lExportRow.SupplierCode = "Supplier Code";
            lExportRow.WarehouseLocation = "Warehouse Location";
            lExportRow.DCCode = "DC Code";
            lExportRow.ChannelAdvisorStoreTitle = "ChannelAdvisor Store Title";
            lExportRow.ChannelAdvisorStoreDescription = "ChannelAdvisor Store Description";
            lExportRow.Classification = "Classification";
            lExportRow.Warranty = "Warranty";
            lExportRow.Label = "Labels";
            lExportRow.Attribute1Name = "Attribute1Name";
            lExportRow.Attribute1Value = "Attribute1Value";
            lExportRow.Attribute2Name = "Attribute2Name";
            lExportRow.Attribute2Value = "Attribute2Value";
            lExportRow.Attribute3Name = "Attribute3Name";
            lExportRow.Attribute3Value = "Attribute3Value";
            lExportRow.Attribute4Name = "Attribute4Name";
            lExportRow.Attribute4Value = "Attribute4Value";
            lExportRow.Attribute5Name = "Attribute5Name";
            lExportRow.Attribute5Value = "Attribute5Value";
            lExportRow.Attribute6Name = "Attribute6Name";
            lExportRow.Attribute6Value = "Attribute6Value";
            lExportRow.Attribute7Name = "Attribute7Name";
            lExportRow.Attribute7Value = "Attribute7Value";

            return lExportRow;
        }
    }
}
