using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    [Route("api/tasks")]
    public class UpdateTaskCompletionStatusController : BaseController
    {
        public UpdateTaskCompletionStatusController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPut("{taskId}/completion")]
        public async Task<ActionResult> UpdateTaskCompletionStatus(Guid taskId, bool isCompleted)
        {
            var result = await _mediator.Send(new UpdateTaskCompletionStatusCommand(taskId, isCompleted));
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }

    public class UpdateTaskCompletionStatusCommand : IRequest<Result>
    {
        public Guid TaskId { get; }
        public bool IsCompleted { get; }

        public UpdateTaskCompletionStatusCommand(Guid taskId, bool isCompleted)
        {
            TaskId = taskId;
            IsCompleted = isCompleted;
        }
    }

    public class UpdateTaskCompletionStatusCommandHandler : IRequestHandler<UpdateTaskCompletionStatusCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public UpdateTaskCompletionStatusCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(UpdateTaskCompletionStatusCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems.Include(x => x.TaskGroup).FirstOrDefaultAsync(x => x.Id == request.TaskId);
            if (task == null)
            {
                return Result.Failure("Task not found");
            }

            var result = TaskItem.SetCompletionStatus(task, request.IsCompleted);
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
