using ProductivityBook.API.Common;
using ProductivityBook.API.Features.TaskGroupFeature;

namespace ProductivityBook.API.Features.TaskItemFeature
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; private set; }

        public bool IsCompleted { get; private set; }

        public TaskGroup TaskGroup { get; private set; }

        private TaskItem()
        {
            Title = string.Empty;
            TaskGroup = null!;
            IsCompleted = false;
        }

        public static Result<TaskItem> Create(TaskGroup taskGroup, string title)
        {
            if (taskGroup == null)
                return Result<TaskItem>.Failure("Task group is required.");

            if (taskGroup.IsActive == false)
                return Result<TaskItem>.Failure("Task group is not active.");

            var taskItem = new TaskItem
            {
                TaskGroup = taskGroup,
                Title = title,
                IsCompleted = false
            };

            return Result<TaskItem>.Success(taskItem);
        }

        public static Result<TaskItem> SetCompletionStatus(TaskItem taskItem, bool isCompleted)
        {
            if (taskItem.TaskGroup.IsActive == false)
                return Result<TaskItem>.Failure("Task group is not active.");

            taskItem.IsCompleted = isCompleted;
            return Result<TaskItem>.Success(taskItem);
        }

        public static Result<TaskItem> SetTitle(TaskItem taskItem, string title)
        {
            if (taskItem.TaskGroup.IsActive == false)
                return Result<TaskItem>.Failure("Task group is not active.");

            if (string.IsNullOrWhiteSpace(title))
                return Result<TaskItem>.Failure("Title is required.");

            taskItem.Title = title;
            return Result<TaskItem>.Success(taskItem);
        }
    }
}
