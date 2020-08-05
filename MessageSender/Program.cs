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
            services.AddScoped<DigitCreation>();

            while(true)
            {
                var provider = services.BuildServiceProvider().GetRequiredService<IServiceProvider>();
                using var scope = provider.CreateScope();
                var client = scope.ServiceProvider.GetRequiredService< DigitCreation>();
                client.Start();
                Thread.Sleep(100);
            }
        }
    }
}
