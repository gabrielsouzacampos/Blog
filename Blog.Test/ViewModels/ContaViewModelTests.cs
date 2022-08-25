using Blog.ViewModels;
using Blog.ViewModels.Contas;
using System.ComponentModel.DataAnnotations;

namespace Blog.Test.ViewModels;

[TestClass]
public class ContaViewModelTests
{
    [TestMethod()]
    public void EditorFuncaoUsuarioViewModelSucesso()
    {
        EditorFuncaoUsuarioViewModel model = new() {
            CodigoFuncao = 1,
            CodigoUsuario = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }

    [TestMethod()]
    public void EditorFuncaoUsuarioViewModelErroFuncaoNula()
    {
        EditorFuncaoUsuarioViewModel model = new()
        {
            CodigoUsuario = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void EditorFuncaoUsuarioViewModelErroFuncaoInexistente()
    {
        EditorFuncaoUsuarioViewModel model = new()
        {
            CodigoFuncao = 0,
            CodigoUsuario = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void EditorFuncaoViewModelUsuarioErroUsuarioNulo()
    {
        EditorFuncaoUsuarioViewModel model = new()
        {
            CodigoFuncao = 0,
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void EnviarImagemViewModelSucesso()
    {
        EnviarImagemViewModel model = new()
        {
            Base64Image = "Imagem base 64"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }

    [TestMethod()]
    public void EnviarImagemViewModelErroImagemNula()
    {
        EnviarImagemViewModel model = new()
        {
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void LoginViewModelSucesso()
    {
        LoginViewModel model = new()
        {
            Email = "gabriel.s.campos@hotmail.com",
            Senha = "Senha123"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }
    
    [TestMethod()]
    public void LoginViewModelErroEmailInvalido()
    {
        LoginViewModel model = new()
        {
            Email = "gabriel.s.campos",
            Senha = "Senha123"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void LoginViewModelErroEmailNulo()
    {
        LoginViewModel model = new()
        {
            Senha = "Senha123"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void LoginViewModelErroSenhaNula()
    {
        LoginViewModel model = new()
        {
            Email = "gabriel.s.campos@hotmail.com"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void RegistroViewModelSucesso()
    {
        RegistroViewModel model = new()
        {
            NomeUsuario = "Gabriel Souza Campos",
            Email = "gabriel.s.campos@hotmail.com",
            CodigoFuncao = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, true);
    }

    [TestMethod()]
    public void RegistroViewModelErroNomeNulo()
    {
        RegistroViewModel model = new()
        {
            Email = "gabriel.s.campos@hotmail.com",
            CodigoFuncao = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }

    [TestMethod()]
    public void RegistroViewModelErroEmailNulo()
    {
        RegistroViewModel model = new()
        {
            NomeUsuario = "Gabriel Souza Campos",
            CodigoFuncao = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void RegistroViewModelErroEmailInvalido()
    {
        RegistroViewModel model = new()
        {
            NomeUsuario = "Gabriel Souza Campos",
            Email = "gabriel.s.campos",
            CodigoFuncao = 1
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void RegistroViewModelErroCodigoFuncaoNula()
    {
        RegistroViewModel model = new()
        {
            NomeUsuario = "Gabriel Souza Campos",
            Email = "gabriel.s.campos"
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
    
    [TestMethod()]
    public void RegistroViewModelErroCodigoFuncaoInvalida()
    {
        RegistroViewModel model = new()
        {
            NomeUsuario = "Gabriel Souza Campos",
            Email = "gabriel.s.campos",
            CodigoFuncao = 0
        };

        var ctx = new ValidationContext(model);
        var resultados = new List<ValidationResult>();

        var validator = Validator.TryValidateObject(model, ctx, resultados, true);

        Assert.AreEqual(validator, false);
    }
}