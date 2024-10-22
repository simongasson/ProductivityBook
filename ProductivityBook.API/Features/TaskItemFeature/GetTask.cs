using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    [Route("api/task-items")]
    public class GetTaskController : BaseController
    {
        public GetTaskController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("{taskId}")]
        public async Task<ActionResult<GetTaskByIdDto>> GetTask(Guid taskId)
        {
            var result = await _mediator.Send(new GetTaskQuery(taskId));
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }

    public class GetTaskQuery : IRequest<Result<GetTaskByIdDto>>
    {
        public Guid TaskId { get; }

        public GetTaskQuery(Guid taskId)
        {
            TaskId = taskId;
        }
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, Result<GetTaskByIdDto>>
    {
        private readonly ApplicationDbContext _context;

        public GetTaskQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<GetTaskByIdDto>> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems
                .Include(ti => ti.TaskGroup)
                .FirstOrDefaultAsync(ti => ti.Id == request.TaskId);

            if (task == null)
                return Result<GetTaskByIdDto>.Failure("Task not found");

            var taskDto = task.Adapt<GetTaskByIdDto>();
            return Result<GetTaskByIdDto>.Success(taskDto);
        }
    }

    public class GetTaskByIdDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
