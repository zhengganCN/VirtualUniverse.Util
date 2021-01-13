/***********************************************************************************
****作者：zhenggan；创建时间：2020/12/30 23:16:24；更新时间：
************************************************************************************/
namespace VirtualUniverse.NpoiOperation.Models.WordModels
{
    /// <summary>
    /// 类说明：表格单元格样式
    /// </summary>
    public class TableCellStyle
    {
        /// <summary>
        /// 垂直对齐方式
        /// </summary>
        public EnumWordTableCellVerticalAlignment VerticalAlignment { get; set; } = EnumWordTableCellVerticalAlignment.Center;
        /// <summary>
        /// 水平对齐方式
        /// </summary>
        public EnumWordTableCellHorizontalAlignment HorizontalAlignment { get; set; } = EnumWordTableCellHorizontalAlignment.Left;
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string TextColor { get; set; }
        /// <summary>
        /// 单元格颜色
        /// </summary>
        public string CellColor { get; set; }
    }
}
