using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Assuntos
{
    public class UpdateAssuntoCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
        public string Descricao { get; set; }
    }

    public class UpdateAssuntoHandler : Validator, IRequestHandler<UpdateAssuntoCommand, Response>
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAssuntoHandler(IAssuntoRepository assuntoRepository, IUnitOfWork unitOfWork)
        {
            _assuntoRepository = assuntoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(UpdateAssuntoCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new UpdateAssuntoValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var assunto = await _assuntoRepository.FindAsync(request.Codigo);

            if (assunto == null)
            {
                AddNotification("Assunto não encontrado");

                return Response.Error(Notifications);
            }

            assunto.Update(request.Descricao);

            _assuntoRepository.Update(assunto);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class UpdateAssuntoValidator : AbstractValidator<UpdateAssuntoCommand>
    {
        public UpdateAssuntoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty()
                .WithMessage("A descrição é obrigatória");

            RuleFor(x => x.Descricao).MaximumLength(20)
                .WithMessage("A descrição deverá ter no máximo 20 caracteres");
        }
    }
}
