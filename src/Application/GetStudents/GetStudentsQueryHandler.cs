using System.Linq;
using System.Threading.Tasks;
using Domain.Aggregates.StudentAggregate;
using Domain.Handlers;

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
                .GetByAgeAsync(age: query.Age, pageNumber: query.Page);

            var totalCount = await _studentReadRepository
                .GetTotalCountByAgeAsync(query.Age);

            return new GetStudentsResponse
            {
                Students = result.ToList(),
                TotalCount = totalCount
            };
        }
    }
}
