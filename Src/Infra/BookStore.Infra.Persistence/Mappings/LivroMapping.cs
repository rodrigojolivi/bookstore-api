using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Persistence.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(x => x.Codigo);

            //builder.Property(x => x.Codigo).HasColumnName("CodL");

            builder.Property(x => x.Titulo)
                .HasColumnType("VARCHAR(40)");

            builder.Property(x => x.Editora)
                .HasColumnType("VARCHAR(40)");

            builder.Property(x => x.AnoPublicacao)
                .HasColumnType("VARCHAR(4)");

            builder.ToTable("Livro");
        }
    }
}
