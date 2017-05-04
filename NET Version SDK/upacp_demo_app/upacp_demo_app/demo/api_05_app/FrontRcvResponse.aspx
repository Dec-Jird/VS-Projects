<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FrontRcvResponse.aspx.cs" Inherits="upacp_demo_app.demo.api_05_app.FrontRcvResponse" %>

<%@ Import Namespace="com.unionpay.acp.sdk" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <table  border="1">
    <%
        // **************演示前台接收银联返回报文交易结果展示***********************
        
        /*if (Request.HttpMethod == "POST")
        {
            // 使用Dictionary保存参数
            Dictionary<string, string> resData = new Dictionary<string, string>();

            NameValueCollection coll = Request.Form;

            string[] requestItem = coll.AllKeys;

            for (int i = 0; i < requestItem.Length; i++)
            {
                resData.Add(requestItem[i], Request.Form[requestItem[i]]);
            }*/

            string data = "accNo=6226********0048&accessType=0&bizType=000201&currencyCode=156&encoding=UTF-8&merId=777290058110048&orderId=20170324101151&queryId=201703241011516777928&respCode=00&respMsg=成功[0000000]&settleAmt=1&settleCurrencyCode=156&settleDate=0324&signMethod=01&signPubKeyCert=-----BEGIN CERTIFICATE-----\n" +
