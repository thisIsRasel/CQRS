using System.Threading.Tasks;
using Application.CreateStudent;
using Domain.Aggregates.StudentAggregate;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace UnitTests.Application
{
    public class CreateStudentCommandHandlerTests
    {
        [Fact(DisplayName = "Should Create Student")]
        public async Task ShouldCreateStudent()
        {
            var command = new CreateStudentCommand
            {
                FirstName = "Rasel",
                LastName = "Ahammed",
            };

            var mocker = new AutoMocker();
            var handler = mocker.CreateInstance<CreateStudentCommandHandler>();

            var studentWriteRepository = mocker.GetMock<IStudentWriteRepository>();
            studentWriteRepository
                .Setup(r => r.CreateStudentAsync(It.IsAny<Student>()))
                .Returns(Task.CompletedTask);

            var response = await handler.HandleAsync(command);
            Assert.True(response);
        }
    }
}
