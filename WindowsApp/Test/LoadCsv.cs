
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.IO;

namespace ChannelAdvisor
{
    class LoadCsv
    {
        private HttpWebRequest request;
        private CookieContainer cookieContainer = new CookieContainer();
        private string _uid = "sales@bargainsdelivered.com";
        private string _pwd = "bdentry2";
        private string _url1 = "https://prod.petra.com/index.php/index/login";
        private string _urldwl = "https://prod.petra.com/index.php/index/download";

        public bool doLogin(string uid, string pwd, string url)
        {
            // Create a request using a URL that can receive a post. 
            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;

            // Set the Method property of the request to POST.
            request.Method = "POST";

            // Set the ContentType property of the WebRequest.
            request.ContentType = "application/x-www-form-urlencoded";

            // Create POST data and convert it to a byte array.
            string postData = "username=" + uid + "&password=" + pwd;
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            // Set the ContentLength property of the WebRequest.
            request.ContentLength = byteArray.Length;
            request.KeepAlive = true;

            // Get the request stream.
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            response.Close();
            return true;

        }

        public void downloadFile(string url, string fileName)
        {
            // Create a request using a URL that can receive a post. 
            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;

            // Set the Method property of the request to GET.
            request.Method = "GET";

            // Get the response.
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(responseStream))
                    {
                        using (StreamWriter writer = new StreamWriter(fileName, false))
                        {
                            writer.Write(reader.ReadToEnd());
                            writer.Flush();
                            writer.Close();
                        }
                    }
                    responseStream.Close();
                }
                response.Close();
            }
        }

        public void doLogout(string url)
        {
            // Create a request using a URL that can receive a post. 
            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.CookieContainer = cookieContainer;

            // Set the Method property of the request to POST.
            request.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            response.Close();
        }
        
        public void DoTest()
        {
            if (doLogin(_uid, _pwd, _url1))
                downloadFile(_urldwl, "E:\\papa\\Myprodlist.csv");
        }
      
    }
}