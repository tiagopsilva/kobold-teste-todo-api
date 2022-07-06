using Kobold.TodoApp.Api.Extensions;
using Kobold.TodoApp.Api.Models.Groups;
using System.ComponentModel.DataAnnotations;

namespace Kobold.TodoApp.Api.Models.Todos
{
    public class TodoWithGroupViewModel
    {
        public TodoWithGroupViewModel() { }

        public TodoWithGroupViewModel(string description, string group, bool done = false)
        {
            Done = done;
            Description = description;
            if (group.IsPresent())
                Group = new GroupViewModel(group);
        }

        public bool Done { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o campo Descrição")]
        [StringLength(
            maximumLength: TodoMetadataInfo.DescriptionMaxLength,
            MinimumLength = TodoMetadataInfo.DescriptionMinLength,
            ErrorMessage = "O campo Descrição deve conter entre {1} e {2} caracteres")]
        public string Description { get; set; }

        public GroupViewModel Group { get; set; }
    }
}
