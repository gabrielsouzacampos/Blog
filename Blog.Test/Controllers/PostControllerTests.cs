using Blog.Models;

namespace Blog.Controllers.Tests;

[TestClass()]
public class PostControllerTests
{
    private List<Post> _posts = new()
    {
        new Post
        {
            CodigoPost = 1,
            Corpo = "Corpo do post",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo",
            DataCadastro = DateTime.Now,
            Categoria = new()
            {
                CodigoCategoria = 1,
                NomeCategoria = "Frontend",
                DescricaoCategoria = "Descricação"
            }
        },
        new Post
        {
            CodigoPost = 2,
            Corpo = "Corpo do post",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo",
            DataCadastro = DateTime.Now,
            Categoria = new()
            {
                CodigoCategoria = 1,
                NomeCategoria = "Frontend",
                DescricaoCategoria = "Descricação"
            }
        },
        new Post
        {
            CodigoPost = 3,
            Corpo = "Corpo do post",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo",
            DataCadastro = DateTime.Now,
            Categoria = new()
            {
                CodigoCategoria = 1,
                NomeCategoria = "Frontend",
                DescricaoCategoria = "Descricação"
            }
        },
    };

    [TestMethod()]
    public void BuscaPostPorCodigoSucesso()
    {
        bool sucesso = false;
        int id = 1;

        var post = _posts.Where(x => x.CodigoPost == id).ToList();

        if (!post.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaPostPorCodigoErroNulo()
    {
        bool sucesso = false;
        int id = 4;

        var post = _posts.Where(x => x.CodigoPost == id).ToList();

        if (!post.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
    
    [TestMethod()]
    public void BuscaPostPorCategoriaSucesso()
    {
        bool sucesso = false;
        int id = 1;

        var post = _posts.Where(x => x.Categoria.CodigoCategoria == id).ToList();

        if (!post.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaPostPorCategoriaErroNulo()
    {
        bool sucesso = false;
        int id = 2;

        var post = _posts.Where(x => x.Categoria.CodigoCategoria == id).ToList();

        if (!post.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
}
