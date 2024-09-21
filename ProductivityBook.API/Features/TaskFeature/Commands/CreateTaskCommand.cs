using Mapster;
using MediatR;
using ProductivityBook.API.Features.TaskFeature.Models;

namespace ProductivityBook.API.Features.TaskFeature.Commands
{
    public class CreateTaskCommand : IRequest<TaskDto>
    {
        public required string Title { get; set; }
        public bool IsCompleted { get; set; } = false;
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, TaskDto>
    {
        private readonly TaskDbContext _context;

        public CreateTaskCommandHandler(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<TaskDto> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = request.Adapt<TaskEntity>();

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task.Adapt<TaskDto>();
        }
    }
}
