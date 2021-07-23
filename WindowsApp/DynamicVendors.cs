using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class DynamicVendors : Form
    {
        DAL dal = new DAL();

        public DynamicVendors()
        {
            InitializeComponent();
        }

        private void DynamicVendors_Load(object sender, EventArgs e)
        {
            PopulateGrid();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnSelectFolder_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();

        //    browse.Description = "Please select a folder to scan files";
        //    browse.ShowNewFolderButton = true;
        //    browse.RootFolder = Environment.SpecialFolder.MyComputer;
        //    browse.SelectedPath = Environment.SpecialFolder.MyComputer.ToString();

        //    if (browse.ShowDialog() == DialogResult.OK)
        //    {
        //        txtFolder.Text = browse.SelectedPath;

        //        if (txtFolder.Text.Substring(txtFolder.Text.Length - 2, 1) != "\\")
        //            txtFolder.Text += "\\";

        //    }
        //}


        /// <summary>
        /// 
        /// </summary>
        private void PopulateGrid()
        {
            dgVendors.DataSource = null;
            DataTable dtVendors = dal.GetDynamicVendors().Tables[0];
            dgVendors.DataSource = dtVendors;

            //Format table
            dgVendors.Columns["ID"].Visible=false;
            dgVendors.Columns["Vendor"].Width = 170;
            dgVendors.Columns["Folder"].Width = 300;
            dgVendors.Columns["FileArchive"].Width = 300;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(dgVendors.SelectedRows.Count > 0)) return;
            if (Util.ShowConfirmation("Are you sure you want to delete the selected Vendor: " + dgVendors.SelectedRows[0].Cells["Vendor"].Value.ToString() + "?"))
            {
                try
                {
                    int vendorID = (int)dgVendors.SelectedRows[0].Cells["ID"].Value;
                    dal.DeleteDynamicVendor(vendorID);

                    PopulateGrid();
                }
                catch (Exception ex)
                {
                    Util.ShowMessage(ex.Message);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgVendors.SelectedRows.Count > 0)
            {
                //display selected row values
                DataGridViewRow dgRow = dgVendors.SelectedRows[0];

                int vendorId;
                if (int.TryParse(dgRow.Cells["ID"].Value.ToString(), out vendorId))
                {
                    ChannelAdvisor.Vendor vendor = dal.GetVendor(vendorId);
                    VendorEdit editForm = new VendorEdit();
                    editForm.VendorInfo = vendor;
                    editForm.Title = "Edit vendor";
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        PopulateGrid();
                        Util.ShowMessage("Vendor updated successfully");
                    }
                }
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void addVendor_Click(object sender, EventArgs e)
        {
            VendorEdit editForm = new VendorEdit();
            editForm.VendorInfo = new Vendor();
            editForm.VendorInfo.Type = VendorType.Dynamic;
            editForm.Title = "Add new vendor";
            if (editForm.ShowDialog() == DialogResult.OK)
            {
                PopulateGrid();
                Util.ShowMessage("Vendor added successfully");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OtherVendorsFTPOptions form = new OtherVendorsFTPOptions();
            if (form.ShowDialog() == DialogResult.OK)
            {
                Util.ShowMessage("Vendor CSV information added successfully");
            }
        }
    }
}
