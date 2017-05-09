using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using com.unionpay.acp.sdk;
using MyLog;

//Too young,Too simple,Too naive,Too foolish.
namespace UnionPayDLL
{
    //创建Delphi中可见的接口，用于将Delphi代码和dll内部函数，如下面的AplusB
    public interface UnionPayDLLInterface   

    {
        string GetUnionpayOrder(string merchant_id, string order_id, string order_time, string order_amount, string callbackInfo,
            string request_url, string   notify_url);

        bool UnionpayVerify(string pay_ret_data);
    }

    //Delphi声明调用使用的类名：TestCSharpClass
    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class UnionPayDLLClass : UnionPayDLLInterface
    {

        public string GetUnionpayOrder(string merchant_id, string order_id, string order_time, string order_amount, string callbackInfo,
            string request_url, string notify_url)
        {
            string result_tn = "";
            /**
             * 重要：联调测试时请仔细阅    读注释！
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
            param["currencyCode"] = "156";//交易币种

            //TODO 以下信息需要填写
            //param["frontUrl"] = SDKConfig.FrontUrl;  //前台通知地址      
            param["backUrl"] = notify_url;//"http://182.254.244.236:3358/ucpay";  //后台通知地址
            param["merId"] = merchant_id;//"777290058143823";//商户号，请改自己的测试商户号，此处默认取demo演示页面传递的参数
            param["orderId"] = order_id;//DateTime.Now.ToString("yyyyMMddHHmmssfff");//商户订单号，8-32位数字字母，不能含“-”或“_”，此处默认取demo演示页面传递的参数，可以自行定制规则
            param["txnTime"] = order_time;//DateTime.Now.ToString("yyyyMMddHHmmss");//订单发送时间，格式为YYYYMMDDhhmmss，取北京时间，此处默认取demo演示页面传递的参数，参考取法： DateTime.Now.ToString("yyyyMMddHHmmss")
            param["txnAmt"] = order_amount;//"1";//交易金额，单位分，此处默认取demo演示页面传递的参数
            param["reqReserved"] = callbackInfo;//"TNYOOCallbackInfoUnionPay";//callbackInfo 商户自定义保留域，交易应答时会原样返回
            //string url = "https://gateway.test.95516.com/gateway/api/appTransReq.do"; //request_url

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

            Dictionary<String, String> rspData = AcpService.Post(param, request_url, System.Text.Encoding.UTF8);

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8))
                {
                    //Console.Write("商户端验证返回报文签名成功。\n");
                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取 
                    if ("00" == respcode)
                    {
                        //成功
                        result_tn = rspData["tn"];
                    }
                }
            }

            return result_tn;//返回数据
        }

        public bool UnionpayVerify(string pay_ret_data)
        {
            bool result = false;
            Dictionary<String, String> rspData = SDKUtil.CoverStringToDictionary(pay_ret_data, System.Text.Encoding.UTF8);
            Log.i("支付回调：" + SDKUtil.CreateLinkString(rspData, false, false, System.Text.Encoding.UTF8) + "\r\n");

            if (rspData.Count != 0)
            {
                if (AcpService.Validate(rspData, System.Text.Encoding.UTF8))
                {
                    //Console.Write("商户端验证，支付回调报文签名成功。\r\n");
                    Log.i("商户端验证，支付回调报文签名成功。\r\n");

                    string respcode = rspData["respCode"]; //其他应答参数也可用此方法获取
                    if ("00" == respcode)
                    {
                        //支付成功
                        result = true;
                        Log.i("支付成功 txnAmt：" + rspData["txnAmt"] + "\r\n");
                        Log.i("CallbackInfo：" + rspData["reqReserved"] + "\r\n");
                    }
                    else
                    {
                        //其他应答码做以失败处理
                        Log.i("失败：" + rspData["respMsg"] + "\r\n");
                    }
                } 
                else
                {
                    Log.i("商户端验证返回报文签名失败。\r\n");
                }
            }
            else
            {
                Log.i("请求失败");
            }
            return result;
        }



        #region UnionPayDLLInterface 成员实现

        string UnionPayDLLInterface.GetUnionpayOrder(string merchant_id, string order_id, string order_time, string order_amount, string callbackInfo,
            string request_url, string notify_url)
        {
            UnionPayDLLClass unionpay = new UnionPayDLLClass();

            return unionpay.GetUnionpayOrder(merchant_id, order_id, order_time, order_amount, callbackInfo, request_url, notify_url);
        }

        bool UnionPayDLLInterface.UnionpayVerify(string pay_ret_data)
        {
            UnionPayDLLClass unionpay = new UnionPayDLLClass();

            return unionpay.UnionpayVerify(pay_ret_data);
        }

        #endregion

    }
}

