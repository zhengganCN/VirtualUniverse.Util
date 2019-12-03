using System;
using System.Collections.Generic;
using System.Text;

namespace Util.ModelResult
{
    public class Pagination
    {
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
