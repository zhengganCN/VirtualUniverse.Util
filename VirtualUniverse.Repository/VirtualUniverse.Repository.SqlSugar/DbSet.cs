using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/26 21:57:54；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.SqlSugar
{
    /// <summary>
    /// 类说明：抽象表操作
    /// </summary>
    public abstract class DbSet<T> : ISimpleClient<T> where T : class, new()
    {
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public abstract IDeleteable<T> AsDeleteable();

        public abstract IInsertable<T> AsInsertable(List<T> insertObjs);

        public abstract IInsertable<T> AsInsertable(T insertObj);

        public abstract IInsertable<T> AsInsertable(T[] insertObjs);

        public abstract ISugarQueryable<T> AsQueryable();

        public abstract ISqlSugarClient AsSugarClient();

        public abstract ITenant AsTenant();

        public abstract IUpdateable<T> AsUpdateable(List<T> updateObjs);

        public abstract IUpdateable<T> AsUpdateable(T updateObj);

        public abstract IUpdateable<T> AsUpdateable(T[] updateObjs);

        public abstract SimpleClient<ChangeType> Change<ChangeType>() where ChangeType : class, new();

        public abstract int Count(Expression<Func<T, bool>> whereExpression);

        public abstract Task<int> CountAsync(Expression<Func<T, bool>> whereExpression);

        public abstract bool Delete(Expression<Func<T, bool>> whereExpression);

        public abstract bool Delete(T deleteObj);

        public abstract Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression);

        public abstract Task<bool> DeleteAsync(T deleteObj);

        public abstract bool DeleteById(dynamic id);

        public abstract Task<bool> DeleteByIdAsync(dynamic id);

        public abstract bool DeleteByIds(dynamic[] ids);

        public abstract Task<bool> DeleteByIdsAsync(dynamic[] ids);

        public abstract T GetById(dynamic id);

        public abstract Task<T> GetByIdAsync(dynamic id);

        public abstract List<T> GetList();

        public abstract List<T> GetList(Expression<Func<T, bool>> whereExpression);

        public abstract Task<List<T>> GetListAsync();

        public abstract Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

        public abstract List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page);

        public abstract List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        public abstract List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page);

        public abstract List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        public abstract Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page);

        public abstract Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        public abstract Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page);

        public abstract Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        public abstract T GetSingle(Expression<Func<T, bool>> whereExpression);

        public abstract Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);

        public abstract bool Insert(T insertObj);

        public abstract Task<bool> InsertAsync(T insertObj);

        public abstract bool InsertRange(List<T> insertObjs);

        public abstract bool InsertRange(T[] insertObjs);

        public abstract Task<bool> InsertRangeAsync(List<T> insertObjs);

        public abstract Task<bool> InsertRangeAsync(T[] insertObjs);

        public abstract long InsertReturnBigIdentity(T insertObj);

        public abstract Task<long> InsertReturnBigIdentityAsync(T insertObj);

        public abstract int InsertReturnIdentity(T insertObj);

        public abstract Task<int> InsertReturnIdentityAsync(T insertObj);

        public abstract bool IsAny(Expression<Func<T, bool>> whereExpression);

        public abstract Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression);

        public abstract bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

        public abstract bool Update(T updateObj);

        public abstract Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression);

        public abstract Task<bool> UpdateAsync(T updateObj);

        public abstract bool UpdateRange(List<T> updateObjs);

        public abstract bool UpdateRange(T[] updateObjs);

        public abstract Task<bool> UpdateRangeAsync(List<T> updateObjs);

        public abstract Task<bool> UpdateRangeAsync(T[] updateObjs);
    }
}
