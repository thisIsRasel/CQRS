using System.Threading.Tasks;
using Application.CreateStudent;
using Application.GetStudentDetail;
using Application.GetStudents;
using Domain.Aggregates.StudentAggregate;
using Domain.Dispatchers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ILogger<StudentController> _logger;

        public StudentController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            ILogger<StudentController> logger)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudents([FromQuery] GetStudentsQuery query)
        {
            var result = await _queryDispatcher
                .DispatchAsync<GetStudentsQuery, GetStudentsResponse>(query);

            _logger.LogInformation("List of {Count} students found", result.TotalCount);

            return Ok(result);
        }

        [HttpGet("{ItemId}")]
        public async Task<ActionResult> GetStudentDetail([FromRoute] GetStudentDetailQuery query)
        {
            var result = await _queryDispatcher
                .DispatchAsync<GetStudentDetailQuery, Student>(query);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateStudent([FromBody] CreateStudentCommand command)
        {
            //var response = await _commandDispatcher
            //    .DispatchAsync<CreateStudentCommand, bool>(command);

            //return Ok(response);

            await _commandDispatcher.DispatchToQueueAsync(command);

            return Accepted();
        }
    }
}
