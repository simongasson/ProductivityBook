using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskGroupFeature;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    public class TaskItem : BaseEntity
    {
        public required string Title { get; set; }

        public bool IsCompleted { get; set; }

        public Guid TaskGroupId { get; set; }
    }
}
