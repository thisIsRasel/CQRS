using System;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Dispatchers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace Infrastructure.Dispatchers
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command)
            where TCommand : notnull
        {
            var type = typeof(ICommandHandler<TCommand, TResponse>);

            var service = _serviceProvider.GetService(type);
            if (service is null)
            {
                throw new InvalidOperationException(
                    $"No command handler found for command: {typeof(TCommand).Name}");
            }

            var handler = (ICommandHandler<TCommand, TResponse>)service;
            return handler.HandleAsync(command);
        }

        public Task DispatchToQueueAsync<TCommand>(TCommand command)
            where TCommand : notnull
        {
            var exchangeName = _configuration["ExchangeName"];
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout);

            var message = GetMessage(command);
            var body = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(message));

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: body);

            return Task.CompletedTask;
        }

        private static RabbitMQMessage GetMessage<TCommand>(TCommand command)
            where TCommand : notnull
        {
            var message = new RabbitMQMessage
            {
                MessageId = Guid.NewGuid().ToString(),
                MessageType = command.GetType().ToString(),
                Message = JsonConvert.SerializeObject(command),
            };

            return message;
        }
    }
}
