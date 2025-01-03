using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.Infra.Persistence.Mappings
{
    public class AssuntoMapping : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.HasKey(x => x.Codigo);

            //builder.Property(x => x.Codigo).HasColumnName("CodAs");

            builder.Property(x => x.Descricao)
                .HasColumnType("VARCHAR(20)");

            builder.ToTable("Assunto");
        }
    }
}
