using System.ComponentModel.DataAnnotations;

namespace TaskBoard.Api.Models
{
    public class Column
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Name { get; set; } = default!;
        public int Order { get; set; }
        public List<TaskItem> Tasks { get; set; } = new();
    }
}
