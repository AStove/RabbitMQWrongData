using Common;
using MessageReceiver;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;

namespace RabbitMQWrongData
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
            services.AddScoped<DigitCreationMessaging>();

            // Start the service
            services.BuildServiceProvider().GetRequiredService<DigitCreationMessaging>();

            while(true)
            {
                Thread.Sleep(200);
            }
        }
    }
}
