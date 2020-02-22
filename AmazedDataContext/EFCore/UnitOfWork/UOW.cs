using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AmazedDataContext.EFCore.Interface;

namespace AmazedDataContext.EFCore.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UOW : IUOW
    {
        /// <summary>
        /// 工作单元构造函数
        /// </summary>
        /// <param name="context">sqlserver数据库上下文</param>
        public UOW(DbContext context)
        {
            DbContext = context;
        }
        /// <summary>
        /// sqlserver数据库上下文
        /// </summary>
        public DbContext DbContext { get; private set; }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            DbContext.Database.CommitTransaction();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            DbContext.Database.RollbackTransaction();
        }
        /// <summary>
        /// 事务开始
        /// </summary>
        public void Transaction()
        {
            DbContext.Database.BeginTransaction();
        }
    }
}
