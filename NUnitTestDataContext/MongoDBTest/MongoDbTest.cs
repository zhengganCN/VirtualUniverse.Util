using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedDataContext.MongoDBTest
{
    class MongoDbTest
    {
        [SetUp]
        public void SetUp() { }
        [Test]
        public void FindOneTest()
        {
            var context = new CommonMongoDbContext();
            var ss = context.TbStudent;
            var student= context.TbStudent.FindOne(Guid.NewGuid().ToString());
            Assert.IsNotNull(student);
        }
    }
}
