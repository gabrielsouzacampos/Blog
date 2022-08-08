using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

public class PFuncaoUsuario
{
    [NotMapped]
    public int CodigoUsuario { get; set; } = 0;
}