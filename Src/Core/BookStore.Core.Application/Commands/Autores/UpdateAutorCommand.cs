using System.Text.Json.Serialization;
using BookStore.Core.Application.Responses;
using BookStore.Core.Application.Validators;
using BookStore.Core.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace BookStore.Core.Application.Commands.Autores
{
    public class UpdateAutorCommand : IRequest<Response>
    {
        [JsonIgnore]
        public int Codigo { get; set; }
        public string Nome { get; set; }
    }

    public class UpdateAutorHandler : Validator, IRequestHandler<UpdateAutorCommand, Response>
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAutorHandler(IAutorRepository autorRepository, IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> Handle(UpdateAutorCommand request, CancellationToken cancellationToken)
        {
            if (!await IsValidAsync(new UpdateAutorValidator(), request, cancellationToken)) return Response.Error(Notifications);

            var autor = await _autorRepository.FindAsync(request.Codigo);

            if (autor == null)
            {
                AddNotification("Autor não encontrado");

                return Response.Error(Notifications);
            }

            autor.Update(request.Nome);

            _autorRepository.Update(autor);

            await _unitOfWork.CommitAsync();

            return Response.Success();
        }
    }

    public class UpdateAutorValidator : AbstractValidator<UpdateAutorCommand>
    {
        public UpdateAutorValidator()
        {
            RuleFor(x => x.Nome).NotEmpty()
                .WithMessage("O nome é obrigatório");

            RuleFor(x => x.Nome).MaximumLength(40)
                .WithMessage("O nome deverá ter no máximo 40 caracteres");
        }
    }
}
