using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using MediatR;

namespace BookStore.Core.Application.Queries.Assuntos
{
    public class GetAssuntosQuery : IRequest<Response>
    {

    }

    public class GetAssuntosHandler : Validator, IRequestHandler<GetAssuntosQuery, Response>
    {
        private readonly IAssuntoRepository _livroRepository;

        public GetAssuntosHandler(IAssuntoRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        public async Task<Response> Handle(GetAssuntosQuery request, CancellationToken cancellationToken)
        {
            var query = await _livroRepository.GetAsync();

            var result = query.Select(x => new GetAssuntosResponse
            {
                Codigo = x.Codigo,
                Descricao = x.Descricao
            });

            return Response.Success(result);
        }
    }

    public class GetAssuntosResponse
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }
}
