using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductivityBook.API.Common;
using ProductivityBook.API.Database;

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
        public Guid TaskGroupId { get; set; }
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
            var taskGroup = await _context.TaskGroups.FindAsync(request.TaskGroupId);
            if (taskGroup == null) 
                return Result<Guid>.Failure("Task group not found");

            var task = request.Adapt<TaskItem>();
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();

            return Result<Guid>.Success(task.Id);
        }
    }
}
