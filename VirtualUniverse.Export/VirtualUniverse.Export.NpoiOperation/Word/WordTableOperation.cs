using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VirtualUniverse.Export.NpoiOperation.Models.WordModels;

/***********************************************************************************
****作者：zhenggan；创建时间：2020/12/30 23:20:43；更新时间：
************************************************************************************/
namespace VirtualUniverse.Export.NpoiOperation.Word
{
    /// <summary>
    /// 类说明：Word表格操作
    /// </summary>
    public static class WordTableOperation
    {
        /// <summary>
        /// 创建表格
        /// </summary>
        /// <param name="xWPFDocument"></param>
        /// <param name="xWPFTable"></param>
        public static void CreateTable(XWPFDocument xWPFDocument, out XWPFTable xWPFTable)
        {
            xWPFTable = xWPFDocument.CreateTable();
            CT_Tbl ctTbl = xWPFDocument.Document.body.GetTblArray().Last();
            //设置表水平居中
            var cT_TblPr = ctTbl.AddNewTblPr();

            cT_TblPr.jc = new CT_Jc
            {
                val = ST_Jc.center
            };

            CT_TblLayoutType type = cT_TblPr.AddNewTblLayout();
            type.type = ST_TblLayoutType.@fixed;
            //设置表宽度
            var cT_TblWidth = ctTbl.AddNewTblPr().AddNewTblW();
            cT_TblWidth.w = "8000";
            cT_TblWidth.type = ST_TblWidth.dxa;

            xWPFTable.RemoveRow(0);
        }

        /// <summary>
        /// 创建表格行
        /// </summary>
        /// <param name="xWPFTable"></param>
        /// <param name="xWPFTableRow"></param>
        public static void CreateRow(XWPFTable xWPFTable, out XWPFTableRow xWPFTableRow)
        {
            xWPFTableRow = new XWPFTableRow(new CT_Row(), xWPFTable);
            xWPFTable.AddRow(xWPFTableRow);
        }

        /// <summary>
        /// 创建表格单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="xWPFTableCell"></param>
        public static void CreateCell(XWPFTableRow row, out XWPFTableCell xWPFTableCell)
        {
            xWPFTableCell = row.AddNewTableCell();
        }

        /// <summary>
        /// 设置表格单元格的宽度
        /// </summary>
        /// <param name="xWPFTableCell">单元格</param>
        /// <param name="width">宽度</param>
        public static void SetCellWidth(XWPFTableCell xWPFTableCell, int? width = null)
        {
            CT_TcPr m_Pr = xWPFTableCell.GetCTTc().AddNewTcPr();
            if (width != null)
            {
                m_Pr.tcW = new CT_TblWidth
                {
                    w = width.ToString(),//单元格宽
                    type = ST_TblWidth.dxa//固定宽度，auto为自动伸缩
                };
            }
        }

        /// <summary>
        /// 向表格单元格添加文本
        /// </summary>
        /// <param name="xWPFTableCell">单元格</param>
        /// <param name="text">文本</param>
        /// <param name="tableCellStyle">单元格样式</param>
        public static void SetText(XWPFTableCell xWPFTableCell, string text, TableCellStyle tableCellStyle = null)
        {
            if (tableCellStyle is null)
            {
                tableCellStyle = new TableCellStyle();
            }
            var lines = HandleTableCellText(text);
            if (lines is null || lines.Length == 0)
            {
                return;
            }
            xWPFTableCell.SetText(lines[0]);
            if (lines.Length > 1)
            {
                AddTextNewLine(xWPFTableCell, lines);
            }

            var ctTc = xWPFTableCell.GetCTTc();
            CT_TcPr cT_TcPr = ctTc.IsSetTcPr() ? ctTc.tcPr : ctTc.AddNewTcPr();
            cT_TcPr.AddNewVAlign().val = (ST_VerticalJc)((int)tableCellStyle.VerticalAlignment);//垂直居中
            ctTc.GetPList()[0].AddNewPPr().AddNewJc().val = (ST_Jc)((int)tableCellStyle.HorizontalAlignment);
        }

        /// <summary>
        /// 向表格单元格追加文本
        /// </summary>
        /// <param name="xWPFTableCell">单元格</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static void AppendText(XWPFTableCell xWPFTableCell, string text)
        {
            AddTextNewLine(xWPFTableCell, HandleTableCellText(text));
        }

        /// <summary>
        /// 换行后追加文本
        /// </summary>
        /// <param name="xWPFTableCell">单元格</param>
        /// <param name="lines">文本行</param>
        private static void AddTextNewLine(XWPFTableCell xWPFTableCell, string[] lines)
        {
            var paragraph = xWPFTableCell.AddParagraph();
            for (int i = 1; i < lines.Length; i++)
            {
                XWPFRun xWPFRun = paragraph.CreateRun();
                xWPFRun.SetText(lines[i]);
                if (i != lines.Length - 1)
                {
                    xWPFRun.AddBreak(BreakType.TEXTWRAPPING);
                }
            }
        }

        /// <summary>
        /// 处理表格单元格文本，获取按行读取的文本列表
        /// </summary>
        /// <param name="text">文本</param>
        /// <returns></returns>
        private static string[] HandleTableCellText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return Array.Empty<string>();
            }
            var textLines = new List<string>();
            var bytes = Encoding.UTF8.GetBytes(text);
            using MemoryStream memory = new MemoryStream(bytes);
            using StreamReader streamReader = new StreamReader(memory);
            string line;
            var index = 0;
            while ((line = streamReader.ReadLine()) != null)
            {
                if (index == 0 && string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                textLines.Add(line);
            }
            return textLines.ToArray();
        }

        /// <summary>
        /// 设置单元格背景色
        /// </summary>
        /// <param name="xWPFTableCell">单元格</param>
        /// <param name="color">颜色，十六进制颜色代码</param>
        public static void SetCellBacogroundColor(XWPFTableCell xWPFTableCell, string color)
        {
            xWPFTableCell.SetColor(color);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="table">表</param>
        /// <param name="fromCol">合并的起始列，从0开始</param>
        /// <param name="toCol">合并的结束列</param>
        /// <param name="fromRow">合并的起始行，从0开始</param>
        /// <param name="toRow">合并的结束行</param>
        /// <returns>合并后的单元格</returns>
        public static XWPFTableCell MergeCells(XWPFTable table, int fromCol, int toCol, int fromRow, int toRow)
        {
            for (int rowIndex = fromRow; rowIndex <= toRow; rowIndex++)
            {
                if (fromCol < toCol)
                {
                    table.GetRow(rowIndex).MergeCells(fromCol, toCol);
                }
                XWPFTableCell rowcell = table.GetRow(rowIndex).GetCell(fromCol);
                CT_Tc cttc = rowcell.GetCTTc();
                if (cttc.tcPr == null)
                {
                    cttc.AddNewTcPr();
                }
                if (rowIndex == fromRow)
                {
                    // The first merged cell is set with RESTART merge value  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.restart;
                }
                else
                {
                    // Cells which join (merge) the first one, are set with CONTINUE  
                    rowcell.GetCTTc().tcPr.AddNewVMerge().val = ST_Merge.@continue;
                }
            }
            return table.GetRow(fromRow).GetCell(fromCol);
        }
    }
}
