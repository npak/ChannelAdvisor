using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using log4net;
using log4net.Config;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ChannelAdvisor
{
    public class FTP
    {
        public readonly ILog log = LogManager.GetLogger(typeof(FTP));

        private string _server;
        private string _username;
        private string _password;
        private bool enableSsl;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="server"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public FTP(string server, string username, string password, bool enableSsl = false)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            _server = server;
            _username = username;
            _password = password;
            this.enableSsl = enableSsl;
        }//end constructor

        /// <summary>
        /// Method to get files
        /// </summary>
        /// <returns></returns>
        public List<String> GetFilesOLD()
        {
            List<String> files = new List<string>();
            FtpWebRequest reqFTP;
           System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);
           // System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);

            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ServicePointManager_ServerCertificateValidationCallback);

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_server));
            reqFTP.UseBinary = true;
            reqFTP.EnableSsl = enableSsl;
           
            reqFTP.Credentials = new NetworkCredential(_username, _password);
            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            WebResponse response;
            try
            {
                response = reqFTP.GetResponse();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }


            StreamReader reader = new StreamReader(response.GetResponseStream());

            string line = reader.ReadLine();
            while (line != null)
            {
                files.Add(line);
                line = reader.ReadLine();
            }//end while

            reader.Close();
            response.Close();

            return files;
        }//end method

        // for test
        public List<String> GetFiles()
        {
            List<String> files = new List<string>();
            FtpWebRequest reqFTP;
            System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            // System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, Errors) => true);

            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ValidateServerCertificate);
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(ServicePointManager_ServerCertificateValidationCallback);

            // this works, because the protocol is included in the string

            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_server.Normalize()));
            reqFTP.UseBinary = true;
            reqFTP.EnableSsl = false; // enableSsl;

            reqFTP.Credentials = new NetworkCredential(_username.Normalize() , _password.Normalize());

            reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
            WebResponse response;
            try
            {
                response = reqFTP.GetResponse();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                throw;
            }


            StreamReader reader = new StreamReader(response.GetResponseStream());

            string line = reader.ReadLine();
            while (line != null)
            {
                files.Add(line);
                line = reader.ReadLine();
            }//end while

            reader.Close();
            response.Close();

            return files;
        }//end method

        //for test
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {

            System.Diagnostics.Debug.WriteLine(certificate);

            return true;

        }

        /// <summary>
        /// Method to download file from FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        public void WebClientDownloadFile(string fileName, string folderPath)
        {
            using (WebClient webClient = new WebClient())
            {
                _server = _server.Replace("//public_ftp", "/public_ftp");
                //ftp://132.148.245.57/public_ftp
                webClient.Credentials = new NetworkCredential(_username, _password);
  
                try
                {
                    webClient.DownloadFile(_server + fileName, folderPath + fileName);
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }//end try
            }


        }//end method

        /// <summary>
        /// Method to download file from FTP
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="folderPath"></param>
        public void DownloadFile(string fileName, string folderPath)
        {
            FtpWebRequest reqFTP;

            //folderPath = <<The full path where the file is to be created.>>, 
            //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
            using (FileStream outputStream = new FileStream(folderPath + fileName, FileMode.Create))
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_server + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_username, _password);
                
                FtpWebResponse response;

                try
                {
                    response = (FtpWebResponse)reqFTP.GetResponse();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }//end try


                //if (response == null) throw new Exception("Could not connect to FTP server.");

                Stream ftpStream = response.GetResponseStream();
                try
                {
                    long cl = response.ContentLength;
                    int bufferSize = 2048;
                    int readCount;
                    byte[] buffer = new byte[bufferSize];
                   
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                    while (readCount > 0)
                    {
                        outputStream.Write(buffer, 0, readCount);
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                    }
                }
                finally
                {
                    ftpStream.Close();
                    ftpStream.Dispose();
                    outputStream.Close();
                    outputStream.Dispose();
                    response.Close();

                }
            }
        }//end method

        public void DownloadFile(string fileNameFrom, string fileNameTo, string folderPath)
        {
            FtpWebRequest reqFTP;

            //folderPath = <<The full path where the file is to be created.>>, 
            //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
            using (FileStream outputStream = new FileStream(folderPath + "\\" + fileNameTo, FileMode.Create))
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(_server + fileNameFrom));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(_username, _password);
                FtpWebResponse response;

                try
                {
                    response = (FtpWebResponse)reqFTP.GetResponse();
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message);
                    throw ex;
                }//end try


                //if (response == null) throw new Exception("Could not connect to FTP server.");

                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
        }//end method

    }//end class

}//end namespace
