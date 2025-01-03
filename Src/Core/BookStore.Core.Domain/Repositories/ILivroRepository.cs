using BookStore.Core.Domain.Entities;

namespace BookStore.Core.Domain.Repositories
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<Livro> FindLivroByCodigoAsync(int codigo);
        Task<IEnumerable<Livro>> GetReportAsync();
    }
}
