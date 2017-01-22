using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.AndroidPublisher.v2;
using Google.Apis.AndroidPublisher.v2.Data;
using Google.Apis.Auth.OAuth2.Responses;

namespace CibGooglePlay
{
    public interface ICibGooglePlay
    {
        string VerifyBill(string p12Name, string account, string PackedName, string ItemID, string token);
    }

    [ClassInterface(ClassInterfaceType.None)]
    public class TCibGooglePlay : ICibGooglePlay
    {

        public string VerifyBill(string p12Name, string account, string PackedName, string ItemID, string token)
        {
            
            try
            {
                //应用程序域AppDomain，作用：代码隔离，它是一个应用程序在其中执行的独立环境
                AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", "dll");//SetData：为指定的应用程序域属性分配指定值。
                AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", "dll");

                //String serv iceAccountEmail = "902399004986-4a3mct91lob522lrp5q38temdr9vp31b@developer.gserviceaccount.com";
                String serviceAccountEmail = account;
                //String serviceAccountEmail = "78050930768-compute@developer.gserviceaccount.com";
                //serviceAccountEmail = "dot-inapp@api-project-747314688519.iam.gserviceaccount.com";

                //var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);
                //var certificate = new X509Certificate2(@"Google Play Android Developer-7960cc81208d.p12", "notasecret", X509KeyStorageFlags.Exportable);
                // 使用字节数组、密码和密钥存储标志,初始化X509Certificate2类的新实例。
                var certificate = new X509Certificate2(p12Name, "notasecret", X509KeyStorageFlags.Exportable);

                ServiceAccountCredential credential = new ServiceAccountCredential( 
                new ServiceAccountCredential.Initializer(serviceAccountEmail)//Constructs a new initializer using the given id
                {
                    //
                    Scopes = new[] { AndroidPublisherService.Scope.Androidpublisher }//
                    //Scopes = new[] {"https://www.googleapis.com/auth/androidpublisher"} 

                }.FromCertificate(certificate));//从给点定证书中提取


                // Create the service.
                var service = new AndroidPublisherService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "cib",             
                });

                //给定商品ID(itemId)、packedname、token（及支付返回的purchaseToken），查询商品当前的购买和消耗状态
                var request = service.Purchases.Products.Get(PackedName, ItemID, token);

                var result = request.Execute();//返回数据结构见：https://developers.google.com/android-publisher/api-ref/purchases/products

                //System.Console.WriteLine("result: " + result.ToString());

                string retStr = "{\"purchaseTimeMillis\":" + result.PurchaseTimeMillis + ",\"purchaseState\":" + result.PurchaseState + ",\"developerPayload\":" + "\"" + result.DeveloperPayload + "\"}";
                return retStr;

                /*if (result.PurchaseState == 0)
                    return @"true";
                else
                    return "false";
                */ 
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        #region ICibGooglePlay 成员
        string ICibGooglePlay.VerifyBill(string p12Name, string account, string PackedName, string ItemID, string token)
        {
            TCibGooglePlay cib = new TCibGooglePlay();
            return cib.VerifyBill(p12Name, account, PackedName, ItemID, token);
        }
        #endregion
    }
}