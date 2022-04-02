using System.Threading.Tasks;

namespace Domain
{
    public interface ICommandDispatcher
    {
        Task<TResponse> DispatchAsync<TCommand, TResponse>(TCommand command);

        Task DispatchToQueueAsync<TCommand>(TCommand command);
    }
}
