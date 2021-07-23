using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace ChannelAdvisor
{
    public partial class Settings : Form
    {
        DAL dal = new DAL();

        public Settings()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Load(object sender, EventArgs e)
        {
            DisplayValues();


        }//end event

        /// <summary>
        /// 
        /// </summary>
        private void DisplayValues()
        {
            DataTable dt = dal.GetSettings().Tables[0];

            string isAutoUpdateDisabled = "";
            

            //get autoupate
            DataRow[] dr = dt.Select("Key = '" + SettingsConstant.Auto_Update_Disable + "'");
            if (dr.GetLength(0) > 0)
            {
                isAutoUpdateDisabled = dr[0]["Value"].ToString();

                if (isAutoUpdateDisabled == "1")
                {
                    chkDisableAutoUpdate.Checked = true;
                }
            }//end if


            //get max skus
            dr = dt.Select("Key = '" + SettingsConstant.Max_SKU_Update + "'");
            if (dr.GetLength(0) > 0)
            {
             
                txtMaxSkusToUpdate.Text = dr[0]["Value"].ToString().Trim();
            }//end if

            //get negative qty
            dr = dt.Select("Key = '" + SettingsConstant.Inventory_Negative_QTY + "'");
            if (dr.GetLength(0) > 0)
            {
                txtNegativeQty.Text = dr[0]["Value"].ToString().Trim().Replace("-","");
            }//end if

            //get negative qty check
            dr = dt.Select("Key = '" + SettingsConstant.Inventory_Negative_QTY_Check + "'");
            if (dr.GetLength(0) > 0)
            {
                txtNegativeQtyCheck.Text = dr[0]["Value"].ToString().Trim();
            }//end if

            //get cache update interval
            dr = dt.Select("Key = '" + SettingsConstant.Cache_Update_Interval + "'");
            if (dr.GetLength(0) > 0)
            {
                DisplayTime(dr[0]["Value"].ToString());
            }//end if

        }//end method


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(IsValidated())
            {
                if(SaveValues())
                {
                    Util.ShowMessage("Record Saved!");
                }
                else
                {
                    Util.ShowMessage("There was some problem while trying to save the record. The record could not be saved.");
                }

            }//end if
            
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool SaveValues()
        {
            bool isSaved = false;   
            //save auto update
            string autoUpdate="0";

            if(chkDisableAutoUpdate.Checked == true) autoUpdate = "1";
            isSaved = dal.UpdateSetting(SettingsConstant.Auto_Update_Disable, autoUpdate);
            if(!isSaved) return false;

            //save max no of skus to update
            isSaved = dal.UpdateSetting(SettingsConstant.Max_SKU_Update, txtMaxSkusToUpdate.Text.Trim());
            if (!isSaved) return false;

            //save negative qty
            isSaved = dal.UpdateSetting(SettingsConstant.Inventory_Negative_QTY,"-" + txtNegativeQty.Text.Trim());
            if (!isSaved) return false;

            //save negative qty check
            isSaved = dal.UpdateSetting(SettingsConstant.Inventory_Negative_QTY_Check, txtNegativeQtyCheck.Text.Trim());
            if (!isSaved) return false;

            //save cache update interval
            isSaved = dal.UpdateSetting(SettingsConstant.Cache_Update_Interval, StringTime());
            if (!isSaved) return false;

            return isSaved;
        }//end method

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool IsValidated()
        {
            if (txtMaxSkusToUpdate.Text.Trim() == "")
            {
                Util.ShowMessage("Please enter Max No of SKU's to Update At A Single Time");
                txtMaxSkusToUpdate.Focus();
                return false;
            }
            if(!Regex.IsMatch(txtMaxSkusToUpdate.Text.Trim(), @"^\d+$"))
            {
                Util.ShowMessage("Please enter a valid number for Max No of SKU's to Update At A Single Time");
                txtMaxSkusToUpdate.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtNegativeQty.Text.Trim(), @"^\d+$"))
            {
                Util.ShowMessage("Please enter a valid number for 'Send Qty As'");
                txtNegativeQty.Focus();
                return false;
            }

            if (!ValidateTime())
            {
                Util.ShowMessage("Please enter a valid data for 'Cache Reload Shedule'");
                return false;
            }

            return true;
        }

        private bool ValidateTime()
        {
            if (txtSun.Enabled)
            {
                if (!IsValidTime(txtSun.Text))
                    return false;
            }
            if (txtMon.Enabled)
            {
                if (!IsValidTime(txtMon.Text))
                    return false;
            }
            if (txtTue.Enabled)
            {
                if (!IsValidTime(txtTue.Text))
                    return false;
            }
            if (txtWed.Enabled)
            {
                if (!IsValidTime(txtWed.Text))
                    return false;
            }
            if (txtThu.Enabled)
            {
                if (!IsValidTime(txtThu.Text))
                    return false;
            }
            if (txtFri.Enabled)
            {
                if (!IsValidTime(txtFri.Text))
                    return false;
            }
            if (txtSat.Enabled)
            {
                if (!IsValidTime(txtSat.Text))
                    return false;
            }
            return true;
        }

        private string StringTime()
        {
            string str ="";
            if (txtSun.Enabled)
            {
               str +="0;"+ txtSun.Text+",";
            }
            if (txtMon.Enabled)
            {
                str += "1;" + txtMon.Text + ",";
            }
            if (txtTue.Enabled)
            {
                str += "2;" + txtTue.Text + ",";
            }
            if (txtWed.Enabled)
            {
                str += "3;" + txtWed.Text + ",";
            }
            if (txtThu.Enabled)
            {
                str += "4;" + txtThu.Text + ",";
            }
            if (txtFri.Enabled)
            {
                str += "5;" + txtFri.Text + ",";
            }
            if (txtSat.Enabled)
            {
                str += "6;" + txtSat.Text + ",";
            }
            if (str.Length>0)
                str = str.Remove(str.Length - 1);
            return str;
        }

        private void DisplayTime(string str)
        {
            string[] rows = str.Split(',');
            string[] timerow;
            foreach (string r in rows)
            {
                timerow = r.Split(';');
                switch (timerow[0])
                {
                    case "0":
                        checkBoxSun.Checked = true;
                        txtSun.Text = timerow[1];
                        break;

                    case "1":
                        checkBoxMon.Checked = true;
                        txtMon.Text = timerow[1];
                        break;
                    case "2":
                        checkBoxTue.Checked = true;
                        txtTue.Text = timerow[1];
                        break;
                    case "3":
                        checkBoxWed.Checked = true;
                        txtWed.Text = timerow[1];
                        break;
                    case "4":
                        checkBoxThu.Checked = true;
                        txtThu.Text = timerow[1];
                        break;
                    case "5":
                        checkBoxFri.Checked = true;
                        txtFri.Text = timerow[1];
                        break;
                    case "6":
                        checkBoxSat.Checked = true;
                        txtSat.Text = timerow[1];
                        break;
                }
            }
            
        }

        private bool IsValidTime(string thetime)
        {
            Regex checktime =
                new Regex(@"^(?:0?[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

            return checktime.IsMatch(thetime);
        }
        private void checkBoxSun_CheckedChanged(object sender, EventArgs e)
        {
            txtSun.Enabled = checkBoxSun.Checked;
        }

        private void checkBoxMon_CheckedChanged(object sender, EventArgs e)
        {
            txtMon.Enabled = checkBoxMon.Checked;
        }

        private void checkBoxTue_CheckedChanged(object sender, EventArgs e)
        {
            txtTue.Enabled = checkBoxTue.Checked;
        }

        private void checkBoxWed_CheckedChanged(object sender, EventArgs e)
        {
            txtWed.Enabled = checkBoxWed.Checked;
        }

        private void checkBoxThu_CheckedChanged(object sender, EventArgs e)
        {
            txtThu.Enabled = checkBoxThu.Checked;
        }

        private void checkBoxFri_CheckedChanged(object sender, EventArgs e)
        {
            txtFri.Enabled = checkBoxFri.Checked;
        }

        private void checkBoxSat_CheckedChanged(object sender, EventArgs e)
        {
            txtSat.Enabled = checkBoxSat.Checked;
        }//end method

    }//end class

}//end namespace
