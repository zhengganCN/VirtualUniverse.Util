using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Util.SocketUtil
{
    /// <summary>
    /// 服务端Socket
    /// </summary>
    public class SocketServer
    {
        /// <summary>
        /// 服务端发送接收数据
        /// </summary>
        /// <param name="iPEndPoint">本地终结点</param>
        /// <param name="backlog">挂起连接队列的最大长度</param>
        public void SocketSendReceive(IPEndPoint iPEndPoint, int backlog)
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            using Socket socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(iPEndPoint);
            socket.Listen(backlog);
            while (true)
            {
                using Socket handler = socket.Accept();
                while (true)
                {
                    List<byte> data = new List<byte>();
                    byte[] buffer = new byte[1024];
                    int byteSize = 0;
                    do
                    {
                        byteSize = handler.Receive(buffer, buffer.Length, SocketFlags.None);
                        data.AddRange(buffer);
                    }
                    while (byteSize == buffer.Length);
                    handler.Send(SetSendMessage(data.ToArray()));
                }
            }
        }
        /// <summary>
        /// 设置发送数据
        /// 需重写该函数，若不重写该函数，则默认发送一个空byte数组
        /// </summary>
        /// <param name="receiveData">接收到的数据</param>
        /// <returns></returns>
        public virtual byte[] SetSendMessage(byte[] receiveData)
        {
            return Array.Empty<byte>();
        }

    }
}
