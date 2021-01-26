using NUnit.Framework;
using VirtualUniverse.Repository.SqlSugar;

namespace VirtualUniverse.Repository.SqlSugarTest
{
    public class DbSetTest
    {
        [Test]
        public void DbSetCanUseTest()
        {
            using var context = new TestDbContext();
            context.TbAnswer.GetList();
            Assert.Pass();
        }
    }
}