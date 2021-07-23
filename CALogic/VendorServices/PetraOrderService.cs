using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Linq;
using System.Net;
using System.Data.OleDb;
using System.ComponentModel;
using System.Data;
using Sgml;
using System.Configuration;
using ChannelAdvisor.Utility;

namespace ChannelAdvisor
{
    public class PetraOrderService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private HttpWebRequest request;
       
        private string _orderFtpUrl = "";
        private string _orderusername = "";
        private string _orderpassword = "";
        private string _folderName = "";
        private string _archiveName = "";

        /// <summary>
        /// 
        /// </summary>
        public PetraOrderService()
        {
       
            new DAL().GetPetraOrderInfo(out _orderFtpUrl,
                                        out _orderusername,
                                        out _orderpassword,
                                        out _folderName, out _archiveName);

         //   _localFolder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase).Replace("file:\\", "") + "\\Temp\\";
            VendorInfo = new DAL().GetVendor((int)VendorName.PetraOrderModule);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Petra"));
           // _localFileName = "tempcsv" + VendorInfo.ID.ToString() + ".csv";

        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return ProcessOrderFiles();
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            return ProcessOrderFiles();

        }//end method


        public InventoryUpdateServiceDTO ProcessOrderFiles()
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();

            string url = "ftp://" + _orderFtpUrl + "/" + _folderName +"/";  //  out
            string fileNamePrefix = "shipmentconfirmation";

            FTP ftp = new FTP(url, _orderusername, _orderpassword, false);
            // Get list of files
            List<string> files = ftp.GetFiles();
            //string filename;
            string fileString;
            invUpdateSrcvDTO.SuccesMessage = "No files found.";
            foreach (string file in files)
            {
                if (file.ToLower().EndsWith(".txt") && file.ToLower().StartsWith(fileNamePrefix))
                {
                    // call recreate
                    fileString= GetRecreatedOrderModule(file);
                    if (string.IsNullOrWhiteSpace(fileString))
                        invUpdateSrcvDTO.SuccesMessage = "Couldn't convert  Petra file: " + file;
                    else
                    {
                        if (RenameFile(file))
                            UploadFileFromString(fileString, file);
                        // move to archive
                        invUpdateSrcvDTO.SuccesMessage = "Petra files has been successfully converted.";
                    }
                }
                
            }
            invUpdateSrcvDTO.WithoutResult = true;

            return invUpdateSrcvDTO;
        }

        private string GetRecreatedOrderModule(string filename)
        {
           // string url = "ftp://" + _orderFtpUrl + "/out/" + filename;
            string url = "ftp://" + _orderFtpUrl + "/" + _folderName + "/" + filename;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            request.Credentials = new NetworkCredential(_orderusername, _orderpassword);

            request.Method = WebRequestMethods.Ftp.DownloadFile;

            string line;
            string[] row;
            var sb = new System.Text.StringBuilder();
            string currentLineCode = ""; // 1th OH, 2 OD 
            EmailManager em = new Utility.EmailManager();
             
            using (Stream stream = request.GetResponse().GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                currentLineCode = "";
                while (!reader.EndOfStream)
                {
                    // the righ order has to has OH line and OD line
                    // we set currendLineCode to to right value: the first =OH the second =OD
                    if (currentLineCode == "OH")
                        currentLineCode = "OD";
                    else
                        currentLineCode = "OH";

                    line = reader.ReadLine();
                    row = line.Split('\t');
                    // check if it is Petra reply
                    if (line.ToUpper().Contains("ORDER IS INCOMPLETE") )
                    {
                        //send Petra reply by mail
                        em.sendEmail(em.petraReplyErrorBody(filename,row[8]));
                        return "";
                    }
                    // check if the line is right.
                    if (row[0] == currentLineCode)
                    {
                        if (row[0] == "OH")
                            line += "\t";
                        else
                            line += Environment.NewLine;

                        sb.Append(line);
                    }
                    else
                    {
                        //send error mail
                        em.sendEmail(em.petraOrderServiceErrorBody(filename));
                        return "";
                    }

                }
                if (currentLineCode == "OH")
                {
                    //send error mail
                    em.sendEmail(em.petraOrderServiceErrorBody(filename));
                    return "";
                }
            }
            return sb.ToString();
        }
        private bool RenameFile(string filename)
        {
            try
            {
               // string url = "ftp://" + _orderFtpUrl + "/out/" + filename;
                string url = "ftp://" + _orderFtpUrl + "/" + _folderName + "/" + filename;

                //string urlArchive = "/out/archive/" + filename;
                string urlArchive = "/" + _folderName + "/archive/" + filename;
                Uri serverFile = new Uri(url);
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(url);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.Credentials = new NetworkCredential(_orderusername, _orderpassword);
                reqFTP.UseBinary = true;
                reqFTP.RenameTo = urlArchive;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private bool UploadFileFromString(string str,string filename)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(str);

                string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
                string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
                string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();

                //FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + _orderFtpUrl + "/out/" + filename);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ftpAddress + "/" + _archiveName + "/" +  filename);

                request.Credentials = new NetworkCredential(username, password);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UsePassive = true;
                request.KeepAlive = true;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                return true;
            }

            catch(Exception ex)
            {
                return false;
            }
        }

        private bool RenameFileOLD(string filename)
        {
            try
            {
                string url = "ftp://" + _orderFtpUrl + "/out/" + filename;
                string urlArchive = "ftp://" + _orderFtpUrl + "/out/archive/" + filename;
                Uri serverFile = new Uri(url);
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(serverFile);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.Credentials = new NetworkCredential(_orderusername, _orderpassword);
                reqFTP.RenameTo = urlArchive;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



    }//end class
}//end namespace
