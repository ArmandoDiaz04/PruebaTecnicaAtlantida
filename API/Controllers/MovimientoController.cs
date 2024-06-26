using API.Core.Application.Features.FeatureMovimientos.DTOs;
using API.Core.Application.Features.FeatureMovimientos.ViewModels;
using API.Core.Application.Features.FeatureTarjeta.DTOs;
using API.Core.Application.Features.FeatureTarjeta.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static API.Core.Application.Features.FeatureMovimientos.Commands.AddMovimiento;
using static API.Core.Application.Features.FeatureMovimientos.Queries.GetMovimientoById;
using static API.Core.Application.Features.FeatureMovimientos.Queries.GetMovimientosByTarjeta;
using static API.Core.Application.Features.FeatureTarjeta.Queries.GetTarjetaById;
using static API.Core.Application.Features.FeatureTarjeta.Queries.GetTarjetaWithInfo;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : Controller
    {
        private readonly IMediator mediator;

        public MovimientoController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("info/{id}")]
        [ProducesResponseType(typeof(MovimientosTarjetaDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MovimientosTarjetaDto>> GetById(int id)
        {
            return Ok(await mediator.Send(new GetMovimientoByIdQuery(id)));
        }

        [HttpPost("movimientos")]
        [ProducesResponseType(typeof(MovimientoTarjetaVM), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<TarjetaCreditoVM>> GetMovimientos(GetMovimientosByTarjetaQuery request )
        {
            return Ok(await mediator.Send(request));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Post(AddMovimientoCommand request)
        {
            return Ok(await mediator.Send(request));
        }
    }
}
