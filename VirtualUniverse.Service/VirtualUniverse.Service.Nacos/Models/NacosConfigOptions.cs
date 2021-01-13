using System.Collections.Generic;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/4 9:23:13；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.Nacos.Models
{
    /// <summary>
    /// 类 描 述：Consul配置选项
    /// </summary>
    public class NacosConfigOptions
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
        /// 通过<see cref="NacosConfigRead"/>构造函数的environment参数来判断使用某个环境的配置
        /// <br></br>
        /// <see cref="NacosConfigRead"/>构造函数的environment参数必须存在于Environment字典的key中，否则抛出异常
        /// </summary>
        public Dictionary<string, ConfigEnvironmentOptions> Environments { get; set; }
        /// <summary>
        /// 配置读取间隔，时间单位 ms
        /// </summary>
        public int Interval { get; set; } = 1000;
        /// <summary>
        /// Nacos选项
        /// </summary>
        public NacosOptions NacosOptions { get; set; } = new NacosOptions();
    }
}
