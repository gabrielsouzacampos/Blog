using Blog.Models;

namespace Blog.Controllers.Tests;

[TestClass()]
public class TagControllerTests
{
    private readonly List<Tag> _tags = new()
    {
        new Tag
        {
            CodigoTag = 1,
            NomeTag = "FrontEnd",
            DescricaoTag = "FrontEnd"
        },
        new Tag
        {
            CodigoTag = 2,
            NomeTag = "BackEnd",
            DescricaoTag = "BackEnd"
        },
        new Tag
        {
            CodigoTag = 3,
            NomeTag = "Full Stack",
            DescricaoTag = "Full Stack"
        },
    };
    
    [TestMethod()]
    public void BuscaTagPorCodigoSucesso()
    {
        bool sucesso = false;
        int id = 1;

        var tag = _tags.Where(x => x.CodigoTag == id).ToList();

        if (!tag.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaTagPorCodigoErroNulo()
    {
        bool sucesso = false;
        int id = 4;

        var tag = _tags.Where(x => x.CodigoTag == id).ToList();

        if (!tag.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
}
