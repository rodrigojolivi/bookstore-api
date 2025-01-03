using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;

namespace BookStore.Infra.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookStoreContext _context;

        public UnitOfWork(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
