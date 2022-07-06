using AutoMapper;
using Kobold.TodoApp.Api.Extensions;
using Kobold.TodoApp.Api.Models.Groups;
using Kobold.TodoApp.Api.Models.Todos;
using System.Collections.Generic;
using System.Linq;

namespace Kobold.TodoApp.Api.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoRepository _todoRepository;
        private readonly GroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public TodoService(TodoRepository todoRepository, GroupRepository groupRepository, IMapper mapper)
        {
            _todoRepository = todoRepository;
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public IEnumerable<TodoResultViewModel> Get()
        {
            return _todoRepository.Get().Select(_mapper.Map<TodoResultViewModel>);
        }

        public IEnumerable<TodoWithGroupResultViewModel> GetWithGroup()
        {
            return _todoRepository.Get().Select(_mapper.Map<TodoWithGroupResultViewModel>);
        }

        public TodoResultViewModel Get(int id)
        {
            return _mapper.Map<TodoResultViewModel>(_todoRepository.Get(id));
        }

        public TodoWithGroupResultViewModel GetWithGroup(int id)
        {
            return _mapper.Map<TodoWithGroupResultViewModel>(_todoRepository.Get(id));
        }

        public TodoWithGroupResultViewModel Create(TodoViewModel todovm)
        {
            var todo = _mapper.Map<Todo>(todovm);
            if (todovm.GroupId.HasValue)
                todo.Group = _groupRepository.Get(todovm.GroupId.Value);
            return _mapper.Map<TodoWithGroupResultViewModel>(_todoRepository.Create(todo));
        }

        public TodoWithGroupResultViewModel Create(TodoWithGroupViewModel todovm)
        {
            var todo = _mapper.Map<Todo>(todovm);
            todo = _todoRepository.Create(todo);

            if (todovm.Group?.Name.IsPresent() == true)
            {
                var group = _mapper.Map<Group>(todovm.Group);
                todo.Group = _groupRepository.Create(group);
                todo.Group.Todos.Add(todo);
            }

            return _mapper.Map<TodoWithGroupResultViewModel>(todo);
        }

        public TodoWithGroupResultViewModel Update(int id, TodoUpdateViewModel todovm)
        {
            var todo = _mapper.Map<Todo>(todovm);
            todo.Id = id;

            todo.Group = _groupRepository.Get(todovm.GroupId ?? 0);

            if (todo.Group == null)
                _todoRepository.RemoveFromTodosTheGroupWithId(todovm.GroupId ?? 0);

            return _mapper.Map<TodoWithGroupResultViewModel>(_todoRepository.Update(todo));
        }

        public bool Remove(int id)
        {
            var success = _todoRepository.Remove(id);
            _groupRepository.RemoveTodoFromGroups(id);
            return success;
        }
    }
}
