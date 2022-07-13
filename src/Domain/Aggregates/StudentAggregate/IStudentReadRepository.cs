using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.StudentAggregate
{
    public interface IStudentReadRepository
    {
        Task<Student?> GetAsync(string itemId);

        Task<IEnumerable<Student>> GetByAgeAsync(
            int age, int pageNumber);

        Task<int> GetTotalCountByAgeAsync(int age);
    }
}
