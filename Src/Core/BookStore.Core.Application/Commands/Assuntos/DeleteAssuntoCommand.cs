using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Assuntos
{
    public class DeleteAssuntoCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
    }

    public class DeleteAssuntoHandler : Validator, IRequestHandler<DeleteAssuntoCommand, Response>
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAssuntoHandler(IAssuntoRepository assuntoRepository, IUnitOfWork unitOfWork)
        {
            _assuntoRepository = assuntoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(DeleteAssuntoCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new DeleteAssuntoValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var assunto = await _assuntoRepository.FindAsync(request.Codigo);

            if (assunto == null)
            {
                AddNotification("Assunto não encontrado");

                return Response.Error(Notifications);
            }

            _assuntoRepository.Remove(assunto);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class DeleteAssuntoValidator : AbstractValidator<DeleteAssuntoCommand>
    {
        public DeleteAssuntoValidator()
        {
            RuleFor(x => x.Codigo).NotEmpty()
                .WithMessage("O código é obrigatório");
        }
    }
}
