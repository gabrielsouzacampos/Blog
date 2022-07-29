using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Categorias;

public class EditorCategoriaViewModel
{
    [Required(ErrorMessage = "O nome da categoria é obrigatório")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "O nome da categoria deve conter entre 3 e 40 caracteres")]
    public string NomeCategoria { get; set; }
    
    [Required(ErrorMessage = "A descrição da categoria é obrigatório")]
    public string DescricaoCategoria { get; set; }
}