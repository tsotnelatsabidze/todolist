using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using TodoListApp.Services.Interfaces;
using TodoListApp.WebApi.Models.Models;

namespace TodoListApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        public ITodoListService TodoListService { get; set; }

        private readonly IMapper mapper;

        public TodoListController(ITodoListService todoListService, IMapper mapper)
        {
            this.TodoListService = todoListService;
            this.mapper = mapper;
        }

        [EnableQuery]
        [HttpGet(Name = "GetToDoLists")]
        public ActionResult<TodoListDto> GetToDoLists()
        {
            var todoList = this.TodoListService.GetTodoLists();
            return this.Ok(todoList);
        }

        [EnableQuery]
        [HttpGet("{todoListId}", Name = "GetToDoList")]
        public ActionResult<TodoListDto> GetToDoList(int todoListId)
        {
            var todoList = this.TodoListService.GetTodoListById(todoListId);
            return Ok(todoList);
        }

        [HttpPost(Name = "CreateToDoList")]
        public ActionResult<TodoListDto> CreateToDoList([FromBody] TodoListCreateDto todoList)
        {
            var result = this.TodoListService.CreateTodoList(this.mapper.Map<Services.Models.TodoList>(todoList));
            return this.Ok(result);
        }

        [HttpDelete("{id}", Name = "DeleteToDoList")]
        public ActionResult DeleteToDoList(int id)
        {
            try
            {
                this.TodoListService.DeleteTodoList(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPut("{id}", Name = "UpdateToDoList")]
        public ActionResult<TodoListDto> UpdateToDoList(int id, [FromBody] TodoListUpdateDto todoList)
        {
            try
            {
                var result = TodoListService.UpdateTodoList(id, this.mapper.Map<Services.Models.TodoList>(todoList));
                return Ok(result);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
