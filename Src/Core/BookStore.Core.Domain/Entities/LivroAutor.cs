namespace BookStore.Core.Domain.Entities
{
    public class LivroAutor
    {
        public LivroAutor(int codigoLivro, int codigoAutor)
        {
            CodigoLivro = codigoLivro;
            CodigoAutor = codigoAutor;
        }

        public int CodigoLivro { get; private set; }
        public virtual Livro Livro { get; private set; }
        public int CodigoAutor { get; private set; }
        public virtual Autor Autor { get; private set; }
    }
}
