using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ChannelAdvisor.Utility
{
    public class EmailManager
    {
 
        public string adminemail = System.Configuration.ConfigurationManager.AppSettings["adminemail"].ToString();
        public string host = System.Configuration.ConfigurationManager.AppSettings["smtpHost"].ToString();
        public int smtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtport"].ToString());
        public string userName = System.Configuration.ConfigurationManager.AppSettings["smtemail"].ToString();
        public string password = System.Configuration.ConfigurationManager.AppSettings["smtpassword"].ToString();

        string subject = "Notifications from BD Updater app for " + DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

        /// <summary>
        ///  On production we use GoDaddy email server.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="subject"></param>
        /// <param name="email"></param>

        //channelad2019@gmail.com
        //channelAdvisor_01
        public string sendEmail(string body)
        {

            try
            {
                var credentials = new NetworkCredential(userName, password);

                var client = new System.Net.Mail.SmtpClient()
                {
                    Port = smtpPort, //587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = host,
                    EnableSsl = false,
                    Credentials = credentials,
                    
                };

                var mail = new MailMessage()
                {
                    From = new System.Net.Mail.MailAddress("no-reply@goddady.com", "no-reply BD Updater"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(new System.Net.Mail.MailAddress(adminemail));

                client.Send(mail);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string sendEmail(string body, string subject, string email)
        {

            try
            {
                var credentials = new NetworkCredential(userName, password);

                var client = new System.Net.Mail.SmtpClient()
                {
                    Port = smtpPort, //587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Host = host,
                    EnableSsl = false,
                    Credentials = credentials
                };

                var mail = new MailMessage()
                {
                    From = new System.Net.Mail.MailAddress("no-reply@goddady.com", "no-reply BD Updater"),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(new System.Net.Mail.MailAddress(email));

                client.Send(mail);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #region Morris XML Creator Messages
        public string morrisXmlUploadErrorBody(string filename)
        {
            string body = "File Upload Failed.";
            body += "<br />Ftp folder name: Ready to Upload"; 
            body += "<br />File name: " + filename;
            return body;
        }

        public string morrisXmlPostErrorBody(string order)
        {
            string body = "Order Post Failed.";
            body += "<br />Order # : " + order;
            //body += "<br />Order: " + order;
            return body;
        }
        public string morrisXmlStreetCharacterlimitErrorBody(string filename, string poNumber)
        {
            //error for being over character limit
            string body = "Steet1/Street2. Error for being over character limit.";
            body += "<br /><br />File name: " + filename;
            body += "<br /><br />Order nuber: " + poNumber;
            return body;
        }

        public string morrisXmlLineCharacterlimitErrorBody(string filename, string poNumber)
        {
            //error for being over character limit
            string body = "Line1. Error for being over character limit.";
            body += "<br /><br />File name: " + filename;
            body += "<br /><br />Order nuber: " + poNumber;
            return body;
        }
        public string morrisXmlMovePartOfLine1Body(string filename, string poNumber)
        {
            string body = "Moved part of street1 to street2";
            body += "<br /><br />File name: " + filename;
            body += "<br /><br />Order nuber: " + poNumber;
            return body;
        }
        #endregion

        #region Petra Order Service Messages 
        public string petraOrderServiceErrorBody(string filename)
        {
            string body = "Order processing  is failed.";
            //body += "<br />Ftp folder name: Ready to Upload";
            body += "<br />File name: " + filename;
            return body;
        }

        public string petraReplyErrorBody(string filename, string errorMessage)
        {
            string body = "Order processing  is failed.";
            //body += "<br />Ftp folder name: Ready to Upload";
            body += "<br />File name: " + filename;
            body += "<br /> " + errorMessage;

            return body;
        }
        #endregion

        #region Petra Order Reformat Messages 
        public string petraOrderReformatErrorBody()
        {
            string body = "Petra Outgoing processing  is failed.";
            //body += "<br />File name: " + filename;
            return body;
        }

        public string petraOrderReformatUploadErrorBody(string filename)
        {
            string body = "Order Outgoing file uploading  is failed.";
            body += "<br />File name: " + filename;
            return body;
        }

        public string petraOrderReformatRenameErrorBody(string filename)
        {
            string body = "Order Outgoing file renaming  is failed.";
            body += "<br />File name: " + filename;
            return body;
        }

        #endregion

    }

}
