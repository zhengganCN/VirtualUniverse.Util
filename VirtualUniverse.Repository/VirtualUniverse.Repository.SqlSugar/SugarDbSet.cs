using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/26 22:33:29；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.SqlSugar
{
    /// <summary>
    /// 类说明：DbSet实现类
    /// </summary>
    public sealed class SugarDbSet<T> : DbSet<T> where T : class, new()
    {
        private readonly SimpleClient<T> SimpleClient;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sqlSugarClient">客户端</param>
        public SugarDbSet(ISqlSugarClient sqlSugarClient)
        {
            SimpleClient = new SimpleClient<T>(sqlSugarClient);
        }

#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
        public override IDeleteable<T> AsDeleteable()
        {
            return SimpleClient.AsDeleteable();
        }

        public override IInsertable<T> AsInsertable(List<T> insertObjs)
        {
            return SimpleClient.AsInsertable(insertObjs);
        }

        public override IInsertable<T> AsInsertable(T insertObj)
        {
            return SimpleClient.AsInsertable(insertObj);
        }

        public override IInsertable<T> AsInsertable(T[] insertObjs)
        {
            return SimpleClient.AsInsertable(insertObjs);
        }

        public override ISugarQueryable<T> AsQueryable()
        {
            return SimpleClient.AsQueryable();
        }

        public override ISqlSugarClient AsSugarClient()
        {
            return SimpleClient.AsSugarClient();
        }

        public override ITenant AsTenant()
        {
            return SimpleClient.AsTenant();
        }

        public override IUpdateable<T> AsUpdateable(List<T> updateObjs)
        {
            return SimpleClient.AsUpdateable(updateObjs);
        }

        public override IUpdateable<T> AsUpdateable(T updateObj)
        {
            return SimpleClient.AsUpdateable(updateObj);
        }

        public override IUpdateable<T> AsUpdateable(T[] updateObjs)
        {
            return SimpleClient.AsUpdateable(updateObjs);
        }

        public override SimpleClient<ChangeType> Change<ChangeType>()
        {
            return SimpleClient.Change<ChangeType>();
        }

        public override int Count(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.Count(whereExpression);
        }

        public override Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.CountAsync(whereExpression);
        }

        public override bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.Delete(whereExpression);
        }

        public override bool Delete(T deleteObj)
        {
            return SimpleClient.Delete(deleteObj);
        }

        public override Task<bool> DeleteAsync(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.DeleteAsync(whereExpression);
        }

        public override Task<bool> DeleteAsync(T deleteObj)
        {
            return SimpleClient.DeleteAsync(deleteObj);
        }

        public override bool DeleteById(dynamic id)
        {
            return SimpleClient.DeleteById(id);
        }

        public override Task<bool> DeleteByIdAsync(dynamic id)
        {
            return SimpleClient.DeleteByIdAsync(id);
        }

        public override bool DeleteByIds(dynamic[] ids)
        {
            return SimpleClient.DeleteByIds(ids);
        }

        public override Task<bool> DeleteByIdsAsync(dynamic[] ids)
        {
            return SimpleClient.DeleteByIdsAsync(ids);
        }

        public override T GetById(dynamic id)
        {
            return SimpleClient.GetById(id);
        }

        public override Task<T> GetByIdAsync(dynamic id)
        {
            return SimpleClient.GetByIdAsync(id);
        }

        public override List<T> GetList()
        {
            return SimpleClient.GetList();
        }

        public override List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.GetList(whereExpression);
        }

        public override Task<List<T>> GetListAsync()
        {
            return SimpleClient.GetListAsync();
        }

        public override Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.GetListAsync(whereExpression);
        }

        public override List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page)
        {
            return SimpleClient.GetPageList(whereExpression, page);
        }

        public override List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return SimpleClient.GetPageList(whereExpression, page, orderByExpression, orderByType);
        }

        public override List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page)
        {
            return SimpleClient.GetPageList(conditionalList, page);
        }

        public override List<T> GetPageList(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return SimpleClient.GetPageList(conditionalList, page, orderByExpression, orderByType);
        }

        public override Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page)
        {
            return SimpleClient.GetPageListAsync(whereExpression, page);
        }

        public override Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return SimpleClient.GetPageListAsync(whereExpression, page, orderByExpression, orderByType);
        }

        public override Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page)
        {
            return SimpleClient.GetPageListAsync(conditionalList, page);
        }

        public override Task<List<T>> GetPageListAsync(List<IConditionalModel> conditionalList, PageModel page, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return SimpleClient.GetPageListAsync(conditionalList, page, orderByExpression, orderByType);
        }

        public override T GetSingle(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.GetSingle(whereExpression);
        }

        public override Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.GetSingleAsync(whereExpression);
        }

        public override bool Insert(T insertObj)
        {
            return SimpleClient.Insert(insertObj);
        }

        public override Task<bool> InsertAsync(T insertObj)
        {
            return SimpleClient.InsertAsync(insertObj);
        }

        public override bool InsertRange(List<T> insertObjs)
        {
            return SimpleClient.InsertRange(insertObjs);
        }

        public override bool InsertRange(T[] insertObjs)
        {
            return SimpleClient.InsertRange(insertObjs);
        }

        public override Task<bool> InsertRangeAsync(List<T> insertObjs)
        {
            return SimpleClient.InsertRangeAsync(insertObjs);
        }

        public override Task<bool> InsertRangeAsync(T[] insertObjs)
        {
            return SimpleClient.InsertRangeAsync(insertObjs);
        }

        public override long InsertReturnBigIdentity(T insertObj)
        {
            return SimpleClient.InsertReturnBigIdentity(insertObj);
        }

        public override Task<long> InsertReturnBigIdentityAsync(T insertObj)
        {
            return SimpleClient.InsertReturnBigIdentityAsync(insertObj);
        }

        public override int InsertReturnIdentity(T insertObj)
        {
            return SimpleClient.InsertReturnIdentity(insertObj);
        }

        public override Task<int> InsertReturnIdentityAsync(T insertObj)
        {
            return SimpleClient.InsertReturnIdentityAsync(insertObj);
        }

        public override bool IsAny(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.IsAny(whereExpression);
        }

        public override Task<bool> IsAnyAsync(Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.IsAnyAsync(whereExpression);
        }

        public override bool Update(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.Update(columns, whereExpression);
        }

        public override bool Update(T updateObj)
        {
            return SimpleClient.Update(updateObj);
        }

        public override Task<bool> UpdateAsync(Expression<Func<T, T>> columns, Expression<Func<T, bool>> whereExpression)
        {
            return SimpleClient.UpdateAsync(columns, whereExpression);
        }

        public override Task<bool> UpdateAsync(T updateObj)
        {
            return SimpleClient.UpdateAsync(updateObj);
        }

        public override bool UpdateRange(List<T> updateObjs)
        {
            return SimpleClient.UpdateRange(updateObjs);
        }

        public override bool UpdateRange(T[] updateObjs)
        {
            return SimpleClient.UpdateRange(updateObjs);
        }

        public override Task<bool> UpdateRangeAsync(List<T> updateObjs)
        {
            return SimpleClient.UpdateRangeAsync(updateObjs);
        }

        public override Task<bool> UpdateRangeAsync(T[] updateObjs)
        {
            return SimpleClient.UpdateRangeAsync(updateObjs);
        }
    }
}
