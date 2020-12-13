using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedModelResult
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="count">项总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页面大小</param>
        public Pagination(int count, int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling((double)count / pageSize);
        }
        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; private set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; private set; }
        /// <summary>
        /// 项总数
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 页面总数
        /// </summary>
        public int PageCount { get; private set; }
    }
}
