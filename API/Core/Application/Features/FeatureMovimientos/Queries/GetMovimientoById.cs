using API.Core.Application.Contracts;
using API.Core.Application.Exceptions;
using API.Core.Application.Features.FeatureMovimientos.DTOs;
using API.Core.Application.Features.FeatureTarjeta.DTOs;
using API.Core.Application.Features.FeatureTarjeta.Queries;
using API.Core.Application.Helpers;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.FeatureMovimientos.Queries
{
    public class GetMovimientoById
    {
        public class GetMovimientoByIdQuery : IRequest<MovimientosTarjetaDto>
        {
            public int id { get; set;}

            public GetMovimientoByIdQuery(int id)
            {
                this.id = id;
            }
        }

        public class GetMovimientoByIdQueryHandler : IRequestHandler<GetMovimientoByIdQuery, MovimientosTarjetaDto>
        {
            private readonly IMapper mapper;
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<GetMovimientoByIdQueryHandler> logger;

            public GetMovimientoByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetMovimientoByIdQueryHandler> logger)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }


            public async Task<MovimientosTarjetaDto> Handle(GetMovimientoByIdQuery request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(GetMovimientoById));


                var tipo = await unitOfWork.Repository<MovimientosTarjeta>().GetByIdAsync(request.id);
                if (tipo == null)
                    throw new NotFoundException(nameof(MovimientosTarjeta), request.id);


                MensajeLogs.FinalizoEjecucion(logger, nameof(GetMovimientoById));
                return mapper.Map<MovimientosTarjetaDto>(tipo);
            }
        }
    }
}
