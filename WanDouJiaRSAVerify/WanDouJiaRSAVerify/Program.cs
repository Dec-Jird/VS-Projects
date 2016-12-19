using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WandoujiaRSA;

namespace TestConsoleApp
{
    class Program
    {
        /*      
               static void Main(string[] args)
               {
                   string publickey = @"MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCd95FnJFhPinpNiE/h4VA6bU1rzRa5+a25BxsnFX8TzquWxqDCoe4xG6QKXMXuKvV57tTRpzRo2jeto40eHKClzEgjx9lTYVb2RFHHFWio/YGTfnqIPTVpi7d7uHY+0FZ0lYL5LlW4E2+CQMxFOPRwfqGzMjs1SDlH7lVrLEVy6QIDAQAB";
                   string data = "{\"timeStamp\":1363848203377,\"orderId\":100001472,\"money\":4000,\"chargeType\":\"BALANCEPAY\",\"appKeyId\":100000000,\"buyerId\":1,\"cardNo\":null}";
                   string sign = "VwnhaP9gAbDD2Msl3bFnvsJfgz3NOAqM/JVexl1myHfsrHX3cRrFXz86cNO+oNYWBBM7m/5ZdtHRpSArZWFuZHysKfirO3BynUaIYSAiD2J1Xio5q9+Yr83cI/ESyemVAt7lK4lMW3ReSwmAcOs0kDZLAxVIb++EPy0y2NpH4kI=";
            
                   Console.WriteLine("RSA验签!");
                   bool verified = RSAUtils.VerifyData(data, sign, publickey);
            
                   if(verified)
                       Console.WriteLine("验签成功！");
                   else
                       Console.WriteLine("验签失败！");



                   // Keep the console window open in debug mode.
                   Console.WriteLine("Press any key to exit.");
                   Console.ReadKey();
               }
        //*/
    }
}
