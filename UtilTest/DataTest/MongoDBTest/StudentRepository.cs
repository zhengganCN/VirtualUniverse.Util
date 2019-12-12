using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Util.Data.Repository.MongoDBRepository;
using Util.Data.UOW.MongoDBUOW;

namespace UtilTest.DataTest.MongoDBTest
{
    class StudentRepository : Repository<Student>
    {
        private readonly DbContext context;
        public StudentRepository(DbContext context) : base(context)
        {
            this.context = context;
        }

        public override IMongoCollection<Student> GetMongoCollection(UnitOfWork uow)
        {
            return base.GetMongoCollection(uow);
        }

        private IMongoCollection<Score> GetScoreCollection(UnitOfWork uow)
        {
            return uow.database.GetCollection<Score>(nameof(Score));
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
                GetMongoCollection(uow).InsertOne(student);
                if (triggerException)
                {
                    throw new Exception();
                }
                GetScoreCollection(uow).InsertOne(score);
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
