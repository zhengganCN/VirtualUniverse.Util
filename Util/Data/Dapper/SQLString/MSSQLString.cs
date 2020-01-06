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
