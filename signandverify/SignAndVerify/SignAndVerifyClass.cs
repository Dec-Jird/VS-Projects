using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;




namespace SignAndVerify
{
    public interface SignAndVerifyInterface
    {
        string HmacSha1Sign(string text, string key, string type);

        string RsaSha1Sign(string content, string privateKey);

        string RsaMd5Sign(string content, string privateKey);

        bool RsaMd5Verify(string BillInfo, string PublicKey, string sign);

        bool RsaSha1Verify(string BillInfo, string PublicKey, string sign);
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class SignAndVerifyClass : SignAndVerifyInterface
    {
        // 字符编码格式 ，目前支持  utf-8
        public static string input_charset = "utf-8";

        public string HmacSha1Sign(string text, string key, string type)
        {
            Encoding encode = Encoding.GetEncoding("UTF-8");
            byte[] byteData = encode.GetBytes(text);
            byte[] byteKey = encode.GetBytes(key);
            HMACSHA1 hmac = new HMACSHA1(byteKey);
            CryptoStream cs = new CryptoStream(Stream.Null, hmac, CryptoStreamMode.Write);
            cs.Write(byteData, 0, byteData.Length);
            cs.Close();
            string retStr = "";

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

        public string RsaSha1Sign(string content, string privateKey)//华为使用该方式进行订单签名
        {
            return RSA.signSHA1(content, privateKey, input_charset);
        }

        public string RsaMd5Sign(string content, string privateKey)
        {
            return RSA.sign(content, privateKey, input_charset);
        }

        public bool RsaMd5Verify(string BillInfo, string PublicKey, string sign)
        {
            try
            {
                // 验签
                if (RSA.verify(BillInfo, sign, PublicKey, input_charset))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool RsaSha1Verify(string BillInfo, string PublicKey, string sign)//华为使用该方式进行验证支付返回签名
        {
            try
            {
                // 验签
                if (RSA.verifySHA1(BillInfo, sign, PublicKey, input_charset))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

        }

        #region SignAndVerifyInterface 成员


        string SignAndVerifyInterface.HmacSha1Sign(string text, string key, string type)
        {
            SignAndVerifyClass TemSignAndVerify = new SignAndVerifyClass();
            return TemSignAndVerify.HmacSha1Sign(text, key, type);
        }
        #endregion

        #region SignAndVerifyInterface 成员
        string SignAndVerifyInterface.RsaSha1Sign(string content, string privateKey) {
            SignAndVerifyClass TemSignAndVerify = new SignAndVerifyClass();
            return TemSignAndVerify.RsaSha1Sign(content, privateKey);
        }
        #endregion

        #region SignAndVerifyInterface 成员
        string SignAndVerifyInterface.RsaMd5Sign(string content, string privateKey)
        {
            SignAndVerifyClass TemSignAndVerify = new SignAndVerifyClass();
            return TemSignAndVerify.RsaMd5Sign(content, privateKey);
        }
        #endregion

        #region SignAndVerifyInterface 成员
        bool SignAndVerifyInterface.RsaMd5Verify(string BillInfo, string PublicKey, string sign)
        {
            SignAndVerifyClass TemHmacSha1 = new SignAndVerifyClass();
            return TemHmacSha1.RsaMd5Verify(BillInfo, PublicKey, sign);
        }
        #endregion

        #region SignAndVerifyInterface 成员
        bool SignAndVerifyInterface.RsaSha1Verify(string BillInfo, string PublicKey, string sign)
        {
            SignAndVerifyClass TemHmacSha1 = new SignAndVerifyClass();
            return TemHmacSha1.RsaSha1Verify(BillInfo, PublicKey, sign);
        }
        #endregion
    }
}
