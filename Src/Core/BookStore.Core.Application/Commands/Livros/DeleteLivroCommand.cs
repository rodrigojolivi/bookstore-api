using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Livros
{
    public class DeleteLivroCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class DeleteLivroHandler : Validator, IRequestHandler<DeleteLivroCommand, Response>
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLivroHandler(ILivroRepository livroRepository, IUnitOfWork unitOfWork)
        {
            _livroRepository = livroRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteLivroCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new DeleteLivroValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var livro = await _livroRepository.FindAsync(request.Codigo);

            if (livro == null)
            {
                AddNotification("Livro não encontrado");

                return Response.Error(Notifications);
            }

            _livroRepository.Remove(livro);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class DeleteLivroValidator : AbstractValidator<DeleteLivroCommand>
    {
        public DeleteLivroValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty()
                .WithMessage("O código é obrigatório");
        }
    }
}
