using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace UtilTest.DataTest.EFCoreTest.SQLServerTest
{
    class SQLServerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StaticConfigurationValues.MSSQLConnectionString);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Score> Scores { get; set; }

    }
}
