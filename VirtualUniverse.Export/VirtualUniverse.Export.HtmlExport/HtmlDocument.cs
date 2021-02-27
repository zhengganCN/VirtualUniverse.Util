using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualUniverse.Export.HtmlExport
{
    /// <summary>
    /// 类 描 述：Html文档
    /// </summary>
    public class HtmlDocument
    {
        private const string HtmlFlag = "<!DOCTYPE html>";
        private readonly List<HtmlNodeObject> HtmlNodeObjects = new List<HtmlNodeObject>();



        private readonly StringBuilder builder = new StringBuilder();
        /// <summary>
        /// Html节点
        /// </summary>
        public HtmlNodeObject Html { get; set; } = new HtmlNodeObject
        {
            NodeName = EnumHtmlNodeType.Html
        };
        /// <summary>
        /// Head节点
        /// </summary>
        public virtual HtmlNodeObject Head { get; set; } = new HtmlNodeObject
        {
            NodeName = EnumHtmlNodeType.Head
        };
        /// <summary>
        /// Body节点
        /// </summary>
        public virtual HtmlNodeObject Body { get; set; } = new HtmlNodeObject
        {
            NodeName = EnumHtmlNodeType.Body
        };

        /// <summary>
        /// 初始化
        /// </summary>
        public HtmlDocument()
        {
            InitHtml();
        }

        /// <summary>
        /// 初始化Html
        /// </summary>
        public void InitHtml()
        {
            AddStyleNode();
            AddScriptNode();
            AddHtmlNode();
            AddHtmlNode(Html, Head);
            AddHtmlNode(Html, Body);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <returns></returns>
        public byte[] Write()
        {
            var html = GenerateString(HtmlNodeObjects, builder);
            return Encoding.UTF8.GetBytes(HtmlFlag + html);
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="path"></param>
        public void Write(string path)
        {
            var html = GenerateString(HtmlNodeObjects, builder);
            using var fs = new FileStream(path, FileMode.Create);
            fs.Write(Encoding.UTF8.GetBytes(HtmlFlag + html));
        }

        internal static string GenerateString(List<HtmlNodeObject> htmlNodeObjects,StringBuilder builder)
        {
            foreach (var htmlNodeObject in htmlNodeObjects)
            {
                if (htmlNodeObject.NodeName == EnumHtmlNodeType.UserDefine)
                {
                    builder.Append(htmlNodeObject.UserDefineNode);
                }
                else
                {
                    var labelName = Enum.GetName(typeof(EnumHtmlNodeType), htmlNodeObject.NodeName).ToLower();
                    builder.Append('<');
                    builder.Append(labelName);
                    if (htmlNodeObject.NodeProperties != null)
                    {
                        foreach (var property in htmlNodeObject.NodeProperties)
                        {
                            builder.Append(' ');
                            builder.Append(property);
                        }
                    }
                    builder.Append('>');
                    if (!string.IsNullOrEmpty(htmlNodeObject.NodeContent))
                    {
                        builder.Append(htmlNodeObject.NodeContent);
                    }
                    if (htmlNodeObject.ChildrenHtmlNodeObject != null)
                    {
                        GenerateString(htmlNodeObject.ChildrenHtmlNodeObject,builder);
                    }
                    builder.Append("</");
                    builder.Append(labelName);
                    builder.Append('>');
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 添加样式
        /// </summary>
        public virtual void AddStyleNode()
        {
        }

        /// <summary>
        /// 添加脚本
        /// </summary>
        public virtual void AddScriptNode()
        {
        }

        /// <summary>
        /// 添加&lt;Html&gt;标签节点
        /// </summary>
        public virtual void AddHtmlNode()
        {
            HtmlNodeObjects.Add(Html);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="parant">父节点</param>
        /// <param name="node">子节点</param>
        /// <returns></returns>
        public HtmlNodeObject AddHtmlNode(HtmlNodeObject parant, HtmlNodeObject node)
        {
            if (parant.ChildrenHtmlNodeObject is null)
            {
                parant.ChildrenHtmlNodeObject = new List<HtmlNodeObject>();
            }
            parant.ChildrenHtmlNodeObject.Add(node);
            return node;
        }
    }
}
