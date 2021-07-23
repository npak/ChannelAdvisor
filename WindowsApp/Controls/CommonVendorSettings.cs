using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

using ChannelAdvisor;

namespace ChannelAdvisor
{
    public partial class CommonVendorSettings : UserControl
    {
        private Vendor _vendorInfo;
        public Vendor VendorInfo
        {
            get { return _vendorInfo; }
            set
            {
                if (value != null)
                {
                    _vendorInfo = value;
                    BindData();
                }
            }
        }

        public bool IsNameEditable
        {
            get { return nameText.Enabled; }
            set { nameText.Enabled = value; }
        }

        public CommonVendorSettings()
        {
            InitializeComponent();

            outOfStockThresholdText.InvalidInput += new EventHandler<EventArgs>(SetInvalidInput);
            outOfStockQuantityText.InvalidInput += new EventHandler<EventArgs>(SetInvalidInput);
        }

        private void BindData()
        {
            nameText.DataBindings.Add("Text", _vendorInfo, "Name");
            folderText.DataBindings.Add("Text", _vendorInfo, "Folder");
            fileArchiveText.DataBindings.Add("Text", _vendorInfo, "FileArchive");
            setOutOfStock.DataBindings.Add("Checked", _vendorInfo, "SetOutOfStockIfNotPresented");
            prefixName.DataBindings.Add("Text", _vendorInfo, "SkuPrefix");
            outOfStockThresholdText.DataBindings.Add("Text", _vendorInfo, "OutOfStockThreshold", true);
            outOfStockQuantityText.DataBindings.Add("Text", _vendorInfo, "OutOfStockQuantity", true);
            labelText.DataBindings.Add("Text", _vendorInfo, "Label");
        }

        private void SetInvalidInput(object sender, EventArgs e)
        {
            commonSettingsErrorProvider.SetError((Control)sender, "Should be an integer value");
        }

        /// <summary>
        /// Validate input data
        /// </summary>
        /// <returns>True if data is valid, otherwise False</returns>
        public bool ValidateInput()
        {
            commonSettingsErrorProvider.Clear();
            bool result = true;

            if ((IsNameEditable) && (string.IsNullOrEmpty(nameText.Text)))
            {
                commonSettingsErrorProvider.SetError(nameText, "Name field cannot be empty");
                result = false;
            }

            if (prefixName.Text.Length > 10)
            {
                commonSettingsErrorProvider.SetError(prefixName, "SKU prefix should be less then 10 characters length");
                result = false;
            }

            if (labelText.Text.Length > 50)
            {
                commonSettingsErrorProvider.SetError(labelText, "Label should be less then 50 characters length");
                result = false;
            }

            if (result)
            {
                DBAccess db = new DBAccess();
                // Validate duplicate vendor names
                SqlParameter idParam = new SqlParameter("@VendorId", _vendorInfo.ID);
                SqlParameter nameParam = new SqlParameter("@VendorName", _vendorInfo.Name);
                SqlParameter isNameExists = new SqlParameter();
                isNameExists.Direction = ParameterDirection.ReturnValue;
                db.ExecuteCommand("IsVendorNameAlreadyExists", idParam, nameParam, isNameExists);
                if (Convert.ToBoolean(isNameExists.Value))
                {
                    commonSettingsErrorProvider.SetError(nameText, "Vendor with the same name already exists");
                    result = false;
                }
                if (_vendorInfo.ID != 12 && _vendorInfo.ID != 14 && _vendorInfo.ID != 16 && _vendorInfo.ID != 17)
                {
                    if (!string.IsNullOrEmpty(_vendorInfo.SkuPrefix))
                    {
                        // Validate duplicate sku prefix
                        SqlParameter idPrefixParam = new SqlParameter("@VendorId", _vendorInfo.ID);
                        SqlParameter namePrefixParam = new SqlParameter("@SkuPrefix", _vendorInfo.SkuPrefix);
                        SqlParameter isPrefixExists = new SqlParameter();
                        isPrefixExists.Direction = ParameterDirection.ReturnValue;
                        db.ExecuteCommand("IsSkuPrefixAlreadyExists", idPrefixParam, namePrefixParam, isPrefixExists);
                        if (Convert.ToBoolean(isPrefixExists.Value))
                        {
                            commonSettingsErrorProvider.SetError(prefixName, "Vendor with the same SKU prefix already exists");
                            result = false;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Save common settings for vendor
        /// </summary>
        /// <returns>True if save operation completed successfully, otherwise False</returns>
        public bool SaveVendorCommonSettings()
        {
            if (ValidateInput())
            {
                // Necessary because bindings are not working (may be because textboxes are read only)
                _vendorInfo.Folder = folderText.Text;
                _vendorInfo.FileArchive = fileArchiveText.Text;

                return _vendorInfo.Save();
            }

            return false;
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            commonSettingsFolderDialog.SelectedPath = folderText.Text;
            if (commonSettingsFolderDialog.ShowDialog() == DialogResult.OK)
            {
                folderText.Text = commonSettingsFolderDialog.SelectedPath;

                if (folderText.Text.Substring(folderText.Text.Length - 2, 1) != "\\")
                    folderText.Text += "\\";
            }
        }

        private void fileArchiveButton_Click(object sender, EventArgs e)
        {
            commonSettingsFolderDialog.SelectedPath = fileArchiveText.Text;
            if (commonSettingsFolderDialog.ShowDialog() == DialogResult.OK)
            {
                fileArchiveText.Text = commonSettingsFolderDialog.SelectedPath;

                if (fileArchiveText.Text.Substring(fileArchiveText.Text.Length - 2, 1) != "\\")
                    fileArchiveText.Text += "\\";
            }
        }
    }
}
