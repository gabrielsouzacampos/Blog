using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Contas;

public class RegistroViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string NomeUsuario { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido")]
    public string Email { get; set; }
}