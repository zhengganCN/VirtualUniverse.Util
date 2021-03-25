using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Test.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/30 16:23:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class WeekBackgroundServiceTest
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        [Test]
        [Order(1)]
        public void WeekTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks1";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            WeekBackgroundService helloBackgroundService = new WeekBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            semaphore.Release();
            Assert.Pass();
        }
        [Test]
        [Order(2)]
        public void StartTimeBigerEndTimeTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks2";
            WeekBackgroundService.StartTime = "6;00:00:00";
            WeekBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(3)]
        public void ErrorStartTimeTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks3";
            WeekBackgroundService.StartTime = "1;121212";
            WeekBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(4)]
        public void ErrorEndTimeTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks4";
            WeekBackgroundService.StartTime = "1;12:12:12";
            WeekBackgroundService.EndTime = "5;121212";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(5)]
        public void StartTimeIsEmptyTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks5";
            WeekBackgroundService.StartTime = string.Empty;
            WeekBackgroundService.EndTime = "3;00:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(6)]
        public void EndTimeIsEmptyTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks6";
            WeekBackgroundService.StartTime = "4;12:12:12";
            WeekBackgroundService.EndTime = string.Empty;
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(7)]
        public void ResetExecutionTimeTest()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks7";
            WeekBackgroundService.SetExecutionTimes = 2;
            WeekBackgroundService.StartTime = "1;00:00:00";
            WeekBackgroundService.EndTime = "7;22:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            WeekBackgroundService helloBackgroundService = new WeekBackgroundService();
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
            WeekBackgroundService.FileDirectory = "week_tasks8";
            WeekBackgroundService.StartTime = "1-00:00:00";
            WeekBackgroundService.EndTime = "7-22:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(9)]
        public void StartTimeOrEndTimeDayNotNumber()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks9";
            WeekBackgroundService.StartTime = "微软;00:00:00";
            WeekBackgroundService.EndTime = "微软;22:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(10)]
        public void StartTimeOrEndTimeDayNotBetween1And7()
        {
            semaphore.Wait();
            WeekBackgroundService.FileDirectory = "week_tasks10";
            WeekBackgroundService.StartTime = "0;00:00:00";
            WeekBackgroundService.EndTime = "29;22:00:00";
            if (Directory.Exists(WeekBackgroundService.FileDirectory))
            {
                Directory.Delete(WeekBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new WeekBackgroundService();
            });
            semaphore.Release();
        }
    }
}
