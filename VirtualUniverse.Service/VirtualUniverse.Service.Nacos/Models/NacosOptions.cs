/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 11:45:38；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Nacos.Models
{
    /// <summary>
    /// 类 描 述：Nacos选项
    /// </summary>
    public class NacosOptions
    {
        /// <summary>
        /// 默认超时时间
        /// </summary>
        public int DefaultTimeOut { get; set; } = 8000;
        /// <summary>
        /// key
        /// </summary>
        public string AccessKey { get; set; }
        /// <summary>
        /// 私钥
        /// </summary>
        public string SecretKey { get; set; }
        /// <summary>
        /// 终结点
        /// </summary>
        public string EndPoint { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 名称空间
        /// </summary>
        public string Namespace { get; set; } = "public";
    }
}
