using API.Core.Application.Contracts;
using API.Core.Application.Exceptions;
using API.Core.Application.Features.FeatureMovimientos.ViewModels;
using API.Core.Application.Features.FeatureTarjeta.ViewModels;
using API.Core.Application.Helpers;
using AutoMapper;
using MediatR;
using static API.Core.Application.Features.FeatureTarjeta.Queries.GetTarjetaWithInfo;

namespace API.Core.Application.Features.FeatureMovimientos.Queries
{
    public class GetMovimientosByTarjeta
    {
        public class GetMovimientosByTarjetaQuery : IRequest<List<MovimientoTarjetaVM>>
        {
            public int idTarjeta { get; set; }
            public string numeroTarjeta { get; set; }
            public int tipo { get; set; }
            public int mes { get; set; }
            public int anio { get; set; }

            public GetMovimientosByTarjetaQuery(int idTarjeta, string numeroTarjeta, int tipo, int mes, int anio)
            {
                this.idTarjeta = idTarjeta;
                this.numeroTarjeta = numeroTarjeta;
                this.tipo = tipo;
                this.mes = mes;
                this.anio = anio;
            }
        }

        public class GetMovimientosByTarjetaQueryHandler : IRequestHandler<GetMovimientosByTarjetaQuery, List<MovimientoTarjetaVM>>
        {
            private readonly IMapper mapper;
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<GetMovimientosByTarjetaQueryHandler> logger;

            public GetMovimientosByTarjetaQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetMovimientosByTarjetaQueryHandler> logger)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }


            public async Task<List<MovimientoTarjetaVM>> Handle(GetMovimientosByTarjetaQuery request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(GetTarjetaWithInfoQuery));

                var movimientos = await unitOfWork.RepositoryMovimiento.GetMovimientosAsync(request.idTarjeta, request.numeroTarjeta, request.tipo, request.mes, request.anio);

                if (movimientos == null)
                {
                    throw new NotFoundException(nameof(MovimientoTarjetaVM), request.numeroTarjeta);
                }

                MensajeLogs.FinalizoEjecucion(logger, nameof(GetTarjetaWithInfoQuery));

                return movimientos;
            }
        }
    }
}
