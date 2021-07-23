using System;
using System.Collections.Generic;
using System.Text;
using FileHelpers;

namespace ChannelAdvisor
{
    /// <summary>
    /// Represents export row for duplicate SKUs
    /// </summary>
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    public class DuplicateSKU
    {
        //[FieldQuoted()]
        public string UPC;
        //[FieldQuoted()]
        public string SKU;
        //[FieldQuoted()]
        public string NewSKU;

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DuplicateSKU GetHeaderClass()
        {
            DuplicateSKU duplicateSKU = new DuplicateSKU();
            duplicateSKU.UPC = "UPC";
            duplicateSKU.SKU = "SKU";
            duplicateSKU.NewSKU = "NewSKU";

            return duplicateSKU;
        }
    }
}
