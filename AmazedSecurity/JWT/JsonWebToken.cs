using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AmazedSecurity.JWT
{
    /// <summary>
    /// jwt帮助类
    /// </summary>
    public class JsonWebToken
    {
        private readonly string Issuer;
        private readonly int Expires;
        private readonly byte[] Key;
        private readonly string Audience;
        private string UserId;
        private string Authorities;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="issuer">JWT的签发者</param>
        /// <param name="expires">过期时间间隔</param>
        /// <param name="key">加密密钥</param>
        /// <param name="audience"></param>
        public JsonWebToken(string issuer, int expires, byte[] key, string audience)
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
        /// <param name="authorities"></param>
        public void SetAuthorities(string authorities)
        {
            Authorities = authorities;
        }
        /// <summary>
        /// 获取token中的用户ID
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetUserId(string token)
        {
            var jwt = new JwtSecurityToken(token);
            var claim = jwt.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
            return claim?.Value;
        }
        /// <summary>
        /// 获取token中的权限ID列表
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetAuthorities(string token)
        {
            var jwt= new JwtSecurityToken(token);
            var claim = jwt.Claims.FirstOrDefault(claim => claim.Type == nameof(Authorities));
            return claim?.Value;
        }

        /// <summary>
        /// 生成Token
        /// </summary>
        /// <returns></returns>
        public string GenerateToken()
        {
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, string.IsNullOrEmpty(UserId)?"":UserId),
                new Claim(nameof(Authorities),string.IsNullOrEmpty(Authorities)?"":Authorities)
            };
            var key = new SymmetricSecurityKey(Key);
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
