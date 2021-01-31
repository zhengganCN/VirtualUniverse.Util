using System;
using System.IO;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Models;
using VirtualUniverse.BackgroundService.Models.StartUpModes;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/11 0:00:27；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test.Services
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class DayBackgroundService : BaseBackgroundService
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
        public static string FileDirectory = "day_tasks";
        public static string StartTime { get; set; } = "00:00:00";
        public static string EndTime { get; set; } = "22:00:00";
        public static bool StartNow { get; set; } = false;
        public static int SetExecutionTimes { get; set; } = 5;

        protected override void OnConfiguration(BackgroundServiceBuilder backgroundServiceBuilder)
        {
            backgroundServiceBuilder.SetStartUpDateTime(EnumStartUpMode.Day, StartTime, EndTime)
                .SetStartNow(StartNow).SetExecutionTimes(SetExecutionTimes).SetInterval(1000);
        }
    }
}
