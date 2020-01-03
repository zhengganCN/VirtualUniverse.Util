using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.Data.Dapper;

namespace UtilTest.DataTest.DapperTest
{
    class DapperDeleteTest
    {
        private Repository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new Repository<TdD>(StaticConfigurationValues.MySQLConnectionString);
        }

        [Test]
        public void DeleteOne()
        {
            var record= repository.FindAll();
            var result= repository.DeleteOne(record.First());
            Assert.AreEqual(1, result);
        }
        [Test]
        public void DeleteMany()
        {
            var record = repository.FindAll();
            var result = repository.DeleteMany(record.Take(2));
            Assert.AreEqual(2, result);
        }
        [Test]
        public void DeleteManyWithCondition()
        {
            var entity = new TdD();
            var condition = $"Where {nameof(entity.IsDeleted)}=0";
            var record = repository.FindAll(condition);
            var result = repository.DeleteMany(condition);
            Assert.AreEqual(record.Count(), result);
        }
        [Test]
        public async Task DeleteOneAsync()
        {
            var record = await repository.FindAllAsync();
            var result = await repository.DeleteOneAsync(record.First());
            Assert.AreEqual(1, result);
        }
        [Test]
        public async Task DeleteManyAsync()
        {
            var record = await repository.FindAllAsync();
            var result =await repository.DeleteManyAsync(record.Take(2));
            Assert.AreEqual(2, result);
        }
        [Test]
        public async Task DeleteManyWithConditionAsync()
        {
            var entity = new TdD();
            var condition = $"Where {nameof(entity.IsDeleted)}=0";
            var record = await repository.FindAllAsync(condition);
            var result = await repository.DeleteManyAsync(condition);
            Assert.AreEqual(record.Count(), result);
        }
    }
}
