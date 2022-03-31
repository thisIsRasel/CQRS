using System.Threading.Tasks;

namespace Domain
{
    public interface ICommandHandler<TCommand, TResponse>
    {
        Task<TResponse> HandleAsync(TCommand command);
    }
}
