using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class EMGExportShippingRow
    {
        #region Fields
        public string SKU { get; set; }

        public string Classification { get; set; }

        public string WarehouseLocation { get; set; }

        public string Weight { get; set; }
        #endregion

        public static EMGExportShippingRow GetHeaderRow()
        {
            EMGExportShippingRow row = new EMGExportShippingRow();
            row.SKU = "SKU/Item number";
            row.Classification = "Classification";
            row.WarehouseLocation = "Warehouse Location #";
            row.Weight = "Weight";
            return row;
        }

        public static void ExportEMGShippingData(string fileName, IList<EMGExportShippingRow> rows)
        {
            FileHelperEngine<EMGExportShippingRow> engine = new FileHelperEngine<EMGExportShippingRow>();
            engine.WriteFile(fileName, rows);
        }
    }
}
