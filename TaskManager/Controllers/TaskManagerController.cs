using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Model;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        public ITasks _tasks;
        public TaskManagerController(ITasks tasks)
        {
            _tasks = tasks;
        }
        
        [HttpGet("All Tasks")]
        public IActionResult Get()
        {
            return Ok(_tasks.GetTasks());
        }

        [HttpGet("GetById")]
        public IActionResult Get(int id)
        {
            return Ok(_tasks.GetTaskById(id));
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost("Add Task")]
        public IActionResult Post([FromBody]TaskBody task)
        {
            _tasks.AddTask(task);
            return Ok();
        }
        [Authorize(Roles =Role.Admin)]
        [HttpPut("Updatetask")]
        public IActionResult Put(int id , [FromBody] TaskBody task)
        {
            _tasks.UpdateTask(id, task);
            return Ok();
        }

        [HttpDelete("Delete")]
        [Authorize(Roles = Role.Admin)]
        public IActionResult Delete(int id)
        {
            _tasks.RemoveTask(id);
            return Ok();
        }
    }
}
