using System;
using System.IO;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/2/1 0:04:15；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test.Services
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class StartNowBackgroundService : BaseBackgroundService
    {
        protected override Task ExecuteAsync()
        {
            if (!Directory.Exists(FileDirectory))
            {
                Directory.CreateDirectory(FileDirectory);
            }
            File.CreateText($"{FileDirectory}/{DateTime.Now:yyyyMMddHHmmss}={ExecutionTimes.ExecutionTimesInLoop}.txt");
            return Task.CompletedTask;
        }
        public static string FileDirectory = "startnow_tasks";
        public static string StartTime { get; set; } = "00:00:00";
        public static string EndTime { get; set; } = "22:00:00";
        public static bool StartNow { get; set; } = true;
        public static int SetExecutionTimes { get; set; } = 5;
        public static int Interval { get; set; } = 1000;

        protected override void OnConfiguration(BackgroundServiceBuilder backgroundServiceBuilder)
        {
            backgroundServiceBuilder.SetStartNow(StartNow).SetExecutionTimes(SetExecutionTimes).SetInterval(Interval);
        }
    }
}
