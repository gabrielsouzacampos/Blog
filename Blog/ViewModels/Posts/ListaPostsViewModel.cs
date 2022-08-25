namespace Blog.ViewModels.Posts;

public class ListaPostsViewModel
{
    public int CodigoPost { get; set; }

    public string Titulo { get; set; }

    public string DescricaoPost { get; set; }

    public DateTime DataCadastro { get; set; }

    public string Categoria { get; set; }

    public string Autor { get; set; }

    public object Tags { get; set; }
}