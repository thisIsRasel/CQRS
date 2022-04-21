using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.StudentAggregate
{
    public interface IStudentReadRepository
    {
        Task<IReadOnlyList<Student>> GetStudentsAsync();

        Task<int> GetStudentsCountAsync(int age);

        Task<IEnumerable<Student>> GetStudentsByAgeAsync(int age, int pageNumber);

        Task<Student> GetStudentByIdAsync(string itemId);
    }
}
