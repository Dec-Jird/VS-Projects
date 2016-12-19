/*
Copyright 2012 Google Inc

Licensed under the Apache License, Version 2.0(the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using System;
using System.Security.Cryptography.X509Certificates;

using CibGooglePlay;

namespace Google.Apis.Samples.PlusServiceAccount
{
    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {

                TCibGooglePlay cib = new TCibGooglePlay();
                //System.Console.WriteLine(cib.VerifyBill("com.Tnyoo.xf", "com.tnyoo.xf.5", "lhndemkjbijifbmogbhfeojc.AO-J1OxUoRYInvzM_X2r5mn7Pb37u02HfXAiImtpJAkpX8ZGIYOOFFv3LJhUlgJkibyIWp7Cj68QYRdRc3tgFlCq77bW8TfTGAVg1T6FhmAMtpjqAqRADEA"));

                 Console.ReadLine();
                /*Console.WriteLine("Plus API - Service Account");
                Console.WriteLine("==========================");

                String serviceAccountEmail = "SERVICE_ACCOUNT_EMAIL_HERE";

                var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);

                ServiceAccountCredential credential = new ServiceAccountCredential(
                   new ServiceAccountCredential.Initializer(serviceAccountEmail)
                   {
                       Scopes = new[] { AndroidPublisherService.Scope.Androidpublisher }
                   }.FromCertificate(certificate));

                // Create the service.
                var service = new AndroidPublisherService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "cib",
                });

                var request = service.Purchases.Products.Get("com.Tnyoo.xf", "com.tnyoo.xf.5", "lhndemkjbijifbmogbhfeojc.AO-J1OxUoRYInvzM_X2r5mn7Pb37u02HfXAiImtpJAkpX8ZGIYOOFFv3LJhUlgJkibyIWp7Cj68QYRdRc3tgFlCq77bW8TfTGAVg1T6FhmAMtpjqAqRADEA");

                var result = request.Execute();
                if (result.PurchaseState == 0)
                    Console.WriteLine(@"{result:true}");
                else
                    Console.WriteLine("{result:false}");*/

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}