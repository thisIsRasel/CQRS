using System.Threading.Tasks;

namespace Domain
{
    public interface IQueryHandler<TQuery, TResponse>
    {
        Task<TResponse> HandleAsync(TQuery query);
    }
}
