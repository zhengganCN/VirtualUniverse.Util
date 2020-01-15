using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Util.Configuration;

namespace UtilTest
{
    public static class StaticConfigurationValues
    {
        private static readonly string _path;
        static StaticConfigurationValues()
        {
            var path = Directory.GetCurrentDirectory();
            _path = Path.Combine(path, "App.json");
        }
        public static string MySQLConnectionString 
        { 
            get 
            { 
                return GetJsonConfiguration().GetValue("MySQL:ConnectionString"); 
            } 
        }
        public static string MSSQLConnectionString
        {
            get
            {
                return GetJsonConfiguration().GetValue("MSSQL:ConnectionString");
            }
        }
        public static string MongoDBConnectionString
        {
            get
            {
                return new JsonConfiguration(_path).GetValue("MongoDB:ConnectionString");
            }
        }
        public static string MongoDBDatabase
        {
            get
            {
                return GetJsonConfiguration().GetValue("MongoDB:Database");
            }
        }
        #region jwt
        public static string JWTAudience
        {
            get
            {
                return GetJsonConfiguration().GetValue("JWT:Audience");
            }
        }

        private static JsonConfiguration GetJsonConfiguration()
        {
            return new JsonConfiguration(_path);
        }

        public static string JWTIssuer
        {
            get
            {
                return GetJsonConfiguration().GetValue("JWT:Issuer");
            }
        }
        public static string JWTIssuerSigningKey
        {
            get
            {
                return GetJsonConfiguration().GetValue("JWT:IssuerSigningKey");
            }
        }
        public static string JWTExpires
        {
            get
            {
                return GetJsonConfiguration().GetValue("JWT:Expires");
            }
        }
        #endregion
        #region AES
        public static string AESKey128
        {
            get
            {
                return GetJsonConfiguration().GetValue("AES:Key128");
            }
        }
        public static string AESKey192
        {
            get
            {
                return GetJsonConfiguration().GetValue("AES:Key192");
            }
        }
        public static string AESKey256
        {
            get
            {
                return GetJsonConfiguration().GetValue("AES:Key256");
            }
        }
        #endregion
        #region DES
        public static string DESKey64
        {
            get
            {
                return GetJsonConfiguration().GetValue("DES:Key64");
            }
        }
        #endregion
        #region RSA
        public static string RSAPkcs8PrivateKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs8PrivateKey1024");
            }
        }
         public static string RSAPkcs1PublicKey1024FromPkcs8PrivateKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PublicKey1024FromPkcs8PrivateKey1024");
            }
        }
        public static string RSAPkcs1PrivateKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PrivateKey1024");
            }
        }
        public static string RSAPkcs1PublicKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PublicKey1024");
            }
        }
        public static string RSAPkcs8PrivateKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs8PrivateKey2048");
            }
        }
        public static string RSAPkcs1PublicKey2048FromPkcs8PrivateKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PublicKey2048FromPkcs8PrivateKey2048");
            }
        }
        public static string RSAPkcs1PrivateKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PrivateKey2048");
            }
        }
        public static string RSAPkcs1PublicKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Pkcs1PublicKey2048");
            }
        }
        public static string RSAPassword
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Password");
            }
        }
        public static string RSAEncryptedPkcs8PrivateKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:EncryptedPkcs8PrivateKey1024");
            }
        }
        public static string RSAEncryptedPkcs8PrivateKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:EncryptedPkcs8PrivateKey2048");
            }
        }
        public static string RSADecryptedPkcs8PrivateKey1024
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:DecryptedPkcs8PrivateKey1024");
            }
        }
        public static string RSADecryptedPkcs8PrivateKey2048
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:DecryptedPkcs8PrivateKey2048");
            }
        }
        public static string RSAPublic
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Public");
            }
        }
        public static string RSAEncryptedDataBase64
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:EncryptedDataBase64");
            }
        }
        public static string RSAData
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:Data");
            }
        }
        public static string RSADataExtraLong
        {
            get
            {
                return GetJsonConfiguration().GetValue("RSA:DataExtraLong");
            }
        }
        #endregion
    }
}
