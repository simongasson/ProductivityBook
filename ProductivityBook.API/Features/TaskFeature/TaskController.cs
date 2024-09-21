using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskFeature.Commands;
using ProductivityBook.API.Features.TaskFeature.Models;
using ProductivityBook.API.Features.TaskFeature.Queries;
using System.Net;

namespace ProductivityBook.API.Features.TaskFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : BaseController
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto?>> GetTask(Guid id)
        {
            return await _mediator.Send(new GetTaskQuery { Id = id });
        }

        [HttpGet]
        public async Task<ActionResult<IList<TaskDto>>> GetTasks()
        {
            return await _mediator.Send(new GetTasksQuery());
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> CreateTask(CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Result>> DeleteTask(Guid id)
        {
            var result = await _mediator.Send(new DeleteTaskCommand { Id = id });

            if (result.IsSuccess)
            {
                return Ok();
            }
            else
            {
                return NotFound(result.Error);
            }
        }
    }
}
