using System.Threading.Tasks;

namespace Domain.Dispatchers
{
    public interface IQueryDispatcher
    {
        Task<TResponse> DispatchAsync<TQuery, TResponse>(TQuery query)
            where TQuery : notnull;
    }
}
