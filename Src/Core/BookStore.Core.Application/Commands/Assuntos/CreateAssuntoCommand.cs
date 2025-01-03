using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Assuntos
{
    public class CreateAssuntoCommand : IRequest<Response>
    {
        public string Descricao { get; set; }
    }

    public class CreateAssuntoHandler : Validator, IRequestHandler<CreateAssuntoCommand, Response>
    {
        private readonly IAssuntoRepository _assuntoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAssuntoHandler(IAssuntoRepository assuntoRepository, IUnitOfWork unitOfWork)
        {
            _assuntoRepository = assuntoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(CreateAssuntoCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new CreateAssuntoValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var assunto = new Assunto(request.Descricao);

            _assuntoRepository.Add(assunto);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class CreateAssuntoValidator : AbstractValidator<CreateAssuntoCommand>
    {
        public CreateAssuntoValidator()
        {
            RuleFor(x => x.Descricao).NotEmpty()
                .WithMessage("A descrição é obrigatório");

            RuleFor(x => x.Descricao).MaximumLength(20)
                .WithMessage("A descrição deverá ter no máximo 20 caracteres");
        }
    }
}
