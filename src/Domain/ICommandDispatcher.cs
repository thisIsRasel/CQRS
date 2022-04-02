using System.Threading.Tasks;

namespace Domain
{
    public interface ICommandDispatcher
    {
        Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command)
            where TCommand : notnull;

        Task DispatchToQueueAsync<TCommand>(TCommand command)
            where TCommand : notnull;
    }
}
