using BookStore.Core.Application.Queries.Relatorios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Service.Api.Controllers
{
    [Route("api/relatorios")]
    public class RelatorioController : CustomController
    {
        private readonly IMediator _mediator;

        public RelatorioController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> ExecuteAsync([FromQuery] GenerateReportQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }
    }
}
