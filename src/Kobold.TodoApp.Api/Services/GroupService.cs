using AutoMapper;
using Kobold.TodoApp.Api.Models.Groups;
using System.Collections.Generic;
using System.Linq;

namespace Kobold.TodoApp.Api.Services
{
    public class GroupService : IGroupService
    {
        private readonly GroupRepository _groupRepository;
        private readonly TodoRepository _todoRepository;
        private readonly IMapper _mapper;

        public GroupService(GroupRepository groupRepository, TodoRepository todoRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public IEnumerable<GroupResultViewModel> Get()
        {
            return _groupRepository.Get().Select(_mapper.Map<GroupResultViewModel>);
        }

        public IEnumerable<GroupWithTodosResultViewModel> GetWithTodos()
        {
            return _groupRepository.Get().Select(_mapper.Map<GroupWithTodosResultViewModel>);
        }

        public GroupResultViewModel Get(int id)
        {
            return _mapper.Map<GroupResultViewModel>(_groupRepository.Get(id));
        }

        public GroupWithTodosResultViewModel GetWithTodos(int id)
        {
            return _mapper.Map<GroupWithTodosResultViewModel>(_groupRepository.Get(id));
        }

        public GroupResultViewModel Create(GroupViewModel groupvm)
        {
            var group = _mapper.Map<Group>(groupvm);
            return _mapper.Map<GroupResultViewModel>(_groupRepository.Create(group));
        }

        public GroupResultViewModel Update(int id, GroupViewModel groupvm)
        {
            var group = _mapper.Map<Group>(groupvm);
            group.Id = id;
            return _mapper.Map<GroupResultViewModel>(_groupRepository.Update(group));
        }

        public bool Remove(int id)
        {
            var success = _groupRepository.Remove(id);
            _todoRepository.RemoveFromTodosTheGroupWithId(id);
            return success;
        }
    }
}
