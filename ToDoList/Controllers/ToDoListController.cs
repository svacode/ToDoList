using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Entities;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly DataContext _dataContext;
        public ToDoListController(DataContext context)
        {
            _dataContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<ToDo>>> SeeAllNotes()
        {
            var notes = await _dataContext.ToDos.ToListAsync();
            return Ok(notes);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDo>> SeeOneList(int id)
        {
            var notes = await _dataContext.ToDos.FindAsync(id);
            if (notes == null)
            {
                return BadRequest("Not Found");
            }
            return Ok(notes);

        }

        [HttpGet("time/{timeline}")]
        public async Task<ActionResult<List<ToDo>>> GetListByDay(string timeline)
        {
            var notes = await _dataContext.ToDos
                                          .Where(ToDo => ToDo.Timeline == timeline)
                                          .ToListAsync();
            if (notes.Count == 0)
                return BadRequest("Not Found");
            return Ok(notes);
        }

        [HttpGet("Done")]
        public async Task<ActionResult<List<ToDo>>> GetDoneTasks(bool done)
        {
            var notes = await _dataContext.ToDos
                                          .Where(ToDo => ToDo.Done == true)
                                          .ToListAsync();
            if (notes.Count == 0)
                return BadRequest("Not Found");
            return Ok(notes);
        }
        [HttpGet("Not Done")]
        public async Task<ActionResult<List<ToDo>>> GetUndoneTasks(bool done)
        {
            var notes = await _dataContext.ToDos
                                          .Where(ToDo => ToDo.Done == false)
                                          .ToListAsync();
            if (notes.Count == 0)
                return BadRequest("Not Found");
            return Ok(notes);
        }

        [HttpGet("Priority level")]
        public async Task<ActionResult<List<ToDo>>> FilterByPriority(Priority priorityLevel)
        {
            var notes = await _dataContext.ToDos
                                                .Where(ToDo => ToDo.Priority == priorityLevel)
                                                .ToListAsync();
            if (notes.Count == 0)
                return BadRequest("Not found");
            return Ok(notes);
        }

        [HttpPost]
        public async Task<ActionResult<List<ToDo>>> AddList(ToDo list)
        {
            if (!Enum.IsDefined(typeof(Priority), list.Priority))
                return BadRequest("Invalid priority level. Use Low, Medium or High");
            await _dataContext.ToDos.AddAsync(list);
            _dataContext.SaveChanges();
            return Ok(await _dataContext.ToDos.ToListAsync());

        }
        [HttpPut]
        public async Task<ActionResult<List<ToDo>>> ChangeList(int id,  ToDo updatedList)
        {

            var dbList = await _dataContext.ToDos.FindAsync(id);
            if (dbList == null)
                return BadRequest("Not Found");
            dbList.Timeline = updatedList.Timeline;
            dbList.Header = updatedList.Header;
            dbList.Description = updatedList.Description;
            dbList.TimeToFinish = updatedList.TimeToFinish;
            dbList.Deadline = updatedList.Deadline;
            dbList.Done = updatedList.Done;
            dbList.Priority = updatedList.Priority;
            if (!Enum.IsDefined(typeof(Priority), updatedList.Priority))
                return BadRequest("Invalid priority level. Use Low, Medium or High");
            _dataContext.SaveChanges();
            return Ok(dbList);
        }
        [HttpDelete]
        public async Task<ActionResult<List<ToDo>>> DeleteList(int id)
        {
            var dbList = await _dataContext.ToDos.FindAsync(id);
            if (dbList == null)
                return BadRequest("Not Found");
            _dataContext.ToDos.Remove(dbList);
            _dataContext.SaveChanges();
            return Ok(await _dataContext.ToDos.ToListAsync());
        }
        

    }


}
