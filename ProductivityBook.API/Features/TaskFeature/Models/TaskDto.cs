namespace ProductivityBook.API.Features.TaskFeature.Models
{
    public class TaskDto
    {
        public required Guid Id { get; set; }
        public required string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
