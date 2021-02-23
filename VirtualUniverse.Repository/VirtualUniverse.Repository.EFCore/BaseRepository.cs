using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualUniverse.IRepository;
using VirtualUniverse.Repository.Model.Models;

namespace VirtualUniverse.Repository.EFCore
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public static class BaseRepository
    {
        #region 删除
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int Delete<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = context.Remove(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public static int Delete<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            context.RemoveRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> DeleteAsync<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = context.Remove(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">实体</param>
        /// <returns></returns>
        public static async Task<int> DeleteAsync<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            context.RemoveRange(entities);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
        #region 插入
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int Insert<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = context.Add(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static int Insert<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            context.AddRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> InsertAsync<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = await context.AddAsync(entity).ConfigureAwait(false);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static async Task<int> InsertAsync<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            await context.AddRangeAsync(entities).ConfigureAwait(false);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
        #region 标记删除

        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int MarkDelete<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
            typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }

        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="deleteFlagName">删除标记</param>
        /// <param name="deleteTimeAttrName">删除时间字段名</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int MarkDelete<TEntity>(this DbContext context, TEntity entity, string deleteFlagName,
            string deleteTimeAttrName, bool tracking = true)
        {
            typeof(TEntity).GetProperty(deleteFlagName).SetValue(entity, true);
            typeof(TEntity).GetProperty(deleteTimeAttrName).SetValue(entity, DateTime.Now);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }

        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static int MarkDelete<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
                typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            }
            context.UpdateRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <param name="deleteFlagName">删除标记</param>
        /// <param name="deleteTimeAttrName">删除时间字段名</param>
        /// <returns></returns>
        public static int MarkDelete<TEntity>(this DbContext context, IList<TEntity> entities, string deleteFlagName,
            string deleteTimeAttrName)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(deleteFlagName).SetValue(entity, true);
                typeof(TEntity).GetProperty(deleteTimeAttrName).SetValue(entity, DateTime.Now);
            }
            context.UpdateRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> MarkDeleteAsync<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
            typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="deleteFlagName">删除标记</param>
        /// <param name="deleteTimeAttrName">删除时间字段名</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> MarkDeleteAsync<TEntity>(this DbContext context, TEntity entity, string deleteFlagName,
            string deleteTimeAttrName, bool tracking = true)
        {
            typeof(TEntity).GetProperty(deleteFlagName).SetValue(entity, true);
            typeof(TEntity).GetProperty(deleteTimeAttrName).SetValue(entity, DateTime.Now);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static async Task<int> MarkDeleteAsync<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, true);
                typeof(TEntity).GetProperty(nameof(Entity.DeleteTime)).SetValue(entity, DateTime.Now);
            }
            context.UpdateRange(entities);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <param name="deleteFlagName">删除标记</param>
        /// <param name="deleteTimeAttrName">删除时间字段名</param>
        /// <returns></returns>
        public static async Task<int> MarkDeleteAsync<TEntity>(this DbContext context, IList<TEntity> entities,
            string deleteFlagName, string deleteTimeAttrName)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(deleteFlagName).SetValue(entity, true);
                typeof(TEntity).GetProperty(deleteTimeAttrName).SetValue(entity, DateTime.Now);
            }
            context.UpdateRange(entities);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
        #region 标记未删除
        /// <summary>
        /// 修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int UnmarkDelete<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }
        /// <summary>
        /// 修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static int UnmarkDelete<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            }
            context.UpdateRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> UnmarkDeleteAsync<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 异步修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static async Task<int> UnmarkDeleteAsync<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                typeof(TEntity).GetProperty(nameof(Entity.IsDeleted)).SetValue(entity, false);
            }
            context.UpdateRange(entities);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static int Update<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return context.SaveChanges();
        }
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static int Update<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            context.UpdateRange(entities);
            return context.SaveChanges();
        }
        /// <summary>
        /// 异步更新一条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entity">实体</param>
        /// <param name="tracking">是否跟踪</param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync<TEntity>(this DbContext context, TEntity entity, bool tracking = true)
        {
            var entityEntry = context.Update(entity);
            if (tracking)
            {
                entityEntry.State = EntityState.Detached;
            }
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="entities">多条实体</param>
        /// <returns></returns>
        public static async Task<int> UpdateAsync<TEntity>(this DbContext context, IList<TEntity> entities)
        {
            context.UpdateRange(entities);
            return await context.SaveChangesAsync().ConfigureAwait(false);
        }

        #endregion
    }
}
