/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 11:41:26；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Nacos.Models
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    public class ConfigEnvironmentOptions
    {
        /// <summary>
        /// 读取的配置写入的配置文件名称，默认值为“appsettings.json”
        /// </summary>
        public string ConfigFileName { get; set; } = "appsettings.json";
        public string DataId { get; set; }
        public string Group { get; set; } = "DEFAULT_GROUP";
        public string Tenant { get; set; }
    }
}
