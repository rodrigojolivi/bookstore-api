using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Livros
{
    public class GetLivrosQuery : IRequest<Response>
    {

    }

    public class GetLivrosHandler : Validator, IRequestHandler<GetLivrosQuery, Response>
    {
        private readonly ILivroRepository _livroRepository;

        public GetLivrosHandler(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<Response> Handle(GetLivrosQuery request, CancellationToken cancellationToken)
        {
            var query = await _livroRepository.GetAsync();

            var result = query.Select(x => new GetLivrosResponse
            {
                Codigo = x.Codigo,
                Titulo = x.Titulo,
                AnoPublicacao = x.AnoPublicacao
            });

            return Response.Success(result);
        }
    }

    public class GetLivrosResponse
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string AnoPublicacao { get; set; }
    }
}
