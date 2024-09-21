using MediatR;
using ProductivityBook.API.Common;

namespace ProductivityBook.API.Features.TaskFeature.Commands
{    public class DeleteTaskCommand : IRequest<Result>
    {
        public required Guid Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Result>
    {
        private readonly TaskDbContext _context;

        public DeleteTaskCommandHandler(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await _context.Tasks.FindAsync(command.Id);

            if (task == null)
            {
                return Result.Failure("Task not found.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
    }
}