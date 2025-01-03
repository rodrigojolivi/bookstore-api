using System.Linq.Expressions;
using BookStore.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Infra.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetView(string view)
        {
            //using (var file = new StreamReader(view))
            //{
            //    view = await file.ReadToEndAsync();   
            //}

            var query = @"SELECT [l2].[Codigo], [l2].[AnoPublicacao], [l2].[Edicao], [l2].[Editora], [l2].[Titulo], [s].[CodigoLivro], [s].[CodigoAutor], [s].[Codigo], [s].[Nome], [s0].[CodigoLivro], [s0].[CodigoAssunto], [s0].[Codigo], [s0].[Descricao]
FROM (
    SELECT TOP(1) [l].[Codigo], [l].[AnoPublicacao], [l].[Edicao], [l].[Editora], [l].[Titulo]
    FROM [Livro] AS [l]
    WHERE [l].[Codigo] = @__codigo_0
) AS [l2]
LEFT JOIN (
    SELECT [l0].[CodigoLivro], [l0].[CodigoAutor], [a].[Codigo], [a].[Nome]
    FROM [Livro_Autor] AS [l0]
    INNER JOIN [Autor] AS [a] ON [l0].[CodigoAutor] = [a].[Codigo]
) AS [s] ON [l2].[Codigo] = [s].[CodigoLivro]
LEFT JOIN (
    SELECT [l1].[CodigoLivro], [l1].[CodigoAssunto], [a0].[Codigo], [a0].[Descricao]
    FROM [Livro_Assunto] AS [l1]
    INNER JOIN [Assunto] AS [a0] ON [l1].[CodigoAssunto] = [a0].[Codigo]
) AS [s0] ON [l2].[Codigo] = [s0].[CodigoLivro]
ORDER BY [l2].[Codigo], [s].[CodigoLivro], [s].[CodigoAutor], [s].[Codigo], [s0].[CodigoLivro], [s0].[CodigoAssunto]";

            return await _context.Database.SqlQueryRaw<TEntity>($"SELECT * FROM Relatorio", query).ToListAsync();
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).FirstOrDefaultAsync();
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }       
    }
}
