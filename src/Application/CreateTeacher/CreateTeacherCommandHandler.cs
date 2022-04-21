using Domain;
using System.Threading.Tasks;

namespace Application.CreateTeacher
{
    public sealed class CreateTeacherCommandHandler
        : ICommandHandler<CreateTeacherCommand, string>
    {
        public Task<string> HandleAsync(CreateTeacherCommand command)
        {
            return Task.FromResult("From Create Teacher Command Handler");
        }
    }
}
