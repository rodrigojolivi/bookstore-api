using System.Reflection;
using BookStore.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infra.Persistence.Contexts
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<LivroAssunto> LivroAssuntos { get; set; }
        public DbSet<LivroAutor> LivroAutores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
