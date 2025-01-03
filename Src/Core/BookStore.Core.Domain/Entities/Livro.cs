

namespace BookStore.Core.Domain.Entities
{
    public class Livro
    {
        public Livro(string titulo, string editora, int edicao, string anoPublicacao)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;

            LivroAutores = [];
            LivroAssuntos = [];
        }

        public int Codigo { get; private set; }
        public string Titulo { get; private set; }
        public string Editora { get; private set; }
        public int Edicao { get; private set; }
        public string AnoPublicacao { get; private set; }
        public IList<LivroAutor> LivroAutores { get; private set; }
        public IList<LivroAssunto> LivroAssuntos { get; private set; }

        public void AddAutores(IEnumerable<int> codigoAutores)
        {
            LivroAutores.Clear();

            foreach (var codigoAutor in codigoAutores)
            {
                LivroAutores.Add(new LivroAutor(Codigo, codigoAutor));
            }
        }

        public void AddAssuntos(IEnumerable<int> codigoAssuntos)
        {
            LivroAssuntos.Clear();

            foreach (var codigoAssunto in codigoAssuntos)
            {
                LivroAssuntos.Add(new LivroAssunto(Codigo, codigoAssunto));
            }
        }

        public void Update(string titulo, string editora, int edicao, string anoPublicacao)
        {
            Titulo = titulo;
            Editora = editora;
            Edicao = edicao;
            AnoPublicacao = anoPublicacao;
        }
    }
}
