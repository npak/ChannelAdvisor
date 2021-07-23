using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Configuration;

namespace ChannelAdvisor
{
    public partial class RunShipstation : Form
    {
        private List<RateToOutput> _listRate = new List<RateToOutput>();
        public RunShipstation(List<RateToOutput> listrate)
        {
            _listRate = listrate;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Shipstation_Load(object sender, EventArgs e)
        {
            caDataGridView1.AutoGenerateColumns = false;
            BindingSource b1 = new BindingSource();
            b1.DataSource = _listRate;
            caDataGridView1.DataSource = null;
            caDataGridView1.DataSource = b1;
        }

 

    }
}
