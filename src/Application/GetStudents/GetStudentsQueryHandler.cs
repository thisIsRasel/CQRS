using Domain;
using Domain.Aggregates.StudentAggregate;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.GetStudents
{
    public class GetStudentsQueryHandler
        : IQueryHandler<GetStudentsQuery, IReadOnlyList<Student>>
    {
        private readonly IStudentReadRepository _studentReadRepository;

        public GetStudentsQueryHandler(
            IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }

        public async Task<IReadOnlyList<Student>> HandleAsync(GetStudentsQuery query)
        {
            var result = await _studentReadRepository
                .GetAllStudentsAsync();

            return result;
        }
    }
}
