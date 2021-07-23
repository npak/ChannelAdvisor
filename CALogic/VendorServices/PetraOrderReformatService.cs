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
    public class PetraOrderReformatService : IVendorService
    {
        public Vendor VendorInfo { get; set; }
        private string ftpAddress = ConfigurationManager.AppSettings["CSVFTP"].ToString();
        private string username = ConfigurationManager.AppSettings["CSVFTPUser"].ToString();
        private string password = ConfigurationManager.AppSettings["CSVFTPPsw"].ToString();
        //private string host = "/home/inventory1";

        private HttpWebRequest request;
       
        private string _orderFtpUrl = "";
        private string _orderusername = "";
        private string _orderpassword = "";
        private string _inFolderName = "";
        private string _outFolderName = "";
        private string _archiveFolderName = "";
        public PetraOrderReformatService()
        {
       
            new DAL().GetPetraOrderReformatInfo(out _orderFtpUrl,
                                        out _orderusername,
                                        out _orderpassword,
                                        out _inFolderName, out _outFolderName, out _archiveFolderName);

            VendorInfo = new DAL().GetVendor((int)VendorName.PetraOrderReformat);
            if (VendorInfo == null)
                throw new Exception(string.Format("Cannot load data for Petra Order Reformat"));
        }//end constructor

        /// <summary>
        /// This method is called by the windows service. It is different from 
        /// the method called by PreviewAndUpdate as it does not update the markup prices.
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForService()
        {
            return ReformData();
        }//end method

        /// <summary>
        /// This method is called by the windows Preview And Update form. 
        /// </summary>
        /// <param name="vendorID"></param>
        /// <returns></returns>
        public InventoryUpdateServiceDTO GetInventoryListForPreviewAndUpdate(int profileID)
        {
            return ReformData();

        }//end method

        private InventoryUpdateServiceDTO ReformData()
        {
            InventoryUpdateServiceDTO invUpdateSrcvDTO = new InventoryUpdateServiceDTO();
            EmailManager em = new Utility.EmailManager();
            try
            {
                invUpdateSrcvDTO.WithoutResult = true;
                string uri = "ftp://" + ftpAddress + "/" + _inFolderName + "/";
                List<string> list = GetFilesList();
                FtpWebRequest ftpRequest;
                FtpWebResponse response;
                StreamReader streamReader;
                Boolean isFirst = true;
                string str = "";
                if (list.Count > 0)
                {
                    foreach (string filename in list)
                    {

                        ftpRequest = (FtpWebRequest)WebRequest.Create(uri + filename);
                        ftpRequest.Credentials = new NetworkCredential(username, password);
                        ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                        response = (FtpWebResponse)ftpRequest.GetResponse();
                        streamReader = new StreamReader(response.GetResponseStream());

                        if (!isFirst)
                        {
                            streamReader.ReadLine();
                        }
                        else
                            isFirst = false;

                        str += streamReader.ReadToEnd().Replace("\"", "");
                        streamReader.Close();

                    }
                    str += Environment.NewLine;
                    str += "DONE";
                    //
                    string uploadFileName = list[list.Count - 1];

                    if (UploadFileFromString(str, uploadFileName))
                    {
                        invUpdateSrcvDTO.SuccesMessage = "Petra Outgoing has beed reformatted";
                        foreach (string filename in list)
                        {
                            if (!RenameFile(filename))
                            {
                                invUpdateSrcvDTO.SuccesMessage = "Petra Outgoing file rename process was failed";
                                em.sendEmail(em.petraOrderReformatRenameErrorBody(filename)); 
                                break;
                            }
                        }
                    }
                    else
                    {
                        em.sendEmail(em.petraOrderReformatUploadErrorBody(uploadFileName)); 
                        invUpdateSrcvDTO.SuccesMessage = "Petra Outgoing file uploading was failed";
                    }
                }
                else
                    invUpdateSrcvDTO.SuccesMessage = "No files found.";
            }
            catch(Exception ex)
            {
                //send error mail
                invUpdateSrcvDTO.SuccesMessage = ex.Message;
                em.sendEmail(em.petraOrderReformatErrorBody());
            }

            return invUpdateSrcvDTO;
        }
        private List<string> GetFilesList()
        {
            string url = "ftp://" + ftpAddress + "/" + _inFolderName + "/";
            var reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(url));
            reqFTP.Credentials = new NetworkCredential(username, password);
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string ss = reader.ReadToEnd();
            // ss =response.StatusDescription;
            string[] ll = ss.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            List<string> list = new List<string>();
            foreach (string r in ll)
                if (r.Contains(".txt") && r.StartsWith("8007462670"))
                    list.Add(r);
            reader.Close();
            response.Close();

            return list;
        }
        private bool UploadFileFromString(string str, string filename)
        {
            try
            {
                System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
                Byte[] bytes = encoding.GetBytes(str);

                string url = "ftp://" + _orderFtpUrl + "/" + _outFolderName + "/" + filename;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);

                request.Credentials = new NetworkCredential(_orderusername, _orderpassword);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UsePassive = true;
                request.KeepAlive = true;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Close();

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        private bool RenameFile(string filename)
        {
            try
            {
                string url = "ftp://" + ftpAddress + "/" + _inFolderName + "/" + filename;

                //string urlArchive = "/out/archive/" + filename;
                string urlArchive = "/" + _archiveFolderName + "/" + filename;
                Uri serverFile = new Uri(url);
                FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(url);
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.Credentials = new NetworkCredential(username, password);
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


        //public void ProcessOrderFiles()
        //{
        //    string url = "ftp://" + _orderFtpUrl + "/" + _inFolderName +"/";  //  out
        //    string fileNamePrefix = "ShipmentConfirmation";

        //    FTP ftp = new FTP(url, _orderusername, _orderpassword, false);
        //    // Get list of files
        //    List<string> files = ftp.GetFiles();
        //    //string filename;
        //    string fileString;
        //    foreach (string file in files)
        //    {
        //        if (file.EndsWith(".txt") && file.StartsWith(fileNamePrefix))
        //        {
        //            // call recreate
        //            fileString= GetRecreatedOrderModule(file);
        //            if (RenameFile(file))
        //                UploadFileFromString(fileString, file); 
        //            // move to archive
        //        }
        //    }
        //}

        //private string GetRecreatedOrderModule(string filename)
        //{
        //   // string url = "ftp://" + _orderFtpUrl + "/out/" + filename;
        //    string url = "ftp://" + _orderFtpUrl + "/" + _inFolderName + "/" + filename;

        //    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
        //    request.Credentials = new NetworkCredential(_orderusername, _orderpassword);

        //    request.Method = WebRequestMethods.Ftp.DownloadFile;

        //    string line;
        //    string[] row;
        //    var sb = new System.Text.StringBuilder();
        //    using (Stream stream = request.GetResponse().GetResponseStream())
        //    using (StreamReader reader = new StreamReader(stream))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            line = reader.ReadLine();
        //            //row = line.Split(',');
        //            row = line.Split('\t');
        //            if (row[0] == "OH")
        //                line += "\t";
        //            else
        //                line += Environment.NewLine;
        //                //line = line.Replace("\r\n", "\t").Replace("\r", "\t").Replace("\n", "\t");
        //            sb.Append(line);

        //        }
        //    }
        //    return sb.ToString();
        //}


    }//end class
}//end namespace
