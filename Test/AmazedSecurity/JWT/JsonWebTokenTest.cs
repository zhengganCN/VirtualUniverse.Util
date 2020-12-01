using AmazedSecurity.JWT;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.AmazedSecurity.Security.JWT
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
            jwt.SetAuthorities("");
            var s = jwt.GenerateToken();
            Assert.IsNotEmpty(s);
        }
    }
}
