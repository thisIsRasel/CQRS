using System;
using System.Threading.Tasks;
using Domain;
using Domain.Dispatchers;

namespace Infrastructure.Dispatchers
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<TResponse> DispatchAsync<TQuery, TResponse>(TQuery query)
            where TQuery : notnull
        {
            var type = typeof(IQueryHandler<TQuery, TResponse>);

            var service = _serviceProvider.GetService(type);
            if (service is null)
            {
                throw new InvalidOperationException(
                    $"No query handler found for query: {typeof(TQuery).Name}");
            }

            var handler = (IQueryHandler<TQuery, TResponse>)service;
            return handler.HandleAsync(query);
        }
    }
}
