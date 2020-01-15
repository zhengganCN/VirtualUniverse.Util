using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Security.JWT;

namespace UtilTest.Security.JWT
{
    class JsonWebTokenTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        [Test]
        public void GenerateJWT()
        {
            JsonWebToken jwt = new JsonWebToken(StaticConfigurationValues.JWTIssuer, int.Parse(StaticConfigurationValues.JWTExpires),
                        StaticConfigurationValues.JWTIssuerSigningKey, StaticConfigurationValues.JWTAudience);
            jwt.SetUserId("asdasd");
            jwt.SetPermissions("");
            var s = jwt.GenerateToken();
            Assert.IsNotEmpty(s);
        }
    }
}
