using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Data.Dapper
{
    public static class SQLString
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
            return $"INSERT INTO {typeof(TEntity).Name} ({argumentNames.ToString()}) " +
                $"VALUES({argumentValues.ToString()})";
        }
        public static string UpdateSQLString<TEntity>(TEntity entity)
        {
            var propertyInfos = entity.GetType().GetProperties();
            StringBuilder argumentNamesValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfoName = propertyInfos[i].Name;
                argumentNamesValues.Append(propertyInfoName);
                argumentNamesValues.Append("=@");
                argumentNamesValues.Append(propertyInfoName);
                if (propertyInfos.Length != i + 1)
                {
                    argumentNamesValues.Append(',');
                }
            }
            return $"UPDATE {typeof(TEntity).Name} SET {argumentNamesValues.ToString()} Where Id=@Id";
        }
        public static string UpdateSQLString<TEntity>(object updateFields)
        {
            var propertyInfos = updateFields.GetType().GetProperties();
            StringBuilder argumentNamesValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfoName = propertyInfos[i].Name;
                argumentNamesValues.Append(propertyInfoName);
                argumentNamesValues.Append("=@");
                argumentNamesValues.Append(propertyInfoName);
                if (propertyInfos.Length != i + 1)
                {
                    argumentNamesValues.Append(',');
                }
            }
            return $"UPDATE {typeof(TEntity).Name} SET {argumentNamesValues.ToString()} Where Id=@Id";
        }
        public static string UpdateSQLString<TEntity>(object updateFields, string conditionString)
        {
            var propertyInfos = updateFields.GetType().GetProperties();
            StringBuilder argumentNamesValues = new StringBuilder();
            for (int i = 0; i < propertyInfos.Length; i++)
            {
                var propertyInfoName = propertyInfos[i].Name;
                argumentNamesValues.Append(propertyInfoName);
                argumentNamesValues.Append("=@");
                argumentNamesValues.Append(propertyInfoName);
                if (propertyInfos.Length != i + 1)
                {
                    argumentNamesValues.Append(',');
                }
            }
            return $"UPDATE {typeof(TEntity).Name} SET {argumentNamesValues.ToString()} {conditionString}";
        }
        public static string DeleteSQLString<TEntity>()
        {
            return $"DELETE FROM {typeof(TEntity).Name} Where Id=@Id";
        }
        public static string DeleteSQLString<TEntity>(string conditionString)
        {
            return $"DELETE FROM {typeof(TEntity).Name} {conditionString}";
        }
        public static string QuerySQLString<TEntity>()
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
            return $"SELECT {argumentNames.ToString()} FROM {typeof(TEntity).Name}";
        }
        public static string QuerySQLString<TEntity>(string conditionString)
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
            return $"SELECT {argumentNames.ToString()} FROM {typeof(TEntity).Name} {conditionString}";
        }
        public static string QuerySQLString<TEntity>(string conditionString, string SequnceField, EnumSequence sequence, int pageIndex, int pageSize)
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
            return $"SELECT {argumentNames.ToString()} FROM {typeof(TEntity).Name} {conditionString} {seqSQLString} LIMIT {(pageIndex - 1) * pageSize},{pageSize}";
        }
        public static string MarkDeleteSQLString<TEntity>()
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=1 Where Id=@Id";
        }
        public static string MarkDeleteSQLString<TEntity>(string conditionString)
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=1 {conditionString}";
        }
        public static string MarkUnDeleteSQLString<TEntity>()
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=0 Where Id=@Id";
        }
        public static string MarkUnDeleteSQLString<TEntity>(string conditionString)
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=0 {conditionString}";
        }
    }
}
