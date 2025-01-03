using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Persistence.Mappings
{
    public class LivroAutorMapping : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.HasKey(x => new { x.CodigoLivro, x.CodigoAutor });

            builder.Property(x => x.CodigoLivro).ValueGeneratedNever();

            builder.Property(x => x.CodigoAutor).ValueGeneratedNever();

            //builder.Property(x => x.CodigoLivro).HasColumnName("Livro_CodL");

            //builder.Property(x => x.CodigoAutor).HasColumnName("Autor_CodAu");

            builder.HasOne(x => x.Livro).WithMany(x => x.LivroAutores)
                .HasForeignKey(x => x.CodigoLivro);

            builder.HasOne(x => x.Autor).WithMany(x => x.LivroAutores)
                .HasForeignKey(x => x.CodigoAutor);

            //builder.HasOne(x => x.Livro).WithMany(x => x.LivroAutores)
            //    .HasForeignKey(x => x.CodigoLivro).HasConstraintName("Livro_Autor_FKIndex1");

            //builder.HasOne(x => x.Autor).WithMany(x => x.LivroAutores)
            //    .HasForeignKey(x => x.CodigoAutor).HasConstraintName("Livro_Autor_FKIndex2");

            builder.ToTable("Livro_Autor");
        }
    }
}
