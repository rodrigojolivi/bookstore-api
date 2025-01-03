using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infra.Persistence.Repositories
{
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(BookStoreContext context)
            : base(context)
        {

        }

        public async Task<Livro> FindLivroByCodigoAsync(int codigo)
        {
            return await _dbSet.Where(x => x.Codigo == codigo)
                .Include(x => x.LivroAutores).ThenInclude(x => x.Autor)
                .Include(x => x.LivroAssuntos).ThenInclude(x => x.Assunto)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Livro>> GetReportAsync()
        {
            return await _dbSet.AsNoTracking()
                .Include(x => x.LivroAutores).ThenInclude(x => x.Autor)
                .Include(x => x.LivroAssuntos).ThenInclude(x => x.Assunto)
                .ToListAsync();
        }
    }
}
