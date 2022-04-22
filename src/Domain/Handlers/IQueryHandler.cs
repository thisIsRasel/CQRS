using System.Threading.Tasks;

namespace Domain.Handlers
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : notnull
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
