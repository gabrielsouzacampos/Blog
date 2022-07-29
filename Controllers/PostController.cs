using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;

[ApiController]
public class PostController : ControllerBase
{
    private readonly BlogDataContext _context;
    
    public PostController(BlogDataContext context)
    {
        _context = context;
    }

    [HttpGet("v1/posts")]
    public async Task<IActionResult> Get(
        [FromQuery]int page = 0, 
        [FromQuery]int pageSize = 25
    )
    {
        try
        {
            var contador = await _context.Posts.AsNoTracking().CountAsync();

            var posts = await _context
                .Posts
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.Categoria)
                .Select(x => new ListaPostsViewModel{
                    CodigoPost = x.CodigoPost,
                    Titulo = x.Titulo,
                    DescricaoPost = x.DescricaoPost,
                    DataCadastro = x.DataCadastro,
                    Categoria = x.Categoria.NomeCategoria,
                    Autor = $"{x.Usuario.NomeUsuario} {x.Usuario.Email}"
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync();

            return Ok(new ResultadoViewModel<dynamic>(new {
                total = contador,
                page,
                pageSize,
                posts
            }));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<List<Post>>("Falha interna na aplicação."));
        }
    }

    [HttpGet("v1/posts/{id:int}")]
    public async Task<IActionResult> DetalhesAsync([FromRoute]int id)
    {
        try
        {
            var post = await _context
                .Posts
                .AsNoTracking()
                .Include(x => x.Usuario)
                .ThenInclude(x => x.Funcoes)
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.CodigoPost.Equals(id));

            if (post == null)
                return NotFound(new ResultadoViewModel<Post>("Conteúdo não encontrado."));

            return Ok(new ResultadoViewModel<Post>(post));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Post>("Falha interna na aplicação."));
        }
    }

    [HttpGet("v1/posts/{categoria}")]
    public async Task<IActionResult> BuscaPorCategoriaAsync(
        [FromRoute]string categoria,
        [FromQuery]int page = 0,
        [FromQuery]int pageSize = 25 
    )
    {
        try
        {
            var contador = await _context.Posts.AsNoTracking().CountAsync();
            
            var posts = await _context
                .Posts
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.Categoria)
                .Where(x => x.Categoria.NomeCategoria.Equals(categoria))
                .Select(x => new ListaPostsViewModel{
                    CodigoPost = x.CodigoPost,
                    Titulo = x.Titulo,
                    DescricaoPost = x.DescricaoPost,
                    DataCadastro = x.DataCadastro,
                    Categoria = x.Categoria.NomeCategoria,
                    Autor = $"{x.Usuario.NomeUsuario} {x.Usuario.Email}"
                })
                .Skip(page * pageSize)
                .Take(pageSize)
                .OrderByDescending(x => x.DataCadastro)
                .ToListAsync();

            return Ok(new ResultadoViewModel<dynamic>(new {
                total = contador,
                page,
                pageSize,
                posts
            }));
        }
        catch (System.Exception)
        {
          return StatusCode(500, new ResultadoViewModel<List<Post>>("Falha interna na aplicação."));
        }
    }
}