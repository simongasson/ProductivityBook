using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductivityBook.API.Features.TaskFeature.Models;

namespace ProductivityBook.API.Features.TaskFeature.Queries
{
    public class GetTasksQuery : IRequest<List<TaskDto>>
    {
    }

    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, List<TaskDto>>
    {
        private readonly TaskDbContext _context;

        public GetTasksQueryHandler(TaskDbContext context)
        {
            _context = context;
        }

        public Task<List<TaskDto>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return _context.Tasks.ProjectToType<TaskDto>().ToListAsync();
        }
    }
}
