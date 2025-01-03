using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Entities;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Autores
{
    public class CreateAutorCommand : IRequest<Response>
    {
        public string Nome { get; set; }
    }

    public class CreateAutorHandler : Validator, IRequestHandler<CreateAutorCommand, Response>
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAutorHandler(IAutorRepository autorRepository, IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(CreateAutorCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new CreateAutorValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var autor = new Autor(request.Nome);

            _autorRepository.Add(autor);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class CreateAutorValidator : AbstractValidator<CreateAutorCommand>
    {
        public CreateAutorValidator()
        {
            RuleFor(x => x.Nome).NotEmpty()
                .WithMessage("O nome é obrigatório");

            RuleFor(x => x.Nome).MaximumLength(40)
                .WithMessage("O nome deverá ter no máximo 40 caracteres");
        }
    }
}
