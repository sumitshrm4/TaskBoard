using TaskBoard.Api.Data;
using TaskBoard.Api.Models;
using TaskBoard.Api.Repositories;

namespace TaskBoard.Api.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _tasks;
        private readonly AppDbContext _db;

        public TaskService(ITaskRepository tasks, AppDbContext db)
        {
            _tasks = tasks;
            _db = db;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem dto, CancellationToken ct = default)
        {
            dto.Id = Guid.NewGuid();
            await _tasks.AddAsync(dto, ct);
            await _db.SaveChangesAsync(ct);
            return dto;
        }

        public async Task<TaskItem?> GetTaskAsync(Guid id, CancellationToken ct = default) =>
            await _tasks.GetByIdAsync(id, ct);

        public async Task<TaskItem?> UpdateTaskAsync(Guid id, TaskItem updated, CancellationToken ct = default)
        {
            var task = await _tasks.GetByIdAsync(id, ct);
            if (task == null) return null;

            task.Name = updated.Name;
            task.Description = updated.Description;
            task.Deadline = updated.Deadline;
            task.IsFavorited = updated.IsFavorited;
            task.SortOrder = updated.SortOrder;
            task.AttachmentUrl = updated.AttachmentUrl;

            await _tasks.UpdateAsync(task, ct);
            await _db.SaveChangesAsync(ct);
            return task;
        }

        public async Task<bool> DeleteTaskAsync(Guid id, CancellationToken ct = default)
        {
            var task = await _tasks.GetByIdAsync(id, ct);
            if (task == null) return false;
            await _tasks.DeleteAsync(id, ct);
            await _db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<bool> MoveTaskAsync(Guid taskId, Guid targetColumnId, CancellationToken ct = default)
        {
            var task = await _tasks.GetByIdAsync(taskId, ct);
            if (task == null) return false;
            task.ColumnId = targetColumnId;
            await _tasks.UpdateAsync(task, ct);
            await _db.SaveChangesAsync(ct);
            return true;
        }
    }
}
