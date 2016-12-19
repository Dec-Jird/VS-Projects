using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace DephiUseCSharp
{

    //创建Delphi中可见的接口，用于将Delphi代码和dll内部函数，如下面的AplusB
    public interface TestCSharpInterface
    {
        long TestPlus(long a, long b);
        long TestMultiply(long a, long b);
    }

	//Delphi声明调用使用的类名：TestCSharpClass
    [ClassInterface(ClassInterfaceType.None)]//这里需要using System.Runtime.InteropServices;
    public class TestCSharpClass : TestCSharpInterface
    {
        public long AplusB(long a, long b)
        {
            return a + b;
        }

        public long AmultiplyB(long a, long b)
        {
            return a * b;
        }

        #region TestCSharpInterface 成员实现

        long TestCSharpInterface.TestPlus(long a, long b)
        {
            TestCSharpClass test = new TestCSharpClass();
            
            return test.AplusB(a, b);
        }

        long TestCSharpInterface.TestMultiply(long a, long b)
        {
            TestCSharpClass test = new TestCSharpClass();

            return test.AmultiplyB(a, b);
        }

        #endregion
    }
}
