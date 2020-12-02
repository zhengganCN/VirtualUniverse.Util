using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazedDataContext.EFCore.Repository.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : Entity, new()
    {
        int Delete(IList<TEntity> entities);
        int Delete(TEntity entity);
        Task<int> DeleteAsync(IList<TEntity> entities);
        Task<int> DeleteAsync(TEntity entity);
        void Dispose();
        TEntity Find(params object[] primaryKeys);
        Task<TEntity> FindAsync(params object[] primaryKey);
        int Insert(IList<TEntity> entities);
        int Insert(TEntity entity);
        Task<int> InsertAsync(IList<TEntity> entities);
        Task<int> InsertAsync(TEntity entity);
        int MarkDelete(IList<TEntity> entities);
        int MarkDelete(TEntity entity);
        Task<int> MarkDeleteAsync(IList<TEntity> entities);
        Task<int> MarkDeleteAsync(TEntity entity);
        int UnmarkDelete(IList<TEntity> entities);
        int UnmarkDelete(TEntity entity);
        Task<int> UnmarkDeleteAsync(IList<TEntity> entities);
        Task<int> UnmarkDeleteAsync(TEntity entity);
        int Update(IList<TEntity> entities);
        int Update(TEntity entity);
        Task<int> UpdateAsync(IList<TEntity> entities);
        Task<int> UpdateAsync(TEntity entity);
    }
}
