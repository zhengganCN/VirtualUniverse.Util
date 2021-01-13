using VirtualUniverse.Repository.Model.Interfaces;

namespace VirtualUniverse.Repository.Model.Models
{
    /// <summary>
    /// 查询模型基类
    /// </summary>
    public class QueryBaseModel : IQueryBaseModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 页面索引
        /// </summary>
        public int PageIndex { get; set; } = 1;
        /// <summary>
        /// 页面大小
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
