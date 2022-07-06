using Kobold.TodoApp.Api.Models.Groups;
using Kobold.TodoApp.Api.Services;
using System;
using System.Collections.Generic;

namespace Kobold.TodoApp.Api.Tests.Middlewares
{
    public class GroupFakeService : IGroupService
    {
        public GroupResultViewModel Create(GroupViewModel groupvm)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupResultViewModel> Get()
        {
            throw new NotImplementedException();
        }

        public GroupResultViewModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupWithTodosResultViewModel> GetWithTodos()
        {
            throw new NotImplementedException();
        }

        public GroupWithTodosResultViewModel GetWithTodos(int id)
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public GroupResultViewModel Update(int id, GroupViewModel groupvm)
        {
            throw new NotImplementedException();
        }
    }
}
