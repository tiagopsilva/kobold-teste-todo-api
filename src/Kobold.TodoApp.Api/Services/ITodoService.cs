using Kobold.TodoApp.Api.Models.Todos;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Services
{
    public interface ITodoService
    {
        IEnumerable<TodoResultViewModel> Get();
        IEnumerable<TodoWithGroupResultViewModel> GetWithGroup();
        TodoResultViewModel Get(int id);
        TodoWithGroupResultViewModel GetWithGroup(int id);
        TodoWithGroupResultViewModel Create(TodoViewModel todovm);
        TodoWithGroupResultViewModel Create(TodoWithGroupViewModel todovm);
        TodoWithGroupResultViewModel Update(int id, TodoUpdateViewModel todovm);
        bool Remove(int id);
    }
}