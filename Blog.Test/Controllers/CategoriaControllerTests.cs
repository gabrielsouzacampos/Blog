using Blog.Models;

namespace Blog.Controllers.Tests;

[TestClass()]
public class CategoriaControllerTests
{
    private readonly List<Categoria> _categorias = new()
    {
        new Categoria
        {
            CodigoCategoria = 1,
            NomeCategoria = "FrontEnd",
            DescricaoCategoria = "Categoria de frontend"
        },
        new Categoria
        {
            CodigoCategoria = 2,
            NomeCategoria = "BackEnd",
            DescricaoCategoria = "Categoria de backend"
        },
        new Categoria
        {
            CodigoCategoria = 3,
            NomeCategoria = "Full stack",
            DescricaoCategoria = "Categoria de full stack"
        },
    };

    [TestMethod()]
    public void BuscaCategoriaPorCodigoSucesso()
    {
        bool sucesso = false;
        int id = 1;

       var categoria = _categorias.Where(x => x.CodigoCategoria == id).ToList();

        if (!categoria.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaCategoriaPorCodigoErroNulo()
    {
        bool sucesso = false;
        int id = 4;

        var categoria = _categorias.Where(x => x.CodigoCategoria == id).ToList();

        if (!categoria.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
}
