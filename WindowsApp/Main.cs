using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Xml.XPath;
using System.Xml;
using System.Xml.Linq;
using log4net;
using System.Data.OleDb;
using HtmlAgilityPack;

using System.Threading;
//
using System.Configuration;
using System.Linq;
using System.Globalization;
//
using MarketplaceWebServiceProducts;
using ChannelAdvisor.Objects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ChannelAdvisor;
using System.Net.Mail;

namespace ChannelAdvisor
{
    public partial class Main : Form
    {
        private bool counter = true;
        Thread threadShipstation;

        private bool mwsCounter = true;
        Thread threadAmazonAPI;

        private List<RateToOutput> _listRate = new List<RateToOutput>();
        private List<MwsResult> _listMwsResult = new List<MwsResult>();

        public readonly ILog log = LogManager.GetLogger(typeof(Main));
        public List<StockItem> AllItems = new List<StockItem>();
       
        //private HttpWebRequest request;
        private CookieContainer cookieContainer = new CookieContainer();
        //string cookieHeader = "";

        string mWynitScraper = @"c:\users\public\developer\wynit extractor\scraper\WynitWebScraper.exe";
        string mShippingSpreadsheetCreator = @"c:\users\public\developer\shipping spreadsheet creator\ShippingSpreadsheetCreator.exe";
 
