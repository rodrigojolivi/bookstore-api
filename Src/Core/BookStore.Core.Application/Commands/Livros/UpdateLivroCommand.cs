using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Livros
{
    public class UpdateLivroCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; }
        public IEnumerable<int> CodigoAutores { get; set; }
        public IEnumerable<int> CodigoAssuntos { get; set; }
    }

    public class UpdateLivroHandler : Validator, IRequestHandler<UpdateLivroCommand, Response>
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateLivroHandler(ILivroRepository livroRepository, IUnitOfWork unitOfWork)
        {
            _livroRepository = livroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(UpdateLivroCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new UpdateLivroValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var livro = await _livroRepository.FindLivroByCodigoAsync(request.Codigo);

            if (livro == null)
            {
                AddNotification("Livro não encontrado");

                return Response.Error(Notifications);
            }

            livro.Update(request.Titulo, request.Editora, request.Edicao, request.AnoPublicacao);

            livro.AddAutores(request.CodigoAutores);

            livro.AddAssuntos(request.CodigoAssuntos);

            _livroRepository.Update(livro);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class UpdateLivroValidator : AbstractValidator<UpdateLivroCommand>
    {
        public UpdateLivroValidator()
        {
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
