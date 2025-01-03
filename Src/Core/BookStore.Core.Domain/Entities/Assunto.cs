
namespace BookStore.Core.Domain.Entities
{
    public class Assunto
    {
        public Assunto(string descricao)
        {
            Descricao = descricao;
        }

        public int Codigo { get; private set; }
        public string Descricao { get; private set; }
        public IList<LivroAssunto> LivroAssuntos { get; private set; }

        public void Update(string descricao)
        {
            Descricao = descricao;
        }
    }
}
