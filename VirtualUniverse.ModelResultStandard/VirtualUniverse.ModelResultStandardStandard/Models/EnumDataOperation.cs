using System.ComponentModel;

namespace VirtualUniverse.ModelResultStandard.Models
{
    /// <summary>
    /// 数据操作结果枚举
    /// </summary>
    public enum EnumDataOperation
    {
        /// <summary>
        /// 数据添加成功
        /// </summary>
        [Description("数据添加成功")]
        InsertSucceed = 102001,
        /// <summary>
        /// 数据添加失败
        /// </summary>
        [Description("数据添加失败")]
        InsertFaild = 102002,
        /// <summary>
        /// 数据删除成功
        /// </summary>
        [Description("数据删除成功")]
        DeleteSucceed = 102003,
        /// <summary>
        /// 数据删除失败
        /// </summary>
        [Description("数据删除失败")]
        DeleteFaild = 102004,
        /// <summary>
        /// 数据更新成功
        /// </summary>
        [Description("数据更新成功")]
        UpdateSucceed = 102005,
        /// <summary>
        /// 数据更新失败
        /// </summary>
        [Description("数据更新失败")]
        UpdateFaild = 102006,
        /// <summary>
        /// 数据查询成功
        /// </summary>
        [Description("数据查询成功")]
        SelectSucceed = 102007,
        /// <summary>
        /// 数据查询失败
        /// </summary>
        [Description("数据查询失败")]
        SelectFaild = 102008
    }
}
