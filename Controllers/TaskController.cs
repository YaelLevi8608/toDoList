using Microsoft.AspNetCore.Mvc;
using myTasks.Models;
using myTasks.Interfaces;

namespace myTasks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        ITaskService TaskService;
        public TaskController(ITaskService TaskService)
        {
            this.TaskService = TaskService;
        }

        [HttpGet]
        public ActionResult<List<MyTask>?> GetAll(string token) =>
            TaskService.GetAll(token);


        [HttpGet("{id}")]
        public ActionResult<MyTask> Get(int id)
        {
            var task = TaskService.Get(id);

            if (task == null)
                return NotFound();

            return task;
        }

        [HttpPost]
        public IActionResult Create(MyTask task)
        {
            TaskService.Add(task);
            return CreatedAtAction(nameof(Create), new { id = task.Id }, task);

        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, MyTask task)
        {
            if (id != task.Id)
                return BadRequest();
            var existingTask = TaskService.Get(id);
            if (existingTask is null)
                return NotFound();
            TaskService.Update(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var t = TaskService.Get(id);
            if (t is null)
                return NotFound();

            TaskService.Delete(id);

            return Content(TaskService.Count.ToString());
        }
    }
}