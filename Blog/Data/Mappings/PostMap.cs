using Blog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Data.Mappings;

public class PostMap : IEntityTypeConfiguration<Post>
{
    public void Configure(
        EntityTypeBuilder<Post> builder
    )
    {
        builder
            .HasOne(x => x.Usuario)
            .WithMany(x => x.Posts)
            .HasConstraintName("FK_POST_TEM_USUARIO")
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.Categoria)
            .WithMany(x => x.Posts)
            .HasConstraintName("FK_POST_TEM_CATEGORIA")
            .OnDelete(DeleteBehavior.Cascade);

        builder
                .HasMany(x => x.Tags)
                .WithMany(x => x.Posts)
                .UsingEntity<Dictionary<string, object>>(
                    "POSTTAG",
                    tag => tag
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("CDTAG")
                        .HasConstraintName("FK_TEM_TAG")
                        .OnDelete(DeleteBehavior.Cascade),
                    post => post
                        .HasOne<Post>()
                        .WithMany()
                        .HasForeignKey("CDPOST")
                        .HasConstraintName("FK_TEM_POST")
                        .OnDelete(DeleteBehavior.Cascade));
    }
}
