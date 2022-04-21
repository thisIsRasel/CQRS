using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.StudentAggregate;

namespace Application.GetStudents
{
    public sealed class GetStudentsQueryHandler
        : IQueryHandler<GetStudentsQuery, GetStudentsResponse>
    {
        private readonly IStudentReadRepository _studentReadRepository;

        public GetStudentsQueryHandler(
            IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }

        public async Task<GetStudentsResponse> HandleAsync(
            GetStudentsQuery query)
        {
            var result = await _studentReadRepository
                .GetStudentsByAgeAsync(age: query.Age, pageNumber: query.Page);

            var totalCount = await _studentReadRepository
                .GetStudentsCountAsync(query.Age);

            return new GetStudentsResponse
            {
                Students = result.ToList(),
                TotalCount = totalCount
            };
        }
    }
}
