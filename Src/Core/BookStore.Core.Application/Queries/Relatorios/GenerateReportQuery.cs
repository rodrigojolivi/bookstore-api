using BookStore.Core.Application.Responses;
using BookStore.Core.Domain.Repositories;
using BookStore.Infra.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Core.Application.Queries.Relatorios
{
    public class GenerateReportQuery : IRequest<Response>
    {
        public string Livro { get; set; }
        public string Autor { get; set; }
        public string Assunto { get; set; }
    }

    public class GenerateReportHandler : IRequestHandler<GenerateReportQuery, Response>
    {
        private readonly ILivroRepository _livroRepository;
        private readonly BookStoreContext _context;

        public GenerateReportHandler(ILivroRepository livroRepository, BookStoreContext context)
        {
            _livroRepository = livroRepository;
            _context = context;
        }

        public async Task<Response> Handle(GenerateReportQuery request, CancellationToken cancellationToken)
        {
            var query = await _livroRepository.GetReportAsync();

            var result = query.Select(x => new GenerateReportResponse
            {
                Codigo = x.Codigo,
                Titulo = x.Titulo,
                Editora = x.Editora,
                Edicao = x.Edicao,
                AnoPublicacao = x.AnoPublicacao,

                Autores = x.LivroAutores.Select(x => new GenerateReportResponse.AutorResponse 
                {
                    Codigo = x.Autor.Codigo,
                    Nome = x.Autor.Nome

                }).ToList(),

                Assuntos = x.LivroAssuntos.Select(x => new GenerateReportResponse.AssuntoResponse
                {
                    Codigo = x.Assunto.Codigo,
                    Descricao = x.Assunto.Descricao

                }).ToList()
            });

            return Response.Success(result);
        }
    }

    public class GenerateReportResponse
    {
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public IList<AutorResponse> Autores { get; set; }
        public IList<AssuntoResponse> Assuntos { get; set; }

        public class AutorResponse
        {
            public int Codigo { get; set; }
            public string Nome { get; set; }
        }

        public class AssuntoResponse
        {
            public int Codigo { get; set; }
            public string Descricao { get; set; }
        }
    }
}
