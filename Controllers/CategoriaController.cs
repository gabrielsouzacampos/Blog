using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Categorias;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers;

[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly BlogDataContext _context;
    private readonly IMemoryCache _cache;

    public CategoriaController(BlogDataContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet("v1/categorias")]
    public IActionResult Get()
    {
        try
        {
            var categorias = _cache.GetOrCreate("CategoriasCache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return GetCategorias();
            });

            return Ok(new ResultadoViewModel<List<Categoria>>(categorias));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<List<Categoria>>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<List<Categoria>>("Falha interna na aplicação."));
        }
    }

    [HttpGet("v1/categorias/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CodigoCategoria == id);

            if(categoria == null)
                return NotFound(new ResultadoViewModel<Categoria>("Conteúdo não encontrado."));

            return Ok(new ResultadoViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Falha interna na aplicação."));
        }
    }

    [HttpPost("v1/categorias/")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PostAsync([FromBody]EditorCategoriaViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoViewModel<List<Categoria>>(ModelState.GetErrors()));
            }

            var categoria = new Categoria{
                NomeCategoria = model.NomeCategoria,
                DescricaoCategoria = model.DescricaoCategoria.ToLower()
            };

            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return Created($"v1/categorias/{categoria.CodigoCategoria}", new ResultadoViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Falha interna na aplicação."));
        }
    }

    [HttpPut("v1/categorias/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutAsync([FromRoute]int id,[FromBody]EditorCategoriaViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoViewModel<List<Categoria>>(ModelState.GetErrors()));
            }
                
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CodigoCategoria == id);

            if(categoria == null)
                return NotFound(new ResultadoViewModel<Categoria>("Conteúdo não encontrado."));
 
            categoria.NomeCategoria = model.NomeCategoria;
            categoria.DescricaoCategoria = model.DescricaoCategoria;

            _context.Categorias.Update(categoria);
            await _context.SaveChangesAsync();

            return Ok(new ResultadoViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Falha interna na aplicação."));
        }
    }

    [HttpDelete("v1/categorias/{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteAsync([FromRoute]int id)
    {
        try
        {
            var categoria = await _context.Categorias.FirstOrDefaultAsync(x => x.CodigoCategoria == id);

            if(categoria == null)
                return NotFound(new ResultadoViewModel<Categoria>("Conteúdo não encontrado."));

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new ResultadoViewModel<Categoria>(categoria));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Categoria>("Falha interna na aplicação."));
        }
    }
    
    private List<Categoria> GetCategorias()
    {
        return _context.Categorias.ToList();
    }
}