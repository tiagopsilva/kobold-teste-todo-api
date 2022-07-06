using AutoMapper;
using Kobold.TodoApp.Api.Models.Groups;
using Kobold.TodoApp.Api.Models.Todos;
using System;

namespace Kobold.TodoApp.Api.Profiles
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TodoViewModel, Todo>()
                .ForMember(src => src.DataCriacao, dest => dest.MapFrom(p => DateTime.Now));

            CreateMap<TodoWithGroupViewModel, Todo>()
                .ForMember(src => src.DataCriacao, dest => dest.MapFrom(p => DateTime.Now))
                .ForMember(src => src.Group, dest => dest.Condition(p => p.Group != null));

            CreateMap<TodoUpdateViewModel, Todo>();

            CreateMap<Todo, TodoResultViewModel>();
            CreateMap<Todo, TodoWithGroupResultViewModel>();

            CreateMap<GroupViewModel, Group>();

            CreateMap<Group, GroupResultViewModel>();
            CreateMap<Group, GroupWithTodosResultViewModel>();
        }
    }
}
