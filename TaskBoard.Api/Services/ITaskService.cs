using TaskBoard.Api.Models;

namespace TaskBoard.Api.Services
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskItem dto, CancellationToken ct = default);
        Task<TaskItem?> GetTaskAsync(Guid id, CancellationToken ct = default);
        Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem updated, CancellationToken ct = default);
        Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct = default);
        Task<bool> MoveTaskAsync(Guid taskId, Guid targetColumnId, CancellationToken ct = default);
    }
}
