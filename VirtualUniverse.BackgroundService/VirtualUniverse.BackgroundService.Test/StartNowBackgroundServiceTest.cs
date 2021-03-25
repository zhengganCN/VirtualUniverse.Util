using NUnit.Framework;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Test.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/1 0:06:04；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class StartNowBackgroundServiceTest
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        [Test]
        [Order(1)]
        public void StartNowTest()
        {
            semaphore.Wait();
            StartNowBackgroundService.FileDirectory = "startnow_tasks1";
            if (Directory.Exists(StartNowBackgroundService.FileDirectory))
            {
                Directory.Delete(StartNowBackgroundService.FileDirectory, true);
            }
            StartNowBackgroundService helloBackgroundService = new StartNowBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 10).Wait();
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(2)]
        public void DisposeTest()
        {
            semaphore.Wait();
            StartNowBackgroundService.FileDirectory = "startnow_tasks2";
            if (Directory.Exists(StartNowBackgroundService.FileDirectory))
            {
                Directory.Delete(StartNowBackgroundService.FileDirectory, true);
            }
            using StartNowBackgroundService helloBackgroundService = new StartNowBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 3).Wait();
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(3)]
        public void StartNowNotLimitExecutionTimesTest()
        {
            semaphore.Wait();
            StartNowBackgroundService.FileDirectory = "startnow_tasks3";
            StartNowBackgroundService.SetExecutionTimes = -1;
            if (Directory.Exists(StartNowBackgroundService.FileDirectory))
            {
                Directory.Delete(StartNowBackgroundService.FileDirectory, true);
            }
            StartNowBackgroundService helloBackgroundService = new StartNowBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 10).Wait();
            semaphore.Release();
            Assert.Pass();
        }

        [Test]
        [Order(4)]
        public void StartNowNotLimitExecutionTimesMinIntervalTest()
        {
            semaphore.Wait();
            StartNowBackgroundService.FileDirectory = "startnow_tasks4";
            StartNowBackgroundService.SetExecutionTimes = -1;
            StartNowBackgroundService.Interval = 100;
            if (Directory.Exists(StartNowBackgroundService.FileDirectory))
            {
                Directory.Delete(StartNowBackgroundService.FileDirectory, true);
            }
            StartNowBackgroundService helloBackgroundService = new StartNowBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
            Task.Delay(1000 * 10).Wait();
            semaphore.Release();
            Assert.Pass();
        }
    }
}
