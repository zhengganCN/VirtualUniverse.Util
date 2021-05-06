using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualUniverse.MQ.RabbitMQ.Interfaces;

/***********************************************************************************
****作者：zhenggan；创建时间：2021/4/16 15:14:43；更新时间：
************************************************************************************/
namespace VirtualUniverse.MQ.RabbitMQ
{
    /// <summary>
    /// 类 描 述：连接器创建
    /// </summary>
    class ConnectionCreate : IConnectionCreate
    {
        public void SetConnectionFactory(string hostName, string userName, string password, int port)
        {
            if (!(hostName == ConnectionFactory?.HostName &&
                userName == ConnectionFactory?.UserName &&
                password == ConnectionFactory?.Password &&
                port == ConnectionFactory?.Port))
            {
                ConnectionFactory = new ConnectionFactory
                {
                    UserName = userName,
                    Password = password,
                    HostName = hostName,
                    Port = port
                };
            }
        }
        /// <summary>
        /// 连接工厂
        /// </summary>
        public ConnectionFactory ConnectionFactory { get;private set; }

        public IConnection CreateConnection(IList<string> hostnames, string clientProvidedName, ref string connectionId)
        {
            if (IsExistConnection(connectionId, out IConnection connection) && IsConnectionOpen(connectionId, connection))
            {
                return connection;
            }
            connection = ConnectionFactory.CreateConnection(hostnames, clientProvidedName);
            AddConnectionToMqOptionBuilder(ref connectionId, connection);
            return connection;
        }

        public IConnection CreateConnection(IList<string> hostnames, ref string connectionId)
        {
            if (IsExistConnection(connectionId, out IConnection connection) && IsConnectionOpen(connectionId, connection))
            {
                return connection;
            }
            connection = ConnectionFactory.CreateConnection(hostnames);
            AddConnectionToMqOptionBuilder(ref connectionId, connection);
            return connection;
        }

        public IConnection CreateConnection(string clientProvidedName, ref string connectionId)
        {
            if (IsExistConnection(connectionId, out IConnection connection) && IsConnectionOpen(connectionId, connection))
            {
                return connection;
            }
            connection = ConnectionFactory.CreateConnection(clientProvidedName);
            AddConnectionToMqOptionBuilder(ref connectionId, connection);
            return connection;
        }

        public IConnection CreateConnection(ref string connectionId)
        {
            if (IsExistConnection(connectionId, out IConnection connection) && IsConnectionOpen(connectionId, connection))
            {
                return connection;
            }
            connection = ConnectionFactory.CreateConnection();
            AddConnectionToMqOptionBuilder(ref connectionId, connection);
            return connection;
        }
        private bool IsExistConnection(string connectionId,out IConnection connection)
        {
            connection = default;
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                return false;
            }
            else
            {
                return MQContextOptionsBuilder.Connections.TryGetValue(connectionId,out connection);
            }
        }

        private bool IsConnectionOpen(string connectionId, IConnection connection)
        {
            if (!connection.IsOpen)
            {
                MQContextOptionsBuilder.DeleteConnection(connectionId);
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 添加连接器和通道与连接器的关系到Mq的配置类中
        /// </summary>
        /// <param name="connectionId">连接器Id</param>
        /// <param name="connection">连接器</param>
        private static void AddConnectionToMqOptionBuilder(ref string connectionId, IConnection connection)
        {
            if (string.IsNullOrWhiteSpace(connectionId))
            {
                connectionId = Guid.NewGuid().ToString();
            }
            MQContextOptionsBuilder.AddConnection(connectionId, connection);
        }
    }
}
