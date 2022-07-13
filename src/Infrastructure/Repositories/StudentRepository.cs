using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.StudentAggregate;
using Domain.Aggregates.StudentAggregate.Specifications;

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

        public async Task<Student?> GetAsync(string itemId)
        {
            return await _repository.GetItemAsync(itemId);
        }

        public async Task<IEnumerable<Student>> GetByAgeAsync(
            int age, int pageNumber)
        {
            var specification = new StudentAgeSpecification(age);

            specification.ApplyPaging(pageNumber: pageNumber, pageSize: 2);

            return await _repository.GetItemsAsync(specification);
        }

        public async Task<int> GetTotalCountByAgeAsync(int age)
        {
            var specification = new StudentAgeSpecification(age);

            return await _repository.CountAsync(specification);
        }

        public async Task CreateStudentAsync(Student student)
        {
            await _repository.InsertAsync(student);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            await _repository.UpdateAsync(student);
        }

        public async Task RemoveStudentAsync(Student student)
        {
            await _repository.DeleteAsync(student);
        }
    }
}
