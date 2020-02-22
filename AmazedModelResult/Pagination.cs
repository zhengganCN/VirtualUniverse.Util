using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedModelResult.ModelResult
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="pageIndex">页面索引，默认为1</param>
        /// <param name="pageSize">页面大小，默认为10</param>
        public Pagination(int pageIndex = 1, int pageSize = 10)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 项总数
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 页面总数
        /// </summary>
        public int PageCount { get; set; }
    }
}
