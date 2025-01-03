using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Livros
{
    public class FindLivroByCodigoQuery : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class FindLivroByCodigoHandler : IRequestHandler<FindLivroByCodigoQuery, Response>
    {
        private readonly ILivroRepository _livroRepository;

        public FindLivroByCodigoHandler(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<Response> Handle(FindLivroByCodigoQuery request, CancellationToken cancellationToken)
        {
            var query = await _livroRepository.FindLivroByCodigoAsync(request.Codigo);

            if (query == null) return Response.Success();

            var result = new FindLivroByCodigoResponse
            {
                Codigo = query.Codigo,
                Titulo = query.Titulo,
                Editora = query.Editora,
                Edicao = query.Edicao,
                AnoPublicacao = query.AnoPublicacao,

                Autores = query.LivroAutores.Select(x => new FindLivroByCodigoResponse.AutoresResponse
                {
                    Codigo = x.Autor.Codigo,
                    Nome = x.Autor.Nome

                }).ToList(),

                Assuntos = query.LivroAssuntos.Select(x => new FindLivroByCodigoResponse.AssuntosResponse
                {
                    Codigo = x.Assunto.Codigo,
                    Descricao = x.Assunto.Descricao

                }).ToList()
            };

            return Response.Success(result);
        }
    }

    public class FindLivroByCodigoResponse
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public IList<AutoresResponse> Autores { get; set; }
        public IList<AssuntosResponse> Assuntos { get; set; }

        public class AutoresResponse
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
        }

        public class AssuntosResponse
        {
            public int Codigo { get; set; }
            public string Descricao { get; set; }
        }
    }
}
