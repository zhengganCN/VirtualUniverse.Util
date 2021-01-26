using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/26 14:09:19；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.SqlSugar
{
    /// <summary>
    /// 类 描 述：数据库上下文类
    /// </summary>
    public abstract class DbContext : IDisposable
    {
        private bool disposedValue;
        private readonly DbContextOptionsBuilder builder = new DbContextOptionsBuilder();
        internal SqlSugarClient SugarClient { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        protected DbContext()
        {
            LoadConfiguration();
            Init();
        }

        /// <summary>
        /// 配置SqlSugar连接参数等
        /// </summary>
        /// <param name="builder"></param>
        protected internal abstract void OnConfiguring(DbContextOptionsBuilder builder);

        private void LoadConfiguration()
        {
            OnConfiguring(builder);
        }

        private void Init()
        {
            SugarClient = new SqlSugarClient(SetConnectionConfig());
            SetLogger();
            InstantiationDbSet();
        }

        /// <summary>
        /// 实例化DbSet
        /// </summary>
        private void InstantiationDbSet()
        {
            var properties = GetType().GetProperties();
            var genericTypeName = typeof(DbSet<>).Name;
            foreach (var property in properties)
            {
                if (property.PropertyType.Name == genericTypeName && property.GetValue(this) is null)
                {
                    var type = property.PropertyType.GenericTypeArguments[0];
                    var genericType = typeof(SugarDbSet<>);
                    type = genericType.MakeGenericType(type);
                    var instanceObject = Activator.CreateInstance(type, SugarClient);
                    property.SetValue(this, instanceObject);
                }
            }
        }
        
        private ConnectionConfig SetConnectionConfig()
        {
            return new ConnectionConfig
            {
                ConnectionString = builder.ConnectionString,
                DbType = builder.DbType,
                IsAutoCloseConnection = true,
                IsShardSameThread = true,
                InitKeyType = InitKeyType.Attribute,
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }
            };
        }

        /// <summary>
        /// 输出调试日志
        /// </summary>
        private void SetLogger()
        {
            SugarClient.Aop.OnLogExecuting = (sql, pars) =>
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(sql);
                stringBuilder.Append("\n");
                foreach (var par in pars)
                {
                    stringBuilder.Append("参数名：");
                    stringBuilder.Append(par.ParameterName);
                    stringBuilder.Append("参数值：");
                    stringBuilder.Append(par.Value);
                    stringBuilder.Append("\n");
                }
                builder.Logger.LogDebug(stringBuilder.ToString());
            };
        }

        /// <summary>
        /// 获取客户端
        /// </summary>
        /// <returns></returns>
        public SqlSugarClient GetSqlSugarClient()
        {
            return SugarClient;
        }

        /// <summary>
        /// 销毁
        /// </summary>
        /// <param name="disposing">释放托管状态</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // 释放托管状态(托管对象)
                }
                SugarClient.Close();
                disposedValue = true;
            }
        }
        
        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            // 不要更改此代码。请将清理代码放入“Dispose(bool disposing)”方法中
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
