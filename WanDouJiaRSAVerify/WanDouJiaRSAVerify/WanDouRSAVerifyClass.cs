using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using WandoujiaRSA;

//需要控制台输出结果时修改：
//点工程WanDouJiaRSAVerify->属性，修改输出类型为：控制台应用程序，修改默认命名空间为：TestConsoleApp，保存-run

//需要生成dll文件时修改：(首先注释Program.cs文件下面的Main函数代码）
////点工程WanDouJiaRSAVerify->属性，修改输出类型为：类库，修改默认命名空间为：WanDouJiaRSAVerify，保存-run

namespace WanDouJiaRSAVerify
{
    //创建Delphi中可见的接口，用于将Delphi代码和dll内部函数，如下面的AplusB
    public interface WanDouRSAVerifyInterface
    {
        bool RSAVerify(string data, string sign, string publickey);
    }

    //Delphi声明调用使用的类名：TestCSharpClass
    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class WanDouRSAVerifyClass : WanDouRSAVerifyInterface
    {
        public bool RSAVerify(string data, string sign, string publickey)
        {
            return RSAUtils.VerifyData(data, sign, publickey);
        }

        #region WanDouRSAVerifyInterface 成员实现

        bool WanDouRSAVerifyInterface.RSAVerify(string data, string sign, string publickey)
        {
            WanDouRSAVerifyClass rsa = new WanDouRSAVerifyClass();

            return rsa.RSAVerify(data,sign,publickey);
        }

        #endregion
    }
   
}
