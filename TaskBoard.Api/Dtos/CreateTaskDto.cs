namespace TaskBoard.Api.Dtos
{
    public class CreateTaskDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public Guid ColumnId { get; set; }
    }
}
