using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualUniverse.Security.JWT.Models
{
    /// <summary>
    /// jwt参数模型
    /// </summary>
    public class JWTParamModel
    {
        /// <summary>
        /// JWT的签发者
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public string Expires { get; set; }
        /// <summary>
        /// 密钥
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 接受token者
        /// </summary>
        public string Audience { get; set; }
    }
}
