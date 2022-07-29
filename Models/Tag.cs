using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models;

[Table("TAG")]
public class Tag
{
    [Key]
    [Column("CDTAG")]
    public int CodigoTag { get; set; }

    [Column("NMTAG")]
    public string NomeTag { get; set; }

    [Column("DSTAG")]
    public string DescricaoTag { get; set; }

    public IList<Post> Posts { get; set; }
}