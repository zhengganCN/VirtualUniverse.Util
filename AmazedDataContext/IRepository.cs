using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AmazedDataContext
{
    interface IRepository<T>
    {
        T FindOne(object id);
        IList<T> FindAll();
        Task<T> FindOneAsync(object id);
        Task<IList<T>> FindAllAsync();
        int UpdateOne(T entity);
        int UpdateMany(IList<T> entities);
        Task<int> UpdateOneAsync(T entity);
        Task<int> UpdateManyAsync(IList<T> entities);
        int InsertOne(T entity);
        int InsertMany(IList<T> entities);
        Task<int> InsertOneAsync(T entity);
        Task<int> InsertManyAsync(IList<T> entities);
        int PhysicalDeleteOne(object id);
        int PhysicalDeleteMany(IList<object> ids);
        Task<int> PhysicalDeleteOneAsync(object id);
        Task<int> PhysicalDeleteManyAsync(IList<object> ids);
        int LogicalDeleteOne(object id);
        int LogicalDeleteMany(IList<object> ids);
        Task<int> LogicalDeleteOneAsync(object id);
        Task<int> LogicalDeleteManyAsync(IList<object> ids);
    }
}
