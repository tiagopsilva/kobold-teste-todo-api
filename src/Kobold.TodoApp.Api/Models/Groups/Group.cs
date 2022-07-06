using Kobold.TodoApp.Api.Models.Todos;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Models.Groups
{
    public class Group
    {
        public Group()
        {
            Todos = new List<Todo>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public List<Todo> Todos { get; }
    }
}
