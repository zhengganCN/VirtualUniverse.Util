using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirtualUniverse.BackgroundService.Models;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/11 0:00:27；更新时间：
************************************************************************************/
namespace VirtualUniverse.BackgroundService.Test.Services
{
    /// <summary>
    /// 类说明：
    /// </summary>
    class HelloBackgroundService : BaseBackgroundService
    {
        protected override Task ExecuteAsync()
        {
            return Task.Run(() =>
            {
                Task.Delay(1000);
            });
        }

        protected override void OnConfiguration(BackgroundServiceBuilder backgroundServiceBuilder)
        {
            //backgroundServiceBuilder.SetExecutionTimes(2);
        }
    }
}
