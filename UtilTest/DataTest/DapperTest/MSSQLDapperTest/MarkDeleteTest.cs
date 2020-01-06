using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Dapper;
using Util.Data.Dapper.Repository;

namespace UtilTest.DataTest.DapperTest.MSSQLDapperTest
{
    class MarkDeleteTest
    {
        private MSSQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MSSQLRepository<TdD>(StaticConfigurationValues.MSSQLConnectionString);
        }
        [Test]
        public void MarkDeleteOne()
        {
            var record= repository.FindAll();
            var result= repository.MarkDeleteOne(record.First());
            Assert.IsTrue(1 == result);
        }
        [Test]
        public void MarkDeleteMany()
        {
            var record = repository.FindAll();
            var result = repository.MarkDeleteMany(record.Take(2));
            Assert.IsTrue(2 == result);
        }
        [Test]
        public void MarkDeleteManyWithCondition()
        {
            var record = repository.FindAll();
            var entity = record.First();
            var result = repository.MarkDeleteMany($"Where {nameof(entity.Id)}='{entity.Id}'");
            Assert.IsTrue(1 == result);
        }
        [Test]
        public async Task MarkDeleteOneAsync()
        {
            var record = await repository.FindAllAsync();
            var result = await repository.MarkDeleteOneAsync(record.First());
            Assert.IsTrue(1 == result);
        }
        [Test]
        public async Task MarkDeleteManyAsync()
        {
            var record = await repository.FindAllAsync();
            var result =await repository.MarkDeleteManyAsync(record.Take(2));
            Assert.IsTrue(2 == result);
        }
        [Test]
        public async Task MarkDeleteManyWithConditionAsync()
        {
            var record = await repository.FindAllAsync();
            var entity = record.First();
            var result = await repository.MarkDeleteManyAsync($"Where {nameof(entity.Id)}='{entity.Id}'");
            Assert.IsTrue(1 == result);
        }
    }
}
