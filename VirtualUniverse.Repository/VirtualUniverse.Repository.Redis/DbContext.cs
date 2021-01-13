using StackExchange.Redis;
using System;
using VirtualUniverse.Repository.Redis.Repository;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/2 14:34:55；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Redis
{
    /// <summary>
    /// 类说明：RedisDB上下文
    /// </summary>
    public abstract class DbContext : IDisposable
    {
        internal ConfigurationOptions ConfigurationOptions { get; private set; }
        private readonly DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        private bool disposedValue;
        /// <summary>
        /// Redis仓储类
        /// </summary>
        public RedisRepository RedisRepository
        {
            get
            {
                return new RedisRepository(ConfigurationOptions, builder.Db);
            }
        }
        /// <summary>
        /// 初始化构造函数
        /// </summary>
        protected DbContext()
        {
            LoadConfiguration();
        }
        /// <summary>
        /// 配置Redis连接参数等
        /// </summary>
        /// <param name="builder"></param>
        protected internal abstract void OnConfiguring(DbContextOptionsBuilder builder);

        /// <summary>
        /// 加载配置项
        /// </summary>
        private void LoadConfiguration()
        {
            OnConfiguring(builder);
            ConfigurationOptions = ConfigurationOptions.Parse(builder.SslHost);
            ConfigurationOptions.Password = builder.Password;
        }
        /// <summary>
        /// 清理
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }
                RedisRepository.Dispose();
                disposedValue = true;
            }
        }
        /// <summary>
        /// 清理
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
