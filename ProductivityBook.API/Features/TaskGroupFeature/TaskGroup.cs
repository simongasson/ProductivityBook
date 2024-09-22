using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskItemFeature;

namespace ProductivityBook.API.Features.TaskGroupFeature
{
    public class TaskGroup : BaseEntity
    {
        public DateTimeOffset Date { get; set; }

        public ICollection<TaskItem> Tasks { get; set; } = [];
    }
}
