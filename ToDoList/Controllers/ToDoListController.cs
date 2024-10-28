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
        [HttpPut]
        public async Task<ActionResult<List<ToDo>>> ChangeList(ToDo updatedList)
        {
            var dbList = await _dataContext.ToDos.FindAsync(updatedList.Id);
            if (dbList == null)
                return BadRequest("Not Found");
            dbList.Timeline = updatedList.Timeline;
            dbList.One = updatedList.One;
            dbList.Two = updatedList.Two;
            dbList.Three = updatedList.Three;
            dbList.Four = updatedList.Four;
            dbList.Five = updatedList.Five;
            dbList.Six = updatedList.Six;
            dbList.Seven = updatedList.Seven;
            dbList.Eight = updatedList.Eight;
            _dataContext.SaveChanges();
            return Ok(dbList);
        }
    }


}
