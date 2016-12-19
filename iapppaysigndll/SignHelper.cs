using System;



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
        }
    }
    
}
