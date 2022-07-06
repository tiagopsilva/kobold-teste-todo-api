using System.ComponentModel.DataAnnotations;

namespace Kobold.TodoApp.Api.Models.Todos
{
    public class TodoViewModel
    {
        public TodoViewModel()
        {
        }

        public TodoViewModel(bool done, string description, int? groupId)
        {
            Done = done;
            Description = description;
            GroupId = groupId;
        }

        public bool Done { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o campo Descri��o")]
        [StringLength(
            maximumLength: TodoMetadataInfo.DescriptionMaxLength, 
            MinimumLength = TodoMetadataInfo.DescriptionMinLength, 
            ErrorMessage = "O campo Descri��o deve conter entre {1} e {2} caracteres")]
        public string Description { get; set; }

        public int? GroupId { get; set; }
    }
}
