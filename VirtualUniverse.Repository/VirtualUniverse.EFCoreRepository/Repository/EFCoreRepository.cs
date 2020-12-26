using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VirtualUniverse.IRepository;
using VirtualUniverse.Repository.Model;
using VirtualUniverse.Repository.Model.Models;

namespace VirtualUniverse.EFCoreRepository.Repository
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class EFCoreRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly DbContext _context;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="context"></param>
        protected EFCoreRepository(DbContext context)
        {
            _context = context;
        }

        private DbSet<TEntity> GetDbSet()
        {
            return _context.Set<TEntity>();
        }
        /// <summary>
        /// 清理函数
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        #region 删除
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int Delete(TEntity entity)
        {
            GetDbSet().Remove(entity).State = EntityState.Detached;
            return _context.SaveChanges();
        }
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public int Delete(IList<TEntity> entities)
        {
            GetDbSet().RemoveRange(entities);
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(TEntity entity)
        {
            GetDbSet().Remove(entity).State = EntityState.Detached;
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public Task<int> DeleteAsync(IList<TEntity> entities)
        {
            GetDbSet().RemoveRange(entities);
            return _context.SaveChangesAsync();
        }
        #endregion
        #region 查询
        /// <summary>
        /// 查询一条实体
        /// </summary>
        /// <param name="primaryKeys">主键值</param>
        /// <returns></returns>
        public TEntity Find(params object[] primaryKeys)
        {
            return GetDbSet().Find(primaryKeys);
        }
        /// <summary>
        /// 异步查询一条实体
        /// </summary>
        /// <param name="primaryKey">主键值</param>
        /// <returns></returns>
        public async Task<TEntity> FindAsync(params object[] primaryKey)
        {
            return await GetDbSet().FindAsync(primaryKey);
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
            GetDbSet().Add(entity).State = EntityState.Detached;
            return _context.SaveChanges();
        }
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Insert(IList<TEntity> entities)
        {
            GetDbSet().AddRange(entities);
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> InsertAsync(TEntity entity)
        {
            GetDbSet().AddAsync(entity);
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public Task<int> InsertAsync(IList<TEntity> entities)
        {
            GetDbSet().AddRangeAsync(entities);
            return _context.SaveChangesAsync();
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
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
            typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            GetDbSet().Update(entity).State = EntityState.Detached;
            return _context.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int MarkDelete(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
                typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            }
            GetDbSet().UpdateRange(entities);
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> MarkDeleteAsync(TEntity entity)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
            typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            GetDbSet().Update(entity).State = EntityState.Detached; 
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public Task<int> MarkDeleteAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
                typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            }
            GetDbSet().UpdateRange(entities);
            return _context.SaveChangesAsync();
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
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            GetDbSet().Update(entity).State = EntityState.Detached;
            return _context.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int UnmarkDelete(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            }
            GetDbSet().UpdateRange(entities);
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> UnmarkDeleteAsync(TEntity entity)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            GetDbSet().Update(entity).State = EntityState.Detached;
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public Task<int> UnmarkDeleteAsync(IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            }
            GetDbSet().UpdateRange(entities);
            return _context.SaveChangesAsync();
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
            GetDbSet().Update(entity).State = EntityState.Detached;
            return _context.SaveChanges();
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public int Update(IList<TEntity> entities)
        {
            GetDbSet().UpdateRange(entities);
            return _context.SaveChanges();
        }
        /// <summary>
        /// 异步更新一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> UpdateAsync(TEntity entity)
        {
            GetDbSet().UpdateRange(entity);
            return _context.SaveChangesAsync();
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public Task<int> UpdateAsync(IList<TEntity> entities)
        {
            GetDbSet().UpdateRange(entities);
            return _context.SaveChangesAsync();
        }
        
        #endregion
    }
}
