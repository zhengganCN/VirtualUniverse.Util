using NUnit.Framework;
using VirtualUniverse.Extension.Newtonsoft.Json.Test.Models;

namespace VirtualUniverse.Extension.Newtonsoft.Json.Test
{
    public class SerializeObjectTest
    {
        /// <summary>
        /// 泛型序列化排除或保留字段
        /// </summary>
        [Test]
        public void SerializeObjectGenericTest()
        {
            SchoolDto schoolDto = new SchoolDto
            {
                History = "12",
                Name = "小学"
            };
            var json = JsonConvertExtension.SerializeObject(schoolDto, schoolDto => new
            {
                schoolDto.Name
            }, false);
            Assert.IsTrue(json == "{\"History\":\"12\"}");
        }
        /// <summary>
        /// 指定要排除或保留的属性
        /// </summary>
        [Test]
        public void PropertyNameSerializeObjectTest()
        {
            SchoolDto schoolDto = new SchoolDto
            {
                History = "12",
                Name = "小学"
            };
            var json = JsonConvertExtension.SerializeObject(schoolDto, new string[] { "Name" }, false);
            Assert.IsTrue(json == "{\"History\":\"12\"}");
        }
    }
}