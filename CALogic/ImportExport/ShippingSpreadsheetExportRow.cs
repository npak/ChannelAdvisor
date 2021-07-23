using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor.ImportExport
{
    [DelimitedRecord("\t")]
    public class ShippingSpreadsheetExportRow
    {
        #region Fields
        public string InventoryNumber;

        public string Classification;

        public string Weight;

        public string Attribute1Name;

        public string Attribute1Value;

        public string Attribute2Name;

        public string Attribute2Value;

        public string ShipZoneName;

        public string ShipCarrierCode;

        public string ShipClassCode;

        public string ShipRateFirstItem;

        public string ShipRateAdditionalItem;
        #endregion

        public static ShippingSpreadsheetExportRow GetHeaderRow()
        {
            ShippingSpreadsheetExportRow lHeaderRow = new ShippingSpreadsheetExportRow();
            lHeaderRow.InventoryNumber = "Inventory Number";
            lHeaderRow.Classification = "Classification";
            lHeaderRow.Weight = "Weight";
            lHeaderRow.Attribute1Name = "Attribute1Name";
            lHeaderRow.Attribute1Value = "Attribute1Value";
            lHeaderRow.Attribute2Name = "Attribute2Name";
            lHeaderRow.Attribute2Value = "Attribute2Value";
            lHeaderRow.ShipZoneName = "Ship Zone Name";
            lHeaderRow.ShipCarrierCode = "Ship Carrier Code";
            lHeaderRow.ShipClassCode = "Ship Class Code";
            lHeaderRow.ShipRateFirstItem = "Ship Rate First Item";
            lHeaderRow.ShipRateAdditionalItem = "Ship Rate Additional Item";

            return lHeaderRow;
        }
    }
}
