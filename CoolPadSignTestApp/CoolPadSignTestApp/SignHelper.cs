using System;
using SignAndVerify;


namespace iapppay
{
    namespace sign
    {
        class SignHelper
        {
            // 字符编码格式 ，目前支持  utf-8
            public static string input_charset = "utf-8";

            public static bool verify(string content, string sign, string pubKey)
            {
                return RSA.verify(content, sign, pubKey, input_charset);
            }
            public static string sign(string content, string privateKey)
            {
                return RSA.sign(content, privateKey, input_charset);
            }

            public static bool SignAndVerify_verify(string content, string sign, string pubKey)
            {
                return RSA22.verify(content, sign, pubKey, input_charset);
            }
            public static string SignAndVerify_sign(string content, string privateKey)
            {
                return RSA22.sign(content, privateKey, input_charset);
            }
        }
    }
    
}
