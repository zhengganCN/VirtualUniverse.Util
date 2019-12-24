using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Security.Cryptography
{
    /// <summary>
    /// RSA加密/解密,非对称算法
    /// </summary>
    public class RSA
    {
        ///// <summary>
        ///// RSA私钥格式转换，java->.net
        ///// </summary>
        ///// <param name="privateKey">java生成的RSA私钥</param>
        ///// <returns></returns>
        //private string RSAPrivateKeyJava2DotNet(string privateKey)
        //{
        //    RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));

        //    return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
        //        Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
        //}
        ///// <summary>
        ///// RSA私钥格式转换，.net->java
        ///// </summary>
        ///// <param name="privateKey">.net生成的私钥</param>
        ///// <returns></returns>
        //private string RSAPrivateKeyDotNet2Java(string privateKey)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(privateKey);
        //    BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
        //    BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
        //    BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
        //    BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
        //    BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
        //    BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
        //    BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
        //    BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));

        //    RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);

        //    PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
        //    byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
        //    return Convert.ToBase64String(serializedPrivateBytes);
        //}

        ///// <summary>
        ///// RSA公钥格式转换，java->.net
        ///// </summary>
        ///// <param name="publicKey">java生成的公钥</param>
        ///// <returns></returns>
        //private string RSAPublicKeyJava2DotNet(string publicKey)
        //{
        //    RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
        //    return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
        //        Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
        //        Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
        //}

        ///// <summary>
        ///// RSA公钥格式转换，.net->java
        ///// </summary>
        ///// <param name="publicKey">.net生成的公钥</param>
        ///// <returns></returns>
        //private string RSAPublicKeyDotNet2Java(string publicKey)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.LoadXml(publicKey);
        //    BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
        //    BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
        //    RsaKeyParameters pub = new RsaKeyParameters(false, m, p);

        //    SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
        //    byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
        //    return Convert.ToBase64String(serializedPublicBytes);
        //}

        //public override RSAParameters ImportRSAKeyPairs(string privateKey, bool isJavaKeys)
        //{
        //    if (isJavaKeys)
        //    {
        //        privateKey = RSAPrivateKeyJava2DotNet(privateKey);
        //    }
        //    XmlDocument xml = new XmlDocument();
        //    xml.LoadXml(privateKey);
        //    RSAParameters rsaParameters = new RSAParameters
        //    {
        //        D = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("D")[0].InnerText),
        //        DP = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("DP")[0].InnerText),
        //        DQ = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("DQ")[0].InnerText),
        //        Exponent = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText),
        //        InverseQ = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText),
        //        Modulus = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText),
        //        P = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("P")[0].InnerText),
        //        Q = Convert.FromBase64String(xml.DocumentElement.GetElementsByTagName("Q")[0].InnerText)
        //    };
        //    return rsaParameters;
        //}

        ///// <summary>
        ///// RSA加密
        ///// </summary>
        ///// <param name="DataToEncrypt"></param>
        ///// <param name="RSAKeyInfo"></param>
        ///// <param name="DoOAEPPadding"></param>
        ///// <returns></returns>
        //public override byte[] Encrypt(byte[] dataToEncrypt, RSAParameters rsaParameters)
        //{
        //    try
        //    {
        //        byte[] encryptedData;  //加密后的数据
        //        using (RSA RSA = RSA.Create())
        //        {
        //            RSA.ImportParameters(rsaParameters);
        //            //验证要加密的数据是否超长
        //            if (VaildDataIsMoreLength(dataToEncrypt, rsaParameters))
        //            {
        //                //当需要加密的数据过长时，需要循环加密的次数
        //                var time = Math.Ceiling((double)dataToEncrypt.Length / (rsaParameters.D.Length - 11));
        //                List<byte> temp = new List<byte>();//临时变量，存储分段加密的加密后数据
        //                var length = rsaParameters.D.Length - 11;//分段加密时，每段能加密的字符字节数
        //                for (int i = 0; i < time; i++)
        //                {
        //                    temp.AddRange(RSA.Encrypt(dataToEncrypt.Skip(i * length).Take(length).ToArray(), RSAEncryptionPadding.Pkcs1));
        //                }
        //                encryptedData = temp.ToArray();
        //            }
        //            else
        //            {
        //                encryptedData = RSA.Encrypt(dataToEncrypt, RSAEncryptionPadding.Pkcs1);
        //            }
        //        }
        //        _logger.LogInformation("数据加密成功");
        //        return encryptedData;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        _logger.LogError("加密失败；异常信息：{0}", e.Message);
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// RSA解密
        ///// </summary>
        ///// <param name="DataToDecrypt"></param>
        ///// <param name="RSAKeyInfo"></param>
        ///// <param name="DoOAEPPadding"></param>
        ///// <returns></returns>
        //public override byte[] Decrypt(byte[] dataToDecrypt, RSAParameters rsaParameters)
        //{
        //    try
        //    {
        //        byte[] decryptedData = null;
        //        using (RSA RSA = RSA.Create())
        //        {
        //            RSA.ImportParameters(rsaParameters);
        //            //当需要解密的数据过长时，需要循环解密的次数
        //            var time = dataToDecrypt.Length / rsaParameters.D.Length;
        //            List<byte> temp = new List<byte>();//临时变量，存储分段解密后的数据
        //            var length = rsaParameters.D.Length;//分段解密时，每段能解密的字符字节数
        //            for (int i = 0; i < time; i++)
        //            {
        //                var data = dataToDecrypt.Skip(i * length).Take(length).ToArray();
        //                temp.AddRange(RSA.Decrypt(data, RSAEncryptionPadding.Pkcs1));
        //            }
        //            decryptedData = temp.ToArray();
        //        }
        //        _logger.LogInformation("数据解密成功");
        //        return decryptedData;
        //    }
        //    catch (CryptographicException e)
        //    {
        //        _logger.LogError("解密失败；异常信息：{0}", e.Message);
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// //验证要加密的数据是否超长
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="rsaParameters"></param>
        ///// <returns></returns>
        //private bool VaildDataIsMoreLength(byte[] data, RSAParameters rsaParameters)
        //{
        //    if (data.Length > rsaParameters.Q.Length - 11)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
