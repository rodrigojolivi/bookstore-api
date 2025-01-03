using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Autores
{
    public class GetAutoresQuery : IRequest<Response>
    {

    }

    public class GetAutoresHandler : Validator, IRequestHandler<GetAutoresQuery, Response>
    {
        private readonly IAutorRepository _autorRepository;

        public GetAutoresHandler(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<Response> Handle(GetAutoresQuery request, CancellationToken cancellationToken)
        {
            var query = await _autorRepository.GetAsync();

            var result = query.Select(x => new GetAutoresResponse
            {
                Codigo = x.Codigo,
                Nome = x.Nome
            });

            return Response.Success(result);
        }
    }

    public class GetAutoresResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
