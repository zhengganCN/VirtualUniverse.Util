using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Util.Data.Repository
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum SortMode
    {
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Ascending = 1,
        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Descending = 2
    }
}
