using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

[Table("FUNCAO")]
public class Funcao
{
    [Key]
    [Column("CDFUNCAO")]
    public int CodigoFuncao { get; set; }

    [Column("NMFUNCAO")]
    public string NomeFuncao { get; set; }

    [Column("DSFUNCAO")]
    public string DescricaoFuncao { get; set; }

    public IList<Usuario> Usuarios { get; set; }
}