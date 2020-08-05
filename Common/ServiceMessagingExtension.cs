using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class ServiceMessagingExtension
    {
        public static void AddRabbitMQConnection(this IServiceCollection services, Action<RabbitMQOptions> options)
        {
            var optionFinal = new RabbitMQOptions();
            options(optionFinal);

            var factory = new ConnectionFactory() { HostName = optionFinal.HostName, UserName = optionFinal.UserName, Password = optionFinal.Password };
            factory.AutomaticRecoveryEnabled = true;

            var connectionWrapper = new ConnectionWrapper(factory);

            services.AddSingleton(connectionWrapper);
        }
    }

    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
