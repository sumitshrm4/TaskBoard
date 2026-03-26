using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskBoard.Api.Models
{
    public class TaskItem
    {
        [Key] public Guid Id { get; set; }
        [Required] public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public DateTime? Deadline { get; set; }
        public bool IsFavorited { get; set; }
        public int SortOrder { get; set; }
        [ForeignKey(nameof(Column))] public Guid ColumnId { get; set; }
        public Column? Column { get; set; }
        public string? AttachmentUrl { get; set; }
    }
}
