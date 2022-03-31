using System.Threading.Tasks;

namespace Domain
{
    public interface IQueryDispatcher
    {
        Task<TResponse> DispatchAsync<TQuery, TResponse>(TQuery query);
    }
}
