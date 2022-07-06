using Kobold.TodoApp.Api.Models.Groups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kobold.TodoApp.Api.Services
{
    public class GroupRepository
    {
        private static int nextId = 1;
        private static readonly List<Group> Groups = new List<Group>();

        public IEnumerable<Group> Get()
        {
            return Groups;
        }

        public Group Get(int id)
        {
            return Groups.FirstOrDefault(group => group.Id == id);
        }

        public Group Create(Group group)
        {
            var currentGroup = Groups.FirstOrDefault(g => g.Name.Equals(group.Name, StringComparison.OrdinalIgnoreCase));
            if (currentGroup == null)
            {
                group.Id = nextId++;
                Groups.Add(group);
            }
            else
            {
                currentGroup.Name = group.Name;
            }

            return currentGroup ?? group;
        }

        public Group Update(Group group)
        {
            var currentGroup = Groups.FirstOrDefault(g => g.Id == group.Id);
            if (currentGroup != null)
            {
                currentGroup.Name = group.Name;
            }

            return currentGroup ?? group;
        }

        public void RemoveTodoFromGroups(int id)
        {
            foreach(var group in Groups.Where(group => group.Todos.Any(todo => todo.Id == id)))
                group.Todos.RemoveAll(todo => todo.Id == id);
        }

        public bool Remove(int id)
        {
            return Groups.RemoveAll(group => group.Id == id) > 0;
        }
    }
}
