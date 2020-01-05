using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Util.Security.JWT
{
    /// <summary>
    /// jwt帮助类
    /// </summary>
    public class JsonWebToken
    {
        private readonly string Issuer;
        private readonly int Expires;
        private readonly string Key;
        private readonly string Audience;
        private string UserId;
        private string Permissions;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="issuer">JWT的签发者</param>
        /// <param name="expires">过期时间间隔</param>
        /// <param name="key">加密密钥</param>
        /// <param name="audience"></param>
        public JsonWebToken(string issuer, int expires, string key, string audience)
        {
            Issuer = issuer;
            Expires = expires;
            Key = key;
            Audience = audience;
        }
        /// <summary>
        /// 设置用户唯一标识
        /// </summary>
        /// <param name="userId"></param>
        public void SetUserId(string userId)
        {
            UserId = userId;
        }
        /// <summary>
        /// 设置用户权限列表
        /// </summary>
        /// <param name="permissions"></param>
        public void SetPermissions(string permissions)
        {
            Permissions = permissions;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, UserId),
                new Claim("Permissions",Permissions)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));

            var token = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddSeconds(Expires),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
