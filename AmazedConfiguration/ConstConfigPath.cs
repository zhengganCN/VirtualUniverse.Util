using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedConfiguration
{
    /// <summary>
    /// 常量配置文件名称
    /// </summary>
    public static class ConstConfigPath
    {
        /// <summary>
        /// 环境配置文件文件名
        /// </summary>
        public const string Environment = "appsettings.json";
        /// <summary>
        /// 开发环境配置文件名
        /// </summary>
        public const string Development= "appsettings.Development.json";
        /// <summary>
        /// 测试环境配置文件名
        /// </summary>
        public const string Test = "appsettings.Test.json";
        /// <summary>
        /// 生产环境配置文件名
        /// </summary>
        public const string Production = "appsettings.Production.json";
        /// <summary>
        /// 环境配置字段
        /// </summary>
        public const string CurrentEnvironment = "CurrentEnvironment";
        /// <summary>
        /// 配置文件正则匹配
        /// </summary>
        public const string ValueConfigFilter = "appsettings.*.json";
    }
}
