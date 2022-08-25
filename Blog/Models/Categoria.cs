using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;
    
[Table("CATEGORIA")]
public class Categoria
{
    [Key]
    [Column("CDCATEGORIA")]
    public int CodigoCategoria { get; set; }

    [Column("NMCATEGORIA", TypeName = "VARCHAR2")]
    [Required]
    [MinLength(3)]
    [MaxLength(80)]
    public string NomeCategoria { get; set; }

    [Column("DSCATEGORIA", TypeName = "VARCHAR2")]
    [Required]
    [MinLength(3)]
    [MaxLength(255)]
    public string DescricaoCategoria { get; set; }

    public IList<Post> Posts { get; set; }
}