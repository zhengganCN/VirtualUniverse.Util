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
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        private readonly DbContext context;
        public virtual DbSet<TEntity> GetEntity(UnitOfWork uow)
        {
            return uow.DbContext.Set<TEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public virtual int Count(Expression<Func<TEntity, bool>> expression)
        {
            var uow = new UnitOfWork(context);
            return GetEntity(uow).Count(expression);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
        {
            var uow = new UnitOfWork(context);           
            return await GetEntity(uow).CountAsync(expression);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            var uow = new UnitOfWork(context);          
            GetEntity(uow).Remove(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Delete(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entity);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> DeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).RemoveRange(entities);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Find(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            return GetEntity(uow).Find(primaryKey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            var uow = new UnitOfWork(context);
            return await GetEntity(uow).FindAsync(primaryKey);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="keySelector"></param>
        /// <param name="asc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector,bool asc=true, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(predicate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            return asc ?query.OrderBy(keySelector).ToList():query.OrderByDescending(keySelector).ToList();
        }
        public virtual async Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, bool asc = true, int pageIndex = 1, int pageSize = 10)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(predicate)
                 .Skip((pageIndex - 1) * pageSize)
                 .Take(pageSize);
            return asc ?await query.OrderBy(keySelector).ToListAsync() :await query.OrderByDescending(keySelector).ToListAsync();
        }
        
        public virtual IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, bool asc = true)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(predicate);
            return asc ? query.OrderBy(keySelector).ToList() : query.OrderByDescending(keySelector).ToList();
        }

        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, bool asc = true)
        {
            var uow = new UnitOfWork(context);
            var query = GetEntity(uow)
                 .Where(predicate);
            return asc ? await query.OrderBy(keySelector).ToListAsync() : await query.OrderByDescending(keySelector).ToListAsync();
        }
        /// <summary>
        /// 插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).Add(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 插入实体集
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).AddRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步插入实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            await GetEntity(uow).AddAsync(entity);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 异步插入实体集
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> InsertAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            await GetEntity(uow).AddRangeAsync(entities);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 修改实体的删除标识改为true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int MarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 修改实体集的删除标识改为true
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int MarkDelete(IList<TEntity> entities)
        {
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
        /// 异步修改实体的删除标识改为true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> MarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            GetEntity(uow).Update(entity);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 异步修改实体的删除标识改为true
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> MarkDeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, true);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, DateTime.Now);
            }
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int UnmarkDelete(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int UnmarkDelete(IList<TEntity> entities)
        {
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
            typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            GetEntity(uow).Update(entity);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> UnmarkDeleteAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty("IsDeleted").SetValue(entity, false);
                typeof(TEntity).GetProperty("DeleteTime").SetValue(entity, null);
            }
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).Update(entity);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Update(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entities);
            return uow.DbContext.SaveChanges();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entity);
            return await uow.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual async Task<int> UpdateAsync(IList<TEntity> entities)
        {
            var uow = new UnitOfWork(context);
            GetEntity(uow).UpdateRange(entities);
            return await uow.DbContext.SaveChangesAsync();
        }

        
    }
}
