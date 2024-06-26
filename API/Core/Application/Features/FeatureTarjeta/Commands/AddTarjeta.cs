using FluentValidation;
using MediatR;
using AutoMapper;
using API.Core.Application.Contracts;
using API.Core.Application.Helpers;
using API.Core.Domain;

namespace API.Core.Application.Features.FeatureTarjeta.Commands
{
    public class AddTarjeta
    {
        public class AddTarjetaCommand : IRequest
        {
            public string NombreTitular { get; set; }

            public string NumeroTarjeta { get; set; }

            public decimal MontoOtorgado { get; set; }

            public decimal PorceInteres { get; set; }

            public decimal PorceInteresMinimo { get; set; }
        }

        public class AddTarjetaCommandValidator : AbstractValidator<AddTarjetaCommand>
        {
            public AddTarjetaCommandValidator()
            {
                RuleFor(p => p.NombreTitular).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.NumeroTarjeta).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.MontoOtorgado).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.PorceInteres).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.PorceInteresMinimo).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");

            }
        }

        public class AddATarjetaCommandHandler : IRequestHandler<AddTarjetaCommand>
        {

            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper mapper;
            private readonly ILogger<AddATarjetaCommandHandler> logger;

            public AddATarjetaCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddATarjetaCommandHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
                this.logger = logger;
            }

            public  async Task<Unit> Handle(AddTarjetaCommand request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(AddTarjeta));
                var tarjetaToInsert = mapper.Map<TarjetasCredito>(request);

                await unitOfWork.Repository<TarjetasCredito>().InsertAsync(tarjetaToInsert);
                await unitOfWork.SaveChangesAsync();

                MensajeLogs.FinalizoEjecucion(logger, nameof(AddTarjeta));
                return Unit.Value;
            }
        }
    }
}
