using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Persistence.Mappings
{
    public class LivroAssuntoMapping : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.HasKey(x => new { x.CodigoLivro, x.CodigoAssunto });

            builder.Property(x => x.CodigoLivro).ValueGeneratedNever();

            builder.Property(x => x.CodigoAssunto).ValueGeneratedNever();

            //builder.Property(x => x.CodigoLivro).HasColumnName("Livro_CodL");

            //builder.Property(x => x.CodigoAssunto).HasColumnName("Assunto_CodAs");

            builder.HasOne(x => x.Livro).WithMany(x => x.LivroAssuntos)
                .HasForeignKey(x => x.CodigoLivro);

            builder.HasOne(x => x.Assunto).WithMany(x => x.LivroAssuntos)
                .HasForeignKey(x => x.CodigoAssunto);

            //builder.HasOne(x => x.Livro).WithMany(x => x.LivroAssuntos)
            //    .HasForeignKey(x => x.CodigoLivro).HasConstraintName("Livro_Assunto_FKIndex1");

            //builder.HasOne(x => x.Assunto).WithMany(x => x.LivroAssuntos)
            //    .HasForeignKey(x => x.CodigoAssunto).HasConstraintName("Livro_Assunto_FKIndex2");

            builder.ToTable("Livro_Assunto");
        }
    }
}