        public Main()
        {
            InitializeComponent();
            // FIX BUG: if Temp directory not exists, then we get the message "Cannot read filename.xls file"
            string tempPath = Application.StartupPath + "\\Temp";
            if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuEMG_Click(object sender, EventArgs e)
        {
            EMGSettings emgSettings = new EMGSettings();
            emgSettings.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuProfiles_Click(object sender, EventArgs e)
        {
            Profiles profiles = new Profiles();
            profiles.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFrequency_Click(object sender, EventArgs e)
        {
            Frequency freq = new Frequency();
            freq.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuWebService_Click(object sender, EventArgs e)
        {
            WebServiceURLs webServ = new WebServiceURLs();
            webServ.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuMarkup_Click(object sender, EventArgs e)
        {
            PricingMarkup markup = new PricingMarkup();
            markup.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuPreview_Click(object sender, EventArgs e)
        {
            PreviewAndUpdate preview = new PreviewAndUpdate();
            preview.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuViewLogs_Click(object sender, EventArgs e)
        {
            Logs log = new Logs();
            log.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuDuplicateSKUs_Click(object sender, EventArgs e)
        {
            DuplicateSKUs dupSKUs = new DuplicateSKUs();
            dupSKUs.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

        private void mnuVendorProfiles_Click(object sender, EventArgs e)
        {
            VendorProfiles vendorProfiles = new VendorProfiles();
            vendorProfiles.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            //test 
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Resize(object sender, System.EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
            }//end if
        }//end 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon_DoubleClick(object sender, System.EventArgs e)
        {
            Visible = true;
            notifyIcon.Visible = false;
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void mnuWynit_Click(object sender, EventArgs e)
        {
            WynitSettings wynit = new WynitSettings();
            wynit.ShowDialog();
        }


        private void mnuOtherVendors_Click(object sender, EventArgs e)
        {
            DynamicVendors dynamicVendors = new DynamicVendors();
            dynamicVendors.ShowDialog();
        }


        private void instepToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacificCycleSettings pacificCycle = new PacificCycleSettings();
            pacificCycle.ShowDialog();
        }

        private void inlineToysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PacificCycleInlineToysSettings settings = new PacificCycleInlineToysSettings();
            settings.ShowDialog();
        }

        private void mnuDressUpAmerica_Click(object sender, EventArgs e)
        {
            DressUpAmericaSettings dressUpAmerica = new DressUpAmericaSettings();
            dressUpAmerica.ShowDialog();
        }

        
        //private void mnuPicnicTime_Click(object sender, EventArgs e)
        //{
        //    PicnicTimeSettings picnicTime = new PicnicTimeSettings();
        //    picnicTime.ShowDialog();
        //}

        private void mnuSumdex_Click(object sender, EventArgs e)
        {
            SumdexSettings sumdex = new SumdexSettings();
            sumdex.ShowDialog();
        }

        //private void mnuCWR_Click(object sender, EventArgs e)
        //{
        //    CWRSettings cwr = new CWRSettings();
        //    cwr.ShowDialog();
        //}

        private void mnuEMGOrderUpdateSettings_Click(object sender, EventArgs e)
        {
            EMGOrderUpdateSettings emg = new EMGOrderUpdateSettings();
            emg.ShowDialog();
        }

        private void mnuEMGFrequency_Click(object sender, EventArgs e)
        {
            EMGFrequency emg = new EMGFrequency();
            emg.ShowDialog();
        }//end

        private void mnuTransportCodes_Click(object sender, EventArgs e)
        {
            TransportCodes transportCodes = new TransportCodes();
            transportCodes.ShowDialog();
        }

        private void mnuOrderUpdateLogs_Click(object sender, EventArgs e)
        {
            EMGOrderUpdateLogs logs = new EMGOrderUpdateLogs();
            logs.ShowDialog();
        }

        private void mnuGetOrderStatusLogs_Click(object sender, EventArgs e)
        {
            EMGOrderStatusLogs logs = new EMGOrderStatusLogs();
            logs.ShowDialog();
        }

        private void mnuEMGOrdersSent_Click(object sender, EventArgs e)
        {
            EMGPendingOrders pendingOrders = new EMGPendingOrders();
            pendingOrders.ShowDialog();
        }

        private void mnuBasicData_Click(object sender, EventArgs e)
        {
            WynitBasicData lWynitBasicData = new WynitBasicData();
            lWynitBasicData.ShowDialog();
        }

        private void mnuEMGExtractor_Click(object sender, EventArgs e)
        {
            try
            {
                EMGExtractor lEMGExtrator = new EMGExtractor();
                if (lEMGExtrator != null) lEMGExtrator.ShowDialog();
            }
            catch { }
        }

        private void mnuWynitWebScraper_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo lInfo = new ProcessStartInfo();
                lInfo.FileName = mWynitScraper;
                string lFolderName = mWynitScraper.Substring(0, mWynitScraper.LastIndexOf(@"\"));
                lInfo.WorkingDirectory = lFolderName;
                Process.Start(lInfo);
            }
            catch 
            {
                Util.ShowMessage("Wynit Scraper not found");
            }
        }

        private void shippingSpreadsheetCreatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessStartInfo lInfo = new ProcessStartInfo();
                lInfo.FileName = mShippingSpreadsheetCreator;
                string lFolderName = mShippingSpreadsheetCreator.Substring(0, mShippingSpreadsheetCreator.LastIndexOf(@"\"));
                lInfo.WorkingDirectory = lFolderName;
                Process.Start(lInfo);
            }
            catch
            {
                Util.ShowMessage("Shipping Spreadsheet Creator not found");
            }
        }

        #region Picnic Time
        private void menuPicnicTimeSettings_Click(object sender, EventArgs e)
        {
            PicnicTimeSettings picnicTime = new PicnicTimeSettings();
            picnicTime.ShowDialog();
        }

        private void menuPicnicTimeExtractor_Click(object sender, EventArgs e)
        {
            PicnicTimeExtractor lPTE = new PicnicTimeExtractor();
            lPTE.ShowDialog(this);
        }
        #endregion

        private void mnuHaier_Click(object sender, EventArgs e)
        {
            HaierSettings haier = new HaierSettings();
            haier.ShowDialog();
        }

        #region Sunpentown
        private void sunpentownExtractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SunpentownExtractor extractor = new SunpentownExtractor();
            extractor.ShowDialog();
        }
        #endregion

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SearchStats form = new SearchStats();
            form.ShowDialog(this);
        }

        private void cWRSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CWRSettings cwr = new CWRSettings();
            cwr.ShowDialog();
        }

        private void cWRExtractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CWRExtractor extractor = new CWRExtractor();
            extractor.ShowDialog();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RJTSettings settings = new RJTSettings();
            settings.ShowDialog();
        }

        

        private void mnuKwikTek_Click(object sender, EventArgs e)
        {
           KwikTekSettings form = new KwikTekSettings();
            form.ShowDialog();
        }

        private void oceanstarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OceanStarSettings frm = new OceanStarSettings();
            frm.ShowDialog();
        }

        private void RockLinetoolStripMenuItem_Click(object sender, EventArgs e)
        {
            RockLineSettings rockLine = new RockLineSettings();
            rockLine.ShowDialog();
        }

        private void morrisUpdaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisNightlySettings morris = new MorrisNightlySettings();
            morris.ShowDialog();
        }

        private void morrisExtractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisNightlyExtractor morris = new MorrisNightlyExtractor();
            morris.ShowDialog();
        }

        private void petraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PetraSettings petra = new PetraSettings();
            petra.ShowDialog();
        }

        private static void GetPaths(out string pathtemp, out string messagefile)
        {
            //path = Path.Combine(Path.GetTempPath(), "inventoryCache");
            //inventoryPath = Path.Combine(path, "allItems");
            string path = Path.Combine(Application.StartupPath, "inventoryCache");
            pathtemp = path + "temp";
            messagefile = Path.Combine(pathtemp, "message.txt");
        }

