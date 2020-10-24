using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedDataContext.DataTest.EFCoreTest.SQLServerTest
{
    class SQLServerDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StaticConfigurationValues.MSSQLConnectionString);
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Score> Score { get; set; }

    }
}
