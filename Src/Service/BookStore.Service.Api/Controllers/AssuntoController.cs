using BookStore.Core.Application.Commands.Assuntos;
using BookStore.Core.Application.Queries.Assuntos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Service.Api.Controllers
{
    [Route("api/assuntos")]
    public class AssuntoController : CustomController
    {
        private readonly IMediator _mediator;

        public AssuntoController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> ExecuteAsync([FromBody] CreateAssuntoCommand command)
        {
            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return Created("", null);
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo, [FromBody] UpdateAssuntoCommand command)
        {
            command.Codigo = codigo;

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> ExecuteAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var command = new DeleteAssuntoCommand
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(command);

            if (HasNotifications(response)) return BadRequest(response);

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> ExecuteAsync([FromQuery] GetAssuntosQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> FindByCodigoAsync([FromRoute(Name = "codigo")] int codigo)
        {
            var query = new FindAssuntoByCodigoQuery
            {
                Codigo = codigo
            };

            var response = await _mediator.Send(query);

            if (NoData(response)) return NotFound();

            return Ok(response);
        }
    }
}
