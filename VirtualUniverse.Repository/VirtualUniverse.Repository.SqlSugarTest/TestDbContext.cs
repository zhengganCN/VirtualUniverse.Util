using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.Repository.SqlSugar;
using VirtualUniverse.Repository.SqlSugarTest.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/26 22:46:10；更新时间：
************************************************************************************/
namespace VirtualUniverse.Repository.SqlSugarTest
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class TestDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.AddConnectionString(DbType.SqlServer, @"Data Source=(localdb)\ProjectsV13;Initial Catalog=Knowledge;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }

        public DbSet<TbAnswer> TbAnswer { get; set; }
    }
}
