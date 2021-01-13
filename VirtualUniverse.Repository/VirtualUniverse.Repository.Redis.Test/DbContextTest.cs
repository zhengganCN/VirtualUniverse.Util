using NUnit.Framework;

namespace VirtualUniverse.Repository.Redis.Test
{
    public class DbContextTest
    {
        /// <summary>
        ///  DbContext连接测试
        /// </summary>
        [Test]
        public void DbContextConnectTest()
        {
            using var context = new RedisDbContext();
            context.RedisRepository.StringGet("UploadAttendanceCount");
            Assert.Pass();
        }

        /// <summary>
        /// DbContext连接性能测试
        /// </summary>
        [Test]
        public void DbContextConnectPerformanceTestOne()
        {
            for (int i = 0; i < 1000; i++)
            {
                using var context = new RedisDbContext();
                context.RedisRepository.StringGet("UploadAttendanceCount");
            }
            Assert.Pass();
        }
        /// <summary>
        /// DbContext连接性能测试
        /// </summary>
        [Test]
        public void DbContextConnectPerformanceTestTwo()
        {
            using var context = new RedisDbContext();
            for (int i = 0; i < 1000; i++)
            {
                context.RedisRepository.StringGet("UploadAttendanceCount");
            }
            Assert.Pass();
        }
    }
}