using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskItemFeature;

namespace ProductivityBook.API.Features.TaskGroupFeature
{
    public class TaskGroup : BaseEntity
    {
        public DateTimeOffset Date { get; private set; }

        public ICollection<TaskItem> Tasks { get; private set; }

        private TaskGroup()
        {
            Date = DateTimeOffset.Now.Date;
            Tasks = new List<TaskItem>();
        }

        public static Result<TaskGroup> Create()
        {
            return Result<TaskGroup>.Success(new TaskGroup());
        }

        public bool IsActive => Date.Date == DateTimeOffset.Now.Date;
    }
}
