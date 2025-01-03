using BookStore.Core.Application.Commands;
using BookStore.Core.Application.Commands.Livros;
using BookStore.Core.Application.Queries.Assuntos;
using BookStore.Core.Application.Queries.Livros;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Service.Api.Controllers
{
    [Route("api/livros")]
    public class LivroController : CustomController
    {
        private readonly IMediator _mediator;

        public LivroController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> ExecuteAsync([FromBody] CreateLivroCommand command)
        {
            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return Created("", null);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo, [FromBody] UpdateLivroCommand command)
        {
            command.Codigo = codigo;

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var command = new DeleteLivroCommand
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ExecuteAsync([FromQuery] GetLivrosQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> FindByCodigoAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var query = new FindLivroByCodigoQuery
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(query);

            if (NoData(response)) return NotFound();

            return Ok(response);
        }
    }
}
