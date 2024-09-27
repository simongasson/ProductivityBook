using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    [Route("api/tasks")]
    public class DeleteTaskController : BaseController
    {
        public DeleteTaskController(IMediator mediator) : base(mediator)
        {
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            var command = new DeleteTaskCommand(taskId);
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok() : BadRequest(result.Error);
        }
    }

    public class DeleteTaskCommand : IRequest<Result>
    {
        public Guid TaskId { get; }

        public DeleteTaskCommand(Guid taskId)
        {
            TaskId = taskId;
        }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly ApplicationDbContext _context;

        public DeleteTaskCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _context.TaskItems.FindAsync(request.TaskId);
            if (task == null)
            {
                return Result.Failure("Task not found.");
            }

            _context.TaskItems.Remove(task);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}
