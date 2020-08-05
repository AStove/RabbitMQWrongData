using Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageSender
{
    public abstract class BaseApiMessagingClient
    {
        protected IConnection connection;
        protected IModel channel;

        public BaseApiMessagingClient(ConnectionWrapper connectionWrapper)
        {
            this.connection = connectionWrapper.GetConnection();
            this.channel = connection.CreateModel();

            this.channel.ExchangeDeclare("API", ExchangeType.Direct);
        }
    }
}
