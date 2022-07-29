using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class UsuarioMap : IEntityTypeConfiguration<Usuario> {
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
            builder
                .HasMany(x => x.Funcoes)
                .WithMany(x => x.Usuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "FUNCAOUSUARIO",
                    role => role
                        .HasOne<Funcao>()
                        .WithMany()
                        .HasForeignKey("CDFUNCAO")
                        .HasConstraintName("FK_TEM_FUNCAO")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<Usuario>()
                        .WithMany()
                        .HasForeignKey("CDUSUARIO")
                        .HasConstraintName("FK_FUNCAO_TEM_USUARIO")
                        .OnDelete(DeleteBehavior.Cascade));
    }
}