using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.UOW;
using Util.Data.UOW.SQLServerUOW;

namespace Util.Data.Repository.SQLServerRepository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly DbContext context;
        /// <summary>
        /// 构造函数，初始化上下文
        /// </summary>
        /// <param name="context">SQLServer数据库上下文</param>
        public Repository(DbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="uow">SQLServer工作单元</param>
        /// <returns></returns>
        public DbSet<TEntity> GetEntity(UnitOfWork uow)
        {
            if (uow == null)
            {
                throw new ArgumentNullException(nameof(uow));
            }
            return uow.DbContext.Set<TEntity>();
        }
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> condition)
        {
            var uow = new UnitOfWork(context);
            return GetEntity(uow).Count(condition);

        }
        /// <summary>
        /// 异步统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            var uow = new UnitOfWork(context);
            return await GetEntity(uow).CountAsync(condition).ConfigureAwait(true);
        }
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).Remove(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public int Delete(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entity);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entities);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        ///// <summary>
        ///// 查询一条实体
        ///// </summary>
        ///// <returns></returns>
        public TEntity Find(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            return GetEntity(uow).Find(primaryKey);
        }
        ///// <summary>
        ///// 异步查询一条实体
        ///// </summary>
        ///// <returns></returns>
        public async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            return await GetEntity(uow).FindAsync(primaryKey);
        }
        /// <summary>
        /// 异步查询多条实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case SortMode.Ascending:
                    return query.OrderBy(keySelector).ToList();
                case SortMode.Descending:
                    return query.OrderByDescending(keySelector).ToList();
                default:
                    return null;
            };
        }
        /// <summary>
        /// 异步查询多条实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <param name="pageIndex">页面索引</param>
        /// <param name="pageSize">页面大小</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case SortMode.Ascending:
                    return await query.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case SortMode.Descending:
                    return await query.OrderByDescending(keySelector).ToListAsync().ConfigureAwait(true);
                default:
                    return null;
            };
        }
        /// <summary>
        /// 查询所有的实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <returns></returns>
        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(condition);
            switch (sortMode)
            {
                case SortMode.Ascending:
                    return query.OrderBy(keySelector).ToList();
                case SortMode.Descending:
                    return query.OrderByDescending(keySelector).ToList();
                default:
                    return null;
            };
        }
        /// <summary>
        /// 异步查询所有的实体
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <param name="keySelector">排序关键字（根据某个关键字排序）</param>
        /// <param name="sortMode">排序方式，默认顺序</param>
        /// <returns></returns>
        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(condition);
            switch (sortMode)
            {
                case SortMode.Ascending:
                    return await query.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case SortMode.Descending:
                    return await query.OrderByDescending(keySelector).ToListAsync().ConfigureAwait(true);
                default:
                    return null;
            };
        }
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).Add(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Insert(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).AddRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            await GetEntity(uow).AddAsync(entity);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            await GetEntity(uow).AddRangeAsync(entities).ConfigureAwait(true);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int MarkDelete(IList<TEntity> entities)
        {
            if (entities==null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            }
            GetEntity(uow).UpdateRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity(uow).Update(entity);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            }
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int UnmarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int UnmarkDelete(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            }
            GetEntity(uow).UpdateRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity(uow).Update(entity);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> UnmarkDeleteAsync(IList<TEntity> entities)
        {
            if (entities == null)
            {
                throw new ArgumentNullException(nameof(entities));
            }
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            }
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 更新一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Update(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步更新一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entity);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
    }
}
