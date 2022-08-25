using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class EditorFuncaoUsuarioViewModel
{
    [Required(ErrorMessage = "A código do usuário é obrigatório")]
    public int CodigoUsuario { get; set; }

    [Required(ErrorMessage = "A nova função do usuário é obrigatória")]
    [Range(1, 3, ErrorMessage = "O código da função deve ser entre 1 e 3")]
    public int CodigoFuncao { get; set; }
}