using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Livros
{
    public class CreateLivroCommand : IRequest<Response>
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public IEnumerable<int> CodigoAutores { get; set; }
        public IEnumerable<int> CodigoAssuntos { get; set; }
    }

    public class CreateLivroHandler : Validator, IRequestHandler<CreateLivroCommand, Response>
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLivroHandler(IAutorRepository autorRepository,
            ILivroRepository livroRepository, IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(CreateLivroCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new CreateLivroValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var livro = new Livro(request.Titulo, request.Editora, request.Edicao, request.AnoPublicacao);

            livro.AddAutores(request.CodigoAutores);

            livro.AddAssuntos(request.CodigoAssuntos);

            _livroRepository.Add(livro);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class CreateLivroValidator : AbstractValidator<CreateLivroCommand>
    {
        public CreateLivroValidator()
        {
            RuleFor(x => x.CodigoAutores).NotEmpty()
                .WithMessage("O livro deverá ter ao menos um autor");

            RuleFor(x => x.CodigoAssuntos).NotEmpty()
                .WithMessage("O livro deverá ter ao menos um assunto");

            RuleFor(x => x.Titulo).NotEmpty()
                .WithMessage("O título é obrigatório");

            RuleFor(x => x.Titulo).MaximumLength(40)
                .WithMessage("O título deverá ter no máximo 40 caracteres");

            RuleFor(x => x.Editora).NotEmpty()
                .WithMessage("A editora é obrigatória");

            RuleFor(x => x.Editora).MaximumLength(40)
                .WithMessage("A editora deverá ter no máximo 40 caracteres");

            RuleFor(x => x.Edicao).NotEmpty()
                .WithMessage("A edição é obrigatória");

            RuleFor(x => x.AnoPublicacao).NotEmpty()
                .WithMessage("O ano de publicação é obrigatório");

            RuleFor(x => x.AnoPublicacao).MaximumLength(4)
                .WithMessage("O ano de publicação deverá ter no máximo 4 caracteres");
        }
    }
}
