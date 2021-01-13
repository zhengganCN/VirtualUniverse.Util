namespace VirtualUniverse.Repository.Model.Interfaces
{
    /// <summary>
    /// 查询模型基类接口
    /// </summary>
    public interface IQueryBaseModel
    {
        /// <summary>
        /// 关键字
        /// </summary>
        string Keyword { get; set; }
        /// <summary>
        /// 页面索引
        /// </summary>
        int PageIndex { get; set; }
        /// <summary>
        /// 页面大小
        /// </summary>
        int PageSize { get; set; }
    }
}
