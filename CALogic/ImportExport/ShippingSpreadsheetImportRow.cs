using System;
using System.Collections.Generic;
using System.Text;

using FileHelpers;

namespace ChannelAdvisor
{
    [DelimitedRecord("\t")]
    [IgnoreFirst(1)]
    public class ShippingSpreadsheetImportRow
    {
        public string SKU;

        public string Class;

        public string Weight;
    }
}
