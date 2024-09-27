using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;
using ProductivityBook.API.Features.TaskGroupFeature;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    [Route("api/tasks")]
    public class CreateTaskController : BaseController
    {
        public CreateTaskController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask(CreateTaskCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
        }
    }

    public class CreateTaskCommand : IRequest<Result<Guid>>
    {
        public required string Title { get; set; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result<Guid>>
    {
        private readonly ApplicationDbContext _context;

        public CreateTaskCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskGroup = await GetTodayTaskGroupOrNull();
            if (taskGroup == null)
            {
                var createTaskGroupResponse = TaskGroup.Create();
                if (createTaskGroupResponse.IsFailure)
                    return Result<Guid>.Failure(createTaskGroupResponse.Error);

                taskGroup = createTaskGroupResponse.Value;
                await _context.TaskGroups.AddAsync(taskGroup);
            }

            var createTaskResponse = TaskItem.Create(taskGroup, request.Title);
            if (createTaskResponse.IsFailure)
            {
                return Result<Guid>.Failure(createTaskResponse.Error);
            }

            var task = createTaskResponse.Value;

            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();

            return Result<Guid>.Success(task.Id);
        }

        private Task<TaskGroup?> GetTodayTaskGroupOrNull()
        {
            return _context.TaskGroups.FirstOrDefaultAsync(x => x.Date.Date == DateTimeOffset.Now.Date);
        }
    }
}
