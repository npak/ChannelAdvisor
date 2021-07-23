using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class LinnworksCatalog : Form
    {
        public LinnworksCatalog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var linnworksService = new ChannelAdvisor.VendorServices.LinnworksService();

            //check cache to read linnworks catalog
            List<LinnworksInventoryWS.StockItem> linnwork_catalog;
            InventoryCache.LoadCaches(out linnwork_catalog);
            
            //linnwork_catalog[0].
        }
    }
}
