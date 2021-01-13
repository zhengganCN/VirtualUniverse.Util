using NUnit.Framework;
using System.Threading;
using VirtualUniverse.BackgroundService.Test.Services;

namespace VirtualUniverse.BackgroundService.Test
{
    public class Tests
    {

        [Test]
        public void Test1()
        {
            HelloBackgroundService helloBackgroundService = new HelloBackgroundService();
            helloBackgroundService.StartAsync(CancellationToken.None);
        }
    }
}