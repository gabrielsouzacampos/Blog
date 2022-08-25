using Blog.ViewModels;
using Blog.ViewModels.Posts;
using System.ComponentModel.DataAnnotations;

namespace Blog.Test.ViewModels;

[TestClass]
public class PostViewModelTests
{
    [TestMethod()]
    public void EditorPostTagViewModelSucesso()
    {
        EditorPostTagViewModel model = new()
        {
            CodigoPost = 1,
            CodigoTags = new int[ 1 ]
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }
    
    [TestMethod()]
    public void EditorPostTagViewModelErroCodigoFuncaoNulo()
    {
        EditorPostTagViewModel model = new()
        {
            CodigoPost = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void EditorPostViewModelSucesso()
    {
        EditorPostViewModel model = new()
        {
            CodigoCategoria = 1,
            CodigoUsuario = 1,
            Corpo = "Corpo de post",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroCorpoNulo()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroCorpoVazio()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = string.Empty,
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroCorpoMaxCaracteres()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                    aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroDescricaoNula()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroDescricaoVazia()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = string.Empty,
            Sumario = "Sumario",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroDescricaoMaxCaracteres()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                            aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroSumarioNulo()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroSumarioVazio()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Sumario = string.Empty,
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroSumarioMaxCaracteres()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Sumario = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            Titulo = "Titulo"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroTituloNulo()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroTituloVazio()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = string.Empty
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void EditorPostViewModelErroTituloMaxCaracteres()
    {
        EditorPostViewModel model = new()
        {
            CodigoUsuario = 1,
            CodigoCategoria = 1,
            Corpo = "Corpo",
            DescricaoPost = "Descrição do post",
            Sumario = "Sumario",
            Titulo = @"aaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                      aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
}
