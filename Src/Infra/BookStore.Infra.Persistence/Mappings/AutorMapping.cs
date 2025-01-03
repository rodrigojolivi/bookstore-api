using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Persistence.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(x => x.Codigo);

            //builder.Property(x => x.Codigo).HasColumnName("CodAu");

            builder.Property(x => x.Nome)
                .HasColumnType("VARCHAR(40)");

            builder.ToTable("Autor");
        }
    }
}
