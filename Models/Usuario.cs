using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Blog.Models;

[Table("USUARIO")]
public class Usuario
{
    [Key]
    [Column("CDUSUARIO")]
    public int CodigoUsuario { get; set; }

    [Column("NMUSUARIO")]
    public string NomeUsuario { get; set; }

    [Column("DSEMAIL")]
    public string Email { get; set; }

    [Column("DSPASSHASH")]
    [JsonIgnore]
    public string PasswordHash { get; set; }

    [Column("DSBIO")]
    public string Bio { get; set; }

    [Column("DSIMAGEM")]
    public string Imagem { get; set; }

    [Column("DSUSUARIO")]
    public string DescricaoUsuario { get; set; }

    public IList<Funcao> Funcoes { get; set; }

    public IList<Post> Posts { get; set; }
}