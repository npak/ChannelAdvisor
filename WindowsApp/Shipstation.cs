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
    public partial class Shipstation : Form
    {

        public Shipstation()
        {
            InitializeComponent();
        }

      
        private void Shipstation_Load(object sender, EventArgs e)
        {
            cbCarrier.DisplayMember = "name";
            cbCarrier.ValueMember = "code";
            ShipStationService sss = new ShipStationService();
            cbCarrier.DataSource = sss.GetCarriersList();
            cbCarrier.SelectedValue = "ups";

            string str = ",,,,";
            string[] dim = str.Split(',');
            DisplayParameters(sss.GetRateParameters(dim));

        }

        private void DisplayParameters(RateParameters rp)
        {
            
            cbCarrier.SelectedValue = rp.carrierCode;
            txtServCode.Text = rp.serviceCode;
            txtPackCode.Text = rp.packageCode;
            txtFromZip.Text = rp.fromPostalCode;
            txtState.Text = rp.toState;
            txtCountry.Text = rp.toCountry;
            txtToZip.Text = rp.toPostalCode;
            txtCity.Text = rp.toCity;
            txtWeightU.Text = rp.wunits;
            //txtWeightValue.Text = rp.wvalue;
            txtDimUnit.Text = rp.dunits;
            //txtDImLenght.Text = rp.dlength;
            //txtDimWidth.Text = rp.dwidth;
            //txtDimHeight.Text = rp.dheight;
            txtConfirm.Text = rp.confirmation;
            chbResident.Checked = rp.residential== "true" ? true : false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                string ss = cbCarrier.SelectedValue.ToString();
                ShipStationService serv = new ShipStationService();
                List<Rate> listRate = new List<Rate>();
                WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();

                dialogWithWork.ShowWithWork(() =>
                {
                    dialogWithWork.ShowMessage(string.Format("Fetching data from API, please wait..."));
                    string dim = "," + txtWeightValue.Text + "," + Convert.ToInt32(numDimLengh.Value).ToString() + "," + Convert.ToInt32(numDimWidth.Value).ToString() + "," + Convert.ToInt32(numDimHeight.Value).ToString() + ",0";
                    listRate = serv.SearchRatesByParams(ss, dim);
                });

                caDataGridView1.DataSource = listRate;
            }
            else
                MessageBox.Show("Weight is required. Empty or wrong format.");
        }

        private bool isValid()
        {

            System.Text.RegularExpressions.Regex nonNumericRegex = new System.Text.RegularExpressions.Regex(@"^(0|[1-9]\d*)?(\.\d+)?(?<=\d)$");
            if (nonNumericRegex.IsMatch(txtWeightValue.Text))
                return true;
            else
                return false;
        }

    }
}
