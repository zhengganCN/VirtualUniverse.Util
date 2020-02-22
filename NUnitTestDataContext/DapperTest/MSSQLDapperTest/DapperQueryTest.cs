
using AmazedDataContext.Dapper.Repository;
using Dapper;
using MySql.Data.MySqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAmazedDataContext.DataTest.DapperTest.MSSQLDapperTest
{
    public class DapperQueryTest
    {
        private MSSQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MSSQLRepository<TdD>(StaticConfigurationValues.MSSQLConnectionString);
        }

        [Test]
        public void FindOne()
        {
            var entity= repository.FindAll().First();
            entity= repository.FindOne($"Where {nameof(TdD.Id)}='{entity.Id}'");
            Assert.IsNotNull(entity);
        }
        [Test]
        public void FindAll()
        {
            var entities = repository.FindAll();
            Assert.IsNotNull(entities);
        }

        [Test]
        public void FindAllWithCondition()
        {
            var entity = repository.FindAll().First();
            var entities = repository.FindAll($"Where {nameof(TdD.Name)}='{entity.Name}'");
            Assert.IsNotNull(entities);
        }

        [Test]
        public void FindMany()
        {
            var entity = repository.FindAll().First();
            var entities = repository.FindMany($"Where {nameof(TdD.Name)}='{entity.Name}'");
            Assert.IsNotNull(entities);
        }

        [Test]
        public async Task FindOneAsync()
        {
            var entities =await repository.FindAllAsync();
            var entity = entities.First();
            entity =await repository.FindOneAsync($"Where {nameof(TdD.Id)}='{entity.Id}'");
            Assert.IsNotNull(entity);
        }
        [Test]
        public async Task FindAllAsync()
        {
            var entities = await repository.FindAllAsync();
            Assert.IsNotNull(entities);
        }

        [Test]
        public async Task FindAllWithConditionAsync()
        {
            var entities = await repository.FindAllAsync();
            var entity = entities.First();
            entities = await repository.FindAllAsync($"Where {nameof(TdD.Name)}='{entity.Name}'");
            Assert.IsNotNull(entities);
        }

        [Test]
        public async Task FindManyAsync()
        {
            var entities = await repository.FindAllAsync();
            var entity = entities.First();
            entities = await repository.FindManyAsync($"Where {nameof(TdD.Name)}='{entity.Name}'");
            Assert.IsNotNull(entities);
        }

    }
}
