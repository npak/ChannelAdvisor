using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class CWRExportShippingRow
    {
        #region Fields
        public string SKU { get; set; }

        public string Classification { get; set; }

        public string WarehouseLocation { get; set; }

        public string Weight { get; set; }
        #endregion

        public static CWRExportShippingRow GetHeaderRow()
        {
            CWRExportShippingRow row = new CWRExportShippingRow();
            row.SKU = "SKU/Item number";
            row.Classification = "Classification";
            row.WarehouseLocation = "Warehouse Location #";
            row.Weight = "Weight";
            return row;
        }

        public static void ExportCWRShippingData(string fileName, IList<CWRExportShippingRow> rows)
        {
            FileHelperEngine<CWRExportShippingRow> engine = new FileHelperEngine<CWRExportShippingRow>();
            engine.WriteFile(fileName, rows);
        }
    }
}
