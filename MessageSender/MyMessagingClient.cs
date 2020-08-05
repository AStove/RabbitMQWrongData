using Common;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageSender
{
    public class MyMessagingClient : BaseApiMessagingClient
    {
        public MyMessagingClient(ConnectionWrapper connectionWrapper) : base(connectionWrapper)
        {
            this.channel.QueueDeclare("createDigit", true, false, true, null);
        }

        public void SendDigit(int digit)
        {
            this.channel.BasicPublish("API", "createDigit", null, Encoding.UTF8.GetBytes(digit.ToString()));
        }
    }
}
