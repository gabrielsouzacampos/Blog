using System.Data;
using System.Text.RegularExpressions;
using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.Services;
using Blog.ViewModels;
using Blog.ViewModels.Contas;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;
using SecureIdentity.Password;

namespace Blog.Controllers;

[ApiController]
public class ContaController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly EmailService _emailService;
    private readonly BlogDataContext _context;

    public ContaController(
        TokenService tokenService,
        EmailService emailService,
        BlogDataContext context
    )
    {
        _tokenService = tokenService;
        _emailService = emailService;
        _context = context;
    }

    [HttpPost("v1/contas")]
    public async Task<IActionResult> Post([FromBody] RegistroViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var funcao = await _context.Funcoes.FirstOrDefaultAsync(x => x.CodigoFuncao.Equals(model.CodigoFuncao));

        if(funcao == null)
            return NotFound(new ResultadoViewModel<Funcao>("Função não encontrada."));
        
        var senha = PasswordGenerator.Generate(25);

        var usuario = new Usuario
        {
            NomeUsuario = model.NomeUsuario,
            Email = model.Email,
            DescricaoUsuario = $"Descricao usuario: {model.NomeUsuario}",
            Bio = $"Bio usuario: {model.NomeUsuario}",
            Imagem = $"Imagem usuario: {model.NomeUsuario}",
            PasswordHash = PasswordHasher.Hash(senha),
        };

        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            AlteraFuncaoUsuario(usuario.CodigoUsuario, model.CodigoFuncao, "I");

            _emailService.Enviar(
                usuario.NomeUsuario,
                usuario.Email,
                "Bem vindo ao Blog!",
                $"Sua senha é {senha}"
            );

            return Ok(new ResultadoViewModel<dynamic>(data: new { usuario = usuario.Email }));
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultadoViewModel<Usuario>("Não foi possivel realizar sua solicitação.")
            );
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Usuario>("Falha interna na aplicação."));
        }
    }

    [HttpPost("v1/contas/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var usuario = await _context.Usuarios
            .AsNoTracking()
            .Include(x => x.Funcoes)
            .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (usuario == null)
            return StatusCode(401, new ResultadoViewModel<string>("Usuário ou senha inválidos"));

        if (!PasswordHasher.Verify(usuario.PasswordHash, model.Senha))
            return StatusCode(401, new ResultadoViewModel<string>("Usuário ou senha inválidos"));

        try
        {
            var token = _tokenService.GerarToken(usuario);

            return Ok(new ResultadoViewModel<string>(token, null));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Usuario>("Falha interna na aplicação."));
        }
    }

    [Authorize]
    [HttpPost("v1/contas/enviar-imagem")]
    public async Task<IActionResult> EnviarImagem([FromBody] EnviarImagemViewModel model)
    {
        var nomeArquivo = $"{Guid.NewGuid()}.jpg";

        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");
        var bytes = Convert.FromBase64String(data);

        try
        {
            await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{nomeArquivo}", bytes);
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultadoViewModel<string>("Falha interna no servidor"));
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(
            x => x.Email.Equals(User.Identity.Name)
        );

        if (usuario == null)
            return NotFound(new ResultadoViewModel<Usuario>("Usuário não encontrado"));

        usuario.Imagem = $"https://localhost:0000/images/{nomeArquivo}";

        try
        {
            _context.Usuarios.Update(usuario);
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultadoViewModel<string>("Falha interna no servidor"));
        }

        return Ok(new ResultadoViewModel<string>("Imagem alterada com sucesso!", null));
    }

    [Authorize("Admin")]
    [HttpPost("v1/contas/insere-funcao")]
    public async Task<IActionResult> InserirFuncaoUsuario([FromBody]EditorFuncaoUsuarioViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var funcao = await _context.Funcoes.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoFuncao.Equals(model.CodigoFuncao));

        if(funcao == null)
            return NotFound(new ResultadoViewModel<Funcao>("Função não encontrada."));

        var usuario = await _context.Usuarios.Include(x => x.Funcoes).AsNoTracking().FirstOrDefaultAsync(x => x.CodigoUsuario.Equals(model.CodigoUsuario));
        
        if(usuario == null)
            return NotFound(new ResultadoViewModel<Usuario>("Usuário não encontrado."));

        foreach (var funcaoUsuario in usuario.Funcoes)
        {
            if(funcaoUsuario.CodigoFuncao.Equals(funcao.CodigoFuncao))
                return BadRequest(new ResultadoViewModel<Funcao>("Está função já foi designada a este usuário."));
        }

        AlteraFuncaoUsuario(model.CodigoUsuario, model.CodigoFuncao, "I");

        return Ok(new ResultadoViewModel<Funcao>(funcao));
    }

    [Authorize("Admin")]
    [HttpDelete("v1/contas/deleta-funcao")]
    public async Task<IActionResult> DeletaFuncaoUsuario([FromBody]EditorFuncaoUsuarioViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var funcao = await _context.Funcoes.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoFuncao.Equals(model.CodigoFuncao));

        if(funcao == null)
            return NotFound(new ResultadoViewModel<Funcao>("Função não encontrada."));

        var usuario = await _context.Usuarios.Include(x => x.Funcoes).AsNoTracking().FirstOrDefaultAsync(x => x.CodigoUsuario.Equals(model.CodigoUsuario));
        
        if(usuario == null)
            return NotFound(new ResultadoViewModel<Usuario>("Usuário não encontrado."));

        var existeFuncao = false;

        foreach (var funcaoUsuario in usuario.Funcoes)
        {
            if(funcaoUsuario.CodigoFuncao.Equals(funcao.CodigoFuncao))
            {
                existeFuncao = true;
                break;
            }
            else
                existeFuncao = false;
        }

        if (!existeFuncao)
            return BadRequest(new ResultadoViewModel<Funcao>("Função não atribuida ao usuário."));

        AlteraFuncaoUsuario(model.CodigoUsuario, model.CodigoFuncao, "D");

        return Ok(new ResultadoViewModel<Funcao>(funcao));
    }

    private async void AlteraFuncaoUsuario(int codigoUsuario, int codigoFuncao, string strOperacao)
    {
            OracleParameter paramCodigoUsuario = new OracleParameter("codigoUsuario", codigoUsuario);
            OracleParameter paramCodigoFuncao = new OracleParameter("codigoFuncao", codigoFuncao);
            OracleParameter paramStrOperacao = new OracleParameter("strOperacao", strOperacao);
            
            _context.Database
                .ExecuteSqlRaw(
                    "BEGIN BLOG.PROC_POST_TAG(:codigoPost, :codigoTag, :strOperacao); END;",
                    paramCodigoUsuario,
                    paramCodigoFuncao,
                    paramStrOperacao
                );
                
            await _context.SaveChangesAsync();
    }
}
