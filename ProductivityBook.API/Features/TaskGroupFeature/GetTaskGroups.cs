using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

namespace ProductivityBook.API.Features.TaskGroupFeature
{
    [Route("api/task-groups")]
    public class GetTaskGroupsController : BaseController
    {
        public GetTaskGroupsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskGroupDto>>> GetTaskGroups()
        {
            var result = await _mediator.Send(new GetTaskGroupsQuery());
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }

    public class GetTaskGroupsQuery : IRequest<Result<IEnumerable<TaskGroupDto>>>
    {
    }

    public class GetTaskGroupsQueryHandler : IRequestHandler<GetTaskGroupsQuery, Result<IEnumerable<TaskGroupDto>>>
    {
        private readonly ApplicationDbContext _context;

        public GetTaskGroupsQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<TaskGroupDto>>> Handle(GetTaskGroupsQuery request, CancellationToken cancellationToken)
        {
            var taskGroups = await _context.TaskGroups
                .Include(tg => tg.Tasks)
                .ToListAsync();

            var taskGroupDtos = taskGroups.Adapt<IEnumerable<TaskGroupDto>>();
            return Result<IEnumerable<TaskGroupDto>>.Success(taskGroupDtos);
        }
    }

    public class TaskGroupDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public IEnumerable<TaskDto> Tasks { get; set; } = new List<TaskDto>();
    }

    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
