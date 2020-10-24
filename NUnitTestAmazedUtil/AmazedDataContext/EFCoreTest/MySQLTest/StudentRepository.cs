using AmazedDataContext.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestAmazedUtil.AmazedDataContext.DataTest.EFCoreTest.MySQLTest
{
    class StudentRepository : EFRepository<Student>
    {
        private DbContext context;
        public StudentRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        /// <summary>
        /// 修改学生成绩(仅为了模拟事务操作)
        /// </summary>
        /// <param name="student">添加的学生信息</param>
        /// <param name="score">添加的成绩</param>
        /// <param name="triggerException">模拟异常,是否触发异常</param>
        /// <returns></returns>
        public bool InsertStudentScore(Student student, Score score, bool triggerException)
        {
            try
            {
                UOW.Transaction();
                GetEntity().Add(student);
                context.SaveChanges();
                if (triggerException)
                {
                    throw new Exception();
                }
                context.Add(score);
                context.SaveChanges();
                UOW.Commit();
            }
            catch (Exception)
            {
                UOW.Rollback();
                return false;
            }
            return true;
        }
    }
}
