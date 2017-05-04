using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Aop.Api.Util;
using Aop.Api.Domain;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api;
using System.Runtime.InteropServices;

namespace AopApiSDK
{
    //创建Delphi中可见的接口，用于将Delphi代码和dll内部函数，如下面的AplusB
    public interface  AlipaySignAndVerifyInterface
    {
        string GetAliOrder(string ali_gateway, string app_id, string app_priv_key, string ali_pub_key, string subject, string total_amount,
            string our_trade_no, string notify_url, string callbackInfo);

        bool AlipayVerify(string ret_data, string ali_pub_key);
    }

    //Delphi声明调用使用的类名：TestCSharpClass
    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class AlipaySignAndVerifyClass : AlipaySignAndVerifyInterface
    {
        private string CHARSET = "utf-8"; //写死为uft-8
        private string SIGN_TYPE = "RSA2"; //签名类型RSA2

        public string GetAliOrder(string ali_gateway, string app_id, string app_priv_key, string ali_pub_key, string subject, string total_amount, 
            string out_trade_no, string notify_url, string callbackInfo)
        {
            //        //支付宝网关地址 ali_gateway
            //        // -----沙箱地址-----
            //        string serverUrl = "http://openapi.alipaydev.com/gateway.do";
            //        // -----线上地址-----
            //        // string serverUrl = "https://openapi.alipay.com/gateway.do"; 

            IAopClient client = new DefaultAopClient(ali_gateway, app_id, app_priv_key, "json", "1.0", SIGN_TYPE, ali_pub_key, CHARSET, false);

            //实例化具体API对应的request类,类名称和接口名称对应,当前调用接口名称如：alipay.trade.app.pay
            AlipayTradeAppPayRequest request = new AlipayTradeAppPayRequest();
            request.SetNotifyUrl(notify_url); //单独设置回调url
            
            //SDK已经封装掉了公共参数，这里只需要传入业务参数。以下方法为sdk的model入参方式(model和biz_content同时存在的情况下取biz_content)。
            AlipayTradeAppPayModel model = new AlipayTradeAppPayModel();
            //model.Body = "我是测试数据";
            model.Subject = subject; //商品标题
            model.TotalAmount = total_amount; //价格
            model.ProductCode = "QUICK_MSECURITY_PAY"; //固定值 QUICK_MSECURITY_PAY
            model.OutTradeNo = out_trade_no; //我方订单号
            model.TimeoutExpress = "30m"; //订单超时时间定为 30分钟
            model.PassbackParams = callbackInfo;//callbackInfo
            request.SetBizModel(model);

            //这里和普通的接口调用不同，使用的是sdkExecute
            AlipayTradeAppPayResponse response = client.SdkExecute(request);
            
            StringBuilder sb = new StringBuilder(response.Body);
            //sb.Replace(";", "&"); //替换字符串中";"为"&"

            //HttpUtility.HtmlEncode是为了输出到页面时防止被浏览器将关键参数html转义，实际打印到日志以及http传输不会有这个问题
            //Response.Write(HttpUtility.HtmlEncode(response.Body));
            //Console.WriteLine("Response：" + HttpUtility.HtmlEncode(response.Body));
            //Console.WriteLine("Response：" + sb);
            //页面输出的response.Body就是orderString 可以直接给客户端请求，无需再做处理。
            //获取到的字符串，要把分号去掉
            //Console.ReadLine();

            return sb.ToString();
        }

        public bool AlipayVerify(string ret_data, string ali_pub_key)
        {
            //如果未进行urlDecode，则必须进行urlDecode，验证才正确，这里已经在delphi端进行过解码，无需再次解码。
            //returnData = System.Web.HttpUtility.UrlDecode(returnData, System.Text.Encoding.GetEncoding("utf-8"));

            //切记alipaypublickey是支付宝的公钥，请去open.alipay.com对应应用下查看。
            //bool RSACheckV1(IDictionary<string, string> parameters, string alipaypublicKey, string charset, string signType, bool keyFromFile)
            bool flag = AlipaySignature.RSACheckV1(interpretingData(ret_data), ali_pub_key, CHARSET, SIGN_TYPE, false);
            //Console.WriteLine("result of RSACheckV1: " + flag.ToString());
            return flag;
        }

        /// 解析处理支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组 
        private static Dictionary<string, string> interpretingData(string retData)
        {
            string[] sArray = retData.Split('&');

            Dictionary<string, string> sPair = new Dictionary<string, string>();

            for (int i = 0; i < sArray.Length; i++)
            {
                string[] sArray1 = new String[2];
                if (sArray[i].StartsWith("sign="))
                {
                    sArray1[0] = sArray[i].Substring(sArray[i].IndexOf("sign"), 4);

                    sArray1[1] = sArray[i].Substring(sArray[i].IndexOf("=") + 1, (sArray[i].LastIndexOf("=") - sArray[i].IndexOf("="))); //截取 sign的内容字符串
                }
                else
                {
                    sArray1 = sArray[i].Split('=');
                }

                sPair.Add(sArray1[0], sArray1[1]);
            }

            return sPair;
        }

        #region AlipaySignAndVerifyInterface 成员实现

        string AlipaySignAndVerifyInterface.GetAliOrder(string ali_gateway, string app_id, string app_priv_key, string ali_pub_key, string subject, string total_amount,
            string our_trade_no, string notify_url, string callbackInfo)
        {
            AlipaySignAndVerifyClass alipay = new AlipaySignAndVerifyClass();

            return alipay.GetAliOrder(ali_gateway, app_id, app_priv_key, ali_pub_key, subject, total_amount, our_trade_no, notify_url, callbackInfo);
        }

        bool AlipaySignAndVerifyInterface.AlipayVerify(string ret_data, string ali_pub_key)
        {
            AlipaySignAndVerifyClass alipay = new AlipaySignAndVerifyClass();

            return alipay.AlipayVerify(ret_data, ali_pub_key);
        }
        #endregion
    }
}
