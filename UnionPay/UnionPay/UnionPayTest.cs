using com.unionpay.acp.sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.X509;
using UnionPayDLL;

namespace UnionPayTest
{
    class UnionPayTest
    {
        private AsymmetricKeyParameter appVerifyPubKey = null;

        static void Main(string[] args)
        {
            /*System.Reflection.Assembly curPath = System.Reflection.Assembly.GetExecutingAssembly();
            string path = curPath.Location;
            path = System.IO.Directory.GetCurrentDirectory();//D:\Mac\UnionPay\UnionPay\bin\Debug
            Console.Write(path + "\r\n");*/

            GetOrderId();

            PayVerifyTest();


            string Data = "accessType=0&bizType=000201&currencyCode=156&encoding=UTF-8&merId=777290058143823&orderId=20160615092209323&queryId=201703241511197447698&reqReserved=VXNlcklEPTI5MzAyJkl0ZW1JRD0xJkJpbGxJRD0yMDE2MDYxNTA5MjIwOTMyMCZTZXJ2ZXJJRD0x&respCode=00&respMsg=成功[0000000]&settleAmt=1&settleCurrencyCode=156&settleDate=0324&signMethod=01&signPubKeyCert=-----BEGIN CERTIFICATE-----\r\nMIIEOjCCAyKgAwIBAgIFEAJkAUkwDQYJKoZIhvcNAQEFBQAwWDELMAkGA1UEBhMC\r\nQ04xMDAuBgNVBAoTJ0NoaW5hIEZpbmFuY2lhbCBDZXJ0aWZpY2F0aW9uIEF1dGhv\r\ncml0eTEXMBUGA1UEAxMOQ0ZDQSBURVNUIE9DQTEwHhcNMTUxMjA0MDMyNTIxWhcN\r\nMTcxMjA0MDMyNTIxWjB5MQswCQYDVQQGEwJjbjEXMBUGA1UEChMOQ0ZDQSBURVNU\r\nIE9DQTExEjAQBgNVBAsTCUNGQ0EgVEVTVDEUMBIGA1UECxMLRW50ZXJwcmlzZXMx\r\nJzAlBgNVBAMUHjA0MUBaMTJAMDAwNDAwMDA6U0lHTkAwMDAwMDA2MjCCASIwDQYJ\r\nKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMUDYYCLYvv3c911zhRDrSWCedAYDJQe\r\nfJUjZKI2avFtB2/bbSmKQd0NVvh+zXtehCYLxKOltO6DDTRHwH9xfhRY3CBMmcOv\r\nd2xQQvMJcV9XwoqtCKqhzguoDxJfYeGuit7DpuRsDGI0+yKgc1RY28v1VtuXG845\r\nfTP7PRtJrareQYlQXghMgHFAZ/vRdqlLpVoNma5C56cJk5bfr2ngDlXbUqPXLi1j\r\niXAFb/y4b8eGEIl1LmKp3aPMDPK7eshc7fLONEp1oQ5Jd1nE/GZj+lC345aNWmLs\r\nl/09uAvo4Lu+pQsmGyfLbUGR51KbmHajF4Mrr6uSqiU21Ctr1uQGkccCAwEAAaOB\r\n6TCB5jAfBgNVHSMEGDAWgBTPcJ1h6518Lrj3ywJA9wmd/jN0gDBIBgNVHSAEQTA/\r\nMD0GCGCBHIbvKgEBMDEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2ZjYS5jb20u\r\nY24vdXMvdXMtMTQuaHRtMDgGA1UdHwQxMC8wLaAroCmGJ2h0dHA6Ly91Y3JsLmNm\r\nY2EuY29tLmNuL1JTQS9jcmw0NDkxLmNybDALBgNVHQ8EBAMCA+gwHQYDVR0OBBYE\r\nFAFmIOdt15XLqqz13uPbGQwtj4PAMBMGA1UdJQQMMAoGCCsGAQUFBwMCMA0GCSqG\r\nSIb3DQEBBQUAA4IBAQB8YuMQWDH/Ze+e+2pr/914cBt94FQpYqZOmrBIQ8kq7vVm\r\nTTy94q9UL0pMMHDuFJV6Wxng4Me/cfVvWmjgLg/t7bdz0n6UNj4StJP17pkg68WG\r\nzMlcjuI7/baxtDrD+O8dKpHoHezqhx7dfh1QWq8jnqd3DFzfkhEpuIt6QEaUqoWn\r\nt5FxSUiykTfjnaNEEGcn3/n2LpwrQ+upes12/B778MQETOsVv4WX8oE1Qsv1XLRW\r\ni0DQetTU2RXTrynv+l4kMy0h9b/Hdlbuh2s0QZqlUMXx2biy0GvpF2pR8f+OaLuT\r\nAtaKdU4T2+jO44+vWNNN2VoAaw0xY6IZ3/A1GL0x\r\n-----END CERTIFICATE-----&traceNo=744769&traceTime=0324151119&txnAmt=1&txnSubType=01&txnTime=20170324151119&txnType=01&version=5.1.0&signature=KHnZ5C3fytjSLAIVS9zkzc2SR5QDs9L00cvq1cGK8fYXopRAFbZID+eV9qjiDFhn5DUxsXFZUMBXoyzlZ5MOvTk143DMkrRyWOgFnT3+puxq9KC7IJunQFVWa6zTiSrkZP844azoypRbAPmP6NdIVkRq5PTO00cakDdsvuQYohq98LOTsxYuGKuKUWIUsQzQxjsroBWi6qh778r00ywWIQTqWpQgZDWPKBhN62Db84Cvw/q/5YGRKtzR6NiUwhdQqEkV00ud6SYVBe8lp0cFMilyuyiiLjJxSFoa5kwxemI3CiKtN/AhqQjfTDJpBD1QAjrITmjqjUoGfYNuc6xzhw==";
            UnionPayDLLClass unionpay = new UnionPayDLLClass();

            Console.Write("\n[UnionPayDLLClass] 支付验签结果：" + unionpay.UnionpayVerify(Data) + "\n");

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadLine(); 

        }
        public static void GetOrderId(){
            /**
             * 重要：联调测试时请仔细阅读注释！
             * 
             * 产品：跳转网关支付产品<br>
             * 交易：消费：前台跳转，有前台通知应答和后台通知应答<br>
             * 日期： 2015-09<br>
             * 版本： 1.0.0
             * 版权： 中国银联<br>
             * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己需要，按照技术文档编写。该代码仅供参考，不提供编码性能规范性等方面的保障<br>
             * 提示：该接口参考文档位置：open.unionpay.com帮助中心 下载  产品接口规范  《网关支付产品接口规范》，<br>
             *              《平台接入接口规范-第5部分-附录》（内包含应答码接口规范，全渠道平台银行名称-简码对照表)<br>
             *              《全渠道平台接入接口规范 第3部分 文件接口》（对账文件格式说明）<br>
             * 测试过程中的如果遇到疑问或问题您可以：1）优先在open平台中查找答案：
             * 							        调试过程中的问题或其他问题请在 https://open.unionpay.com/ajweb/help/faq/list 帮助中心 FAQ 搜索解决方案
             *                             测试过程中产生的7位应答码问题疑问请在https://open.unionpay.com/ajweb/help/respCode/respCodeList 输入应答码搜索解决方案
             *                          2） 咨询在线人工支持： open.unionpay.com注册一个用户并登陆在右上角点击“在线客服”，咨询人工QQ测试支持。
             * 交易说明:1）以后台通知或交易状态查询交易确定交易成功,前台通知不能作为判断成功的标准.
             *       2）交易状态查询交易（Form_6_5_Query）建议调用机制：前台类交易建议间隔（5分、10分、30分、60分、120分）发起交易查询，如果查询到结果成功，则不用再查询。（失败，处理中，查询不到订单均可能为中间状态）。也可以建议商户使用payTimeout（支付超时时间），过了这个时间点查询，得到的结果为最终结果。
             */

            Dictionary<string, string> param = new Dictionary<string, string>();

            //以下信息非特殊情况不需要改动
            param["version"] = "5.1.0";//版本号
            param["encoding"] = "UTF-8";//编码方式
            param["txnType"] = "01";//交易类型
            param["txnSubType"] = "01";//交易子类
            param["bizType"] = "000201";//业务类型
            param["signMethod"] = "01";//签名方法
            param["channelType"] = "08";//渠道类型
            param["accessType"] = "0";//接入类型
            //param["frontUrl"] = SDKConfig.FrontUrl;  //前台通知地址      
            param["backUrl"] = "http://182.254.244.236:3358/ucpay";  //后台通知地址
            param["currencyCode"] = "156";//交易币种

            //TODO 以下信息需要填写
            param["merId"] = "777290058143823";//商户号，请改自己的测试商户号，此处默认取demo演示页面传递的参数
            param["orderId"] = DateTime.Now.ToString("yyyyMMddHHmmssfff");//商户订单号，8-32位数字字母，不能含“-”或“_”，此处默认取demo演示页面传递的参数，可以自行定制规则
            param["txnTime"] = DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间，格式为YYYYMMDDhhmmss，取北京时间，此处默认取demo演示页面传递的参数，参考取法： DateTime.Now.ToString("yyyyMMddHHmmss")
            param["txnAmt"] = "1";//交易金额，单位分，此处默认取demo演示页面传递的参数
            
            // 请求方保留域，
            // 透传字段，查询、通知、对账文件中均会原样出现，如有需要请启用并修改自己希望透传的数据。
            // 出现部分特殊字符时可能影响解析，请按下面建议的方式填写：
            // 1. 如果能确定内容不会出现&={}[]"'等符号时，可以直接填写数据，建议的方法如下。
            //param["reqReserved"] = "透传信息1|透传信息2|透传信息3";
            // 2. 内容可能出现&={}[]"'符号时：
            // 1) 如果需要对账文件里能显示，可将字符替换成全角＆＝｛｝【】“‘字符（自己写代码，此处不演示）；
            // 2) 如果对账文件没有显示要求，可做一下base64（如下）。
            //    注意控制数据长度，实际传输的数据长度不能超过1024位。
            //    查询、通知等接口解析时使用System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(reqReserved))解base64后再对数据做后续解析。
            //param["reqReserved"] = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("任意格式的信息都可以"));

            //TODO 其他特殊用法请查看 pages/api_05_app/special_use_purchase.htm

            AcpService.Sign(param, System.Text.Encoding.UTF8);  // 签名
            string url = "https://gateway.test.95516.com/gateway/api/appTransReq.do";

            Dictionary<String, String> rspData = AcpService.Post(param, url, System.Text.Encoding.UTF8);

            //Console.Write("\r\n请求到订单：" + SDKUtil.CreateLinkString(rspData, false, true, System.Text.Encoding.UTF8) + "\r\n");

            Console.Write(GetPrintResult(url, param, rspData));
            //AcpService.WriteLog(GetPrintResult(url, param, rspData));

            if (rspData.Count!=0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8))
                {
                    Console.Write("商户端验证返回报文签名成功。\r\n");
                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取
                    if ("00" == respcode)
                    {
                       //成功
                        //TODO
                        Console.Write("成功接收tn：" + rspData["tn"] + "\r\n\n");
                        //AcpService.WriteLog("成功接收tn：" + rspData["tn"] + "\r\n");
                        //Console.Write("后续请将此tn传给手机开发，由他们用此tn调起控件后完成支付。\r\n");
                        //Console.Write("手机端demo默认从仿真获取tn，仿真只返回一个tn，如不想修改手机和后台间的通讯方式，【此页面请修改代码为只输出tn】。\r\n");
                    }
                    else
                    {
                        //其他应答码做以失败处理
                        //TODO
                        Console.Write("失败：" + rspData["respMsg"] + "\r\n\n");
                    }
                }
                else
                {
                  
                   Console.Write("商户端验证返回报文签名失败。\r\n\n");
                }
            }
            else
            {
                Console.Write("请求失败");
            }
        
        }


        /// <summary>
        /// 得到用于打印页面的html
        /// </summary>
        /// <param name="url"></param>
        /// <param name="req"></param>
        /// <param name="resp"></param>
        public static string GetPrintResult(string url, Dictionary<string, string> req, Dictionary<string, string> resp)
        {
            string result = "=============<br>\r\n";
            result = result + "地址：" + url + "<br>\r\n";
            //result = result + "请求：" + System.Web.HttpContext.Current.Server.HtmlEncode(SDKUtil.CreateLinkString(req, false, true, System.Text.Encoding.UTF8)) + "\r\n";
            //result = result + "应答：" + System.Web.HttpContext.Current.Server.HtmlEncode(SDKUtil.CreateLinkString(resp, false, false, System.Text.Encoding.UTF8))+ "\r\n";
            result = result + "请求：" + SDKUtil.CreateLinkString(req, false, true, System.Text.Encoding.UTF8) + "\r\n";
            result = result + "应答：" + SDKUtil.CreateLinkString(resp, false, false, System.Text.Encoding.UTF8) + "\r\n";
            result = result + "=============<br>\r\n";
            return result;
        }

        public static void PayVerifyTest()
        {
            string payResultData = "accessType=0&bizType=000201&currencyCode=156&encoding=UTF-8&merId=777290058143823&orderId=20160615092209320&queryId=201703231509556241618&reqReserved=VXNlcklEPTI5MzAyJkl0ZW1JRD0xJkJpbGxJRD0yMDE2MDYxNTA5MjIwOTMyMCZTZXJ2ZXJJRD0x&respCode=00&respMsg=成功[0000000]&settleAmt=1&settleCurrencyCode=156&settleDate=0323&signMethod=01&signPubKeyCert=-----BEGIN CERTIFICATE-----\r\n" +
"MIIEOjCCAyKgAwIBAgIFEAJkAUkwDQYJKoZIhvcNAQEFBQAwWDELMAkGA1UEBhMC\r\n" +
"Q04xMDAuBgNVBAoTJ0NoaW5hIEZpbmFuY2lhbCBDZXJ0aWZpY2F0aW9uIEF1dGhv\r\n" +
"cml0eTEXMBUGA1UEAxMOQ0ZDQSBURVNUIE9DQTEwHhcNMTUxMjA0MDMyNTIxWhcN\r\n" +
"MTcxMjA0MDMyNTIxWjB5MQswCQYDVQQGEwJjbjEXMBUGA1UEChMOQ0ZDQSBURVNU\r\n" +
"IE9DQTExEjAQBgNVBAsTCUNGQ0EgVEVTVDEUMBIGA1UECxMLRW50ZXJwcmlzZXMx\r\n" +
"JzAlBgNVBAMUHjA0MUBaMTJAMDAwNDAwMDA6U0lHTkAwMDAwMDA2MjCCASIwDQYJ\r\n" +
"KoZIhvcNAQEBBQADggEPADCCAQoCggEBAMUDYYCLYvv3c911zhRDrSWCedAYDJQe\r\n" +
"fJUjZKI2avFtB2/bbSmKQd0NVvh+zXtehCYLxKOltO6DDTRHwH9xfhRY3CBMmcOv\r\n" +
"d2xQQvMJcV9XwoqtCKqhzguoDxJfYeGuit7DpuRsDGI0+yKgc1RY28v1VtuXG845\r\n" +
"fTP7PRtJrareQYlQXghMgHFAZ/vRdqlLpVoNma5C56cJk5bfr2ngDlXbUqPXLi1j\r\n" +
"iXAFb/y4b8eGEIl1LmKp3aPMDPK7eshc7fLONEp1oQ5Jd1nE/GZj+lC345aNWmLs\r\n" +
"l/09uAvo4Lu+pQsmGyfLbUGR51KbmHajF4Mrr6uSqiU21Ctr1uQGkccCAwEAAaOB\r\n" +
"6TCB5jAfBgNVHSMEGDAWgBTPcJ1h6518Lrj3ywJA9wmd/jN0gDBIBgNVHSAEQTA/\r\n" +
"MD0GCGCBHIbvKgEBMDEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2ZjYS5jb20u\r\n" +
"Y24vdXMvdXMtMTQuaHRtMDgGA1UdHwQxMC8wLaAroCmGJ2h0dHA6Ly91Y3JsLmNm\r\n" +
"Y2EuY29tLmNuL1JTQS9jcmw0NDkxLmNybDALBgNVHQ8EBAMCA+gwHQYDVR0OBBYE\r\n" +
"FAFmIOdt15XLqqz13uPbGQwtj4PAMBMGA1UdJQQMMAoGCCsGAQUFBwMCMA0GCSqG\r\n" +
"SIb3DQEBBQUAA4IBAQB8YuMQWDH/Ze+e+2pr/914cBt94FQpYqZOmrBIQ8kq7vVm\r\n" +
"TTy94q9UL0pMMHDuFJV6Wxng4Me/cfVvWmjgLg/t7bdz0n6UNj4StJP17pkg68WG\r\n" +
"zMlcjuI7/baxtDrD+O8dKpHoHezqhx7dfh1QWq8jnqd3DFzfkhEpuIt6QEaUqoWn\r\n" +
"t5FxSUiykTfjnaNEEGcn3/n2LpwrQ+upes12/B778MQETOsVv4WX8oE1Qsv1XLRW\r\n" +
"i0DQetTU2RXTrynv+l4kMy0h9b/Hdlbuh2s0QZqlUMXx2biy0GvpF2pR8f+OaLuT\r\n" +
"AtaKdU4T2+jO44+vWNNN2VoAaw0xY6IZ3/A1GL0x\r\n" +
"-----END CERTIFICATE-----&traceNo=624161&traceTime=0323150955&txnAmt=1&txnSubType=01&txnTime=20170323150955&txnType=01&version=5.1.0&signature=vvZVjyn8MLKgCroYOE8dwBBCkT5fyfbRKQJkl1Lk9SmLspGW0N8SI95GZCNleN3pyTMDY5CjrmVDQsnHhPS8Iur3JiFbIUp64ssPSGkKw3q7RyJbvLjp8527pv95PlRLq47adphFTzctBi0pDzV7IdycjafI9if7vjRSTLYWY1Pelo7/SNsHAUl2jjBC7c6E/6od51Uf4LK8U7k00fmfoDGI+xpReGotGptIS9gajh/KFaE62C88TqcTcObDifbQiY7P7sv+Ysoc7iPMa/Tw3iDUwYVGQexl2pkOtdV4sDbuWpR7AgUqtl2kPsYyqzLKFmeNIJzxCtULMnvBaY6QJg==";

  payResultData = "accessType=0&bizType=000201&currencyCode=156&encoding=UTF-8&merId=777290058143823&orderId=20160615092209323&queryId=201703241511197447698"
    +
    "&reqReserved=VXNlcklEPTI5MzAyJkl0ZW1JRD0xJkJpbGxJRD0yMDE2MDYxNTA5MjIwOTMyMCZTZXJ2ZXJJRD0x&respCode=00&respMsg=成功[0000000]&settleAmt=1&settleCurrencyCode=156&settleDate=0324&signMethod=01&signPubKeyCert=-----BEGIN CERTIFICATE-----\r\n"
    +
    "MIIEOjCCAyKgAwIBAgIFEAJkAUkwDQYJKoZIhvcNAQEFBQAwWDELMAkGA1UEBhMC\r\n" +
    "Q04xMDAuBgNVBAoTJ0NoaW5hIEZpbmFuY2lhbCBDZXJ0aWZpY2F0aW9uIEF1dGhv\r\n" +
    "cml0eTEXMBUGA1UEAxMOQ0ZDQSBURVNUIE9DQTEwHhcNMTUxMjA0MDMyNTIxWhcN\r\n" +
    "MTcxMjA0MDMyNTIxWjB5MQswCQYDVQQGEwJjbjEXMBUGA1UEChMOQ0ZDQSBURVNU\r\n" +
    "IE9DQTExEjAQBgNVBAsTCUNGQ0EgVEVTVDEUMBIGA1UECxMLRW50ZXJwcmlzZXMx\r\n" +
    "JzAlBgNVBAMUHjA0MUBaMTJAMDAwNDAwMDA6U0lHTkAwMDAwMDA2MjCCASIwDQYJ\r\n" +
    "KoZIhvcNAQEBBQADggEPADCCAQoCggEBAMUDYYCLYvv3c911zhRDrSWCedAYDJQe\r\n" +
    "fJUjZKI2avFtB2/bbSmKQd0NVvh+zXtehCYLxKOltO6DDTRHwH9xfhRY3CBMmcOv\r\n" +
    "d2xQQvMJcV9XwoqtCKqhzguoDxJfYeGuit7DpuRsDGI0+yKgc1RY28v1VtuXG845\r\n" +
    "fTP7PRtJrareQYlQXghMgHFAZ/vRdqlLpVoNma5C56cJk5bfr2ngDlXbUqPXLi1j\r\n" +
    "iXAFb/y4b8eGEIl1LmKp3aPMDPK7eshc7fLONEp1oQ5Jd1nE/GZj+lC345aNWmLs\r\n" +
    "l/09uAvo4Lu+pQsmGyfLbUGR51KbmHajF4Mrr6uSqiU21Ctr1uQGkccCAwEAAaOB\r\n" +
    "6TCB5jAfBgNVHSMEGDAWgBTPcJ1h6518Lrj3ywJA9wmd/jN0gDBIBgNVHSAEQTA/\r\n" +
    "MD0GCGCBHIbvKgEBMDEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2ZjYS5jb20u\r\n" +
    "Y24vdXMvdXMtMTQuaHRtMDgGA1UdHwQxMC8wLaAroCmGJ2h0dHA6Ly91Y3JsLmNm\r\n" +
    "Y2EuY29tLmNuL1JTQS9jcmw0NDkxLmNybDALBgNVHQ8EBAMCA+gwHQYDVR0OBBYE\r\n" +
    "FAFmIOdt15XLqqz13uPbGQwtj4PAMBMGA1UdJQQMMAoGCCsGAQUFBwMCMA0GCSqG\r\n" +
    "SIb3DQEBBQUAA4IBAQB8YuMQWDH/Ze+e+2pr/914cBt94FQpYqZOmrBIQ8kq7vVm\r\n" +
    "TTy94q9UL0pMMHDuFJV6Wxng4Me/cfVvWmjgLg/t7bdz0n6UNj4StJP17pkg68WG\r\n" +
    "zMlcjuI7/baxtDrD+O8dKpHoHezqhx7dfh1QWq8jnqd3DFzfkhEpuIt6QEaUqoWn\r\n" +
    "t5FxSUiykTfjnaNEEGcn3/n2LpwrQ+upes12/B778MQETOsVv4WX8oE1Qsv1XLRW\r\n" +
    "i0DQetTU2RXTrynv+l4kMy0h9b/Hdlbuh2s0QZqlUMXx2biy0GvpF2pR8f+OaLuT\r\n" +
    "AtaKdU4T2+jO44+vWNNN2VoAaw0xY6IZ3/A1GL0x\r\n" +
    "-----END CERTIFICATE-----&traceNo=744769&traceTime=0324151119&txnAmt=1&txnSubType=01&txnTime=20170324151119&txnType=01&version=5.1.0" +
    "&signature=KHnZ5C3fytjSLAIVS9zkzc2SR5QDs9L00cvq1cGK8fYXopRAFbZID+eV9qjiDFhn5DUxsXFZUMBXoyzlZ5MOvTk143DMkrRyWOgFnT3+puxq9KC7IJunQFVWa6zTiSrkZP844" +
    "azoypRbAPmP6NdIVkRq5PTO00cakDdsvuQYohq98LOTsxYuGKuKUWIUsQzQxjsroBWi6qh778r00ywWIQTqWpQgZDWPKBhN62Db84Cvw/q/5YGRKtzR6NiUwhdQqEkV00ud6SYVBe8lp0cFMilyuyiiLjJxSFoa5kwxemI3CiKtN/AhqQjfTDJpBD1QAjrITmjqjUoGfYNuc6xzhw==";


            //Dictionary<String, String> rspData = interpretingData(payResultData);
            Dictionary<String, String> rspData = SDKUtil.CoverStringToDictionary(payResultData, System.Text.Encoding.UTF8);

            Console.Write("支付回调：" + SDKUtil.CreateLinkString(rspData, false, false, System.Text.Encoding.UTF8) + "\r\n");
            //AcpService.WriteLog("支付回调：" + SDKUtil.CreateLinkString(rspData, false, false, System.Text.Encoding.UTF8) + "\r\n");

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8))
                {
                    Console.Write("商户端验证，支付回调报文签名成功。\r\n");
                    //AcpService.WriteLog("商户端验证，支付回调报文签名成功。\r\n");
                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取
                    if ("00" == respcode)
                    {
                        //成功
                        //TODO
                        Console.Write("支付成功 txnAmt：" + rspData["txnAmt"] + "\r\n");
                        Console.Write("CallbackInfo：" + rspData["reqReserved"] + "\r\n");
                        
                        //Console.Write("后续请将此tn传给手机开发，由他们用此tn调起控件后完成支付。\r\n");
                        //Console.Write("手机端demo默认从仿真获取tn，仿真只返回一个tn，如不想修改手机和后台间的通讯方式，【此页面请修改代码为只输出tn】。\r\n");
                    }
                    else
                    {
                        //其他应答码做以失败处理
                        Console.Write("失败：" + rspData["respMsg"] + "\r\n");

                    }
                }
                else
                {

                    Console.Write("商户端验证返回报文签名失败。\r\n");
                }
            }
            else
            {
                Console.Write("请求失败");
            }

        }
