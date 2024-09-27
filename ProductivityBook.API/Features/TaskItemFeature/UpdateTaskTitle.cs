using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    [Route("api/tasks")]
    public class UpdateTaskTitleController : BaseController
    {
        public UpdateTaskTitleController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPut("{taskId}/title")]
        public async Task<ActionResult> UpdateTaskTitle(Guid taskId, string title)
        {
            var result = await _mediator.Send(new UpdateTaskTitleCommand(taskId, title));
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }

    public class UpdateTaskTitleCommand : IRequest<Result>
    {
        public Guid TaskId { get; }
        public string Title { get; }

        public UpdateTaskTitleCommand(Guid taskId, string title)
        {
            TaskId = taskId;
            Title = title;
        }
    }

    public class UpdateTaskTitleCommandHandler : IRequestHandler<UpdateTaskTitleCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public UpdateTaskTitleCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTaskTitleCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems.Include(x => x.TaskGroup).FirstOrDefaultAsync(x => x.Id == request.TaskId);
            if (task == null)
            {
                return Result.Failure("Task not found");
            }

            var result = TaskItem.SetTitle(task, request.Title);
            if (result.IsFailure)
            {
                return Result.Failure(result.Error);
            }

            task = result.Value;
            await _context.SaveChangesAsync();
            return Result.Success();
        }
    }
}