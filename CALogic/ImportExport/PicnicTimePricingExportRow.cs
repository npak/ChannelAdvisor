using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    public class PicnicTimePricingExportRow
    {
        #region Fields
        public string SKU { get; set; }

        public string SellerCost { get; set; }

        public string RetailPrice { get; set; }

        public string MAP { get; set; }
        #endregion

        public static PicnicTimePricingExportRow GetHeaderRow()
        {
            PicnicTimePricingExportRow row = new PicnicTimePricingExportRow();
            row.SKU = "Sku";
            row.SellerCost = "Seller cost";
            row.RetailPrice = "Retail price";
            row.MAP = "MAP";
            return row;
        }

        public static void ExportPicnicTimePricingData(string fileName, IList<PicnicTimePricingExportRow> rows)
        {
            FileHelperEngine<PicnicTimePricingExportRow> engine = new FileHelperEngine<PicnicTimePricingExportRow>();
            engine.WriteFile(fileName, rows);
        }
    }
}
