using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;

namespace BookStore.Infra.Persistence.Repositories
{
    public class AssuntoRepository : Repository<Assunto>, IAssuntoRepository
    {
        public AssuntoRepository(BookStoreContext context)
            : base(context)
        {

        }
    }
}
