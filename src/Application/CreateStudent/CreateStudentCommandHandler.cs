using Domain;
using Domain.Aggregates.StudentAggregate;
using System;
using System.Threading.Tasks;

namespace Application.CreateStudent
{
    public class CreateStudentCommandHandler
        : ICommandHandler<CreateStudentCommand, bool>
    {
        private readonly IStudentWriteRepository _studentWriteRepository;

        public CreateStudentCommandHandler(
            IStudentWriteRepository studentWriteRepository)
        {
            _studentWriteRepository = studentWriteRepository;
        }

        public async Task<bool> HandleAsync(CreateStudentCommand command)
        {
            await _studentWriteRepository.CreateStudentAsync(new Student
            {
                ItemId = Guid.NewGuid().ToString(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Age = command.Age,
            });

            return true;
        }
    }
}
