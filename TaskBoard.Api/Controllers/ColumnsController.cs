using Microsoft.AspNetCore.Mvc;
using TaskBoard.Api.Dtos;
using TaskBoard.Api.Models;
using TaskBoard.Api.Services;

namespace TaskBoard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService _service;

        public ColumnsController(IColumnService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> ListColumns()
        {
            var cols = await _service.ListColumnsAsync();
            return Ok(cols);
        }

        [HttpPost]
        public async Task<IActionResult> CreateColumn([FromBody] CreateColumnDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) return BadRequest("Name is required");
            var col = new Column { Name = dto.Name, Order = dto.Order };
            var created = await _service.CreateColumnAsync(col);
            return CreatedAtAction(nameof(GetColumn), new { id = created.Id }, created);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColumn(Guid id)
        {
            var col = await _service.GetColumnAsync(id);
            return col == null ? NotFound() : Ok(col);
        }
    }
}
