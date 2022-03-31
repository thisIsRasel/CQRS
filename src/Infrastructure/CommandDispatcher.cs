using Domain;
using System;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command)
        {
            var type = typeof(ICommandHandler<TCommand, TResponse>);
            var service = (ICommandHandler<TCommand, TResponse>) _serviceProvider.GetService(type);

            if (service is null)
            {
                throw new InvalidOperationException(
                    $"No command handler found for command: {typeof(TCommand).Name}");
            }

            return service.HandleAsync(command);
        }
    }
}