"MIIEOjCCAyKgAwIBAgIFEAJkAUkwDQYJKoZIhvcNAQEFBQAwWDELMAkGA1UEBhMC\n" +
"Q04xMDAuBgNVBAoTJ0NoaW5hIEZpbmFuY2lhbCBDZXJ0aWZpY2F0aW9uIEF1dGhv\n" +
"cml0eTEXMBUGA1UEAxMOQ0ZDQSBURVNUIE9DQTEwHhcNMTUxMjA0MDMyNTIxWhcN\n" +
"MTcxMjA0MDMyNTIxWjB5MQswCQYDVQQGEwJjbjEXMBUGA1UEChMOQ0ZDQSBURVNU\n" +
"IE9DQTExEjAQBgNVBAsTCUNGQ0EgVEVTVDEUMBIGA1UECxMLRW50ZXJwcmlzZXMx\n" +
"JzAlBgNVBAMUHjA0MUBaMTJAMDAwNDAwMDA6U0lHTkAwMDAwMDA2MjCCASIwDQYJ\n" +
"KoZIhvcNAQEBBQADggEPADCCAQoCggEBAMUDYYCLYvv3c911zhRDrSWCedAYDJQe\n" +
"fJUjZKI2avFtB2/bbSmKQd0NVvh+zXtehCYLxKOltO6DDTRHwH9xfhRY3CBMmcOv\n" +
"d2xQQvMJcV9XwoqtCKqhzguoDxJfYeGuit7DpuRsDGI0+yKgc1RY28v1VtuXG845\n" +
"fTP7PRtJrareQYlQXghMgHFAZ/vRdqlLpVoNma5C56cJk5bfr2ngDlXbUqPXLi1j\n" +
"iXAFb/y4b8eGEIl1LmKp3aPMDPK7eshc7fLONEp1oQ5Jd1nE/GZj+lC345aNWmLs\n" +
"l/09uAvo4Lu+pQsmGyfLbUGR51KbmHajF4Mrr6uSqiU21Ctr1uQGkccCAwEAAaOB\n" +
"6TCB5jAfBgNVHSMEGDAWgBTPcJ1h6518Lrj3ywJA9wmd/jN0gDBIBgNVHSAEQTA/\n" +
"MD0GCGCBHIbvKgEBMDEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2ZjYS5jb20u\n" +
"Y24vdXMvdXMtMTQuaHRtMDgGA1UdHwQxMC8wLaAroCmGJ2h0dHA6Ly91Y3JsLmNm\n" +
"Y2EuY29tLmNuL1JTQS9jcmw0NDkxLmNybDALBgNVHQ8EBAMCA+gwHQYDVR0OBBYE\n" +
"FAFmIOdt15XLqqz13uPbGQwtj4PAMBMGA1UdJQQMMAoGCCsGAQUFBwMCMA0GCSqG\n" +
"SIb3DQEBBQUAA4IBAQB8YuMQWDH/Ze+e+2pr/914cBt94FQpYqZOmrBIQ8kq7vVm\n" +
"TTy94q9UL0pMMHDuFJV6Wxng4Me/cfVvWmjgLg/t7bdz0n6UNj4StJP17pkg68WG\n" +
"zMlcjuI7/baxtDrD+O8dKpHoHezqhx7dfh1QWq8jnqd3DFzfkhEpuIt6QEaUqoWn\n" +
"t5FxSUiykTfjnaNEEGcn3/n2LpwrQ+upes12/B778MQETOsVv4WX8oE1Qsv1XLRW\n" +
"i0DQetTU2RXTrynv+l4kMy0h9b/Hdlbuh2s0QZqlUMXx2biy0GvpF2pR8f+OaLuT\n" +
"AtaKdU4T2+jO44+vWNNN2VoAaw0xY6IZ3/A1GL0x\n" +
"-----END CERTIFICATE-----&traceNo=677792&traceTime=0324101151&txnAmt=1&txnSubType=01&txnTime=20170324101151&txnType=01&version=5.1.0&signature=jxSjw7B5FUguBjJ3MUBnviTSVqbVuIEi9aq42oS6riI7FBjJDZvRwcs7WXZbkxoeUIKUIhH83/wUuBlSmfP11yu8UWySL5HBYmx4oiSF1U0R3miqGziKD/4HLZQni9N+smrO0t6t5K2vbqrNzDEl9vfQbAng5ogg41j07fAQzn5AhotgJrttUY8WN2zkggfauB8Xml163NEsNy4qj+Q5lOQhWMwHpb+DaSG565PNQ5BeccTWYe19jXxZ+wYbORux5RZysrvvEt7Y08JAdEkE6mMpplQv1HA91kMLqa8cGZ5kDdVtGPSpVC22WOmiHyiiFnsL01e1/EEv2+SfNn3fVg==";

            data = "accessType=0&bizType=000201&currencyCode=156&encoding=UTF-8&merId=777290058143823&orderId=20160615092209323&queryId=201703241511197447698&reqReserved=VXNlcklEPTI5MzAyJkl0ZW1JRD0xJkJpbGxJRD0yMDE2MDYxNTA5MjIwOTMyMCZTZXJ2ZXJJRD0x&respCode=00&respMsg=成功[0000000]&settleAmt=1&settleCurrencyCode=156&settleDate=0324&signMethod=01&signPubKeyCert=-----BEGIN CERTIFICATE-----\n" +
    "MIIEOjCCAyKgAwIBAgIFEAJkAUkwDQYJKoZIhvcNAQEFBQAwWDELMAkGA1UEBhMC\n" +
    "Q04xMDAuBgNVBAoTJ0NoaW5hIEZpbmFuY2lhbCBDZXJ0aWZpY2F0aW9uIEF1dGhv\n" +
    "cml0eTEXMBUGA1UEAxMOQ0ZDQSBURVNUIE9DQTEwHhcNMTUxMjA0MDMyNTIxWhcN\n" +
    "MTcxMjA0MDMyNTIxWjB5MQswCQYDVQQGEwJjbjEXMBUGA1UEChMOQ0ZDQSBURVNU\n" +
    "IE9DQTExEjAQBgNVBAsTCUNGQ0EgVEVTVDEUMBIGA1UECxMLRW50ZXJwcmlzZXMx\n" +
    "JzAlBgNVBAMUHjA0MUBaMTJAMDAwNDAwMDA6U0lHTkAwMDAwMDA2MjCCASIwDQYJ\n" +
    "KoZIhvcNAQEBBQADggEPADCCAQoCggEBAMUDYYCLYvv3c911zhRDrSWCedAYDJQe\n" +
    "fJUjZKI2avFtB2/bbSmKQd0NVvh+zXtehCYLxKOltO6DDTRHwH9xfhRY3CBMmcOv\n" +
    "d2xQQvMJcV9XwoqtCKqhzguoDxJfYeGuit7DpuRsDGI0+yKgc1RY28v1VtuXG845\n" +
    "fTP7PRtJrareQYlQXghMgHFAZ/vRdqlLpVoNma5C56cJk5bfr2ngDlXbUqPXLi1j\n" +
    "iXAFb/y4b8eGEIl1LmKp3aPMDPK7eshc7fLONEp1oQ5Jd1nE/GZj+lC345aNWmLs\n" +
    "l/09uAvo4Lu+pQsmGyfLbUGR51KbmHajF4Mrr6uSqiU21Ctr1uQGkccCAwEAAaOB\n" +
    "6TCB5jAfBgNVHSMEGDAWgBTPcJ1h6518Lrj3ywJA9wmd/jN0gDBIBgNVHSAEQTA/\n" +
    "MD0GCGCBHIbvKgEBMDEwLwYIKwYBBQUHAgEWI2h0dHA6Ly93d3cuY2ZjYS5jb20u\n" +
    "Y24vdXMvdXMtMTQuaHRtMDgGA1UdHwQxMC8wLaAroCmGJ2h0dHA6Ly91Y3JsLmNm\n" +
    "Y2EuY29tLmNuL1JTQS9jcmw0NDkxLmNybDALBgNVHQ8EBAMCA+gwHQYDVR0OBBYE\n" +
    "FAFmIOdt15XLqqz13uPbGQwtj4PAMBMGA1UdJQQMMAoGCCsGAQUFBwMCMA0GCSqG\n" +
    "SIb3DQEBBQUAA4IBAQB8YuMQWDH/Ze+e+2pr/914cBt94FQpYqZOmrBIQ8kq7vVm\n" +
    "TTy94q9UL0pMMHDuFJV6Wxng4Me/cfVvWmjgLg/t7bdz0n6UNj4StJP17pkg68WG\n" +
    "zMlcjuI7/baxtDrD+O8dKpHoHezqhx7dfh1QWq8jnqd3DFzfkhEpuIt6QEaUqoWn\n" +
    "t5FxSUiykTfjnaNEEGcn3/n2LpwrQ+upes12/B778MQETOsVv4WX8oE1Qsv1XLRW\n" +
    "i0DQetTU2RXTrynv+l4kMy0h9b/Hdlbuh2s0QZqlUMXx2biy0GvpF2pR8f+OaLuT\n" +
    "AtaKdU4T2+jO44+vWNNN2VoAaw0xY6IZ3/A1GL0x\n" +
    "-----END CERTIFICATE-----&traceNo=744769&traceTime=0324151119&txnAmt=1&txnSubType=01&txnTime=20170324151119&txnType=01&version=5.1.0&signature=KHnZ5C3fytjSLAIVS9zkzc2SR5QDs9L00cvq1cGK8fYXopRAFbZID+eV9qjiDFhn5DUxsXFZUMBXoyzlZ5MOvTk143DMkrRyWOgFnT3+puxq9KC7IJunQFVWa6zTiSrkZP844azoypRbAPmP6NdIVkRq5PTO00cakDdsvuQYohq98LOTsxYuGKuKUWIUsQzQxjsroBWi6qh778r00ywWIQTqWpQgZDWPKBhN62Db84Cvw/q/5YGRKtzR6NiUwhdQqEkV00ud6SYVBe8lp0cFMilyuyiiLjJxSFoa5kwxemI3CiKtN/AhqQjfTDJpBD1QAjrITmjqjUoGfYNuc6xzhw==";

            Dictionary<String, String> resData = SDKUtil.CoverStringToDictionary(data, System.Text.Encoding.UTF8);

            Response.Write("支付回调：" + SDKUtil.CreateLinkString(resData, false, false, System.Text.Encoding.UTF8) + "\n");
        
             // 返回报文中不包含UPOG,表示Server端正确接收交易请求,则需要验证Server端返回报文的签名
            if (AcpService.Validate(resData, System.Text.Encoding.UTF8))
                {
                    //Response.Write("商户端验证返回报文签名成功\n");

                    string respcode = resData["respCode"]; //00、A6为成功，其余为失败。其他字段也可按此方式获取。

                    //如果卡号我们业务配了会返回且配了需要加密的话，请按此方法解密
                    //if(resData.ContainsKey("accNo"))
                    //{
                    //    string accNo = SecurityUtil.DecryptData(resData["accNo"], System.Text.Encoding.UTF8); 
                    //}

                    //商户端根据返回报文内容处理自己的业务逻辑 ,DEMO此处只输出报文结果
                    StringBuilder builder = new StringBuilder();

                    builder.Append("<tr><td align=\"center\" colspan=\"2\"><b>商户端接收银联返回报文并按照表格形式输出结果</b></td></tr>");
                    
                    /*for (int i = 0; i < requestItem.Length; i++)
                    {
                        builder.Append("<tr><td width=\"30%\" align=\"right\">" + requestItem[i] + "</td><td style='word-break:break-all'>" + Request.Form[requestItem[i]] + "</td></tr>");
                    }*/
                    
                    builder.Append("<tr><td width=\"30%\" align=\"right\">商户端验证银联返回报文结果</td><td>验证签名成功.</td></tr>");
                    Response.Write(builder.ToString());
                    
                }
                else
                {
                    Response.Write("<tr><td width=\"30%\" align=\"right\">商户端验证银联返回报文结果</td><td>验证签名失败.</td></tr>");
                }       
        
        //}

         %>
         </table>
    </form>
</body>

</html>

    
    
   