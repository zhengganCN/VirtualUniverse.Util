using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data.Repository
{
    interface IRepository<TEntity> where TEntity : class, new()
    {
        #region 查询实体
        public TEntity Find(params object[] primaryKey);
        public Task<TEntity> FindAsync(params object[] primaryKey);
        public IList<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending, int pageIndex = 1, int pageSize = 10);
        public Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending, int pageIndex = 1, int pageSize = 10);
        public IList<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending);
        public Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> keySelector, SortMode sortMode = SortMode.Ascending);
        #endregion
        #region 删除实体（从数据库上删除数据）
        public int Delete(TEntity entity);
        public Task<int> DeleteAsync(TEntity entity);
        public int Delete(IList<TEntity> entities);
        public Task<int> DeleteAsync(IList<TEntity> entities);
        #endregion
        #region 设置实体的删除标记为true（不删除数据库上的数据，只是标记数据的删除标识）
        public int MarkDelete(TEntity entity);
        public Task<int> MarkDeleteAsync(TEntity entity);
        public int MarkDelete(IList<TEntity> entities);
        public Task<int> MarkDeleteAsync(IList<TEntity> entities);
        #endregion
        #region 设置实体的删除标记为false（不删除数据库上的数据，只是标记数据的删除标识）
        public int UnmarkDelete(TEntity entity);
        public Task<int> UnmarkDeleteAsync(TEntity entity);
        public int UnmarkDelete(IList<TEntity> entities);
        public Task<int> UnmarkDeleteAsync(IList<TEntity> entities);
        #endregion
        #region 插入实体
        public int Insert(TEntity entity);
        public Task<int> InsertAsync(TEntity entity);
        public int Insert(IList<TEntity> entities);
        public Task<int> InsertAsync(IList<TEntity> entities);
        #endregion
        #region 更新实体
        public int Update(TEntity entity);
        public Task<int> UpdateAsync(TEntity entity);
        public int Update(IList<TEntity> entities);
        public Task<int> UpdateAsync(IList<TEntity> entities);
        #endregion
        #region 统计
        public int Count(Expression<Func<TEntity, bool>> predicate);
        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        #endregion
    }
}
