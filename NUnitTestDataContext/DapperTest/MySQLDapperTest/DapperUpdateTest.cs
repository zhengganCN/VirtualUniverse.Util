using AmazedDataContext.Dapper.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAmazedDataContext.DataTest.DapperTest.MySQLDapperTest
{
    class DapperUpdateTest
    {
        private MySQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MySQLRepository<TdD>(StaticConfigurationValues.MySQLConnectionString);
        }
        [Test]
        public void UpdateOne()
        {
            var entity= repository.FindAll().First();
            var result = repository.UpdateOne(new TdD
            {
                Id = entity.Id,
                Name = "安达市",
                DeptId = Guid.NewGuid(),
                Salary = 11.37,
                UpdateTime=DateTime.Now,
                IsDeleted = false
            });
            Assert.AreEqual(result, 1);
        }
        [Test]
        public void UpdateMany()
        {
            var entities = repository.FindAll();
            var result = repository.UpdateMany(entities);
            Assert.AreEqual(result, entities.Count());
        }
        [Test]
        public void UpdateManySpecifyField()
        {
            var entities = repository.FindAll();
            var entity = new TdD();
            var result = repository.UpdateMany(entities, new { entity.CreateTime, entity.UpdateTime });
            Assert.AreEqual(result, entities.Count());
        }
        [Test]
        public void UpdateManySpecifyFieldWithCondition()
        {
            var entity = new TdD
            {
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var condition = $"Where {nameof(entity.IsDeleted)}=0";
            var record= repository.FindAll(condition).Count();
            var result = repository.UpdateMany(entity, new { entity.CreateTime, entity.UpdateTime },
                condition);
            Assert.AreEqual(result, record);
        }

        [Test]
        public async Task UpdateOneAsync()
        {
            var entity = await repository.FindAllAsync();
            var result = await repository.UpdateOneAsync(new TdD
            {
                Id = entity.First().Id,
                Name = "安达市",
                DeptId = Guid.NewGuid(),
                Salary = 11.37,
                UpdateTime = DateTime.Now,
                IsDeleted = false
            });
            Assert.AreEqual(result, 1);
        }
        [Test]
        public async Task UpdateManyAsync()
        {
            var entities = await repository.FindAllAsync();
            var result =await repository.UpdateManyAsync(entities);
            Assert.AreEqual(result, entities.Count());
        }
        [Test]
        public async Task UpdateManySpecifyFieldAsync()
        {
            var entities = await repository.FindAllAsync();
            var entity = new TdD();
            var result = await repository.UpdateManyAsync(entities, new { entity.CreateTime, entity.UpdateTime });
            Assert.AreEqual(result, entities.Count());
        }
        [Test]
        public async Task UpdateManySpecifyFieldWithConditionAsync()
        {
            var entity = new TdD
            {
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now
            };
            var condition = $"Where {nameof(entity.IsDeleted)}=0";
            var record = await repository.FindAllAsync(condition);
            var result = await repository.UpdateManyAsync(entity, new { entity.CreateTime, entity.UpdateTime },
                condition);
            Assert.AreEqual(result, record.Count());
        }
    }
}
