using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class PicnicTimeShippingExportRow
    {
        #region Fields
        public string SKU { get; set; }

        public string Classification { get; set; }

        public string WarehouseLocation { get; set; }

        public string Weight { get; set; }
        #endregion

        public static PicnicTimeShippingExportRow GetHeaderRow()
        {
            PicnicTimeShippingExportRow row = new PicnicTimeShippingExportRow();
            row.SKU = "SKU/Item number";
            row.Classification = "Classification";
            row.WarehouseLocation = "Warehouse Location #";
            row.Weight = "Weight";
            return row;
        }

        public static void ExportPicnicTimeShippingData(string fileName, IList<PicnicTimeShippingExportRow> rows)
        {
            FileHelperEngine<PicnicTimeShippingExportRow> engine = new FileHelperEngine<PicnicTimeShippingExportRow>();
            engine.WriteFile(fileName, rows);
        }
    }
}
