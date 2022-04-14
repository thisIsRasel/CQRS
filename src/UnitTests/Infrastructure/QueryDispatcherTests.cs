using System;
using System.Threading.Tasks;
using Infrastructure;
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

        public class TestQuery
        {

        }
    }
}
