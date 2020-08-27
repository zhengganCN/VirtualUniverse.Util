using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedDataContext.Model
{
    /// <summary>
    /// 查询模型基类
    /// </summary>
    public class BaseQueryModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
