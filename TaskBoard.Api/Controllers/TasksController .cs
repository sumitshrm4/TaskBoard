using Microsoft.AspNetCore.Mvc;
using TaskBoard.Api.Dtos;
using TaskBoard.Api.Models;
using TaskBoard.Api.Services;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required");
            var task = new TaskItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Deadline = dto.Deadline,
                ColumnId = dto.ColumnId
            };
            var created = await _service.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetTask), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _service.GetTaskAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskItem updated)
        {
            var task = await _service.UpdateTaskAsync(id, updated);
            return task == null ? NotFound() : Ok(task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var deleted = await _service.DeleteTaskAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        [HttpPost("{id}/move")]
        public async Task<IActionResult> MoveTask(Guid id, [FromBody] Guid targetColumnId)
        {
            var moved = await _service.MoveTaskAsync(id, targetColumnId);
            return moved ? NoContent() : NotFound();
        }
    }
}
