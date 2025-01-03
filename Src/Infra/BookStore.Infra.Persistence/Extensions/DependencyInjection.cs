using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;
using BookStore.Infra.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Infra.Persistence.Extensions
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<BookStoreContext>(options => options.UseSqlServer(connectionString));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAssuntoRepository, AssuntoRepository>();
            services.AddScoped<IAutorRepository, AutorRepository>();
            services.AddScoped<ILivroRepository, LivroRepository>();
        }

        public static async Task ExecuteMigrationPersistenceAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<BookStoreContext>();

            await context.Database.MigrateAsync();
        }
    }
}
