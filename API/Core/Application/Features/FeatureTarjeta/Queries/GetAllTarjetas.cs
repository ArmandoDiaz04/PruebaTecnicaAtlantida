using API.Core.Application.Contracts;
using API.Core.Application.Features.FeatureTarjeta.DTOs;
using API.Core.Application.Helpers;
using API.Core.Domain;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace API.Core.Application.Features.FeatureTarjeta.Queries
{
    public class GetAllTarjetas
    {
        public class GetAllTarjetasQuery : IRequest<List<TarjetasCreditoDto>> { }


        public class GetAllTarjetasQueryHandler : IRequestHandler<GetAllTarjetasQuery, List<TarjetasCreditoDto>>
        {
            private readonly IMapper mapper;
            private readonly IUnitOfWork unitOfWork;
            private readonly ILogger<GetAllTarjetasQueryHandler> logger;

            public GetAllTarjetasQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, ILogger<GetAllTarjetasQueryHandler> logger)
            {
                this.mapper = mapper;
                this.unitOfWork = unitOfWork;
                this.logger = logger;
            }


            public async Task<List<TarjetasCreditoDto>> Handle(GetAllTarjetasQuery request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(GetAllTarjetas));
                var query = unitOfWork.Repository<TarjetasCredito>().GetQuery();

                query = query.Where(p => p.MontoOtorgado > 0);
                var tipos = await query.ToListAsync();

                MensajeLogs.FinalizoEjecucion(logger, nameof(GetAllTarjetas));
                return mapper.Map<List<TarjetasCreditoDto>>(tipos);
            }
        }
    }
}

