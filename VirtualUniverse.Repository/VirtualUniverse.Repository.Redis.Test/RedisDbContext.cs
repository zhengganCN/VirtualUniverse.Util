/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/2 16:22:12；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.Redis.Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class RedisDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.SetConnect("192.168.3.182");
        }
    }
}
