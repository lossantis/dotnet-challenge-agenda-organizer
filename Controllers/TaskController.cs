using Microsoft.AspNetCore.Mvc;
using DotnetTaskApi.Models;

namespace DotnetTaskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly Context.OrganizerContext _context;

        public TaskController(Context.OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetTasks()
        {
            var tasks = _context.UserTasks.ToList();

            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTaskById(int id)
        {
            var task = _context.UserTasks.Find(id);

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("bytitle/{title}")]
        public IActionResult GetTaskByTitle(string title)
        {
            var task = _context.UserTasks.Where(t => t.Title.ToLower().Contains(title));

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("bydescription/{description}")]
        public IActionResult GetTaskByDescription(string description)
        {
            var task = _context.UserTasks.Where(t => t.Description.ToLower().Contains(description));

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("bydate/{date}")]
        public IActionResult GetTaskByDate(DateTime date)
        {
            var task = _context.UserTasks
                .Where(t => t.Date.Date == date.Date)
                .ToList();

            if (task == null || !task.Any())
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("bystatus/{status}")]
        public IActionResult GetTaskByStatus(UserTaskStatus status)
        {
            var task = _context.UserTasks.Where(t => status.Equals(t.Status));

            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost]
        public IActionResult CreateTask(Models.UserTask userTask)
        {
            if (userTask.Date == DateTime.MinValue)
            {
                return BadRequest("Date cannot be empty.");
            }

            _context.UserTasks.Add(userTask);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetTaskById), new { id = userTask.Id }, userTask);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTask(int id, UserTask userTask)
        {
            var existingTask = _context.UserTasks.Find(id);

            if (existingTask == null)
            {
                return NotFound();
            }

            if (userTask.Date == DateTime.MinValue)
            {
                return BadRequest("Date cannot be empty.");
            }

            existingTask.Title = userTask.Title ?? existingTask.Title;
            existingTask.Description = userTask.Description ?? existingTask.Description;
            existingTask.Date = userTask.Date;
            existingTask.Status = userTask.Status;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTask(int id)
        {
            var task = _context.UserTasks.Find(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(task);
            _context.SaveChanges();

            return NoContent();
        }
    }
}