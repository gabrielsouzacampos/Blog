using Blog.ViewModels.Tag;
using System.ComponentModel.DataAnnotations;

namespace Blog.Test.ViewModels;

[TestClass]
public class TagViewModelTests
{
    [TestMethod]
    public void EditorTagViewModelSucesso()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = ".Net",
            DescricaoTag = "Framework para criação de aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroNomeNulo()
    {
        EditorTagViewModel model = new()
        {
            DescricaoTag = "Framework para criação de aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroNomeVazio()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = string.Empty,
            DescricaoTag = "Framework para criação de aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroNomeMinCaracteres()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = "Ab",
            DescricaoTag = "Framework para criação de aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroNomeMaxCaracteres()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            DescricaoTag = "Framework para criação de aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroDescricaoNula()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void EditorTagViewModelErroDescricaoVazia()
    {
        EditorTagViewModel model = new()
        {
            NomeTag = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            DescricaoTag = string.Empty
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
}