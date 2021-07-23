using System;
using System.Collections.Generic;
using System.Text;

namespace ChannelAdvisor
{
    /// <summary>
    /// Common interface for all vendor services
    /// </summary>
    public interface IVendorService
    {
        /// <summary>
        /// Information about vendor
        /// </summary>
        Vendor VendorInfo { get; set; }

        InventoryUpdateServiceDTO GetInventoryListForService();
        InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileId);
    }
}
