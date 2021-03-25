using NUnit.Framework;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Test.Services;

namespace VirtualUniverse.BackgroundService.Test
{
    public class DayBackgroundServiceTest
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        [Test]
        [Order(1)]
        public void DayTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks1";
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            DayBackgroundService helloBackgroundService = new DayBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void EndTimeBigerStartTimeTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks2";
            DayBackgroundService.StartTime = "12:12:12";
            DayBackgroundService.EndTime = "00:00:00";
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new DayBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(3)]
        public void ErrorStartTimeTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks3";
            DayBackgroundService.EndTime = "00:00:00";
            DayBackgroundService.StartTime = "121212";
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new DayBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(4)]
        public void ErrorEndTimeTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks4";
            DayBackgroundService.StartTime = "12:12:12";
            DayBackgroundService.EndTime = "121212";
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            Assert.Throws<ArgumentException>(() =>
            {
                new DayBackgroundService();
            });
            semaphore.Release();
        }

        [Test]
        [Order(5)]
        public void StartTimeIsEmptyTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks5";
            DayBackgroundService.EndTime = "00:00:00";
            DayBackgroundService.StartTime = string.Empty;
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            Assert.DoesNotThrow(() =>
            {
                DayBackgroundService helloBackgroundService = new DayBackgroundService();
                helloBackgroundService.StartAsync(CancellationToken.None);
            });
            semaphore.Release();
        }

        [Test]
        [Order(6)]
        public void EndTimeIsEmptyTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks6";
            DayBackgroundService.StartTime = "12:12:12";
            DayBackgroundService.EndTime = string.Empty;
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            Assert.DoesNotThrow(() =>
            {
                DayBackgroundService helloBackgroundService = new DayBackgroundService();
                helloBackgroundService.StartAsync(CancellationToken.None);
            });
            semaphore.Release();
        }

        [Test]
        [Order(7)]
        public void ResetExecutionTimeTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks7";
            DayBackgroundService.SetExecutionTimes = 2;
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            DayBackgroundService helloBackgroundService = new DayBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 20).Wait();
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(8)]
        public void NoLimitExecutionTimesTest()
        {
            semaphore.Wait();
            DayBackgroundService.FileDirectory = "day_tasks8";
            DayBackgroundService.SetExecutionTimes = -1;
            if (Directory.Exists(DayBackgroundService.FileDirectory))
            {
                Directory.Delete(DayBackgroundService.FileDirectory, true);
            }
            DayBackgroundService helloBackgroundService = new DayBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 20).Wait();
            semaphore.Release();
            Assert.Pass();
        }
    }
}