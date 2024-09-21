using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductivityBook.API.Features.TaskFeature.Commands;
using ProductivityBook.API.Features.TaskFeature.Models;
using ProductivityBook.API.Features.TaskFeature.Queries;

namespace ProductivityBook.API.Features.TaskFeature
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto?>> GetTask(string id)
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
    }
}
