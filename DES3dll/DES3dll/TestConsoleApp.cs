using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DES3;

namespace TestConsoleApp
{
    class TestConsoleApp
    {
        static void Main()
        {
            // Console.WriteLine("hello world");
            // Console.ReadLine();
            System.Text.Encoding utf8 = System.Text.Encoding.UTF8;
            string sourceTxt = "中华人民共和国111222ddd";
            string appSecert = "ekHvMIlPJ3s3aOphE11I42hx";

            Console.WriteLine("原串：" + sourceTxt);

            string result = DES3Utils.Encrypt3DES(sourceTxt, appSecert);
            Console.WriteLine("加密："+result);
            Console.ReadLine();

            string result2 = DES3Utils.Decrypt3DES(result, appSecert);
            Console.WriteLine("解密：" + result2);
            Console.ReadLine();
        }
    }
}
