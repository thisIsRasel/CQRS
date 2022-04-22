using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface ICommandHandler<TCommand, TResponse>
        : IHandler where TCommand : notnull
    {
        Task<TResponse> HandleAsync(TCommand command);
    }

    public interface IHandler
    { 
    }
}
