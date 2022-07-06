using Kobold.TodoApp.Api.Models.Todos;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Models.Groups
{
    public class GroupWithTodosResultViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<TodoResultViewModel> Todos { get; set; }
    }
}
