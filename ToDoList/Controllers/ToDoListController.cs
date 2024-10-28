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
        [HttpGet("{Timeline}")]
        public async Task<ActionResult<List<ToDo>>> SeeOneList(int id)
        {
            var notes = await _dataContext.ToDos.FindAsync(id);
            return Ok(notes);
        }
    }


}
