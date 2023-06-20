using Microsoft.AspNetCore.Mvc;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColumnController : ControllerBase
    {
        private readonly IColumnService _columnService;

        public ColumnController(IColumnService _columnService)
        {
            this._columnService = _columnService;
        }

        [HttpGet]
        public async Task<IActionResult> GetColumns(bool includeTasks = false)
        {
            var columns = _columnService.GetAllColumns(includeTasks);
            return Ok(columns);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetColumnById(Guid id, bool includeTasks=false)
        {
            var column = _columnService.GetColumnById(id, includeTasks);
            if (column == null)
                return NotFound();

            return Ok(column);
        }
    }
}
