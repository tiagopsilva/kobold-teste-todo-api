using System;

namespace Kobold.TodoApp.Api.Models.Todos
{
    public class TodoResultViewModel
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public bool Done { get; set; }

        public string Description { get; set; }
    }
}
