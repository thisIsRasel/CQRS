using System;
using System.Text;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace HostService
{
    public class Program
    {
        private static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            var configuration = host.Services.GetService<IConfiguration>();
            var exchangeName = configuration.GetValue<string>("ExchangeName");

            var handlerResolverService = host.Services
                .GetService<IHandlerResolverService>();
            if (handlerResolverService is null)
            {
                throw new InvalidOperationException(
                    $"Handler resolver service not found!");
            }

            channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: string.Empty);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var data = JsonConvert.DeserializeObject<RabbitMQMessage>(message);

                var (handler, method, parameterType)
                    = handlerResolverService.GetHandler(data.MessageType);

                method.Invoke(handler, new[] { JsonConvert.DeserializeObject(data.Message, parameterType) });
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddServices();
                    services.AddTransient<IHandlerResolverService, HandlerResolverService>();
                });
    }
}
