using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpPostXML;

namespace HttpTest
{
    class Program
    {
        static void Main(string[] args)
        {

            string url = "https://api.mch.weixin.qq.com/pay/unifiedorder";
            string dataStr = "<xml><appid>wx8b163c8fb479c29e</appid><attach>TnyooCallbackInfo</attach><body>100元宝</body><mch_id>1444147502</mch_id><nonce_str>RhgWBMYA1W4SFi2Q</nonce_str><notify_url>http://182.254.148.221:3358/ucpay</notify_url><out_trade_no>2-309829102</out_trade_no><spbill_create_ip>14.23.150.211</spbill_create_ip><total_fee>600</total_fee><trade_type>APP</trade_type><sign>6F3ABDCD9BFFB34ADE6D0E0EE01CB6D8</sign></xml>";


            //byte[] someData = Encoding.GetEncoding("utf-8").GetBytes(dataStr);
            //string postData = Encoding.GetEncoding("ISO-8859-1").GetString(someData);

            
            Console.WriteLine("POST Data: \n" + dataStr);

            HttpPostXMLClass http = new HttpPostXMLClass();

            string returnValue = http.Post(url, dataStr);

            Console.WriteLine("请求结果: \n" + returnValue);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

    }
}

