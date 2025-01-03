using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Assuntos
{
    public class FindAssuntoByCodigoQuery : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class FindAssuntoByCodigoHandler : IRequestHandler<FindAssuntoByCodigoQuery, Response>
    {
        private readonly IAssuntoRepository _assuntoRepository;

        public FindAssuntoByCodigoHandler(IAssuntoRepository assuntoRepository)
        {
            _assuntoRepository = assuntoRepository;
        }

        public async Task<Response> Handle(FindAssuntoByCodigoQuery request, CancellationToken cancellationToken)
        {
            var query = await _assuntoRepository.FindAsync(request.Codigo);

            if (query == null) return Response.Success();

            var result = new FindAssuntoByCodigoResponse
            {
                Codigo = query.Codigo,
                Descricao = query.Descricao
            };

            return Response.Success(result);
        }
    }

    public class FindAssuntoByCodigoResponse
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
