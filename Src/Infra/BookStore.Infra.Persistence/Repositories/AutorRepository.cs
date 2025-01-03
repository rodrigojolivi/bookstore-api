using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;

namespace BookStore.Infra.Persistence.Repositories
{
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(BookStoreContext context)
            : base(context)
        {

        }
    }
}
