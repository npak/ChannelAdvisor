using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MarketplaceWebServiceProducts;
using ChannelAdvisor.Objects;

namespace ChannelAdvisor
{
    public partial class MWSSearch : Form
    {
        private List<MwsResult> _listASIN = new List<MwsResult>();
        public MWSSearch(List<MwsResult> listASIN)
        {
            _listASIN = listASIN;
            InitializeComponent();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            string marketplaceId = cmbSite.SelectedValue.ToString();
            string idType = cmbType.SelectedValue.ToString();
            AmazonMarketplaceWebServiceProducts obj = new AmazonMarketplaceWebServiceProducts(marketplaceId, idType); 

            grvAmazonResult.AutoGenerateColumns = false;
            BindingSource b1 = new BindingSource();
            b1.DataSource = obj.GetData(); 
            grvAmazonResult.DataSource = null;
            grvAmazonResult.DataSource = b1;
            //uload
            obj.UploadAmazonFile();

        }

        private void MWSSearch_Load(object sender, EventArgs e)
        {
            // fill Types
            cmbType.ValueMember = "value";
            cmbType.DisplayMember = "text";
            var listTypes = new[] { new { value = "UPC", text = "UPC" } };
            cmbType.DataSource = listTypes;

            cmbSite.ValueMember = "value";
            cmbSite.DisplayMember = "text";
            var listSite =new[] { new { value = "ATVPDKIKX0DER", text = "Amazon US" }};
            cmbSite.DataSource = listSite;

            BindingSource b1 = new BindingSource();
            b1.DataSource = _listASIN;
            grvAmazonResult.DataSource = null;
            grvAmazonResult.DataSource = b1;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
