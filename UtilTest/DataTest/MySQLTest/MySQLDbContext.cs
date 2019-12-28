using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UtilTest.SQLServerTest.DataTest;

namespace UtilTest.DataTest.MySQLTest
{
    public class MySQLDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=cdb-m815p5nq.cd.tencentcdb.com;user id=root;Password=hyy5201314;persistsecurityinfo=True;port=10088;database=knowledge");
            
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
