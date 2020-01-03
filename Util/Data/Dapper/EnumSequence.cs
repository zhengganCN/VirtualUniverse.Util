using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Util.Data.Dapper
{
    public enum EnumSequence
    {
        [Description("降序")]
        Descending = 1,
        [Description("升序")]
        Ascending = 2
    }
}
