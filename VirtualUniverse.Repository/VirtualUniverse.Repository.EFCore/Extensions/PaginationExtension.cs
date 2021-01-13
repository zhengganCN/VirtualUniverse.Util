using System;
using System.Linq;

namespace VirtualUniverse.Repository.EFCore.Extensions
{
    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class PaginationExtension
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable">可查询</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public static IQueryable<T> ToPagination<T>(this IQueryable<T> queryable, int? pageIndex, int? pageSize)
        {
            if (pageIndex.GetValueOrDefault() <= 0)
            {
                throw new ArgumentException($"{pageIndex}不能小于等于0");
            }
            return queryable.Skip((pageIndex.GetValueOrDefault() - 1) * pageSize.GetValueOrDefault()).Take(pageSize.GetValueOrDefault());
        }
    }
}
