using Common;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessageReceiver
{
    public class DigitCreationMessaging
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private readonly IConnection connection;
        private readonly IModel channel;

        public DigitCreationMessaging(ConnectionWrapper connectionWrapper)
        {
            this.connection = connectionWrapper.GetConnection();
            this.channel = connection.CreateModel();
            this.channel.ExchangeDeclare("API", ExchangeType.Direct);
            this.channel.QueueDeclare("createDigit", true, false, true, null);
            this.channel.QueueBind("createDigit", "API", "createDigit", null);

            var consumer = new EventingBasicConsumer(this.channel);
            consumer.Received += async (s, e) => await OnMessageReceive(s, e);

            channel.BasicConsume(queue: "createDigit", autoAck: true, consumer: consumer);
        }

        private async Task OnMessageReceive(object sender, BasicDeliverEventArgs e)
        {
            await semaphoreSlim.WaitAsync();

            try
            {
                int digit = int.Parse(Encoding.UTF8.GetString(e.Body.Span));
                Console.WriteLine($"Message Received {digit}");
                await Task.Delay(100);
            }
            finally
            {
                semaphoreSlim.Release();
            }

        }
    }
}
