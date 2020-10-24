using AmazedDataContext;
using AmazedDataContext.Dapper.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAmazedUtil.AmazedDataContext.DataTest.DapperTest.MSSQLDapperTest
{
    class DapperInsertTest
    {
        private MSSQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MSSQLRepository<TdD>(StaticConfigurationValues.MSSQLConnectionString);
        }
        
        [Test]
        public void InsertOne()
        {
            var result = repository.InsertOne(new TdD
            {
                Id = Guid.NewGuid(),
                Name = "安达市发生",
                DeptId = Guid.NewGuid(),
                Salary = 11.3,
                IsDeleted = true
            });
            Assert.AreEqual(result, 1);
        }
        [Test]
        public async Task InsertOneAsync()
        {
            var result =await repository.InsertOneAsync(new TdD
            {
                Id = Guid.NewGuid(),
                Name = "安达市发生",
                DeptId = Guid.NewGuid(),
                Salary = 11.3,
                IsDeleted = true
            });
            Assert.AreEqual(result, 1);
        }
        [Test]
        public void InsertMany()
        {
            var result = repository.InsertMany(
                new List<TdD>()
                {
                    new TdD
                    {
                        Id = Guid.NewGuid(),
                        Name = "安达市发生",
                        DeptId = Guid.NewGuid(),
                        Salary = 11.3
                    },
                    new TdD
                    {
                        Id = Guid.NewGuid(),
                        Name = "安达市发生",
                        DeptId = Guid.NewGuid(),
                        Salary = 11.3
                    }
                });
            Assert.AreEqual(result, 2);
        }
        [Test]
        public async Task InsertManyAsync()
        {
            var result =await repository.InsertManyAsync(
                new List<TdD>()
                {
                    new TdD
                    {
                        Id = Guid.NewGuid(),
                        Name = "安达市发生",
                        DeptId = Guid.NewGuid(),
                        Salary = 11.3
                    },
                    new TdD
                    {
                        Id = Guid.NewGuid(),
                        Name = "安达市发生",
                        DeptId = Guid.NewGuid(),
                        Salary = 11.3
                    }
                });
            Assert.AreEqual(result, 2);
        }
    }
    class TdD : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid? DeptId { get; set; }
        public double Salary { get; set; }
    }
}
