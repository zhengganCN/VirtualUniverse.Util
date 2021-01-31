using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Test.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/30 15:53:38；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class MonthBackgroundServiceTest
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        [Test]
        [Order(1)]
        public void MonthTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks1";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            MonthBackgroundService helloBackgroundService = new MonthBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void StartTimeBigerEndTimeTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks2";
            MonthBackgroundService.StartTime = "6;00:00:00";
            MonthBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(3)]
        public void ErrorStartTimeTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks3";
            MonthBackgroundService.StartTime = "1;121212";
            MonthBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(4)]
        public void ErrorEndTimeTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks4";
            MonthBackgroundService.StartTime = "1;12:12:12";
            MonthBackgroundService.EndTime = "5;121212";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(5)]
        public void StartTimeIsEmptyTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks5";
            MonthBackgroundService.StartTime = string.Empty;
            MonthBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(6)]
        public void EndTimeIsEmptyTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks6";
            MonthBackgroundService.StartTime = "4;12:12:12";
            MonthBackgroundService.EndTime = string.Empty;
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(7)]
        public void ResetExecutionTimeTest()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks7";
            MonthBackgroundService.SetExecutionTimes = 2;
            MonthBackgroundService.StartTime = "1;00:00:00";
            MonthBackgroundService.EndTime = "25;22:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            MonthBackgroundService helloBackgroundService = new MonthBackgroundService();
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
            MonthBackgroundService.FileDirectory = "month_tasks8";
            MonthBackgroundService.StartTime = "1-00:00:00";
            MonthBackgroundService.EndTime = "25-22:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(9)]
        public void StartTimeOrEndTimeDayNotNumber()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks9";
            MonthBackgroundService.StartTime = "微软;00:00:00";
            MonthBackgroundService.EndTime = "微软;22:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(10)]
        public void StartTimeOrEndTimeDayNotBetween1And28()
        {
            semaphore.Wait();
            MonthBackgroundService.FileDirectory = "month_tasks10";
            MonthBackgroundService.StartTime = "0;00:00:00";
            MonthBackgroundService.EndTime = "29;22:00:00";
            if (Directory.Exists(MonthBackgroundService.FileDirectory))
            {
                Directory.Delete(MonthBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new MonthBackgroundService();
            });
            semaphore.Release();
        }

    }
}
