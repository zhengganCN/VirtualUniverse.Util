using Microsoft.Extensions.Logging;
using SqlSugar;

namespace VirtualUniverse.Repository.SqlSugar
{
    /// <summary>
    /// 数据库连接配置
    /// </summary>
    public class DbContextOptionsBuilder
    {
        internal ILogger Logger { get; set; }
        internal DbType DbType { get; set; }
        internal string ConnectionString { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public DbContextOptionsBuilder()
        {
            Logger = new LoggerFactory().CreateLogger<DbContext>();
        }
        /// <summary>
        /// 添加日志配置
        /// </summary>
        /// <typeparam name="T">日志类型</typeparam>
        /// <param name="logger">日志</param>
        /// <returns></returns>
        public DbContextOptionsBuilder AddLogger<T>(ILogger<T> logger)
        {
            Logger = logger;
            return this;
        }
        /// <summary>
        /// 添加数据库连接配置
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="connectionString">连接字符串</param>
        /// <returns></returns>
        public DbContextOptionsBuilder AddConnectionString(DbType dbType,string connectionString)
        {
            DbType = dbType;
            ConnectionString = connectionString;
            return this;
        }
    }
}