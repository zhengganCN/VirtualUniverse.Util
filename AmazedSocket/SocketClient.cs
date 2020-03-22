using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;

namespace AmazedSocket
{
    /// <summary>
    /// 客户端Socket
    /// </summary>
    public class SocketClient
    {
        /// <summary>
        /// 客户端发送接收数据
        /// </summary>
        /// <param name="iPEndPoint"></param>
        /// <param name="message"></param>
        /// <param name="sendSize"></param>
        /// <param name="receive"></param>
        /// <param name="receiveSize"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "<挂起>")]
        public void SocketSendReceive(IPEndPoint iPEndPoint, byte[] message, out int sendSize,
            out byte[] receive, out int receiveSize)
        {
            using Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(iPEndPoint);
            sendSize = socket.Send(message);
            List<byte> dataBuffer = new List<byte>();
            byte[] buffer = new byte[1024];
            int bytes;
            do
            {
                bytes = socket.Receive(buffer, buffer.Length, SocketFlags.None);
                dataBuffer.AddRange(buffer);
            }
            while (bytes > 0);
            receive = dataBuffer.ToArray();
            receiveSize = dataBuffer.Count;
        }
    }
}
