using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace VirtualUniverse.Repository.Model.Models
{
    /// <summary>
    /// 实体基类
    /// </summary>
    public class Entity
    {
        /// <summary>
        /// 删除标识
        /// </summary>
        [Description("删除标识")]
        public bool IsDeleted { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 删除时间
        /// </summary>
        [Description("删除时间")]
        public DateTime? DeleteTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Description("更新时间")]
        public DateTime? UpdateTime { get; set; }
    }
}
