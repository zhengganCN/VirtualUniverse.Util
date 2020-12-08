using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazedDataContext.EFCore.Repository.Interfaces
{
    /// <summary>
    /// 仓储基类接口
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : Entity, new()
    {
        /// <summary>
        /// 删除多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Delete(IList<TEntity> entities);
        /// <summary>
        /// 删除一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Delete(TEntity entity);
        /// <summary>
        /// 异步删除多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(IList<TEntity> entities);
        /// <summary>
        /// 异步删除一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(TEntity entity);
        /// <summary>
        /// 查询一条实体
        /// </summary>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        TEntity Find(params object[] primaryKeys);
        /// <summary>
        /// 异步查询一条实体
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(params object[] primaryKey);
        /// <summary>
        /// 插入多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Insert(IList<TEntity> entities);
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Insert(TEntity entity);
        /// <summary>
        /// 异步插入多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> InsertAsync(IList<TEntity> entities);
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> InsertAsync(TEntity entity);
        /// <summary>
        /// 修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int MarkDelete(IList<TEntity> entities);
        /// <summary>
        /// 修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int MarkDelete(TEntity entity);
        /// <summary>
        /// 异步修改多条实体的删除标识，改为true
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> MarkDeleteAsync(IList<TEntity> entities);
        /// <summary>
        /// 异步修改一条实体的删除标识，改为true
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> MarkDeleteAsync(TEntity entity);
        /// <summary>
        /// 修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int UnmarkDelete(IList<TEntity> entities);
        /// <summary>
        /// 修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int UnmarkDelete(TEntity entity);
        /// <summary>
        /// 异步修改多条实体的删除标识，改为false
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UnmarkDeleteAsync(IList<TEntity> entities);
        /// <summary>
        /// 异步修改一条实体的删除标识，改为false
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UnmarkDeleteAsync(TEntity entity);
        /// <summary>
        /// 更新多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        int Update(IList<TEntity> entities);
        /// <summary>
        /// 更新一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Update(TEntity entity);
        /// <summary>
        /// 异步更新多条实体
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(IList<TEntity> entities);
        /// <summary>
        /// 异步更新一条实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<int> UpdateAsync(TEntity entity);
    }
}
