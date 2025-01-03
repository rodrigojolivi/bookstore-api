
namespace BookStore.Core.Domain.Entities
{
    public class Autor
    {
        public Autor(string nome)
        {
            Nome = nome;
        }

        public int Codigo { get; private set; }
        public string Nome { get; private set; }
        public IList<LivroAutor> LivroAutores { get; private set; }

        public void Update(string nome)
        {
            Nome = nome;
        }
    }
}
