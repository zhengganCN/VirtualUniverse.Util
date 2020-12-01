using AmazedDataValidation.ValidationModel;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test.AmazedDataValidation.VerifyAttribute
{
    class VerifyTest
    {
        [SetUp]
        public void SetUp()
        {

        }
        /// <summary>
        /// 验证模型测试
        /// </summary>
        [Test]
        public void Verify()
        {
            SchoolDto school = new SchoolDto
            {
                Classes = new List<ClassDto>
                {
                    new ClassDto
                    {
                        ClassName = "班级",
                        Students = new List<StudentDto>
                        {
                            new StudentDto { Scout = 110 }
                        }
                    }
                }
            };
            ValidationModelState state = new ValidationModelState(school);
            state.VerifyModel();
            JsonConvert.SerializeObject(state.ValidResult);
            Assert.IsTrue(true);
        }
        /// <summary>
        /// 验证模型性能测试
        /// </summary>
        [Test]
        public void VerifyPerformance()
        {
            SchoolDto school = new SchoolDto
            {
                Classes = new List<ClassDto>
                {
                    new ClassDto
                    {
                        ClassName = "班级",
                        Students = new List<StudentDto>
                        {
                            new StudentDto { Scout = 110 }
                        }
                    }
                }
            };
            for (int i = 0; i < 100000; i++)
            {
                ValidationModelState state = new ValidationModelState(school);
                state.VerifyModel();
            }
            Assert.IsTrue(true);
        }
    }
}
