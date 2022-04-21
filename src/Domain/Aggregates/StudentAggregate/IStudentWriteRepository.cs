using System.Threading.Tasks;

namespace Domain.Aggregates.StudentAggregate
{
    public interface IStudentWriteRepository
    {
        Task CreateStudentAsync(Student student);

        Task UpdateStudentAsync(Student student);

        Task RemoveStudentAsync(Student student);
    }
}
