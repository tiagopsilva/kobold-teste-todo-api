using Kobold.TodoApp.Api.Models.Todos;
using System.Collections.Generic;
using System.Linq;

namespace Kobold.TodoApp.Api.Services
{
    public class TodoRepository
    {
        private static int nextId = 1;
        private static readonly List<Todo> Todos = new List<Todo>();

        public IEnumerable<Todo> Get()
        {
            return Todos;
        }

        public Todo Get(int id)
        {
            return Todos.FirstOrDefault(todo => todo.Id == id);
        }

        public Todo Create(Todo todo)
        {
            todo.Id = nextId++;
            Todos.Add(todo);
            return todo;
        }

        public Todo Update(Todo todo)
        {
            var currentTodo = Todos.FirstOrDefault(td => td.Id == todo.Id);
            if (currentTodo != null)
            {
                currentTodo.Done = todo.Done;
                currentTodo.Group = todo.Group;
            }
            return currentTodo;
        }

        public void RemoveFromTodosTheGroupWithId(int id)
        {
            foreach (var todo in Todos.Where(todo => todo.Group?.Id == id))
                todo.Group = null;
        }

        public bool Remove(int id)
        {
            return Todos.RemoveAll(todo => todo.Id == id) > 0;
        }
    }
}
