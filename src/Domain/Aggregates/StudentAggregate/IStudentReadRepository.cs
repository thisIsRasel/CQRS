using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Aggregates.StudentAggregate
{
    public interface IStudentReadRepository
    {
        Task<IReadOnlyList<Student>> GetAllStudentsAsync();
    }
}
