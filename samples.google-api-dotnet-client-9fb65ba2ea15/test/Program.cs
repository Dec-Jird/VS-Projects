using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CibGooglePlay;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Start Verify");
            TCibGooglePlay cib = new TCibGooglePlay();
            //System.Console.WriteLine(cib.VerifyBill("com.Tnyoo.xf", "com.tnyoo.xf.5", "lhndemkjbijifbmogbhfeojc.AO-J1OxUoRYInvzM_X2r5mn7Pb37u02HfXAiImtpJAkpX8ZGIYOOFFv3LJhUlgJkibyIWp7Cj68QYRdRc3tgFlCq77bW8TfTGAVg1T6FhmAMtpjqAqRADEA"));

            //Console.ReadLine();

            System.Console.WriteLine(cib.VerifyBill("Google Play Android Developer-7960cc81208d.p12", "dot-inapp@api-project-747314688519.iam.gserviceaccount.com", "com.playpark.dot", "60_medals_pack",
                "llkfgihekchphgepmfckioai.AO-J1Oza2fBJpqbrWldsc9QQssr4z4S5TjOBFCQk0ZqERzmgXZsBR-TNRFK8VlWBBwm1hHbTj1aORci5X8ZdykOWgQojASAN6_oyiHvzJp-7uRfFaYfOYM8IDyNpZDR6twZo609by2d1"));
            Console.ReadLine();
        }
    }
}
