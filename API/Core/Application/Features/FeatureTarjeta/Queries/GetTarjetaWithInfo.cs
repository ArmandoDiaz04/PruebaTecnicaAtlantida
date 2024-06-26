using API.Core.Application.Contracts;
using API.Core.Application.Exceptions;
using API.Core.Application.Features.FeatureTarjeta.ViewModels;
using API.Core.Application.Helpers;
using AutoMapper;
using MediatR;

namespace API.Core.Application.Features.FeatureTarjeta.Queries
{
    public class GetTarjetaWithInfo
    {

        public class GetTarjetaWithInfoQuery : IRequest<TarjetaCreditoVM>
        {
            public int id { get; set; }
            public string NumeroTarjeta { get; set; }

            public GetTarjetaWithInfoQuery(int id, string NumeroTarjeta)
            {
                this.id = id;
                this.NumeroTarjeta = NumeroTarjeta;
            }
        }

        public class GetTarjetaWithInfoQueryHandler : IRequestHandler<GetTarjetaWithInfoQuery, TarjetaCreditoVM>
        {
            private readonly IMapper mapper;
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<GetTarjetaWithInfoQueryHandler> logger;

            public GetTarjetaWithInfoQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetTarjetaWithInfoQueryHandler> logger)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }


            public async Task<TarjetaCreditoVM> Handle(GetTarjetaWithInfoQuery request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(GetTarjetaWithInfoQuery));

                var tarjeta = await unitOfWork.RepositoryTarjeta.GetByIdAsync(request.id, request.NumeroTarjeta);

                if(tarjeta == null)
                {
                    throw new NotFoundException(nameof(TarjetaCreditoVM), request.id);
                }

                MensajeLogs.FinalizoEjecucion(logger, nameof(GetTarjetaWithInfoQuery));

                return tarjeta;
            }
        }
    }
}
