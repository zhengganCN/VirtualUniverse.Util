/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/3 21:04:51；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Consul.Models
{
    /// <summary>
    /// 类说明：Consul配置选项
    /// </summary>
    public class ConsulConfigOptions
    {
        /// <summary>
        /// 是否监听配置
        /// </summary>
        public bool ListenConfig { get; set; } = true;
        /// <summary>
        /// 是否仅加载本地配置
        /// </summary>
        public bool LoadLocalConfig { get; set; } = false;
        /// <summary>
        /// 环境值，只能是“prod”，“dev”，“test”这三个值之一，如果env的值与上述的任意一个不匹配，则env默认为“dev”
        ///  <br></br>
        /// “prod”读取键为“Production”的值
        /// <br></br>
        /// “dev”读取键为“Development”的值
        /// <br></br>
        /// “test”读取键为“Test”的值
        /// </summary>
        public string Environment { get; set; } = "dev";
        /// <summary>
        /// 配置目录路径，如果有，则必须以“/”结尾
        /// </summary>
        public string ConfigDirectoryPath { get; set; }
        /// <summary>
        /// 配置读取间隔，时间单位 ms
        /// </summary>
        public int Interval { get; set; } = 1000;
    }
}
