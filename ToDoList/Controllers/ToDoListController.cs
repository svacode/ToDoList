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


        [HttpPost]
        public async Task<ActionResult<List<ToDo>>> AddList(ToDo list)
        {
            await _dataContext.ToDos.AddAsync(list);
            _dataContext.SaveChanges();
            return Ok(await _dataContext.ToDos.ToListAsync());

        }
        [HttpPut("{id}")]
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
    }


}
