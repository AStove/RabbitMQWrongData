using Common;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Threading;

namespace MessageSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddRabbitMQConnection(o =>
            {
                o.HostName = "localhost";
                o.UserName = "test";
                o.Password = "test";
            });
            services.AddScoped<MyMessagingClient>();

            var client = services.BuildServiceProvider().GetRequiredService<MyMessagingClient>();
            var i = 0;
            while (true)
            {
                Console.WriteLine($"Sending {i}");
                client.SendDigit(i++);
                Thread.Sleep(100);
            }
        }
    }
}
