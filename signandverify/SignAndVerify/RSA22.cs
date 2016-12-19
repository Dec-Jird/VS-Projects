using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace SignAndVerify
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

        //此处针对google的pkcs1的验证.
        public static bool verifySHA1(string content, string sign, string iapp_pub_key, string input_charset)
        {
            bool result = false;
            byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
            byte[] data = Convert.FromBase64String(sign);
			/*RSAParameters paraPub = ConvertFromPublicKey(iapp_pub_key);
            RSACryptoServiceProvider rsaPub = new RSACryptoServiceProvider();
            rsaPub.ImportParameters(paraPub);
            //result = rsaPub.VerifyData(Data, CryptoConfig.MapNameToOID("SHA1"), data);

			SDKManager.ShowDebug ("verifySHA1", "content", content);
			SDKManager.ShowDebug ("verifySHA1", "sign", sign);
			SDKManager.ShowDebug ("verifySHA1", "iapp_pub_key", iapp_pub_key);


			SHA1 sh = new SHA1CryptoServiceProvider();
			result = rsaPub.VerifyData(Data, sh, data);
			SDKManager.ShowDebug ("result: "+result);*/
			byte[] pubKey = Convert.FromBase64String(iapp_pub_key);
			result = VerifySignature_2048_Bit_PKCS1_v1_5 (Data, data, pubKey);
            return result;
        }
  
		public static RSAParameters GetRsaParameters_2048_Bit_PKCS1_v1_5(byte[] publicKey)
		{
			// From RFC 2313, PKCS #1, Version 1.5:http://tools.ietf.org/html/rfc2313
			// 7.1 Public-key syntax
			//
			// An RSA public key shall have ASN.1 type RSAPublicKey:
			//
			// RSAPublicKey ::= SEQUENCE {
			//      modulus INTEGER, -- n
			//      publicExponent INTEGER -- e }
			//
			// (This type is specified in X.509 and is retained here for
			// compatibility.)
			//
			// The fields of type RSAPublicKey have the following meanings:
			//
			// o    modulus is the modulus n.
			//
			// o    publicExponent is the public exponent e.
			//            
			
			// BER Encoding
			// http://en.wikipedia.org/wiki/Distinguished_Encoding_Rules#DER_encoding
			//
			// ASN.1 Format with DER (subset of BER) encoding
			// http://en.wikipedia.org/wiki/Abstract_Syntax_Notation_One
			
			// It's important to know that the RSAPublicKey is encoded in an ASN.1 (Abstract Syntax Notation One)
			// representation using DER encoding. I had to use a couple articles on Wikipedia to understand
			// ASN.1 and then I manually decoded the public key to determine where the modulus and exponent were
			// located within the 2048 bit public key from Google.
			//
			// Bytes of sample 2048 bit Public Key (hexadecimal) with ASN.1 decoding shown for each byte
			// 30       Identifier: 30 hex = 00110000, P/C = Constructed (1), TAG = SEQUENCE (10000)
			// 82       Length: 82 hex = 130 decimal = 10000010, Long Form Length with 2 octects for length
			// 01       Byte 1/2 of long form length
			// 22       Byte 2/2 of long form length, 0x01 0x22, 00000001 00100010 = 290 bytes
			// 30       Identifier: 30 hex = 00110000, P/C = Constructed (1), TAG = SEQUENCE (10000)
			// 0d       Length: 0d hex = 13 decimal
			// 06       Identifier: 06 hex = 00000110, P/C = Primitive (0), TAG = OBJECT IDENTIFIER (00110)
			// 09       Length: 09 hex = 9 decimal
			// 2a       Byte 1/9 of OBJECT IDENTIFIER
			// 86       Byte 2/9 of OBJECT IDENTIFIER
			// 48       Byte 3/9 of OBJECT IDENTIFIER
			// 86       Byte 4/9 of OBJECT IDENTIFIER
			// f7       Byte 5/9 of OBJECT IDENTIFIER
			// 0d       Byte 6/9 of OBJECT IDENTIFIER
			// 01       Byte 7/9 of OBJECT IDENTIFIER
			// 01       Byte 8/9 of OBJECT IDENTIFIER
			// 01       Byte 9/9 of OBJECT IDENTIFIER
			// 05       Identifier: 05 hex = 00000101, P/C = Primitive (0), TAG = NULL (00101)
			// 00       Length: 00 hex = 0 decimal
			// 03       Identifier: 03 hex = 00000011, P/C = Primitive (0), TAG = BIT STRING (00011)
			// 82       Length: 82 hex = 130 decimal = 10000010, Long Form Length with 2 octects for length
			// 01       Byte 1/2 of long form length
			// 0f       Byte 2/2 of long form length, 0x01 0x0f, 00000001 00010000 = 272 bytes
			// 00       ???? Why 0, what does this mean?
			// 30       Identifier: 30 hex = 00110000, P/C = Constructed (1), TAG = SEQUENCE (10000)
			// 82       Length: 82 hex = 130 decimal = 10000010, Long Form Length with 2 octects for length
			// 01       Byte 1/2 of long form length        
			// 0a       Byte 2/2 of long form length, 0x01 0x0a, 00000001 00001010 = 266 bytes
			// 02       Identifier: 02 hex = 00000010, P/C = Primitive (0), TAG = INTEGER (00010)
			// 82       Length: 82 hex = 130 decimal = 10000010, Long Form Length with 2 octects for length
			// 01       Byte 1/2 of long form length
			// 01       Byte 2/2 of long form length, 0x01 0x01, 00000001 00000001 = 257 bytes
			// 00       Byte 1/257 of modulus (padded left with a 0, leaves 256 actual values)      
			// a9       Byte 2/257 of modulus... public key (modulus) starts here??
			// 87       Byte 3/257 of modulus
			// ....
			// 8f       Byte 255/257 of modulus
			// 14       Byte 256/257 of modulus93       Byte 257/257 of modulus
			// 02       Identifier: 02 hex = 00000010, P/C = Primitive (0), TAG = INTEGER (00010)
			// 03       Length: 03 hex = 3 decimal
			// 01       Byte 1/3 of exponent
			// 00       Byte 2/3 of exponent
			// 01       Byte 3/3 of exponent
			
			// Modulus starts at byte offset 33 and is 2048 bits (256 bytes)
			// Exponent starts at byte offset 291 and is 3 bytes
			
			RSAParameters rsaParameters = new RSAParameters();
			
			int modulusOffset = 33;     // See comments above
			int modulusBytes = 256;     // 2048 bit key
			int exponentOffset = 291;   // See comments above
			int exponentBytes = 3;      // See comments above
			
			byte[] modulus = new byte[modulusBytes];
			for (int i = 0; i < modulusBytes; i++)
				modulus[i] = publicKey[modulusOffset + i];
			
			byte[] exponent = new byte[exponentBytes];
			for (int i = 0; i < exponentBytes; i++)
				exponent[i] = publicKey[exponentOffset + i];
			
			rsaParameters.Modulus = modulus;
			rsaParameters.Exponent = exponent;
			
			return rsaParameters;
		}
		
		public static bool VerifySignature_2048_Bit_PKCS1_v1_5(byte[] data, byte[] signature, byte[] publicKey)
		{
			// Links for information about PKCS #1 version 1.5:
			// RFC 2313: http://tools.ietf.org/html/rfc2313
			// PKCS #1 on Wikipedia: http://en.wikipedia.org/wiki/PKCS_1
			
			
			// Compute an SHA1 hash of the raw data
			SHA1 sha1 = SHA1.Create();
			byte[] hash = sha1.ComputeHash(data);
			
			// Specify the public key
			RSAParameters rsaParameters = GetRsaParameters_2048_Bit_PKCS1_v1_5(publicKey);
			
			// Use RSACryptoProvider to verify the signature with the public key
			RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);
			rsa.ImportParameters(rsaParameters);
			
			RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsaDeformatter.SetHashAlgorithm("SHA1");
			return rsaDeformatter.VerifySignature(hash, signature);
		}

        /**
        * RSA签名
        * @param content 待签名数据
        * @param privateKey 商户私钥
        * @param input_charset 编码格式
        * @return 签名值
        */
        public static string signSHA1(string content, string privateKey, string input_charset)
        {

            byte[] Data = Encoding.GetEncoding(input_charset).GetBytes(content);
            RSACryptoServiceProvider rsa = DecodePemPrivateKey(privateKey);
            //MD5 md5 = new MD5CryptoServiceProvider();
            byte[] signData = rsa.SignData(Data, CryptoConfig.MapNameToOID("SHA1"));
            return Convert.ToBase64String(signData);
        }

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
