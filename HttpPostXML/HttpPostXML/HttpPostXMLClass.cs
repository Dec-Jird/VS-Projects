using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using System.Collections;
using System.IO;
using System.Net;
using System.Security.Cryptography;

using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.IO.Compression;
using System.Text.RegularExpressions;

namespace HttpPostXML
{

    public interface HttpPostXMLInterface
    {
        string PostXML(string url, string data);
    }

    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class HttpPostXMLClass : HttpPostXMLInterface
    {
        public string Post(string url, string data)
        {
            string returnData = null;
            try
            {
                //byte[] buffer = Encoding.ASCII.GetBytes(data);//Encoding.UTF8
                byte[] buffer = Encoding.UTF8.GetBytes(data);//Encoding.UTF8
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(url);
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = buffer.Length;
                Stream postData = webReq.GetRequestStream();
                postData.Write(buffer, 0, buffer.Length);
                postData.Close();

                HttpWebResponse webResp = (HttpWebResponse)webReq.GetResponse();
                Stream answer = webResp.GetResponseStream();
                StreamReader answerData = new StreamReader(answer);
                returnData = answerData.ReadToEnd();
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                //Console.WriteLine(ex.Message);
                returnData = null;
            }
            return returnData.Trim() + "\n";
        }

        #region HttpPostXMLClass 成员实现

        string HttpPostXMLInterface.PostXML(string url, string data)
        {
            HttpPostXMLClass http = new HttpPostXMLClass();

            return http.Post(url, data);
        }

        #endregion
    }
}
