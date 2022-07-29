using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Tag;

public class EditorTagViewModel
{
    [Required(ErrorMessage = "O nome da tag é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome da tag deve conter entre 3 e 40 caracteres")]
    public string NomeTag { get; set; }
    
    [Required(ErrorMessage = "A descrição da tag é obrigatório")]
    public string DescricaoTag { get; set; }
}