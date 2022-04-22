using System;
using System.Threading.Tasks;
using Domain;
using Infrastructure.Dispatchers;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace UnitTests.Infrastructure
{
    public class QueryDispatcherTests
    {
        [Fact(DisplayName = "Should Throw Exception If Query Handler Not Found")]
        public void ShouldThrowExceptionIfQueryHandlerNotFound()
        {
            var query = new TestQuery();

            var mocker = new AutoMocker();
            var queryDispatcher = mocker.CreateInstance<QueryDispatcher>();

            Task Func() => queryDispatcher.DispatchAsync<TestQuery, bool>(query);

            Assert.ThrowsAsync<InvalidOperationException>(Func);
        }

        [Fact(DisplayName = "Should Get Response If Query Handler Found")]
        public async Task ShouldGetResponseIfQueryHandlerFound()
        {
            var query = new TestQuery();
            var queryHandler = new TestQueryHandler();

            var mocker = new AutoMocker();
            var queryDispatcher = mocker.CreateInstance<QueryDispatcher>();

            var serviceProvider = mocker.GetMock<IServiceProvider>();
            serviceProvider
                .Setup(x => x.GetService(It.IsAny<Type>()))
                .Returns(queryHandler);

            var response = await queryDispatcher
                .DispatchAsync<TestQuery, string>(query);

            Assert.NotNull(response);
        }

        public class TestQuery
        {
            public string ItemId { get; set; }
        }

        public class TestQueryHandler : IQueryHandler<TestQuery, string>
        {
            public Task<string> HandleAsync(TestQuery query)
            {
                return Task.FromResult("Rasel Ahammed");
            }
        }
    }
}
