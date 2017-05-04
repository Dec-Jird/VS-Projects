using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Aop.Api.Util;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Jayrock.Json;

namespace Aop.Api.Test
{
    class PublicTest
    {
   //    [STAThread]
   //    static void Main()
   //    {
   //        // 公众号菜单查询
   //        //MenuGet();
   //
   //        // 公众号通知消息签名验证
   //        //CheckSign();
   //
   //        // 公众号验签&解密
   //        //CheckSignAndDecrypt();
   //
   //        // 公众号加密&加签
   //        //EncryptAndSign();
   //
   //        getAliOrderTest();
   //
   //        payVerifyTest();
   //
   //        Console.ReadLine();
   //        
   //    }
   //
   //    private static void payVerifyTest()
   //    {
   //        //支付成功验证回调数据签名测试
   //        string returnData = "total_amount=99.00&buyer_id=2088102170237795&trade_no=2017022521001004790200085088&notify_time=2017-02-25 16:52:11&subject=元宝元宝&sign_type=RSA2&buyer_logon_id=kut***@sandbox.com&auth_app_id=2016080400162673&charset=utf-8&notify_type=trade_status_sync&invoice_amount=99.00&out_trade_no=20170225test233&trade_status=TRADE_SUCCESS&gmt_payment=2017-02-25 16:52:08&version=1.0&point_amount=0.00&sign=Jh78HFoe0Sja6fGiVhzdcrWnetkNtEfg+TEuVZNeZNxIC2z+yNFK9jOcZSYnt+37DBqzPt9CuVCt9Ynd0T9L9jke3xZwdx+4zW9jXYPEJh2IlM+UKkphOJSwWnxvoh3ZdUJD+F7o+G9S1Na6t9fCxh3XBObk3XJYlH0h3GUT0f+UjBwjoSg77f3EYybPb9s4DFrIAWxrMk5YtG1FxOc2HP+H//oomT90w0KW4RYEWdL+nCA9gO504moLI1cosdJrD517BAoslu93SHi14Lebov9AEuKUpq152isuh9oojZTXs/EU9KiZU5XdfFORbctEOrVB0PZWmOmcEXvMPhuxFQ==&gmt_create=2017-02-25 16:52:07&buyer_pay_amount=99.00&receipt_amount=99.00&passback_params=merchantBizType%3d3C%26merchantBizNo%3d2016010101111&fund_bill_list=[{\"amount\":\"99.00\",\"fundChannel\":\"ALIPAYACCOUNT\"}]&app_id=2016080400162673&seller_id=2088102169863750&notify_id=6dfa606b57597325bdbf8caf92e5331m3i&seller_email=wmaunk5878@sandbox.com";
   //        string ALIPAY_PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxvcUUIuarxuws7PCXt3X2iiovbRLc27LTvdRf/pGtP6Qd7MqWdKvGuqnVUAx3MMlYOUo23yRuse/V/JR0QqYU04wHWMZSgf8M6CZvX0bLdPaf7EAw8fMOZCJMI6i1styGHJSIkF58rjIpNYavUf3mdNv4JbX1UDEzdtw30tkgAdl9fwJLNCZFsg1KbWbj8heoeO1rFHr8Fw51tfZhA97mUxB7KU2rP7dVmlcqzXQQ8/EqxBcS4aigpT17EYL1+T4Ney+RuvNdAonGxtwIiOLC1uqCEiaqsD/tT1UN2SxxMwYiqJVL3aovu5GcelXvcadCwcmgXW9zXRLVXESBua8cwIDAQAB";
   //        string CHARSET = "utf-8"; //写死为uft-8  
   //        //如果未进行urlDecode，则必须进行urlDecode，验证才正确，这里已经在delphi端进行过解码，无需再次解码。
   //        //returnData = System.Web.HttpUtility.UrlDecode(returnData, System.Text.Encoding.GetEncoding("utf-8"));
   //
   //        //切记alipaypublickey是支付宝的公钥，请去open.alipay.com对应应用下查看。
   //        //bool RSACheckV1(IDictionary<string, string> parameters, string alipaypublicKey, string charset, string signType, bool keyFromFile)
   //        bool flag = AlipaySignature.RSACheckV1(dataAnalytic(returnData), ALIPAY_PUBLIC_KEY, CHARSET, "RSA2", false);
   //        Console.WriteLine("result of RSACheckV1: "+ flag.ToString());
   //    }
   //
   //    /// 解析支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组 
   //    public static Dictionary<string, string> dataAnalytic(string retData)
   //    {
   //        string[] sArray = retData.Split('&'); 
   //       //string[] sArray = retData.Split(new char[2] { '&', '=' });
   //       //foreach (string s in sArray)
   //       //    Console.WriteLine(s);
   //
   //        Dictionary<string, string> sPair = new Dictionary<string, string>();
   //
   //        for (int i = 0; i < sArray.Length; i++)
   //        {
   //            string[] sArray1 = new String[2];
   //            if (sArray[i].StartsWith("sign=")){
   //                //Substring 截取子串，参数：起始位置，截取长度
   //                sArray1[0] = sArray[i].Substring(sArray[i].IndexOf("sign"),4);
   //                sArray1[1] = sArray[i].Substring(sArray[i].IndexOf("=") + 1, (sArray[i].LastIndexOf("=") - sArray[i].IndexOf("="))); //截取 sign的内容字符串
   //            }else{
   //                sArray1 = sArray[i].Split('=');
   //            }
   //
   //            sPair.Add(sArray1[0], sArray1[1]);
   //        } 
   //
   //        foreach (var item in sPair)
   //            Console.WriteLine(item);
   //
   //        return sPair;
   //    }
   //
   //    private static void getAliOrderTest()
   //    {
   //        //请求订单测试
   //        string APP_ID = "2016080400162673";
   //        string APP_PRIVATE_KEY = "MIIEpAIBAAKCAQEAtYUKdMN9a8/V39apMTnAcen6JmQ1zXV1qGjf7Quu6tA65bbm8Bfsbx8PqcECwfP/tpoWavfpdRtO99o/kY0WCPRjPctAQL2D/vJTE5hW1dK5H4WAWqSpupB5ImypyeBEHFTXEPnYFH5huQajG8uAnktI1jW7XKqTArRaoY3HH2iTLnT2ehi09b3jvl3juZ2/SZrgURl+hkVkwv+0OeOkwsLop+abeEip5WCOaq99huEHvcziuuTwY9C3roey67KKLhdEjO/t3n9w3G3Y485ylnUtKP/R2pxdpnZ8REuLeAkaEv5RnwiHZldrwN0FcYcIdSA828mq4Yj3UU3E/RM2YQIDAQABAoIBAQCLqSk6XY8KfIaaCpdzAHRJMTT+hOvAgTddtBNWVz7l/ADU7b0RzdZkSQnMGmz8vbdpz3SgKM6/A5vmp01xt5PUn/Qbf16YcTg12EyDLxrguZkl35m6JCdTHAWXrvOUF7FP+xbeQN04J2UY7zpgEFuNb29DIWRfD/68ffedhXBHgOkOfVgGB3giRczReu7U5K2c914LAYBK0NcYfImB/NZ2ZEwnhwklE4fT9g0E+3CQXytCm/0ruDEWWjxSCMcBaq+lCM2lelSoqIjRiZ8e+UgHkU3uSxlyCVrqAgW/JAYlGNC15/ANP9jiUv22h0jgJsTPHcRrdRnOOHANBNIvBQaBAoGBAO6gS3d7wTLYeJx4+EMt8cv6Ujy07UJMtiD7mEe00jXLoo2vC8ZSLvANyB9B8DeoqXXXuVkyULLheKrnDNjo4WByjlO+CO4NukMotYElE+EVO98T6nIYVCEijQDfju9kaGtbjx3GZ/IQQw/lrNsHEvvGx6JmfE8Y0hoQGAftp6F1AoGBAMK8WaIReW58+K6ZzCOnUCWAkNacdZxhcDmJfMcPNcg41BD93/zEzWzMB+DeDYLQCyidNz1MrzmodxEQEx03+Whc+sXDsUttMfCGQkUZdkIOb/x82d+hnO9zUNmTO4wRhVv9NWq7bNk8BnfVJtO+u4H9VJCATSD0sIl6Smp4IZe9AoGBALdotRiNIh22jF2YDRl1gtfI+tR2K7Y9x+7p8k2LCdcXQUWtOVuhZzpTHXII+F2PYVCWEnwgVC5pZpnVJObDeBbtdb+f3LU8D+H2tCsjGHh0HaSEZjpzwJYHPGFjczVE840wvnugN9yx6xmY6pcehNTIIEEOjJUu+q3VmOLfI2zRAoGAHedunESysRTf23AiuRboZ9nmZA6CwRD1euByGN9tEuInLrTNwLM4GIz8aLuwt3XbQNFjujYccm48WpJtXP9LfYtJtzTl9P8/u//iDVprnpk4+Tzy+DSJNPwwXjkN2+SU5htsKIe/n1xoYd6Jp9qSUNPmOIp7TaRFt9bftpncDsECgYBPigXrRmocMG/SDWrWySrZw2B1dLuW6suj8aDN+jPufE3I7TbltVMZf7CxIcEykzPyDYn7oh4jQMiLKkVb74u3Gju1XAy79VGoboM0DqrJrP61cWKigZuwpFkWB7h9v249tQCJyzac3U3eVlKbnIf3Y0SaN16ddpqd4rcVX37bwA==";
   //        string ALIPAY_PUBLIC_KEY = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxvcUUIuarxuws7PCXt3X2iiovbRLc27LTvdRf/pGtP6Qd7MqWdKvGuqnVUAx3MMlYOUo23yRuse/V/JR0QqYU04wHWMZSgf8M6CZvX0bLdPaf7EAw8fMOZCJMI6i1styGHJSIkF58rjIpNYavUf3mdNv4JbX1UDEzdtw30tkgAdl9fwJLNCZFsg1KbWbj8heoeO1rFHr8Fw51tfZhA97mUxB7KU2rP7dVmlcqzXQQ8/EqxBcS4aigpT17EYL1+T4Ney+RuvNdAonGxtwIiOLC1uqCEiaqsD/tT1UN2SxxMwYiqJVL3aovu5GcelXvcadCwcmgXW9zXRLVXESBua8cwIDAQAB";
   //        string CHARSET = "utf-8"; //写死为uft-8
   //
   //        IAopClient client = new DefaultAopClient("https://openapi.alipay.com/gateway.do",
   //            APP_ID, APP_PRIVATE_KEY, "json", "1.0", "RSA2", ALIPAY_PUBLIC_KEY, CHARSET, false);
   //
   //        //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
   //        AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
   //        //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
   //        AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
   //        //model.Body = "我是测试数据";
   //        model.Subject = "元宝元宝"; //商品标题
   //        model.TotalAmount = "9.90"; //价格
   //        model.ProductCode = "QUICK_MSECURITY_PAY"; //固定值 QUICK_MSECURITY_PAY
   //        model.OutTradeNo = "20170225test003"; //我方订单号
   //        model.TimeoutExpress = "30m"; //订单超时时间定为 30分钟
   //        model.PassbackParams = "merchantBizType%3d3C%26merchantBizNo%3d2016010101111";//callbackInfo
   //        request.SetBizModel(model);
   //
   //        //这里和普通的接口调用不同，使用的是sdkExecute
   //        AlipayTradeAppPayResponse response = client.SdkExecute(request);
   //        //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
   //
   //        //Response.Write(HttpUtility.HtmlEncode(response.Body));
   //        //Console.WriteLine("Response：" + HttpUtility.HtmlEncode(response.Body));
   //        StringBuilder sb = new StringBuilder(response.Body);
   //        //sb.Replace(";", "&");
   //        Console.WriteLine("从阿里支付获取到定单数据：\n" + sb);
   //        //页面输出的response.Body就是orderString 可以直接给客户端请求，无需再做处理。
   //
   //        //获取到的字符串，要把分号去掉
   //        //Console.ReadLine();
   //    }
   //
   //    private static void CheckSign()
   //    {
   //        IDictionary<string, string> paramsMap = new Dictionary<string, string>();
   //        paramsMap.Add("appId", "2013092500031084");
   //        string privateKeyPem = GetCurrentPath() + "aop-sandbox-RSA-private-c#.pem";
   //        string sign = AlipaySignature.RSASign(paramsMap, privateKeyPem, null,"RSA");
   //        paramsMap.Add("sign", sign);
   //        string publicKey = GetCurrentPath() + "public-key.pem";
   //        bool checkSign = AlipaySignature.RSACheckV2(paramsMap, publicKey);
   //        System.Console.Write("---------公众号通知消息签名验证--------" + "\n\r");
   //        System.Console.Write("checkSign:" + checkSign + "\n\r");
   //    }
   //
   //    private static void MenuGet()
   //    {
   //        IAopClient client = GetAlipayClient();
   //        //AlipayMobilePublicMenuGetRequest req = new AlipayMobilePublicMenuGetRequest();
   //        //AlipayMobilePublicMenuGetResponse res = client.Execute(req);
   //        System.Console.Write("-------------公众号菜单查询-------------" + "\n\r");
   //       // System.Console.Write("Body:" + res.Body + "\n\r");
   //    }
   //
   //    private static IAopClient GetAlipayClient()
   //    {
   //        //支付宝网关地址
   //        // -----沙箱地址-----
   //        string serverUrl = "http://openapi.alipaydev.com/gateway.do";
   //        // -----线上地址-----
   //        // string serverUrl = "https://openapi.alipay.com/gateway.do"; 
   //
   //        //应用ID
   //        string appId = "2013092500031084";
   //        //商户私钥
   //        string privateKeyPem = GetCurrentPath() + "aop-sandbox-RSA-private-c#.pem";
   //
   //        IAopClient client = new DefaultAopClient(serverUrl, appId, privateKeyPem);
   //
   //        return client;
   //    }
   //
   //    private static string GetCurrentPath()
   //    {
   //        string basePath = System.IO.Directory.GetParent(System.Environment.CurrentDirectory).Parent.FullName;
   //        return basePath + "/Test/";
   //    }
   //
   //    public static void CheckSignAndDecrypt()
   //    {
   //        // 参数构建
   //        string charset = "UTF-8";
   //        string bizContent = "<XML><AppId><![CDATA[2013082200024893]]></AppId><FromUserId><![CDATA[2088102122485786]]></FromUserId><CreateTime>1377228401913</CreateTime><MsgType><![CDATA[click]]></MsgType><EventType><![CDATA[event]]></EventType><ActionParam><![CDATA[authentication]]></ActionParam><AgreementId><![CDATA[201308220000000994]]></AgreementId><AccountNo><![CDATA[null]]></AccountNo><UserInfo><![CDATA[{\"logon_id\":\"15858179811\",\"user_name\":\"许旦辉\"}]]></UserInfo></XML>";
   //        string publicKeyPem = GetCurrentPath() + "public-key.pem";
   //        string privateKeyPem = GetCurrentPath() + "aop-sandbox-RSA-private-c#.pem";
   //        IDictionary<string, string> paramsMap = new Dictionary<string, string>();
   //        paramsMap.Add("biz_content", AlipaySignature.RSAEncrypt(bizContent, publicKeyPem, charset));
   //        paramsMap.Add("charset", charset);
   //        paramsMap.Add("service", "alipay.mobile.public.message.notify");
   //        paramsMap.Add("sign_type", "RSA");
   //        paramsMap.Add("sign", AlipaySignature.RSASign(paramsMap, privateKeyPem,null,"RSA"));
   //
   //        // 验签&解密
   //        string resultContent = AlipaySignature.CheckSignAndDecrypt(paramsMap, publicKeyPem, privateKeyPem, true, true);
   //        System.Console.Write("resultContent=" + resultContent+ "\n\r");
   //    }
   //
   //    public static void EncryptAndSign()
   //    {
   //        // 参数构建
   //        string bizContent = "<XML><ToUserId><![CDATA[2088102122494786]]></ToUserId><AppId><![CDATA[2013111100036093]]></AppId><AgreementId><![CDATA[20131111000001895078]]></AgreementId>"
   //                        + "<CreateTime>12334349884</CreateTime>"
   //                        + "<MsgType><![CDATA[image-text]]></MsgType>"
   //                        + "<ArticleCount>1</ArticleCount>"
   //                        + "<Articles>"
   //                        + "<Item>"
   //                        + "<Title><![CDATA[[回复测试加密解密]]></Title>"
   //                        + "<Desc><![CDATA[测试加密解密]]></Desc>"
   //                        + "<Url><![CDATA[http://m.taobao.com]]></Url>"
   //                        + "<ActionName><![CDATA[立即前往]]></ActionName>"
   //                        + "</Item>"
   //                        + "</Articles>" + "<Push><![CDATA[false]]></Push>" + "</XML>";
   //        string publicKeyPem = GetCurrentPath() + "public-key.pem";
   //        string privateKeyPem = GetCurrentPath() + "aop-sandbox-RSA-private-c#.pem";
   //        string responseContent = AlipaySignature.encryptAndSign(bizContent, publicKeyPem, privateKeyPem,"UTF-8",true,true);
   //        System.Console.Write("resultContent=" + responseContent + "\n\r");
   //    }
    }

}
