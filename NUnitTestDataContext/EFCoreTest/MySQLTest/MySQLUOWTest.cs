using AmazedDataContext;
using AmazedDataContext.EFCore.Repository;
using AmazedExtension;
using AmazedMath.Math;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NUnitTestAmazedDataContext.DataTest.EFCoreTest.MySQLTest
{
    class MySQLUOWTest
    {
        EFRepository<Student> repository = null;
        [SetUp]
        public void Setup()
        {
            repository = new EFRepository<Student>(new MySQLDbContext());
        }
        #region Insert
        [Test]
        public void InsertTest()
        {
            var result = repository.Insert(
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                });
            Assert.AreEqual(1, result);
        }
        [Test]
        public async Task InsertAsyncTest()
        {
            var result = await repository.InsertAsync(new Student()
            {
                CreateTime = DateTime.Now,
                IsDeleted = false,
                StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
            });
            Assert.AreEqual(1, result);
        }
        [Test]
        public void InsertManyTest()
        {
            var students = new List<Student>()
            {
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }
            };
            var result = repository.Insert(students);
            Assert.AreEqual(students.Count, result);
        }
        [Test]
        public async Task InsertManyAsyncTest()
        {
            var students = new List<Student>()
            {
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }
            };
            var result = await repository.InsertAsync(students);
            Assert.AreEqual(students.Count, result);
        }
        #endregion
        #region MarkDelete
        [Test]
        public void MarkDeleteTest()
        {
            while (true)
            {
                var query = repository.Find(o => o.Id != null && o.IsDeleted == false, o => o.Id);
                var students = query.ToList();
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = repository.MarkDelete(students[0]);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(1, result);
                    Assert.AreEqual(true, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public async Task MarkDeleteAsyncTestAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == false, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = await repository.MarkDeleteAsync(students[0]);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(1, result);
                    Assert.AreEqual(true, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public void MarkDeleteManyTest()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == false, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = repository.MarkDelete(students);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(students.Count, result);
                    Assert.AreEqual(true, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public async Task MarkDeleteManyAsyncTestAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == false, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = await repository.MarkDeleteAsync(students);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(students.Count, result);
                    Assert.AreEqual(true, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        #endregion
        #region UnmarkDelete
        [Test]
        public void UnmarkDeleteTest()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == true, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = true,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = repository.UnmarkDelete(students[0]);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(1, result);
                    Assert.AreEqual(false, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public async Task UnmarkDeleteAsyncTestAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == true, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = true,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = await repository.UnmarkDeleteAsync(students[0]);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(1, result);
                    Assert.AreEqual(false, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public void UnmarkDeleteManyTest()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == true, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = true,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = repository.UnmarkDelete(students);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(students.Count, result);
                    Assert.AreEqual(false, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        [Test]
        public async Task UnmarkDeleteManyAsyncTestAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null && o.IsDeleted == true, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = true,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var result = await repository.UnmarkDeleteAsync(students);
                    var compareStudent = repository.Find(students[0].Id);
                    Assert.AreEqual(students.Count, result);
                    Assert.AreEqual(false, compareStudent.IsDeleted);
                    return;
                }
            }
        }
        #endregion
        #region Update
        [Test]
        public void Update()
        {
            var student = repository.Find(o => o.Id != null, o => o.Id).First();
            student.UpdateTime = DateTime.Now;
            var result = repository.Update(student);
            Assert.AreEqual(1, result);
        }

        [Test]
        public async Task UpdateAsync()
        {
            var student = repository.Find(o => o.Id != null, o => o.Id).First();
            student.UpdateTime = DateTime.Now;
            var result = await repository.UpdateAsync(student);
            Assert.AreEqual(1, result);
        }

        [Test]
        public void UpdateMany()
        {
            var students = repository.Find(o => o.Id != null, o => o.Id);
            foreach (var student in students)
            {
                student.UpdateTime = DateTime.Now;
            }
            var result = repository.Update(students);
            Assert.AreEqual(students.Count, result);
        }

        [Test]
        public async Task UpdateManyAsync()
        {
            var students = repository.Find(o => o.Id != null, o => o.Id);
            foreach (var student in students)
            {
                student.UpdateTime = DateTime.Now;
            }
            var result = await repository.UpdateAsync(students);
            Assert.AreEqual(students.Count, result);
        }
        #endregion
        #region Delete
        [Test]
        public void Delete()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    int result = repository.Delete(students.First());
                    Assert.AreEqual(1, result);
                    return;
                }
            }
        }
        [Test]
        public async Task DeleteAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    int result = await repository.DeleteAsync(students.First());
                    Assert.AreEqual(1, result);
                    return;
                }
            }
        }
        [Test]
        public void DeleteMany()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    int result = repository.Delete(students);
                    Assert.AreEqual(students.Count, result);
                    return;
                }
            }
        }
        [Test]
        public async Task DeleteManyAsync()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    int result = await repository.DeleteAsync(students);
                    Assert.AreEqual(students.Count, result);
                    return;
                }
            }
        }
        #endregion
        #region Find
        [Test]
        public void Find()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var student = repository.Find(students.First().Id);
                    Assert.IsNotNull(student);
                    return;
                }
            }
        }
        [Test]
        public void FindOrderByDesc()
        {
            while (true)
            {
                var students = repository.Find(o => o.Id != null, o => o.Id, EnumSequence.Descending);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var student = repository.Find(students.First().Id);
                    Assert.IsNotNull(student);
                    return;
                }
            }
        }
        [Test]
        public void FindAllOrderByDesc()
        {
            while (true)
            {
                var students = repository.FindAll(o => o.Id != null, o => o.Id, EnumSequence.Descending);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var student = repository.Find(students.First().Id);
                    Assert.IsNotNull(student);
                    return;
                }
            }
        }
        [Test]
        public async Task FindAsync()
        {
            while (true)
            {
                var students = await repository.FindAsync(o => o.Id != null, o => o.Id);
                if (students.Count <= 0)
                {
                    repository.Insert(new Student()
                    {
                        CreateTime = DateTime.Now,
                        IsDeleted = false,
                        StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                    });
                }
                else
                {
                    var student = await repository.FindAsync(students.First().Id);
                    Assert.IsNotNull(student);
                    return;
                }
            }
        }
        #endregion
        #region Count
        [Test]
        public void Count()
        {
            var students = repository.FindAll(o => o.Id != null, o => o.Id);
            repository.Delete(students);
            var newStudents = new List<Student>()
            {
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }
            };
            var insertResult = repository.Insert(newStudents);
            var result = repository.Count(o => o.Id != null);
            Assert.IsTrue(result == insertResult);
        }
        [Test]
        public async Task CountAsync()
        {
            var students = await repository.FindAllAsync(o => o.Id != null, o => o.Id);
            repository.Delete(students);
            var newStudents = new List<Student>()
            {
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                },
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }
            };
            var insertResult = repository.Insert(newStudents);
            var result = await repository.CountAsync(o => o.Id != null);
            Assert.IsTrue(result == insertResult);
        }
        #endregion
        [Test]
        public void EntityTest()
        {
            var student = new Student()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.Now,
                DeleteTime = DateTime.Now,
                IsDeleted = false,
                StudentName = "Hello World",
                UpdateTime = DateTime.Now
            };
            Assert.IsNotNull(student.Id);
            Assert.IsNotNull(student.CreateTime);
            Assert.IsNotNull(student.DeleteTime);
            Assert.IsNotNull(student.IsDeleted);
            Assert.IsNotNull(student.StudentName);
            Assert.IsNotNull(student.UpdateTime);
        }
        #region 多表事务操作
        /// <summary>
        /// 多表操作无异常
        /// </summary>
        [Test]
        public void MultiTableNoException()
        {
            var studentRepository = new StudentRepository(new MySQLDbContext());
            var result = studentRepository.InsertStudentScore(
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }, new Score()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    MathScore = new RandomNumber().GenerateRandom(0, 100)
                },
                false);
            Assert.IsTrue(result);
        }
        /// <summary>
        /// 多表操作有异常
        /// </summary>
        [Test]
        public void MultiTableException()
        {
            var studentRepository = new StudentRepository(new MySQLDbContext());
            var result = studentRepository.InsertStudentScore(
                new Student()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    StudentName = new RandomNumber().GenerateRandom(10000000, 99999999).ToString()
                }, new Score()
                {
                    CreateTime = DateTime.Now,
                    IsDeleted = false,
                    MathScore = new RandomNumber().GenerateRandom(0, 100)
                },
                true);
            Assert.IsFalse(result);
        }
        #endregion
        [Test]
        public void FF()
        {
            var s = "08d78cee-0469-aefd-1e4a-c12398ab86fc";
            Expression<Func<Student, bool>> expression = o => o.IsDeleted == false;
            expression = expression.And(o => o.Id == Guid.Parse(s));
            var ss = repository.Find(expression, o => o.Id);
        }
        [Test]
        public void GG()
        {
            var viewStudentScore = new EFRepository<VeiwStdentScore>(new MySQLDbContext());
            var s = "08d78cee-0469-aefd-1e4a-c12398ab86fc";
            Expression<Func<VeiwStdentScore, bool>> expression = o => o.StudentId == Guid.Parse(s);
            expression = expression.And(o => o.ScoreId == Guid.Parse("08d78cee-0809-e9fb-846e-103bdf580805"));
            var ss = viewStudentScore.Find(expression, o => o.StudentId);
        }
    }
    public class Student : Entity<Guid>
    {
        public string StudentName { get; set; }
    }
    public class Score : Entity<Guid>
    {
        public double MathScore { get; set; }
    }

    public class VeiwStdentScore
    {
        public Guid StudentId { get; set; }
        public Guid? ScoreId { get; set; }
    }
}
