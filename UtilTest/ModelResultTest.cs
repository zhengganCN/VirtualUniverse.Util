using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Util.ModelResult;

namespace UtilTest
{
    class ModelResultTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestModelResultSuccess()
        {
            var data = new TestDate();
            ModelResult<TestDate> model = new ModelResult<TestDate>();
            model = model.SuccessResult(data, TestEnum.MonDay,
                  new Pagination { Count = 10, PageCount = 1, PageIndex = 1, PageSize = 10 });
            if (model == null)
            {
                Assert.IsTrue(false);
            }
        }

        class TestDate
        {
            public string Name { get; set; }
        }
        enum TestEnum 
        {
            [System.ComponentModel.Description("星期一")]
            MonDay=1,
            [System.ComponentModel.Description("星期二")]
            TrusDay =2
        }


    }
}
