using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/3/6 14:53:10；更新时间：
************************************************************************************/
namespace VirtualUniverse.FTP.FluentFTP
{
    /// <summary>
    /// 类说明：ftp配置
    /// </summary>
    public class FtpConfigurationBuilder
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        internal string Host { get; set; }
        internal int? Port { get; set; }
        internal string UserName { get; set; }
        internal string Password { get; set; }
        public FtpConfigurationBuilder AddHost(string host)
        {
            Host = host;
            return this;
        }
        public FtpConfigurationBuilder AddPort(int port)
        {
            Port = port;
            return this;
        }
        public FtpConfigurationBuilder AddUserName(string userName)
        {
            UserName = userName;
            return this;
        }
        public FtpConfigurationBuilder AddPassword(string password)
        {
            Password = password;
            return this;
        }
    }
}
