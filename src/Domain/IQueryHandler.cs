using System.Threading.Tasks;

namespace Domain
{
    public interface IQueryHandler<TQuery, TResponse>
        where TQuery : notnull
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
