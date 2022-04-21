using System.Collections.Generic;
using Domain.Aggregates.StudentAggregate;

namespace Application.GetStudents
{
    public class GetStudentsResponse
    {
        public IReadOnlyList<Student> Students { get; set; } = default!;

        public int TotalCount { get; set; }
    }
}
