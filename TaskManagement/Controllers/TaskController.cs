

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskManagement.Database.Entities;
using TaskManagement.Services.Contracts;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService _taskService)
        {
            this._taskService = _taskService;
        }


        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] UserTask task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            var createdTask = _taskService.CreateTask(task);
            return Created("Task Created", createdTask);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateTask(UserTask task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            var updatedTask = _taskService.UpdateTask(task);
            return Ok(updatedTask);

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTask(Guid taskId)
        {
            if (taskId== Guid.Empty)
            {
                return BadRequest("Task id is empty");
            }

            _taskService.DeleteTask(taskId);
            return Ok("Task deleted");

        }

        [HttpPost("move")]
        public async Task<IActionResult> MoveTask(Guid taskId, Guid destinationColumnId)
        {
            if (taskId == Guid.Empty || destinationColumnId == Guid.Empty)
            {
                return BadRequest("Task ID or Destination Column Id is empty");
            }

            _taskService.MoveTask(taskId, destinationColumnId);
            return Ok("Task moved");

        }
    }
}

