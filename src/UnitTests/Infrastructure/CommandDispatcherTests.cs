using System;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Dispatchers;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace UnitTests.Infrastructure
{
    public class CommandDispatcherTests
    {
        [Fact(DisplayName = "Should throw exception if command handler not found")]
        public void ShouldThrowExceptionIfCommandHandlerNotFound()
        {
            var command = new TestCommand();

            var mocker = new AutoMocker();
            var commandDispatcher = mocker.CreateInstance<CommandDispatcher>();

            Task Func() => commandDispatcher
                .DispatchAsync<TestCommand, bool>(command);

            Assert.ThrowsAsync<InvalidOperationException>(Func);
        }

        [Fact(DisplayName = "Should dispatch if command handler found")]
        public async Task ShouldDispatchIfCommandHandlerFound()
        {
            var command = new TestCommand();
            var commandHandler = new TestCommandHandler();

            var mocker = new AutoMocker();
            var commandDispatcher = mocker.CreateInstance<CommandDispatcher>();

            var serviceProvider = mocker.GetMock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns(commandHandler);

            var response = await commandDispatcher
                .DispatchAsync<TestCommand, bool>(command);

            Assert.True(response);
        }

        public class TestCommand
        {
            public string Name { get; set; }
        }

        public class TestCommandHandler : ICommandHandler<TestCommand, bool>
        {
            public Task<bool> HandleAsync(TestCommand command)
            {
                return Task.FromResult(true);
            }
        }
    }
}
