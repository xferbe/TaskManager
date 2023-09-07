using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using TaskManager.API.Data.Repositories;
using TaskManager.API.Models.InputModels;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private ITasksRepository _tasksRepository;

        public TasksController(ITasksRepository tasksRepository)
        { 
            _tasksRepository = tasksRepository;
        }


        // GET: api/tasks
        [HttpGet]
        public IActionResult Get()
        {
            var tasksResponse = _tasksRepository.Get();            
            return Ok(tasksResponse);
        }

        // GET api/tasks/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var taskResponse = _tasksRepository.Get(id);

            if (taskResponse == null) 
                return NotFound();
            else 
                return Ok(taskResponse);
        }

        // POST api/tasks
        [HttpPost]
        public IActionResult Post([FromBody] TaskInputModel newTask)
        {
            var task = new Models.Task(newTask.Name, newTask.Details);

            _tasksRepository.Add(task);
            return Created("", task);
        }

        // PUT api/tasks/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] TaskInputModel taskUpdated)
        {
            var task = _tasksRepository.Get(id);

            if (task == null)
                return NotFound();

            task.UpdateTask(taskUpdated.Name, taskUpdated.Details, taskUpdated.Concluded);

            _tasksRepository.Update(id, task);

            return Ok(task);
        }

        // DELETE api/tasks/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var task = _tasksRepository.Get(id); 

            if (task == null) 
                return NotFound();

            _tasksRepository.Delete(id);

            return NoContent();
        }
    }
}
