using Kobold.TodoApp.Api.Models.Groups;
using System;

namespace Kobold.TodoApp.Api.Models.Todos
{
    public class TodoWithGroupResultViewModel
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Done { get; set; }

        public string Description { get; set; }

        public GroupResultViewModel Group { get; set; }
    }
}
