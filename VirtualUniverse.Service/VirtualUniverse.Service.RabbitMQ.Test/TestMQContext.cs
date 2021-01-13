using VirtualUniverse.Service.RabbitMQ.Models;
using VirtualUniverse.Service.RabbitMQ.Services;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/1/5 8:31:59；更新时间：
************************************************************************************/
namespace VirtualUniverse.Service.RabbitMQ.Test
{
    /// <summary>
    /// 类 描 述：
    /// </summary>
    class TestMQContext : MQContext
    {
        public override void OnConfiguration(MQContextOptionsBuilder builder)
        {
            builder.SetConnection("localhost", "guest", "guest");
            
        }
    }
}
