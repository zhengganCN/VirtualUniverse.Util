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
            optionsBuilder.UseMySql(StaticConfigurationValues.MySQLConnectionString);
            
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Score> Scores { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<VeiwStdentScore> VeiwStdentScore { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Score>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StudentName)
                    .HasColumnType("longtext")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");
            });

            modelBuilder.Entity<VeiwStdentScore>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VeiwStdentScore");

                entity.Property(e => e.ScoreId)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.StudentId)
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

        }
    }
}
