using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;

namespace HmacSha1
{
    public interface HmacSha1Interface
    {
        string HmacSha1Sign(string text, string key, string type);
    }


    [ClassInterface(ClassInterfaceType.None)]
    public class HmacSha1Class : HmacSha1Interface
    {
        public string HmacSha1Sign(string text, string key, string type)
        {
            string retStr = "";
            if (type == "mac")
            {
                //金立（HmacSHA1方式进行mac签名）.
                HMACSHA1 hmacsha1 = new HMACSHA1();
                hmacsha1.Key = Encoding.UTF8.GetBytes(key);
                byte[] dataBuffer = Encoding.UTF8.GetBytes(text);
                byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);

                retStr = Convert.ToBase64String(hashBytes); 
            }
            else
            {
                Encoding encode = Encoding.GetEncoding("UTF-8");
                byte[] byteData = encode.GetBytes(text);
                byte[] byteKey = encode.GetBytes(key);
                HMACSHA1 hmac = new HMACSHA1(byteKey);
                CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
                cs.Write(byteData, 0, byteData.Length);
                cs.Close();
                

                if (type == "hex")
                {
                    //小米.
                    retStr = bytesToHexStr(hmac.Hash).ToLower();
                }
                else if (type == "base64")
                {//base64编码.
                    //腾讯.
                    retStr = Convert.ToBase64String(hmac.Hash);
                }
            }
            return retStr;
        }

        public string bytesToHexStr(byte[] bcd)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bcd)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

        #region HmacSha1Interface 成员


        string HmacSha1Interface.HmacSha1Sign(string text, string key, string type)
        {
            HmacSha1Class TemHmacSha1 = new HmacSha1Class();
            return TemHmacSha1.HmacSha1Sign(text, key, type);
        }

        #endregion


    }
}
