using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using DES3;

//需要控制台输出结果时修改：
//点工程DES3Dll->属性，修改输出类型为：控制台应用程序，修改默认命名空间为：TestConsoleApp，保存-run

//需要生成dll文件时修改：(首先注释TestConsoleApp.cs文件下面的Main函数代码）
////点工程DES3Dll->属性，修改输出类型为：类库，修改默认命名空间为：DES3Dll，保存-run

namespace DES3Dll
{
    //创建Delphi中可见的接口，用于将Delphi代码和dll内部函数
    public interface DES3DllInterface
    {
        string Encrypt3DES(string a_strString, string a_strKey);
        string Decrypt3DES(string a_strString, string a_strKey);
    }

    //Delphi声明调用使用的类名：TestCSharpClass
    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class DES3DllClass : DES3DllInterface
    {
        //--------这里调用DES3Utils中加密解密的真正代码-----------
        public string Encrypt3DES(string data, string key)
        {
            return DES3Utils.Encrypt3DES(data, key);
        }

        public string Decrypt3DES(string data, string key)
        {
            return DES3Utils.Decrypt3DES(data, key);
        }
        //--------这里调用DES3Utils中加密解密的真正代码-----------



        //--------这里是Delphi调用的接口的实现-----------
        #region DES3DllInterface 成员实现(Delphi调用的)

        string DES3DllInterface.Encrypt3DES(string data, string key)
        {
            DES3DllClass des = new DES3DllClass();

            return des.Encrypt3DES(data, key);
        }

        string DES3DllInterface.Decrypt3DES(string data, string key)
        {
            DES3DllClass des = new DES3DllClass();

            return des.Decrypt3DES(data, key);
        }
        //--------这里是Delphi调用的接口的实现-----------
        #endregion
    }
   
}
