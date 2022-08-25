using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

[Table("POST")]
public class Post
{
    [Key]
    [Column("CDPOST")]
    public int CodigoPost { get; set; }

    [Column("DSTITULO")]
    public string Titulo { get; set; }

    [Column("DSSUMARIO")]
    public string Sumario { get; set; }

    [Column("DSCORPO")]
    public string Corpo { get; set; }

    [Column("DSPOST")]
    public string DescricaoPost { get; set; }

    [Column("DTCADASTRO")]
    public DateTime DataCadastro { get; set; }

    public virtual int CDUSUARIO { get; set; }
    
    public virtual int CDCATEGORIA { get; set; }
    
    [ForeignKey("CDCATEGORIA")]
    public Categoria Categoria { get; set; }

    [ForeignKey("CDUSUARIO")]
    public Usuario Usuario { get; set; }

    public IList<Tag> Tags { get; set; }
}