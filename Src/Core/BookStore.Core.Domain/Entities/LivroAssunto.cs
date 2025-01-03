namespace BookStore.Core.Domain.Entities
{
    public class LivroAssunto 
    {
        public LivroAssunto(int codigoLivro, int codigoAssunto)
        {
            CodigoLivro = codigoLivro;
            CodigoAssunto = codigoAssunto;
        }

        public int CodigoLivro { get; private set; }
        public virtual Livro Livro { get; private set; }
        public int CodigoAssunto { get; private set; }
        public virtual Assunto Assunto { get; private set; }
    }
}
