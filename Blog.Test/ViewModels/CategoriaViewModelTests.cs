using Blog.ViewModels.Categorias;
using System.ComponentModel.DataAnnotations;

namespace Blog.Test.ViewModels;

[TestClass]
public class CategoriaViewModelTests
{
    [TestMethod()]
    public void ViewModelSucesso()
    {
        EditorCategoriaViewModel model = new()
        {
            NomeCategoria = "Backend",
            DescricaoCategoria = "Realizar o tratamento de dados da aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }

    [TestMethod]
    public void ViewModelComErroNomeNulo()
    {
        EditorCategoriaViewModel model = new()
        {
            DescricaoCategoria = "Realizar o tratamento de dados da aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod]
    public void ViewModelComErroNomeMaxCaracteres()
    {
        EditorCategoriaViewModel model = new()
        {
            NomeCategoria = "Ba",
            DescricaoCategoria = "Realizar o tratamento de dados da aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod]
    public void ViewModelComErroNomeMinCaracteres()
    {
        EditorCategoriaViewModel model = new()
        {
            NomeCategoria = "Aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
            DescricaoCategoria = "Realizar o tratamento de dados da aplicação."
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod]
    public void ViewModelComErroDescricaoNulo()
    {
        EditorCategoriaViewModel model = new()
        {
            NomeCategoria = "Backend"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
}
