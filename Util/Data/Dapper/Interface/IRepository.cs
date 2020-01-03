using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Repository;

namespace Util.Data.Dapper.Interface
{
    interface IRepository<TEntity>
    {
        #region 查询实体
        
        #endregion
        #region 删除实体（从数据库上删除数据）
        
        #endregion
        #region 设置实体的删除标记为true（不删除数据库上的数据，只是标记数据的删除标识）
       
        #endregion
        #region 设置实体的删除标记为false（不删除数据库上的数据，只是标记数据的删除标识）
        
        #endregion
        #region 插入实体
       
        #endregion
        #region 更新实体
       
        #endregion
        #region 统计
        
        #endregion
    }
}
