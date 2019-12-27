//using LinqKit;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Util.Data.Repository;
using Util.Data.Repository.EFRepository;
using UtilTest.SQLServerTest.DataTest;

namespace UtilTest.DataTest.SQLServerTest
{
    class ExtensionTest
    {
        private Repository<Student> repository;
        [SetUp]
        public void SetUp()
        {
            repository = new Repository<Student>(new SQLServerDbContext());
        }
        [Test]
        public void SS()
        {
            Func<int, bool> temp1 = o => o == 1;
            Func<int, bool> temp2 = o => o == 2;
            
            int d = 1;
            
            Expression<Func<int, bool>> expression1 = o => o - 1 == 1;
            Expression<Func<int, bool>> expression2 = o => o - 0 == 2;
            var s= Expression.Invoke(expression2, expression1.Parameters[0]);
            expression1.Compile().Invoke(2);
            
            Expression andAlsoExpr = Expression.AndAlso(
                expression1.Body,
                s
            );
            var ss = Expression.Assign(expression1, Expression.Constant(1));
            var c = Expression.Lambda<Func<int, bool>>(andAlsoExpr,
                expression1.Parameters[0]);//.Compile();//.Invoke();
            //var e= c.Invoke(2);
            var s1= c.Compile();

        }
        [Test]
        public void FF()
        {
            
            Expression<Func<Student, bool>> expression1 = o => o.Id != null;
            Expression<Func<Student, bool>> expression2 = o => o.IsDeleted == false;
            //var temp= expression1.And(expression2);
            if (true)
            {
                //var invoke = Expression.Invoke(expression2, expression1.Parameters[0]);
                var s = Expression.AndAlso(expression1.Body, expression2.Body);
                var s1 = Expression.Lambda<Func<Student, Student, bool>>(s);

                //repository.FindAll(s1,o=>o.Id).ToList();
            }
            
            //PredicateBuilder.True<BaoGaiTouBit>();
            var count = repository.Count(expression1);
        }

        public Expression<Func<T, bool>> And<T>(Expression<Func<T,bool>> baseExpression,
            Expression<Func<T,bool>> expression)
        {
            var s= Expression.AndAlso(baseExpression, expression);
            //Expression.Lambda<Func<Student, bool>>(s,);
            return null;
        }

        [Test]
        public void GG()
        {
            Expression<Func<Student, bool>> expression = t => true;
            expression = expression.And(x => x.Id != null);
            expression = expression.And(y => y.IsDeleted==false);
            var count= repository.Count(expression);
        }
    }
    

}
