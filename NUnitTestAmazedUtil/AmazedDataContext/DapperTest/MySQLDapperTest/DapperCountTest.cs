using AmazedDataContext.Dapper.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAmazedUtil.AmazedDataContext.DataTest.DapperTest.MySQLDapperTest
{
    class DapperCountTest
    {
        private MySQLRepository<TdD> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new MySQLRepository<TdD>(StaticConfigurationValues.MySQLConnectionString);
        }

        [Test]
        public void Count()
        {
            var record = repository.Count();
            Assert.NotNull(record);
        }
        [Test]
        public async Task CountAsync()
        {
            var record = await repository.CountAsync();
            Assert.NotNull(record);
        }
    }
}
