using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace VirtualUniverse.Service.Consul.Services
{
    static class IpAddressOperation
    {
        /// <summary>
        /// 获取局域网IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetLanIPAddress()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Select(p => p.GetIPProperties())
                .SelectMany(p => p.UnicastAddresses)
                .FirstOrDefault(p =>
                    p.Address.AddressFamily == AddressFamily.InterNetwork &&
                    !IPAddress.IsLoopback(p.Address) && p.IsDnsEligible == true &&
                    p.PrefixOrigin == PrefixOrigin.Dhcp)?.Address.ToString();
        }
    }
}
