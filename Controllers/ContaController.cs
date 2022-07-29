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

    [Authorize(Roles = "Admin")]
    [HttpPost("v1/contas")]
    public async Task<IActionResult> Post([FromBody] RegistroViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var senha = PasswordGenerator.Generate(25);

        var usuario = new Usuario
        {
            NomeUsuario = model.NomeUsuario,
            Email = model.Email,
            DescricaoUsuario = $"Descricao usuario: {model.NomeUsuario}",
            Bio = $"Bio usuario: {model.NomeUsuario}",
            Imagem = $"Imagem usuario: {model.NomeUsuario}",
            PasswordHash = PasswordHasher.Hash(senha)
        };

        try
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

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
            return StatusCode(
                500,
                new ResultadoViewModel<Usuario>("Falha interna na aplicação.")
            );
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
            return StatusCode(
                401,
                new ResultadoViewModel<string>("Usuário ou senha inválidos")
            );

        if (!PasswordHasher.Verify(usuario.PasswordHash, model.Senha))
            return StatusCode(
                401,
                new ResultadoViewModel<string>("Usuário ou senha inválidos")
            );

        try
        {
            var token = _tokenService.GerarToken(usuario);

            return Ok(new ResultadoViewModel<string>(token, null));
        }
        catch (System.Exception)
        {
            return StatusCode(
                500,
                new ResultadoViewModel<Usuario>("Falha interna na aplicação.")
            );
        }
    }
    
    [Authorize]
    [HttpPost("v1/contas/enviar-imagem")]
    public async Task<IActionResult> EnviarImagem([FromBody] EnviarImagemViewModel model)
    {
        var nomeArquivo = $"{Guid.NewGuid().ToString()}.jpg";

        var data = new Regex(@"^data:image\/[a-z]+;base64,").Replace(model.Base64Image, "");
        var bytes = Convert.FromBase64String(data);

        try
        {
            await System.IO.File.WriteAllBytesAsync($"wwwroot/images/{nomeArquivo}", bytes);
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<string>("Falha interna no servidor"));
        }

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Equals(User.Identity.Name));

        if (usuario == null)
            return NotFound(new ResultadoViewModel<Usuario>("Usuário não encontrado"));

        usuario.Imagem = $"https://localhost:0000/images/{nomeArquivo}";

        try
        {
            _context.Usuarios.Update(usuario);
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<string>("Falha interna no servidor"));
        }

        return Ok(new ResultadoViewModel<string>("Imagem alterada com sucesso!", null));
    }
}
