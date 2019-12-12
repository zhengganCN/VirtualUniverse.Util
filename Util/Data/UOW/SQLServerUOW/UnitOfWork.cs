using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data.UOW.SQLServerUOW
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DbContext context)
        {
            DbContext = context;
        }

        public DbContext DbContext { get; private set; }

        public void Commit()
        {
            DbContext.Database.CommitTransaction();
        }

        public void Rollback()
        {
            DbContext.Database.RollbackTransaction();
        }

        public void Transaction()
        {
            DbContext.Database.BeginTransaction();
        }
    }
}
