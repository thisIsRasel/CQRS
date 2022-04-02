using System.Threading.Tasks;

namespace Domain
{
    public interface ICommandHandler<TCommand, TResponse>
        : IHandler
    {
        Task<TResponse> HandleAsync(TCommand command);
    }

    public interface IHandler
    { 
    }
}
