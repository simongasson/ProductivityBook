using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskItemFeature;

namespace ProductivityBook.API.Features.TaskGroupFeature
{
    public class TaskGroup : BaseEntity
    {
        public DateTimeOffset Date { get; set; }

        public ICollection<TaskItem> Tasks { get; set; }

        private TaskGroup()
        {
            Date = DateTimeOffset.Now.Date;
            Tasks = new List<TaskItem>();
        }

        public static Result<TaskGroup> Create(DateTimeOffset date) {
            var taskGroup = new TaskGroup
            {
                Date = date,
                Tasks = new List<TaskItem>()
            };

            return Result<TaskGroup>.Success(taskGroup);
        }

        public static Result<TaskGroup> Create()
        {
            return Result<TaskGroup>.Success(new TaskGroup());
        }

        public bool IsActive => Date.Date == DateTimeOffset.Now.Date;
    }
}
