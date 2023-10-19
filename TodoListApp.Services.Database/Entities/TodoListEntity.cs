using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoListApp.Services.Database.Entities
{
    [Table("todo_list")]
    public class TodoListEntity : TodoList
    {
        [Key]
        public new int Id { get; set; }

        public new string? Title { get; set; }
    }
}
