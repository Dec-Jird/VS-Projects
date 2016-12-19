using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace iapppay
{
    namespace sign
    {
        class RSA
        {
            /**
	        * RSA验签名检查
	        * @param content 待签名数据
	        * @param sign 签名值
	        * @param ali_public_key  爱贝公钥
	        * @param input_charset 编码格式
	        * @return 布尔值
	        */
            public static bool verify(string content, string sign, string iapp_pub_key, string input_charset)
            {
                bool result = false;
                byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
                byte[] data = Convert.FromBase64String(sign);
                RSAParameters paraPub = ConvertFromPublicKey(iapp_pub_key);
                RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
                rsaPub.ImportParameters(paraPub);
                MD5 md5 = new MD5CryptoServiceProvider();
                result = rsaPub.VerifyData(Data, md5, data);
                return result;
            }


            public static bool verifySHA1(string content, string sign, string iapp_pub_key, string input_charset)
            {
                bool result = false;
                byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
                byte[] data = Convert.FromBase64String(sign);
                RSAParameters paraPub = ConvertFromPublicKey(iapp_pub_key);
                RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
                rsaPub.ImportParameters(paraPub);
                result = rsaPub.VerifyData(Data, CryptoConfig.MapNameToOID("SHA1"), data);
                return result;
            }

            /**
	        * RSA签名
	        * @param content 待签名数据
	        * @param privateKey 商户私钥
	        * @param input_charset 编码格式
	        * @return 签名值
	        */
            public static string sign(string content, string privateKey, string input_charset)
            {

                byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
                RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
                MD5 md5 = new MD5CryptoServiceProvider();  
                byte[] signData = rsa.SignData(Data, md5);
                return Convert.ToBase64String(signData);
            }

            private static RSACryptoServiceProvider DecodePemPrivateKey(String pemstr)
            {
                byte[] pkcs8privatekey;
                pkcs8privatekey = Convert.FromBase64String(pemstr);
                if (pkcs8privatekey != null)
                {
                    RSACryptoServiceProvider rsa = DecodePrivateKeyInfo(pkcs8privatekey);
                    return rsa;
                }
                else
                    return null;
            }

            private static RSACryptoServiceProvider DecodePrivateKeyInfo(byte[] pkcs8)
            {
                byte[] SeqOID = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
                byte[] seq = new byte[15];

                MemoryStream mem = new MemoryStream(pkcs8);
                int lenstream = (int)mem.Length;
                BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
                byte bt = 0;
                ushort twobytes = 0;

                try
                {
                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();	//advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();	//advance 2 bytes
                    else
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x02)
                        return null;

                    twobytes = binr.ReadUInt16();

                    if (twobytes != 0x0001)
                        return null;

                    seq = binr.ReadBytes(15);		//read the Sequence OID
                    if (!CompareBytearrays(seq, SeqOID))	//make sure Sequence for OID is correct
                        return null;

                    bt = binr.ReadByte();
                    if (bt != 0x04)	//expect an Octet string 
                        return null;

                    bt = binr.ReadByte();		//read next byte, or next 2 bytes is  0x81 or 0x82; otherwise bt is the byte count
                    if (bt == 0x81)
                        binr.ReadByte();
                    else
                        if (bt == 0x82)
                            binr.ReadUInt16();
                    //------ at this stage, the remaining sequence should be the RSA private key

                    byte[] rsaprivkey = binr.ReadBytes((int)(lenstream - mem.Position));
                    RSACryptoServiceProvider rsacsp = DecodeRSAPrivateKey(rsaprivkey);
                    return rsacsp;
                }

                catch (Exception)
                {
                    return null;
                }

                finally { binr.Close(); }

            }

            private static bool CompareBytearrays(byte[] a, byte[] b)
            {
                if (a.Length != b.Length)
                    return false;
                int i = 0;
                foreach (byte c in a)
                {
                    if (c != b[i])
                        return false;
                    i++;
                }
                return true;
            }

            private static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
            {
                byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

                // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
                MemoryStream mem = new MemoryStream(privkey);
                BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
                byte bt = 0;
                ushort twobytes = 0;
                int elems = 0;
                try
                {
                    twobytes = binr.ReadUInt16();
                    if (twobytes == 0x8130)	//data read as little endian order (actual data order for Sequence is 30 81)
                        binr.ReadByte();	//advance 1 byte
                    else if (twobytes == 0x8230)
                        binr.ReadInt16();	//advance 2 bytes
                    else
                        return null;

                    twobytes = binr.ReadUInt16();
                    if (twobytes != 0x0102)	//version number
                        return null;
                    bt = binr.ReadByte();
                    if (bt != 0x00)
                        return null;


                    //------  all private key components are Integer sequences ----
                    elems = GetIntegerSize(binr);
                    MODULUS = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    E = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    D = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    P = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    Q = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DP = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    DQ = binr.ReadBytes(elems);

                    elems = GetIntegerSize(binr);
                    IQ = binr.ReadBytes(elems);

                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                    RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
                    RSAParameters RSAparams = new RSAParameters();
                    RSAparams.Modulus = MODULUS;
                    RSAparams.Exponent = E;
                    RSAparams.D = D;
                    RSAparams.P = P;
                    RSAparams.Q = Q;
                    RSAparams.DP = DP;
                    RSAparams.DQ = DQ;
                    RSAparams.InverseQ = IQ;
                    RSA.ImportParameters(RSAparams);
                    return RSA;
                }
                catch (Exception)
                {
                    return null;
                }
                finally { binr.Close(); }
            }

            private static RSAParameters ConvertFromPublicKey(string pemFileConent)
            {

                byte[] keyData = Convert.FromBase64String(pemFileConent);
                if (keyData.Length < 162)
                {
                    throw new ArgumentException("pem file content is incorrect.");
                }
                byte[] pemModulus = new byte[128];
                byte[] pemPublicExponent = new byte[3];
                Array.Copy(keyData, 29, pemModulus, 0, 128);
                Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
                RSAParameters para = new RSAParameters();
                para.Modulus = pemModulus;
                para.Exponent = pemPublicExponent;
                return para;
            }

            private static int GetIntegerSize(BinaryReader binr)
            {
                byte bt = 0;
                byte lowbyte = 0x00;
                byte highbyte = 0x00;
                int count = 0;
                bt = binr.ReadByte();
                if (bt != 0x02)		//expect integer
                    return 0;
                bt = binr.ReadByte();

                if (bt == 0x81)
                    count = binr.ReadByte();	// data size in next byte
                else
                    if (bt == 0x82)
                    {
                        highbyte = binr.ReadByte();	// data size in next 2 bytes
                        lowbyte = binr.ReadByte();
                        byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                        count = BitConverter.ToInt32(modint, 0);
                    }
                    else
                    {
                        count = bt;		// we already have the data size
                    }



                while (binr.ReadByte() == 0x00)
                {	//remove high order zeros in data
                    count -= 1;
                }
                binr.BaseStream.Seek(-1, SeekOrigin.Current);		//last ReadByte wasn't a removed zero, so back up a byte
                return count;
            }
        }

       
    }
    
}