/*
        private bool ValidateAppResponse(string jsonData, Encoding encoding)
        {
            //获取签名
            Dictionary<string, object> data = SDKUtil.JsonToDictionary(jsonData);

            string dataString = (string)data["data"];
            string signString = (string)data["sign"];

            byte[] signByte = Convert.FromBase64String(signString);
            byte[] dataByte = encoding.GetBytes(dataString);
            IDigest digest = DigestUtilities.GetDigest("SHA1");
            digest.BlockUpdate(dataByte, 0, dataByte.Length);
            byte[] dataDigest = DigestUtilities.DoFinal(digest);

            string digestString = BitConverter.ToString(dataDigest).Replace("-", "").ToLower();

            if (appVerifyPubKey == null)
            {
                using (FileStream fileStream = new FileStream("d:/certs/acp_test_app_verify_sign.cer", FileMode.Open))//TODO: 这个是测试环境的证书，切换生产需要改生产证书。
                {
                    X509Certificate certificate = new X509CertificateParser().ReadCertificate(fileStream);
                    this.appVerifyPubKey = certificate.GetPublicKey();
                }
            }
            byte[] digestByte = encoding.GetBytes(digestString);

            ISigner verifier = SignerUtilities.GetSigner("SHA1WithRSA");
            verifier.Init(false, this.appVerifyPubKey);

            verifier.BlockUpdate(digestByte, 0, digestByte.Length);
            return verifier.VerifySignature(signByte);
        }*/

        /*
        public static void WriteLog(string strLog)
        {
            string sFilePath = "d:\\" + DateTime.Now.ToString("yyyyMM");
            string sFileName = "UnionPay" + DateTime.Now.ToString("dd") + ".log";
            sFileName = sFilePath + "\\" + sFileName; //文件的绝对路径
            if (!Directory.Exists(sFilePath))//验证路径是否存在
            {
                Directory.CreateDirectory(sFilePath);
                //不存在则创建
            }
            FileStream fs;
            StreamWriter sw;
            if (File.Exists(sFileName))
            //验证文件是否存在，有则追加，无则创建
            {
                fs = new FileStream(sFileName, FileMode.Append, FileAccess.Write);
            }
            else
            {
                fs = new FileStream(sFileName, FileMode.Create, FileAccess.Write);
            }
            sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + "   ---   " + strLog);
            sw.Close();
            fs.Close();
        }
        */
        /* 
        /// 解析处理支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组 
        private static Dictionary<string, string> interpretingData(string retData)
        {
            string[] sArray = retData.Split('&');

            Dictionary<string, string> sPair = new Dictionary<string, string>();

            for (int i = 0; i < sArray.Length; i++)
            {
                string[] sArray1 = new String[2];
                
                if (sArray[i].StartsWith("signature="))
                {
                    sArray1[0] = sArray[i].Substring(sArray[i].IndexOf("signature"), 9);

                    sArray1[1] = sArray[i].Substring(sArray[i].IndexOf("=") + 1, (sArray[i].LastIndexOf("=") - sArray[i].IndexOf("="))); //截取 sign的内容字符串
                }
                else
                {
                    sArray1 = sArray[i].Split('=');
                }
                
                //sArray1 = sArray[i].Split('=');
                Console.Write(sArray1[0] +" = "+ sArray1[1]+"\r\n");

                sPair.Add(sArray1[0], sArray1[1]);
            }

            return sPair;
        }
        /* */
    }
}
