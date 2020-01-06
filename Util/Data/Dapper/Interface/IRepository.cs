using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Util.Data.Dapper.Interface
{
    interface IRepository<TEntity>
    {
        #region 插入操作
        /// <summary>
        /// 插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int InsertOne(TEntity entity);
        /// <summary>
        /// 批量插入实体集
        /// </summary>
        /// <param name="entities">实体集</param>
        /// <returns></returns>
        public int InsertMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// 异步插入一条实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> InsertOneAsync(TEntity entity);
        /// <summary>
        /// 异步批量插入实体集
        /// </summary>
        /// <param name="entities">实体集</param>
        /// <returns></returns>
        public Task<int> InsertManyAsync(IEnumerable<TEntity> entities);
        #endregion
        #region 更新操作
        /// <summary>
        /// 更新所有字段，根据实体的Id更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int UpdateOne(TEntity entity);
        /// <summary>
        /// 批量更新所有字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int UpdateMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// 批量更新指定字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <returns></returns>
        public int UpdateMany(IEnumerable<TEntity> entities, object updateFields);
        /// <summary>
        /// 批量更新符合条件的实体的指定字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int UpdateMany(TEntity entity, object updateFields, string conditionString);
        /// <summary>
        /// 异步更新所有字段，根据实体的Id更新实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> UpdateOneAsync(TEntity entity);
        /// <summary>
        /// 异步批量更新所有字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public Task<int> UpdateManyAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 异步批量更新指定字段，根据每条实体的Id更新每条实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <returns></returns>
        public Task<int> UpdateManyAsync(IEnumerable<TEntity> entities, object updateFields);
        /// <summary>
        /// 异步批量更新符合条件的实体的指定字段
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="updateFields">更新字段的匿名对象</param>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<int> UpdateManyAsync(TEntity entity, object updateFields, string conditionString);
        #endregion
        #region 查询操作
        /// <summary>
        /// 根据条件查询返回一条符合的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public TEntity FindOne(string conditionString);
        /// <summary>
        /// 查询所有符合条件的实体，并根据分页条件返回限定数量的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <param name="sequenceField">排序字段</param>
        /// <param name="sequence">排序方式</param>
        /// <param name="pageInde">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindMany(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10);
        /// <summary>
        /// 查询所有实体
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll();
        /// <summary>
        /// 查询所有符合条件的实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public IEnumerable<TEntity> FindAll(string conditionString);
        /// <summary>
        /// 根据条件异步查询返回一条符合的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<TEntity> FindOneAsync(string conditionString);
        /// <summary>
        /// 异步查询所有符合条件的实体，并根据分页条件返回限定数量的数据
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <param name="sequenceField">排序字段</param>
        /// <param name="sequence">排序方式</param>
        /// <param name="pageInde">分页索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindManyAsync(string conditionString, string sequenceField = "Id", EnumSequence sequence = EnumSequence.Ascending, int pageInde = 1, int pageSize = 10);
        /// <summary>
        /// 异步查询所有实体
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindAllAsync();
        /// <summary>
        /// 异步查询所有符合条件的实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<IEnumerable<TEntity>> FindAllAsync(string conditionString);
        #endregion
        #region 删除操作
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int DeleteOne(TEntity entity);
        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int DeleteMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int DeleteMany(string conditionString);
        /// <summary>
        /// 异步删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> DeleteOneAsync(TEntity entity);
        /// <summary>
        /// 异步批量删除实体
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public Task<int> DeleteManyAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件异步删除实体
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<int> DeleteManyAsync(string conditionString);
        #endregion
        #region 标记为删除
        /// <summary>
        /// 更新删除标记为已删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkDeleteOne(TEntity entity);
        /// <summary>
        /// 批量更新删除标记为已删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int MarkDeleteMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件更新删除标记为已删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int MarkDeleteMany(string conditionString);
        /// <summary>
        /// 异步更新删除标记为已删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> MarkDeleteOneAsync(TEntity entity);
        /// <summary>
        /// 异步批量更新删除标记为已删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public Task<int> MarkDeleteManyAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件异步更新删除标记为已删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<int> MarkDeleteManyAsync(string conditionString);
        #endregion
        #region 标记为未删除
        /// <summary>
        /// 更新删除标记为未删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int MarkUnDeleteOne(TEntity entity);
        /// <summary>
        /// 批量更新删除标记为未删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public int MarkUnDeleteMany(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件更新删除标记为未删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public int MarkUnDeleteMany(string conditionString);
        /// <summary>
        /// 异步更新删除标记为未删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public Task<int> MarkUnDeleteOneAsync(TEntity entity);
        /// <summary>
        /// 异步批量更新删除标记为未删除
        /// </summary>
        /// <param name="entities">实体列表</param>
        /// <returns></returns>
        public Task<int> MarkUnDeleteManyAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 根据条件异步更新删除标记为未删除
        /// </summary>
        /// <param name="conditionString">条件字符串</param>
        /// <returns></returns>
        public Task<int> MarkUnDeleteManyAsync(string conditionString);
        #endregion
    }
}
