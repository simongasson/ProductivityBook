using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Features.TaskFeature.Models;

namespace ProductivityBook.API.Features.TaskFeature.Queries
{
    public class GetTaskQuery : IRequest<TaskDto>
    {
        public required string Id { get; set; }
    }

    public class GetTaskQueryHandler : IRequestHandler<GetTaskQuery, TaskDto?>
    {
        private readonly TaskDbContext _context;

        public GetTaskQueryHandler(TaskDbContext context)
        {
            _context = context;
        }

        public Task<TaskDto?> Handle(GetTaskQuery request, CancellationToken cancellationToken)
        {
            return _context.Tasks.ProjectToType<TaskDto>().FirstOrDefaultAsync(x => x.Id == request.Id);
        }
    }
}
