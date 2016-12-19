using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace WandoujiaRSA
{
    public class RSAUtils
    {
        public static bool VerifyData(string originalMessage, string signedMessage, string publickey)
        {
            CSInteropKeys.AsnKeyParser parser = new CSInteropKeys.AsnKeyParser(publickey);
            RSAParameters rsaKeyInfo = parser.ParseRSAPublicKey();
            bool success = false;
            using (var rsa = new RSACryptoServiceProvider())
            {
                byte[] bytesToVerify = Encoding.UTF8.GetBytes(originalMessage);
                byte[] signedBytes = Convert.FromBase64String(signedMessage);
                try
                {
                    rsa.ImportParameters(rsaKeyInfo);
                    success = rsa.VerifyData(bytesToVerify, CryptoConfig.MapNameToOID("SHA1"), signedBytes);
                }
                catch (CryptographicException e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return success;
        }
    }
}
