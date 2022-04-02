using Application.CreateStudent;
using Application.GetStudents;
using Domain;
using Domain.Aggregates.StudentAggregate;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public StudentController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllStudents([FromQuery] GetStudentsQuery query)
        {
            var result = await _queryDispatcher
                .DispatchAsync<GetStudentsQuery, IReadOnlyList<Student>>(query);

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
