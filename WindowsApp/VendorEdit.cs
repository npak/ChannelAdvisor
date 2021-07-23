using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class VendorEdit : Form
    {
        public Vendor VendorInfo 
        {
            get
            {
                return commonSettings.VendorInfo;
            }
            set
            {
                commonSettings.VendorInfo = value;
            }
        }

        public string Title
        {
            set { this.Text = value; }
        }

        public VendorEdit()
        {
            InitializeComponent();

            commonSettings.IsNameEditable = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (!commonSettings.SaveVendorCommonSettings())
            {
                Util.ShowMessage("Couldn't save vendor data");
                return;
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
