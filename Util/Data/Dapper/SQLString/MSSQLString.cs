using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.Dapper.SQLString
{
    /// <summary>
    /// MSSQL增、删、改、查语句帮助类
    /// </summary>
    public class MSSQLString : AbstractSQLString
    {
        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <param name="SequnceField">排序字段</param>
        /// <param name="sequence">排序方式</param>
        /// <param name="pageIndex">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public override string QuerySQLString<TEntity>(string conditionString, string SequnceField, EnumSequence sequence, int pageIndex, int pageSize)
        {
            var propertyInfos = typeof(TEntity).GetProperties();
            StringBuilder argumentNames = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfoName = propertyInfos[i].Name;
                argumentNames.Append(propertyInfoName);
                if (propertyInfos.Length != i + 1)
                {
                    argumentNames.Append(',');
                }
            }
            var seqSQLString = "";
            switch (sequence)
            {
                case EnumSequence.Descending:
                    seqSQLString = $"ORDER BY {SequnceField} DESC";
                    break;
                case EnumSequence.Ascending:
                    seqSQLString = $"ORDER BY {SequnceField} ASC";
                    break;
            }
            return $"SELECT {argumentNames.ToString()} FROM {typeof(TEntity).Name} " +
                $"{conditionString} {seqSQLString} OFFSET {(pageIndex-1)*pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY";
        }
    }
}
