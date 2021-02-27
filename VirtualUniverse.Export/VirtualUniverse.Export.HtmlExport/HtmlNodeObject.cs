using System;
using System.Collections.Generic;
using System.Text;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/24 10:20:15；更新时间：
************************************************************************************/
namespace VirtualUniverse.Export.HtmlExport
{
    /// <summary>
    /// 类 描 述：Html节点对象
    /// </summary>
    public class HtmlNodeObject
    {
        /// <summary>
        /// 节点属性
        /// </summary>
        public List<string> NodeProperties { get; set; }
        /// <summary>
        /// 节点内容
        /// </summary>
        public string NodeContent { get; set; }
        /// <summary>
        /// 节点类型
        /// </summary>
        public EnumHtmlNodeType NodeName { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        public List<HtmlNodeObject> ChildrenHtmlNodeObject { get; set; }
        /// <summary>
        /// 自定义节点名称
        /// </summary>
        public string UserDefineNode { get; set; }
        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public string Export()
        {
            StringBuilder builder = new StringBuilder();
            return HtmlDocument.GenerateString(new List<HtmlNodeObject> { this }, builder);
        }
    }
}
