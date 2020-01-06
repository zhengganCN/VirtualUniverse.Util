using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Util.Data.MongoDB.Repository;
using Util.Data.UOW.MongoDBUOW;

namespace UtilTest.DataTest.MongoDBTest
{
    class StudentRepository : MongoRepository<Student>
    {
        public StudentRepository(string connectionString,string database) : base(connectionString, database)
        {
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
                GetMongoCollection<Student>().InsertOne(student);
                if (triggerException)
                {
                    throw new Exception();
                }
                GetMongoCollection<Score>().InsertOne(score);
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
