using BookStore.Core.Application.Commands.Autores;
using BookStore.Core.Application.Queries.Assuntos;
using BookStore.Core.Application.Queries.Autores;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Service.Api.Controllers
{
    [Route("api/autores")]
    public class AutorController : CustomController
    {
        private readonly IMediator _mediator;

        public AutorController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> ExecuteAsync([FromBody] CreateAutorCommand command)
        {
            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return Created("", null);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo, [FromBody] UpdateAutorCommand command)
        {
            command.Codigo = codigo;

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var command = new DeleteAutorCommand
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ExecuteAsync([FromQuery] GetAutoresQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> FindByCodigoAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var query = new FindAutorByCodigoQuery
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(query);

            if (NoData(response)) return NotFound();

            return Ok(response);
        }
    }
}
