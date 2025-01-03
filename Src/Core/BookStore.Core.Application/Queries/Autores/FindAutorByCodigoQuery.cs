using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Autores
{
    public class FindAutorByCodigoQuery : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class FindAutorByCodigoHandler : IRequestHandler<FindAutorByCodigoQuery, Response>
    {
        private readonly IAutorRepository _autorRepository;

        public FindAutorByCodigoHandler(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<Response> Handle(FindAutorByCodigoQuery request, CancellationToken cancellationToken)
        {
            var query = await _autorRepository.FindAsync(request.Codigo);

            if (query == null) return Response.Success();

            var result = new FindAutorByCodigoResponse
            {
                Codigo = query.Codigo,
                Nome = query.Nome
            };

            return Response.Success(result);
        }
    }

    public class FindAutorByCodigoResponse
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }
}
