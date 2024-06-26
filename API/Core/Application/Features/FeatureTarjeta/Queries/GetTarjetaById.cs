using API.Core.Application.Contracts;
using API.Core.Application.Exceptions;
using API.Core.Application.Features.FeatureTarjeta.DTOs;
using API.Core.Application.Helpers;
using API.Core.Domain;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.FeatureTarjeta.Queries
{
    public class GetTarjetaById
    {
        public class GetTarjetaByIdQuery : IRequest<TarjetasCreditoDto>
        {
            public int id { get; set; }

            public GetTarjetaByIdQuery(int id)
            {
                this.id = id;
            }

        }

        public class GetTarjetaByIdQueryHandler : IRequestHandler<GetTarjetaByIdQuery, TarjetasCreditoDto>
        {
            private readonly IMapper mapper;
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<GetTarjetaByIdQueryHandler> logger;

            public GetTarjetaByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetTarjetaByIdQueryHandler> logger)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }


            public async Task<TarjetasCreditoDto> Handle(GetTarjetaByIdQuery request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(GetTarjetaById));


                var tipo = await unitOfWork.Repository<TarjetasCredito>().GetByIdAsync(request.id);
                if (tipo == null)
                    throw new NotFoundException(nameof(TarjetasCredito), request.id);


                MensajeLogs.FinalizoEjecucion(logger, nameof(GetTarjetaById));
                return mapper.Map<TarjetasCreditoDto>(tipo);
            }
        }


    }
}
