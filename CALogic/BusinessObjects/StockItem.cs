using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class StockItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public decimal? DomesticShipping { get; set; }

    }
}
