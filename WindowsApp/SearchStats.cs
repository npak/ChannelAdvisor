using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChannelAdvisor
{
    public partial class SearchStats : Form
    {
        DAL dal = new DAL();

        public SearchStats()
        {
            InitializeComponent();
        }

        private void SearchStats_Load(object sender, EventArgs e)
        {
            profileCombo.DataSource = dal.GetProfiles().Tables[0];
            profileCombo.DisplayMember = "Profile";
            profileCombo.ValueMember = "ID";
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (profileCombo.SelectedIndex < 0)
                {
                    Util.ShowMessage("Please choose profile");
                    return;
                }

                startButton.Text = "Please wait...";
                startButton.Enabled = false;

                SearchAnalysisResult searchResult = StoreService.GetSearchAnalisisResult(Convert.ToInt32(profileCombo.SelectedValue));
                //
                phraseGrid.AutoGenerateColumns = false;
                phraseGrid.DataSource = searchResult.PhraseOccurrence;
                phraseGrid.Columns[0].DataPropertyName = "SearchCondition";
                phraseGrid.Columns[1].DataPropertyName = "SearchCount";
                //
                skuGrid.AutoGenerateColumns = false;
                skuGrid.DataSource = searchResult.SkuOccurrence;
                skuGrid.Columns[0].DataPropertyName = "SearchCondition";
                skuGrid.Columns[1].DataPropertyName = "SearchCount";
                //
                termGrid.AutoGenerateColumns = false;
                termGrid.DataSource = searchResult.TermOccurrence;
                termGrid.Columns[0].DataPropertyName = "SearchCondition";
                termGrid.Columns[1].DataPropertyName = "SearchCount";

                label5.Text = searchResult.TotalCount.ToString();
                label4.Visible = true;
                label5.Visible = true;
            }
            catch (Exception ex)
            {
                Util.ShowMessage(ex.Message);
            }
            finally
            {
                startButton.Text = "Get statistics";
                startButton.Enabled = true;
            }
        }
    }
}
