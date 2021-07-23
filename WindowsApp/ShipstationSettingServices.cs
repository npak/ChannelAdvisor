using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class ShipstationSettingServices : Form
    {
        DAL dal = new DAL();
        private string strServices = "";

        public ShipstationSettingServices()
        {
            InitializeComponent();
        }

        private void ShipstationSettingServices_Load(object sender, EventArgs e)
        {
           dal.GetServicesString(out strServices);
           DisplayServices();
        }
        private void DisplayServices()
        {
            bool b;
            ShipStationService ser = new ShipStationService();
            List<Service> l = ser.GetServicesList();
            foreach (Service servi in l)
            {
                b = strServices.Contains(servi.code+",");
                checkedListBox1.Items.Add(servi.code, b);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetSelectedServices()
        {
            
            string result = "";
            // Determine if there are any items checked.  
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                // If so, loop through all checked items and print results.  
                for (int x = 0; x <= checkedListBox1.CheckedItems.Count - 1; x++)
                {
                    if (result.Length > 0)
                        result += ",";
                    result += checkedListBox1.CheckedItems[x].ToString();
                }
                
            }  
            return result + ",";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool isSuccess = dal.SaveShipstationServicesString(GetSelectedServices());

            if (isSuccess)
            {
                Util.ShowMessage("Record Saved!");
            }
            else
            {
                Util.ShowMessage("There was some problem while trying to save the record. The record could not be saved.");
            }

        }
    }
}
