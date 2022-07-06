using System.ComponentModel.DataAnnotations;

namespace Kobold.TodoApp.Api.Models.Groups
{
    public class GroupViewModel
    {
        protected GroupViewModel() { }

        public GroupViewModel(string name)
        {
            Name = name;
        }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Informe o campo Nome")]
        [StringLength(
            maximumLength: GroupMetadataInfo.NameMaxLength,
            MinimumLength = GroupMetadataInfo.NameMinLength,
            ErrorMessage = "O campo Nome deve conter entre {1} e {2} caracteres")]
        public string Name { get; set; }
    }
}
