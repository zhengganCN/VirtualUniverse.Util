using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Test.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/30 16:45:36；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class YearBackgroundServiceTest
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        [Test]
        [Order(1)]
        public void YearTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks1";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            YearBackgroundService helloBackgroundService = new YearBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void StartTimeBigerEndTimeTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks2";
            YearBackgroundService.StartTime = "03-06;00:00:00";
            YearBackgroundService.EndTime = "02-03;00:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(3)]
        public void ErrorStartTimeTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks3";
            YearBackgroundService.StartTime = "03-01;121212";
            YearBackgroundService.EndTime = "06-03;00:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(4)]
        public void ErrorEndTimeTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks4";
            YearBackgroundService.StartTime = "03-01;12:12:12";
            YearBackgroundService.EndTime = "03-05;121212";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(5)]
        public void StartTimeIsEmptyTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks5";
            YearBackgroundService.StartTime = string.Empty;
            YearBackgroundService.EndTime = "03-03;00:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(6)]
        public void EndTimeIsEmptyTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks6";
            YearBackgroundService.StartTime = "03-04;12:12:12";
            YearBackgroundService.EndTime = string.Empty;
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(7)]
        public void ResetExecutionTimeTest()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks7";
            YearBackgroundService.SetExecutionTimes = 2;
            YearBackgroundService.StartTime = "01-01;00:00:00";
            YearBackgroundService.EndTime = "12-31;22:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            YearBackgroundService helloBackgroundService = new YearBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 20).Wait();
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(8)]
        public void StartTimeOrEndTimeSplitError()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks8";
            YearBackgroundService.StartTime = "01-01=00:00:00";
            YearBackgroundService.EndTime = "12-31=22:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(9)]
        public void StartTimeOrEndTimeDayNotNumber()
        {
            semaphore.Wait();
            YearBackgroundService.FileDirectory = "year_tasks9";
            YearBackgroundService.StartTime = "0101;00:00:00";
            YearBackgroundService.EndTime = "1231;22:00:00";
            if (Directory.Exists(YearBackgroundService.FileDirectory))
            {
                Directory.Delete(YearBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new YearBackgroundService();
            });
            semaphore.Release();
        }
    }
}
