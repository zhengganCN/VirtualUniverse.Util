using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Util.Data.Repository.EFRepository;
using Util.Data.UOW.EFUOW;

namespace UtilTest.DataTest.MySQLTest
{
    class StudentRepository : Repository<Student>
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
            UnitOfWork uow = null;
            try
            {
                uow = new UnitOfWork(context);
                uow.Transaction();
                GetEntity(uow).Add(student);
                context.SaveChanges();
                if (triggerException)
                {
                    throw new Exception();
                }
                context.Add(score);
                context.SaveChanges();
                uow.Commit();
            }
            catch (Exception)
            {
                uow.Rollback();
                return false;
            }
            return true;
        }
    }
}
