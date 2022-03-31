using Domain;
using Domain.Aggregates.StudentAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class StudentRepository 
        : IStudentReadRepository, IStudentWriteRepository
    {
        private readonly IRepository<Student> _repository;

        public StudentRepository(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<Student>> GetAllStudentsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _repository.InsertAsync(student);
        }
    }
}
