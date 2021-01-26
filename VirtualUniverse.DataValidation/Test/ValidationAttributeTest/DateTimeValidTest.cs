using NUnit.Framework;
using VirtualUniverse.DataValidation.ValidationAttributes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/13 11:46:46；更新时间：
************************************************************************************/
namespace Test.ValidationAttributeTest
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class DateTimeValidTest
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void DateTimeValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.DateTime
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("2020-12-12 12:12:12"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void DateValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.Date
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("2020-12-12"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TimeValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.Time
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("12:12:12"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void DateTimeNoSeparatorValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.DateTimeNoSeparator
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("20201212121212"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void DateNoSeparatorValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.DateNoSeparator
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("20201212"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void TimeNoSeparatorValid(bool option)
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute
            {
                TimeFormat = DateTimeAttribute.EnumTimeFormat.TimeNoSeparator
            };
            if (option)
            {
                Assert.IsTrue(dateTimeAttribute.IsValid("121212"));
            }
            else
            {
                Assert.IsFalse(dateTimeAttribute.IsValid("2020-12-1212:12:12"));
            }
        }

        [Test]
        public void EmptyValid()
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute { TimeFormat = DateTimeAttribute.EnumTimeFormat.DateTimeNoSeparator };
            Assert.IsTrue(dateTimeAttribute.IsValid(""));
        }

        [Test]
        public void NullValid()
        {
            DateTimeAttribute dateTimeAttribute = new DateTimeAttribute();
            Assert.IsTrue(dateTimeAttribute.IsValid(null));
        }
    }
}
