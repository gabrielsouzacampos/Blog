using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Contas;

public class RegistroViewModel
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    public string NomeUsuario { get; set; }

    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "O e-mail é inválido")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A função do novo usuário é obrigatória")]
    [Range(1, 3, ErrorMessage = "O código da função deve ser entre 1 e 3")]
    public int CodigoFuncao { get; set; }
}