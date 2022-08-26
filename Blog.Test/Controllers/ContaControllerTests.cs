using Blog.Models;

namespace Blog.Controllers.Tests;

[TestClass()]
public class ContaControllerTests
{
    private readonly List<Funcao> _funcoes = new()
    {
        new Funcao
        {
            CodigoFuncao = 1,
            NomeFuncao = "Administrador",
            DescricaoFuncao = "Administra o sistema"
        },
        new Funcao
        {
            CodigoFuncao = 2,
            NomeFuncao = "Autor",
            DescricaoFuncao = "Cria os posts"
        },
        new Funcao
        {
            CodigoFuncao = 3,
            NomeFuncao = "Usuário",
            DescricaoFuncao = "Recupera acesso ao sistema"
        },
    };
    
    private readonly List<Usuario> _usuarios = new()
    {
        new Usuario
        {
            CodigoUsuario = 1,
            NomeUsuario = "Gabriel Souza Campos",
            Bio = "Administra o sistema",
            DescricaoUsuario = "Administra o sistema",
            Email = "gabriel.s.campos@hotmail.com",
            PasswordHash = "a1a2a3"
        },
        new Usuario
        {
            CodigoUsuario = 2,
            NomeUsuario = "Rosana Silva",
            Bio = "Cria os posts",
            DescricaoUsuario = "Cria os posts",
            Email = "teste@teste.com",
            PasswordHash = "a1a2a3"
        },
        new Usuario
        {
            CodigoUsuario = 3,
            NomeUsuario = "Heloisa Carvalho",
            Bio = "Administra o sistema",
            DescricaoUsuario = "Administra o sistema",
            Email = "teste1@teste.com",
            PasswordHash = "a1a2a3"
        },
        
    };

    [TestMethod()]
    public void BuscaCodigoFuncaoSucesso()
    {
        bool sucesso = false;
        int id = 1;

        var funcao = _funcoes.Where(x => x.CodigoFuncao == id).ToList();

        if (!funcao.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaCodigoFuncaoErroNulo()
    {
        bool sucesso = false;
        int id = 4;

        var funcao = _funcoes.Where(x => x.CodigoFuncao == id).ToList();

        if (!funcao.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
    
    [TestMethod()]
    public void BuscaUsuarioPorEmailSucesso()
    {
        bool sucesso = false;
        string email = "teste@teste.com";

        var usuario = _usuarios.Where(x => x.Email == email).ToList();

        if (!usuario.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(true, sucesso);
    }
    
    [TestMethod()]
    public void BuscaUsuarioPorEmailErroNulo()
    {
        bool sucesso = false;
        string email = "teste2@teste.com";

        var usuario = _usuarios.Where(x => x.Email == email).ToList();

        if (!usuario.Count.Equals(0))
            sucesso = true;

        Assert.AreEqual(false, sucesso);
    }
}
