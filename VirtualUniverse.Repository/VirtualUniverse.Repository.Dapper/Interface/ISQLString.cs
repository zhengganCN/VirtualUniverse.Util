//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace AmazedDataContext.Dapper.SQLString
//{
//    /// <summary>
//    /// SQL增、删、改、查语句帮助接口
//    /// </summary>
//    public interface ISQLString
//    {
//        /// <summary>
//        /// 插入SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="entity">实体</param>
//        /// <returns></returns>
//        public string InsertSQLString<TEntity>(TEntity entity);
//        /// <summary>
//        /// 更新SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="entity">实体</param>
//        /// <returns></returns>
//        public string UpdateSQLString<TEntity>(TEntity entity);
//        /// <summary>
//        /// 更新SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="updateFields">更新字段的匿名对象</param>
//        /// <returns></returns>
//        public string UpdateSQLString<TEntity>(object updateFields);
//        /// <summary>
//        /// 更新SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="updateFields">更新字段的匿名对象</param>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string UpdateSQLString<TEntity>(object updateFields, string conditionString);
//        /// <summary>
//        /// 删除SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <returns></returns>
//        public string DeleteSQLString<TEntity>();
//        /// <summary>
//        /// 删除SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string DeleteSQLString<TEntity>(string conditionString);
//        /// <summary>
//        /// 查询SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <returns></returns>
//        public string QuerySQLString<TEntity>();
//        /// <summary>
//        /// 查询SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string QuerySQLString<TEntity>(string conditionString);
//        /// <summary>
//        /// 查询SQL语句
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <param name="SequnceField">排序字段</param>
//        /// <param name="sequence">排序方式</param>
//        /// <param name="pageIndex">分页索引</param>
//        /// <param name="pageSize">分页大小</param>
//        /// <returns></returns>
//        public string QuerySQLString<TEntity>(string conditionString, string SequnceField, EnumSequence sequence, int pageIndex, int pageSize);
//        /// <summary>
//        /// 更新某条记录的删除标记为已删除
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <returns></returns>
//        public string MarkDeleteSQLString<TEntity>();
//        /// <summary>
//        /// 根据条件更新删除标记为已删除
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string MarkDeleteSQLString<TEntity>(string conditionString);
//        /// <summary>
//        /// 更新某条记录的删除标记为未删除
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <returns></returns>
//        public string MarkUnDeleteSQLString<TEntity>();
//        /// <summary>
//        /// 根据条件更新删除标记为未删除
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string MarkUnDeleteSQLString<TEntity>(string conditionString);
//        /// <summary>
//        /// 统计符合条件的实体数量
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <returns></returns>
//        public string CountSQLString<TEntity>();
//        /// <summary>
//        /// 统计符合条件的实体数量
//        /// </summary>
//        /// <typeparam name="TEntity">实体类型</typeparam>
//        /// <param name="conditionString">条件字符串</param>
//        /// <returns></returns>
//        public string CountSQLString<TEntity>(string conditionString);
//    }
//}
