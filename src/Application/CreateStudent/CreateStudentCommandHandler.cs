using Domain;
using Domain.Aggregates.StudentAggregate;
using System;
using System.Threading.Tasks;

namespace Application.CreateStudent
{
    public class CreateStudentCommandHandler
        : ICommandHandler<CreateStudentCommand, bool>
    {
        private readonly IStudentReadRepository _studentReadRepository;
        private readonly IStudentWriteRepository _studentWriteRepository;

        public CreateStudentCommandHandler(
            IStudentReadRepository studentReadRepository,
            IStudentWriteRepository studentWriteRepository)
        {
            _studentReadRepository = studentReadRepository;
            _studentWriteRepository = studentWriteRepository;
        }

        public async Task<bool> HandleAsync(CreateStudentCommand command)
        {
            var student = await _studentReadRepository
                .GetStudentByIdAsync(command.ItemId);

            if (student is null)
            {
                student = new Student();
                await CreateStudentAsync(student, command);
                return true;
            }

            await UpdateStudentAsync(student, command);
            return true;
        }

        private async Task CreateStudentAsync(
            Student student, CreateStudentCommand command)
        {
            PrepareStudent(student, command);
            await _studentWriteRepository.CreateStudentAsync(student);
        }

        private async Task UpdateStudentAsync(
            Student student, CreateStudentCommand command)
        {
            PrepareStudent(student, command);
            await _studentWriteRepository.UpdateStudentAsync(student);
        }

        private static void PrepareStudent(
            Student student,
            CreateStudentCommand command)
        {
            student.ItemId = command.ItemId ?? Guid.NewGuid().ToString();
            student.FirstName = command.FirstName;
            student.LastName = command.LastName;
            student.Age = command.Age;
        }
    }
}
