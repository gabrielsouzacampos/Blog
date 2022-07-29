using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModels;
using Blog.ViewModels.Tag;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Blog.Controllers;

[ApiController]
public class TagController : ControllerBase
{
    private readonly BlogDataContext _context;
    private readonly IMemoryCache _cache;

    public TagController(BlogDataContext context, IMemoryCache cache)
    {
        _context = context;
        _cache = cache;
    }

    [HttpGet("v1/tags")]
    public IActionResult Get()
    {
        try
        {
            var tags = _cache.GetOrCreate("TagsCache", entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return GetTags();
            });

            return Ok(new ResultadoViewModel<List<Tag>>(tags));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<List<Tag>>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<List<Tag>>("Falha interna na aplicação."));
        }
    }

    [HttpGet("v1/tags/{id:int}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        try
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.CodigoTag == id);

            if(tag == null)
                return NotFound(new ResultadoViewModel<Tag>("Conteúdo não encontrado."));

            return Ok(new ResultadoViewModel<Tag>(tag));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Falha interna na aplicação."));
        }
    }

    [HttpPost("v1/tags")]
    public async Task<IActionResult> Post ([FromBody]EditorTagViewModel model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResultadoViewModel<List<Tag>>(ModelState.GetErrors()));
            }

            var tag = new Tag {
                NomeTag = model.NomeTag,
                DescricaoTag = model.DescricaoTag
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return Created($"v1/categorias/{tag.CodigoTag}", new ResultadoViewModel<Tag>(tag));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Falha interna na aplicação."));
        }
    }

    [HttpPut("v1/tags/{id:int}")]
    public async Task<IActionResult> Put([FromRoute]int id, [FromBody]EditorTagViewModel model)
    {
        try
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.CodigoTag == id);

            if(tag == null)
                return NotFound(new ResultadoViewModel<Tag>("Conteúdo não encontrado."));

            tag.NomeTag = model.NomeTag;
            tag.DescricaoTag = model.DescricaoTag;

            _context.Tags.Update(tag);
            await _context.SaveChangesAsync();

            return Ok(new ResultadoViewModel<Tag>(tag));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Falha interna na aplicação."));
        }
    }

    [HttpDelete("v1/tags/{id:int}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]int id)
    {
        try
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.CodigoTag.Equals(id));

            if(tag == null)
                return NotFound(new ResultadoViewModel<Tag>("Conteúdo não encontrado."));

            _context.Remove(tag);
            await _context.SaveChangesAsync();

            return Ok(new ResultadoViewModel<Tag>(tag));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Não foi possivel realizar sua solicitação."));
        }
        catch (System.Exception)
        {
            return StatusCode(500, new ResultadoViewModel<Tag>("Falha interna na aplicação."));
        }
    }

    private List<Tag> GetTags()
    {
        return _context.Tags.ToList();
    }
}
