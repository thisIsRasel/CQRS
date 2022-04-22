using System.Threading.Tasks;
using Domain.Handlers;

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
