using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AmazedDataContext
{
    /// <summary>
    /// 排序方式
    /// </summary>
    public enum EnumSequence
    {
        /// <summary>
        /// 降序
        /// </summary>
        [Description("降序")]
        Descending = 1,
        /// <summary>
        /// 升序
        /// </summary>
        [Description("升序")]
        Ascending = 2
    }
}
