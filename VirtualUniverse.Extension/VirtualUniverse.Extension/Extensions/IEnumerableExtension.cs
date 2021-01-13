using System.Collections.Generic;
using System.Linq;

namespace VirtualUniverse.Extension.Extensions
{
    /// <summary>
    /// 枚举接口扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="TSource">实体类型</typeparam>
        /// <param name="sources">数据源</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public static IEnumerable<TSource> ToPagination<TSource>(this IEnumerable<TSource> sources, int? pageIndex, int? pageSize)
        {
            return sources.Skip((pageIndex.GetValueOrDefault(0) - 1) * pageSize.GetValueOrDefault(0)).Take(pageSize.GetValueOrDefault(0));
        }
    }
}
