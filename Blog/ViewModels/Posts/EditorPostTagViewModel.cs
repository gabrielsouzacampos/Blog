using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModels;

public class EditorPostTagViewModel
{
    [Required(ErrorMessage = "A código do post é obrigatório")]
    public int CodigoPost { get; set; }

    [Required(ErrorMessage = "As tags são obrigatórias")]
    [MinLength(1, ErrorMessage = "Insira uma tag")]
    public int[] CodigoTags { get; set; }
}