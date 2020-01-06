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
    class MarkUnDeleteTest
    {
        private MSSQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MSSQLRepository<TdD>(StaticConfigurationValues.MSSQLConnectionString);
        }
        [Test]
        public void MarkUnDeleteOne()
        {
            var record = repository.FindAll();
            var result = repository.MarkUnDeleteOne(record.First());
            Assert.IsTrue(1 == result);
        }
        [Test]
        public void MarkUnDeleteMany()
        {
            var record = repository.FindAll();
            var result = repository.MarkUnDeleteMany(record.Take(2));
            Assert.IsTrue(2 == result);
        }
        [Test]
        public void MarkUnDeleteManyWithCondition()
        {
            var record = repository.FindAll();
            var entity = record.First();
            var result = repository.MarkUnDeleteMany($"Where {nameof(entity.Id)}='{entity.Id}'");
            Assert.IsTrue(1 == result);
        }
        [Test]
        public async Task MarkUnDeleteOneAsync()
        {
            var record = await repository.FindAllAsync();
            var result = await repository.MarkUnDeleteOneAsync(record.First());
            Assert.IsTrue(1 == result);
        }
        [Test]
        public async Task MarkUnDeleteManyAsync()
        {
            var record = await repository.FindAllAsync();
            var result = await repository.MarkUnDeleteManyAsync(record.Take(2));
            Assert.IsTrue(2 == result);
        }
        [Test]
        public async Task MarkUnDeleteManyWithConditionAsync()
        {
            var record = await repository.FindAllAsync();
            var entity = record.First();
            var result = await repository.MarkUnDeleteManyAsync($"Where {nameof(entity.Id)}='{entity.Id}'");
            Assert.IsTrue(1 == result);
        }
    }
}
