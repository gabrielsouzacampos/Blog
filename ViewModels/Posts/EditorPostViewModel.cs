using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels.Posts;

public class EditorPostViewModel
{
    [Required(ErrorMessage = "O código do usuário é obrigatório")]
    public int CodigoUsuario { get; set; }

    [Required(ErrorMessage = "O código da categoria é obrigatório")]
    public int CodigoCategoria { get; set; }

    [Required(ErrorMessage = "O titulo é obrigatório", AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "O titulo deve ter no máximo 255 caracteres")]
    public string Titulo { get; set; }

    [Required(ErrorMessage = "O sumario é obrigatório", AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "O sumario deve ter no máximo 255 caracteres")]
    public string Sumario { get; set; }

    [Required(ErrorMessage = "O corpo do post é obrigatório", AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "O corpo do post deve ter no máximo 255 caracteres")]
    public string Corpo { get; set; }

    [Required(ErrorMessage = "A descrição do post é obrigatório", AllowEmptyStrings = false)]
    [MaxLength(255, ErrorMessage = "A descrição do post deve ter no máximo 255 caracteres")]
    public string DescricaoPost { get; set; }
}