        private void CreateMessagefile(string filepath)
        {
            if (!System.IO.File.Exists(filepath))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(filepath))
                {
                    fs.WriteByte(1);
                }
            }
        }

    
        private void downloadCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string pathtemp;
            string messagefile;
            GetPaths(out pathtemp, out messagefile);
            if (!File.Exists(messagefile))
            {
                CreateMessagefile(messagefile);
                WaitDialogWithWork dialogWithWork = new WaitDialogWithWork();
                dialogWithWork.ShowWithWork(() =>
                {
                    CheckCacheState(messagefile);
                });

                Util.ShowMessage("Cache has been updated.");
            }
            else
                Util.ShowMessage("Cache is being updated.");
        }

        private void CheckCacheState(string messagefile)
        {
            if (WaitDialogWithWork.Current != null)
                WaitDialogWithWork.Current.ShowMessage(string.Format("Downloading cache, please wait..."));
            while (File.Exists(messagefile))
            {
                ;
            }
        }

        private void morrisCompleteSettongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisCompleteSettings form = new MorrisCompleteSettings();
            form.ShowDialog();
        }

        private void morrisCompleteExToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisCompleteExtractor morris = new MorrisCompleteExtractor();
            morris.ShowDialog();
        }

        private void petGearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PetGearSettings form = new PetGearSettings();
            form.ShowDialog();
        }

        private void benchmarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BenchmarkSettings form = new BenchmarkSettings();
            form.ShowDialog();
        }

        private void IsDateFormat(string str)
        {
            var date_regex = @"^(([1-9])|(0[1-9])|(1[0-2]))\/(([1-9])|(0[1-9])|(1\d)|(2\d)|(3[01]))\/((\d\d)||((19|20)\d\d))$";
            str = str.Replace(".", "/").Replace("-", "/");
            if (System.Text.RegularExpressions.Regex.IsMatch(str, date_regex))
                MessageBox.Show("Yse");
            else
                MessageBox.Show("No"); //s = "no";

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisChangesSettings form = new MorrisChangesSettings();
            form.ShowDialog();
        }

        private void extractorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MorrisChangesExtractor form = new MorrisChangesExtractor();
            form.ShowDialog();
        }

        private void reviewCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreviewCache form = new PreviewCache();
            form.ShowDialog();
        }

        private void mnuAZ_Click(object sender, EventArgs e)
        {
            AZSettings form = new AZSettings();
            form.ShowDialog();
        }

        private void mnuShipstationSettings_Click(object sender, EventArgs e)
        {
            ShipstationSettings form = new ShipstationSettings();
            form.ShowDialog();
        }

        private void getRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Shipstation frm = new Shipstation();
            frm.ShowDialog();
        }

        #region Shipstation thread
        
        private void getRate2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            threadShipstation = new Thread(new ThreadStart(GetRateList));
            threadShipstation.IsBackground = true;
            threadShipstation.Start();
            timer1.Enabled = true;

        }

        private void GetRateList()
        {
            ShipStationService sss = new ShipStationService();
            _listRate = sss.GetRatesOutput();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (threadShipstation.IsAlive)
            {
                label1.Text = "The Shipstation API is running ...";
                if (counter)
                { 
                    counter =false;
                    label1.ForeColor = Color.Green;
                }
                else
                   {
                        counter = true;
                        label1.ForeColor= Color.GreenYellow;
                    }
            }
            else
            {
                timer1.Enabled = false;

                threadShipstation.Abort();
                RunShipstation frm = new RunShipstation(_listRate);
                frm.Show();
                label1.Text = "";
            }
        }
        
        #endregion

        private void settingServicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShipstationSettingServices frm = new ShipstationSettingServices();
            frm.ShowDialog();
        }

        private void morrisDailySummary_Click(object sender, EventArgs e)
        {
            MorrisDailySummarySettings frm = new MorrisDailySummarySettings();
            frm.ShowDialog();
        }

        private void mnuGreenSupply_Click(object sender, EventArgs e)
        {
            GreenSupplySettings frm = new GreenSupplySettings();
            frm.ShowDialog();
        }

        private void mnuViking_Click(object sender, EventArgs e)
        {
            VikingSettings frm = new VikingSettings();
            frm.ShowDialog();
        }

        private void mnuNearlyNatural_Click(object sender, EventArgs e)
        {
            NearlyNaturalSettings frm = new NearlyNaturalSettings();
            frm.ShowDialog();
        }

        private void mnuMoteng_Click(object sender, EventArgs e)
        {
            MotengSettings frm = new MotengSettings();
            frm.ShowDialog();
        }
        private void amazonAPISeetingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AmazonMWSSettings fm = new AmazonMWSSettings();
            fm.ShowDialog(); 
        }

        #region Amazon API
        
        private void mnuAmazonAPI_Click(object sender, EventArgs e)
        {
            threadAmazonAPI  = new Thread(new ThreadStart(GetAmazonProducts));
            threadAmazonAPI.IsBackground = true;
            threadAmazonAPI.Start();
            mwsTimer.Enabled = true;
        }

         private void GetAmazonProducts()
        {

            string marketplaceId = "ATVPDKIKX0DER";
            string idType = "UPC";
            AmazonMarketplaceWebServiceProducts obj = new AmazonMarketplaceWebServiceProducts(marketplaceId, idType);
             
            _listMwsResult = obj.GetData();
         
             // upload reult to ftp
            obj.UploadAmazonFile();
        }

         private void mwsTimer_Tick(object sender, EventArgs e)
         {
             if (threadAmazonAPI.IsAlive)
             {
                 mwsFlag.Text = "The Amazon API is running ...";
                 if (mwsCounter)
                 {
                     mwsCounter = false;
                     mwsFlag.ForeColor = Color.DarkRed; // .Green;
                 }
                 else
                 {
                     mwsCounter = true;
                     mwsFlag.ForeColor = Color.RosyBrown ;// .GreenYellow;
                 }
             }
             else
             {
                 mwsTimer.Enabled = false;

                 threadAmazonAPI.Abort();
                 //  
                 //  form Show();
                 MWSSearch form = new MWSSearch(_listMwsResult);
                 form.Show();
                 mwsFlag.Text = "";
             }
         }

         private void morrisXMLCreatorSettingsToolStripMenuItem_Click(object sender, EventArgs e)
         {
             MorrisXMLCreatorSettings form = new MorrisXMLCreatorSettings();
             form.ShowDialog();
         }

        private void blockedSKUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BlockedSKUs form = new BlockedSKUs();
            form.ShowDialog();
        }
        private void mnuMorrisXMLCreator_Click(object sender, EventArgs e)
         {
            ChannelAdvisor.MorrisXMLCreatorService serv = new MorrisXMLCreatorService();
           //serv.PostXml("121925.xml"); 
             serv.CreateXMLDoc();
             MessageBox.Show("Morris XML Creator processed csv files.");
         }

        private void mnuPetraOrderModule_Click(object sender, EventArgs e)
        {
            PetraOrderSettings form = new PetraOrderSettings();
            form.ShowDialog();
        }

        private void mnuPetraOrderReformat_Click(object sender, EventArgs e)
        {
            PetraOrderReformatSettings form = new PetraOrderReformatSettings();
            form.ShowDialog();
        }


        private void mnuSeawide_Click(object sender, EventArgs e)
        {
            SeawideSettings form = new ChannelAdvisor.SeawideSettings();
            form.ShowDialog();  
        }

        private void mnuTWH_Click(object sender, EventArgs e)
        {
            TWHSettings form = new ChannelAdvisor.TWHSettings();
            form.ShowDialog();  
        }
        private void lWAPISettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LWAPISettings form = new LWAPISettings();
            form.ShowDialog();
        }

        private void lWAPIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            LWAPIService serv = new ChannelAdvisor.LWAPIService();
            string ss = serv.GetCSVString();
            serv.UploadFileFromString(ss);
            Cursor.Current = Cursors.AppStarting;
            MessageBox.Show("Orders detail csv file has been uploaded on ftp server.");
        }
        private void morrisWeeklySummary_Click(object sender, EventArgs e)
        {
            MorrisWeeklySummarySettings form = new ChannelAdvisor.MorrisWeeklySummarySettings();
            form.ShowDialog();  
        }
        private void morrisTodaySummary_Click(object sender, EventArgs e)
        {
            // process today files. set date lag =0
            MorrisDailySummaryService morrisobj = new MorrisDailySummaryService();
            morrisobj.Datelag = 0;
            try
            {
                morrisobj.GererateCsv();
                MessageBox.Show("Morris Daily Summary completed."); 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Morris Daily Summary failed."); 
            }
        }
        private void morrisXmlCreateOrder_Click(object sender, EventArgs e)
        {
            ChannelAdvisor.MorrisXMLCreatorService serv = new MorrisXMLCreatorService();
            serv.CreateXMLDoc();
            MessageBox.Show("Morris XML Creator processed csv files.");
        }

        private void shipstationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

    }//end class

    #endregion

}//end namespace
