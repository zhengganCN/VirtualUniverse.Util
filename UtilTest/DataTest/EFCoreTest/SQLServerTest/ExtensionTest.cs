//using LinqKit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Util.Data.EFCore.Repository;
using Util.Extension;

namespace UtilTest.DataTest.EFCoreTest.SQLServerTest
{
    class ExtensionTest
    {
        private EFRepository<Student> repository;
        private IList<Student> students;
        [SetUp]
        public void SetUp()
        {
            repository = new EFRepository<Student>(new SQLServerDbContext());
            students= repository.FindAll(o => o.IsDeleted == false, o => o.Id);
        }

        [Test]
        public void AndTest()
        {
            Expression<Func<Student, bool>> expression = t => true;
            expression= expression.And(x => x.Id != null);
            expression= expression.And(y => y.IsDeleted==false);
            var count= repository.Count(expression);
            Assert.AreEqual(count, students.Where(o => o.Id != null && o.IsDeleted == false).Count());
        }

        [Test]
        public void OrTest()
        {
            Expression<Func<Student, bool>> expression = t => true;
            expression.Or(x => x.Id != null);
            expression.Or(y => y.IsDeleted == false);
            var count = repository.Count(expression);
            Assert.AreEqual(count, students.Where(o => o.Id != null || o.IsDeleted == false).Count());
        }
    }
    

}
