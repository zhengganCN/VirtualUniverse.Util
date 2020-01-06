using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.EFCore.Interface;

namespace Util.Data.EFCore.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 工作单元
        /// </summary>
        public UnitOfWork.UOW UOW { get; private set; }
        /// <summary>
        /// 构造函数，初始化上下文
        /// </summary>
        /// <param name="context">SQLServer数据库上下文</param>
        public EFRepository(DbContext context)
        {
            UOW = new UnitOfWork.UOW(context);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <returns></returns>
        public DbSet<TEntity> GetEntity()
        {
            return UOW.DbContext.Set<TEntity>();
        }
        #region 统计
        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> condition)
        {
            return GetEntity().Count(condition);

        }
        /// <summary>
        /// 异步统计
        /// </summary>
        /// <param name="condition">条件表达式</param>
        /// <returns></returns>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> condition)
        {
            return await GetEntity().CountAsync(condition).ConfigureAwait(true);
        }
        #endregion
        #region 删除
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            GetEntity().Remove(entity);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public int Delete(IList<TEntity> entities)
        {
            GetEntity().RemoveRange(entities);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(TEntity entity)
        {
            GetEntity().RemoveRange(entity);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public async Task<int> DeleteAsync(IList<TEntity> entities)
        {
            GetEntity().RemoveRange(entities);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        #endregion
        #region 查询
        /// <summary>
        /// 查询一条实体
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns></returns>
        public TEntity Find(params object[] primaryKey)
        {
            return GetEntity().Find(primaryKey);
        }
        /// <summary>
        /// 异步查询一条实体
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            return await GetEntity().FindAsync(primaryKey);
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
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var query = GetEntity()
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return query.OrderBy(keySelector).ToList();
                case EnumSequence.Descending:
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
        public async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending, int pageIndex = 1, int pageSize = 10)
        {
            var query = GetEntity()
                 .Where(condition)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return await query.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case EnumSequence.Descending:
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
        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending)
        {
            var query = GetEntity()
                 .Where(condition);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return query.OrderBy(keySelector).ToList();
                case EnumSequence.Descending:
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
        public async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> keySelector, EnumSequence sortMode = EnumSequence.Ascending)
        {
            var query = GetEntity()
                 .Where(condition);
            switch (sortMode)
            {
                case EnumSequence.Ascending:
                    return await query.OrderBy(keySelector).ToListAsync().ConfigureAwait(true);
                case EnumSequence.Descending:
                    return await query.OrderByDescending(keySelector).ToListAsync().ConfigureAwait(true);
                default:
                    return null;
            };
        }
        #endregion
        #region 插入
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            GetEntity().Add(entity);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Insert(IList<TEntity> entities)
        {
            GetEntity().AddRange(entities);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(TEntity entity)
        {
            await GetEntity().AddAsync(entity);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> InsertAsync(IList<TEntity> entities)
        {
            await GetEntity().AddRangeAsync(entities).ConfigureAwait(true);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        #endregion
        #region 标记删除
        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkDelete(TEntity entity)
        {
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity().Update(entity);
            return UOW.DbContext.SaveChanges();
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
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            }
            GetEntity().UpdateRange(entities);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> MarkDeleteAsync(TEntity entity)
        {
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity().Update(entity);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
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
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            }
            GetEntity().UpdateRange(entities);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        #endregion
        #region 标记未删除
        /// <summary>
        /// 修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int UnmarkDelete(TEntity entity)
        {
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity().Update(entity);
            return UOW.DbContext.SaveChanges();
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
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            }
            GetEntity().UpdateRange(entities);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity().Update(entity);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
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
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            }
            GetEntity().UpdateRange(entities);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            GetEntity().Update(entity);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Update(IList<TEntity> entities)
        {
            GetEntity().UpdateRange(entities);
            return UOW.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步更新一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            GetEntity().UpdateRange(entity);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            GetEntity().UpdateRange(entities);
            return await UOW.DbContext.SaveChangesAsync().ConfigureAwait(true);
        }
        #endregion
    }
}
