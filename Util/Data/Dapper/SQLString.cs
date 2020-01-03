using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.Dapper
{
    static class SQLString
    {
        public static string InsertSQLString<TEntity>(TEntity entity)
        {
            var propertyInfos = entity.GetType().GetProperties();
            StringBuilder argumentNames = new StringBuilder();
            StringBuilder argumentValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                argumentNames.Append(propertyInfos[i].Name);
                argumentValues.Append($"@{propertyInfos[i].Name}");
                if (propertyInfos.Length != i + 1)
                {
                    argumentNames.Append(',');
                    argumentValues.Append(',');
                }
            }
            return $"INSERT INTO {entity.GetType().Name} ({argumentNames.ToString()}) " +
                $"VALUES({argumentValues.ToString()})";
        }
        public static string UpdateSQLString<TEntity>(TEntity entity)
        {
            var propertyInfos = entity.GetType().GetProperties();
            StringBuilder argumentNamesValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                argumentNamesValues.Append(propertyInfos[i].Name);
                argumentNamesValues.Append("=@");
                if (propertyInfos.Length != i + 1)
                {
                    argumentNamesValues.Append(',');
                }
            }
            return $"UPDATE {entity.GetType().Name} SET ({argumentNamesValues.ToString()}) Where Id=@Id";
        }
        public static string UpdateSQLString<TEntity>(TEntity entity, string conditionString)
        {
            var propertyInfos = entity.GetType().GetProperties();
            StringBuilder argumentNamesValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                argumentNamesValues.Append(propertyInfos[i].Name);
                argumentNamesValues.Append("=@");
                if (propertyInfos.Length != i + 1)
                {
                    argumentNamesValues.Append(',');
                }
            }
            return $"UPDATE {entity.GetType().Name} SET ({argumentNamesValues.ToString()}) "
                + (string.IsNullOrEmpty(conditionString) ? "Where " + conditionString : "");
        }
        public static string DeleteSQLString<TEntity>(TEntity entity)
        {
            return $"DELETE {entity.GetType().Name} Where Id=@Id";
        }
        public static string DeleteSQLString<TEntity>(TEntity entity, string conditionString)
        {
            return $"DELETE {entity.GetType().Name} "+ (string.IsNullOrEmpty(conditionString) 
                ? "Where " + conditionString : "");
        }

        public static string QueryAllSQLString<TEntity>(TEntity entity)
        {
            return $"SELECT * FROM {entity.GetType().Name}";
        }
    }
}
