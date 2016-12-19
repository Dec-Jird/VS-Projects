using System;
using iapppay.sign;
using System.Runtime.InteropServices;

namespace iapppaySigndll
{
    public interface IiapppaySign
    {
        bool verifyBill(string BillInfo, string PublicKey, string sign);
        string SignBill(string BillInfo, string PrivateKey);
    }


    [ClassInterface(ClassInterfaceType.None)]
    public class TiapppaySign : IiapppaySign
    {
        public TiapppaySign()
        {

        }

        public bool verifyBill(string BillInfo, string PublicKey, string sign)
        {
            try
            {
                // 验签
                if (SignHelper.verify(BillInfo, sign, PublicKey))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } catch
            {
                return false;
            }

        }

        public string SignBill(string BillInfo, string PrivateKey)
        {
            string sign = "";
            try
            {
                // 签名
                sign = SignHelper.sign(BillInfo, PrivateKey);
                return sign;
            }
            catch
            {
                return sign;
            }
        }

        #region IiapppaySign 成员

        bool IiapppaySign.verifyBill(string BillInfo, string PublicKey, string sign)
        {
            TiapppaySign TemSign = new TiapppaySign();
            return TemSign.verifyBill(BillInfo, PublicKey, sign);
        }

        #endregion

        #region IiapppaySign 成员


        string IiapppaySign.SignBill(string BillInfo, string PrivateKey)
        {
            TiapppaySign TemSign = new TiapppaySign();
            return TemSign.SignBill(BillInfo, PrivateKey);
        }

        #endregion
    }


}
