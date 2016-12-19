using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Collections;

using System.IO;

using System.Net;
using System.Security.Cryptography;

   
using System.Net.Security;
using System.Security.Cryptography.X509Certificates; 
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace httpDll
{

    public interface httpDllInterface
    {
        string HttpPost(string url, string data);
        string HttpGet(string url, string data);
    }


    [ClassInterface(ClassInterfaceType.None)]
    public class httpDllClass : httpDllInterface
    {
        public string HttpPost(string url, string data, int timeOutSeconds = 10)
        {
            try
            {

                HttpWebRequest request;

                //如果是发送HTTPS请求   
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(data);
                //request.CookieContainer = cookie;
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(data);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                //response.Cookies = cookie.GetCookies(response.ResponseUri);
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        private bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受   
        }

        public string HttpGet(string url, string data)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url + (data == "" ? "" : "?") + data);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region httpDllInterface 成员


        string httpDllInterface.HttpGet(string url, string data)
        {
            httpDllClass TemHttpGet = new httpDllClass();
            return TemHttpGet.HttpGet(url, data);
        }

        string httpDllInterface.HttpPost(string url, string data)
        {
            httpDllClass TemHttpPost = new httpDllClass();
            return TemHttpPost.HttpPost(url, data);
        }

        #endregion

    }
}
