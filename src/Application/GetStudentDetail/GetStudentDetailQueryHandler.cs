﻿using System.Threading.Tasks;
using Domain;
using Domain.Aggregates.StudentAggregate;

namespace Application.GetStudentDetail
{
    public sealed class GetStudentDetailQueryHandler
        : IQueryHandler<GetStudentDetailQuery, Student>
    {
        private readonly IStudentReadRepository _studentReadRepository;
        public GetStudentDetailQueryHandler(
            IStudentReadRepository studentReadRepository)
        {
            _studentReadRepository = studentReadRepository;
        }

        public async Task<Student> HandleAsync(GetStudentDetailQuery query)
        {
            var student = await _studentReadRepository
                .GetAsync(query.ItemId);

            return student;
        }
    }
}
