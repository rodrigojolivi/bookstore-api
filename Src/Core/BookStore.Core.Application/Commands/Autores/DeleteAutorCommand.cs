using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Autores
{
    public class DeleteAutorCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class DeleteAutorHandler : Validator, IRequestHandler<DeleteAutorCommand, Response>
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAutorHandler(IAutorRepository autorRepository, IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteAutorCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new DeleteAutorValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var autor = await _autorRepository.FindAsync(request.Codigo);

            if (autor == null)
            {
                AddNotification("Autor não encontrado");

                return Response.Error(Notifications);
            }

            _autorRepository.Remove(autor);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class DeleteAutorValidator : AbstractValidator<DeleteAutorCommand>
    {
        public DeleteAutorValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty()
                .WithMessage("O código é obrigatório");
        }
    }
}
