using Kobold.TodoApp.Api.Models.Groups;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Services
{
    public interface IGroupService
    {
        IEnumerable<GroupResultViewModel> Get();
        IEnumerable<GroupWithTodosResultViewModel> GetWithTodos();
        GroupResultViewModel Get(int id);
        GroupWithTodosResultViewModel GetWithTodos(int id);
        GroupResultViewModel Create(GroupViewModel groupvm);
        GroupResultViewModel Update(int id, GroupViewModel groupvm);
        bool Remove(int id);
    }
}