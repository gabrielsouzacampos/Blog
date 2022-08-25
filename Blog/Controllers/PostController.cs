using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

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
                .Include(x => x.Tags)
                .Select(x => new ListaPostsViewModel{
                    CodigoPost = x.CodigoPost,
                    Titulo = x.Titulo,
                    DescricaoPost = x.DescricaoPost,
                    DataCadastro = x.DataCadastro,
                    Categoria = x.Categoria.NomeCategoria,
                    Autor = $"{x.Usuario.NomeUsuario} {x.Usuario.Email}",
                    Tags = x.Tags
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
                .Include(x => x.Tags)
                .Include(x => x.Usuario)
                .ThenInclude(x => x.Funcoes)
                .Include(x => x.Categoria)
                .Select(x => new ListaPostsViewModel{
                    CodigoPost = x.CodigoPost,
                    Titulo = x.Titulo,
                    DescricaoPost = x.DescricaoPost,
                    DataCadastro = x.DataCadastro,
                    Categoria = x.Categoria.NomeCategoria,
                    Autor = $"{x.Usuario.NomeUsuario} {x.Usuario.Email}",
                    Tags = x.Tags
                })
                .FirstOrDefaultAsync(x => x.CodigoPost.Equals(id));

            if (post == null)
                return NotFound(new ResultadoViewModel<Post>("Conteúdo não encontrado."));

            return Ok(new ResultadoViewModel<ListaPostsViewModel>(post));
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
                .Include(x => x.Tags)
                .Where(x => x.Categoria.NomeCategoria.Equals(categoria))
                .Select(x => new ListaPostsViewModel{
                    CodigoPost = x.CodigoPost,
                    Titulo = x.Titulo,
                    DescricaoPost = x.DescricaoPost,
                    DataCadastro = x.DataCadastro,
                    Categoria = x.Categoria.NomeCategoria,
                    Autor = $"{x.Usuario.NomeUsuario} {x.Usuario.Email}",
                    Tags = x.Tags
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

    [HttpPost("v1/posts")]
    [Authorize(Roles = "Autor")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Post([FromBody]EditorPostViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));
        
        var usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoUsuario.Equals(model.CodigoUsuario));

        if(usuario == null)
            return NotFound(new ResultadoViewModel<Usuario>("Autor não encontrado."));

        var categoria = await _context.Categorias.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoCategoria.Equals(model.CodigoCategoria));

        if(categoria == null)
            return NotFound(new ResultadoViewModel<Categoria>("Categoria não encontrada."));

        var post = new Post {
            CDCATEGORIA = model.CodigoCategoria,
            CDUSUARIO = model.CodigoUsuario,
            Titulo = model.Titulo,
            Sumario = model.Sumario,
            Corpo = model.Corpo,
            DescricaoPost = model.DescricaoPost
        };

        try
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return Created($"v1/posts/{post.CodigoPost}", new ResultadoViewModel<Post>(post));
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultadoViewModel<Post>("Não foi possivel realizar sua solicitação.")
            );
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Post>("Falha interna na aplicação."));
        }
    }
    
    [HttpPost("v1/posts/tag")]
    [Authorize(Roles = "Autor")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> InsereTagPost([FromBody]EditorPostTagViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var post = await _context.Posts.Include(x => x.Tags).AsNoTracking().FirstOrDefaultAsync(x => x.CodigoPost.Equals(model.CodigoPost));

        if(post == null)
            return NotFound(new ResultadoViewModel<Post>("Post não encontrado."));

        foreach (var item in model.CodigoTags)
        {
            var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoTag.Equals(item));

            if(tag == null)
                return NotFound(new ResultadoViewModel<Post>($"Tag com o código { item } não encontrado."));

            var postTag = await _context
                .Posts
                .Include(x => x.Tags.Where(t => t.CodigoTag.Equals(item)))
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CodigoPost.Equals(model.CodigoPost));
            
            if(!postTag.Tags.Count.Equals(0))
                return NotFound(new ResultadoViewModel<Post>($"Tag com o código { item } já existente."));
        }

        try
        {
            foreach (var item in model.CodigoTags)
            {
                AlteraPostTag(model.CodigoPost, item, "I");
            }

            return Created($"v1/posts/{model.CodigoPost}", new ResultadoViewModel<Post>(post));
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultadoViewModel<Post>("Não foi possivel realizar sua solicitação.")
            );
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Post>("Falha interna na aplicação."));
        }
    }

    [HttpDelete("v1/posts/tag")]
    [Authorize(Roles = "Autor")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletaTagPost([FromBody]EditorPostTagViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultadoViewModel<string>(ModelState.GetErrors()));

        var post = await _context.Posts.Include(x => x.Tags).AsNoTracking().FirstOrDefaultAsync(x => x.CodigoPost.Equals(model.CodigoPost));

        if(post == null)
            return NotFound(new ResultadoViewModel<Post>("Post não encontrado."));

        foreach (var item in model.CodigoTags)
        {
            var tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.CodigoTag.Equals(item));

            if(tag == null)
                return NotFound(new ResultadoViewModel<Post>($"Tag com o código { item } não encontrado."));

            var postTag = await _context
                .Posts
                .Include(x => x.Tags.Where(t => t.CodigoTag.Equals(item)))
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CodigoPost.Equals(model.CodigoPost));
            
            if(postTag.Tags.Count.Equals(0))
                return NotFound(new ResultadoViewModel<Post>($"Não existe tag com o código { item }."));
        }

        try
        {
            foreach (var item in model.CodigoTags)
            {
                AlteraPostTag(model.CodigoPost, item, "D");
            }

            return Ok(new ResultadoViewModel<Post>(post));
        }
        catch (DbUpdateException)
        {
            return StatusCode(
                500,
                new ResultadoViewModel<Post>("Não foi possivel realizar sua solicitação.")
            );
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Post>("Falha interna na aplicação."));
        }
    }

    private async void AlteraPostTag(int codigoPost, int codigoTag, string strOperacao)
    {
            OracleParameter paramCodigoPost = new OracleParameter("codigoPost", codigoPost);
            OracleParameter paramCodigoTag = new OracleParameter("codigoTag", codigoTag);
            OracleParameter paramStrOperacao = new OracleParameter("strOperacao", strOperacao);
            
            _context.Database
                .ExecuteSqlRaw(
                    "BEGIN BLOG.PROC_POST_TAG(:codigoPost, :codigoTag, :strOperacao); END;",
                    paramCodigoPost,
                    paramCodigoTag,
                    paramStrOperacao
                );
                
            await _context.SaveChangesAsync();
    }
}