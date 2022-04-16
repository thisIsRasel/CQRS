using Domain;
using Domain.Aggregates.StudentAggregate;
using Domain.Aggregates.StudentAggregate.Specifications;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IReadOnlyList<Student>> GetStudentsAsync()
        {
            var result = await _repository.GetItemsAsync();
            return result.ToList();
        }

        public async Task<Student> GetStudentByIdAsync(string itemId)
        {
            return await _repository.GetItemAsync(itemId);
        }

        public async Task<IEnumerable<Student>> GetStudentsByAgeAsync(
            int age)
        {
            var specification = new StudentAgeSpecification(
                age: age);

            return await _repository.GetItemsAsync(specification);
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _repository.InsertAsync(student);
        }
    }
}
