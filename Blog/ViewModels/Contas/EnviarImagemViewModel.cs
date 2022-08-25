using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Contas;

public class EnviarImagemViewModel
{
    [Required(ErrorMessage = "Imagem inválida")]
    public string Base64Image { get; set; }
}