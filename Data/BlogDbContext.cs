using Blog.Models;
using Blog.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class BlogDataContext : DbContext
{
    public BlogDataContext(DbContextOptions<BlogDataContext> options) : base(options) { }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Funcao> Funcoes { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<PFuncaoUsuario> PFuncaoUsuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("BLOG");
        modelBuilder.ApplyConfiguration(new UsuarioMap());
        modelBuilder.ApplyConfiguration(new PostMap());
        modelBuilder.Entity<PFuncaoUsuario>().HasNoKey();
    }
}
