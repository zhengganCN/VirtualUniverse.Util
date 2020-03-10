using System;
using System.Collections.Generic;
using System.Text;

namespace AmazedDataContext.Dapper.SQLString
{
    /// <summary>
    /// SQL增、删、改、查语句抽象帮助类
    /// </summary>
    public abstract class AbstractSQLString : ISQLString
    {
        /// <summary>
        /// 插入SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual string InsertSQLString<TEntity>(TEntity entity)
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
        /// <summary>
        /// 更新SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public virtual string UpdateSQLString<TEntity>(TEntity entity)
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
        /// <summary>
        /// 更新SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <returns></returns>
        public virtual string UpdateSQLString<TEntity>(object updateFields)
        {
            if (updateFields == null)
            {
                throw new ArgumentNullException(nameof(updateFields));
            }
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
        /// <summary>
        /// 更新SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string UpdateSQLString<TEntity>(object updateFields, string conditionString)
        {
            if (updateFields == null)
            {
                throw new ArgumentNullException(nameof(updateFields));
            }
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
        /// <summary>
        /// 删除SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual string DeleteSQLString<TEntity>()
        {
            return $"DELETE FROM {typeof(TEntity).Name} Where Id=@Id";
        }
        /// <summary>
        /// 删除SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string DeleteSQLString<TEntity>(string conditionString)
        {
            return $"DELETE FROM {typeof(TEntity).Name} {conditionString}";
        }
        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual string QuerySQLString<TEntity>()
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
        /// <summary>
        /// 查询SQL语句
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string QuerySQLString<TEntity>(string conditionString)
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
        public abstract string QuerySQLString<TEntity>(string conditionString, string SequnceField, EnumSequence sequence, int pageIndex, int pageSize);
        /// <summary>
        /// 更新某条记录的删除标记为已删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual string MarkDeleteSQLString<TEntity>()
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=1 Where Id=@Id";
        }
        /// <summary>
        /// 根据条件更新删除标记为已删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string MarkDeleteSQLString<TEntity>(string conditionString)
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=1 {conditionString}";
        }
        /// <summary>
        /// 更新某条记录的删除标记为未删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual string MarkUnDeleteSQLString<TEntity>()
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=0 Where Id=@Id";
        }
        /// <summary>
        /// 根据条件更新删除标记为未删除
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string MarkUnDeleteSQLString<TEntity>(string conditionString)
        {
            return $"UPDATE {typeof(TEntity).Name} SET IsDeleted=0 {conditionString}";
        }
        /// <summary>
        /// 统计实体数量
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <returns></returns>
        public virtual string CountSQLString<TEntity>()
        {
            return $"SELECT COUNT(*) AS COUNT FROM  {typeof(TEntity).Name}";
        }
        /// <summary>
        /// 统计符合条件的实体数量
        /// </summary>
        /// <typeparam name="TEntity">实体类型</typeparam>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public virtual string CountSQLString<TEntity>(string conditionString)
        {
            return $"SELECT COUNT(*) AS COUNT FROM  {typeof(TEntity).Name} {conditionString}";
        }
    }
}
