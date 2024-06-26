using API.Core.Application.Contracts;
using API.Core.Application.Helpers;
using API.Core.Domain;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace API.Core.Application.Features.FeatureMovimientos.Commands
{
    public class AddMovimiento
    {
        public class AddMovimientoCommand : IRequest
        {
            public int IdTarjeta { get; set; }
            public string? Descripcion { get; set; }
            public DateTime FechaMovimiento { get; set; }
            public decimal Monto { get; set; }
            public string TipoMovimiento { get; set; }
        }

        public class AddMovimientoCommandValidator : AbstractValidator<AddMovimientoCommand>
        {
            public AddMovimientoCommandValidator()
            {
                RuleFor(p => p.IdTarjeta).Must(Validations.MayorQueCero).WithMessage("Requerido");
                RuleFor(p => p.Monto).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.TipoMovimiento).NotNull().WithMessage("Requerido").NotEmpty().WithMessage("Requerido");
                RuleFor(p => p.FechaMovimiento).NotNull().WithMessage("Requerido")
                .Must(Validations.EsFechaMenorAlaFechaActual).WithMessage("La fecha no puede ser antes de la actual");
            }
        }

        public class AddMovimientoCommandHandler : IRequestHandler<AddMovimientoCommand>
        {
            private readonly IUnitOfWork unitOfWork;
            private readonly IMapper mapper;
            private readonly ILogger<AddMovimientoCommandHandler> logger;

            public AddMovimientoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AddMovimientoCommandHandler> logger)
            {
                this.unitOfWork = unitOfWork;
                this.mapper = mapper;
                this.logger = logger;
            }


            public async Task<Unit> Handle(AddMovimientoCommand request, CancellationToken cancellationToken)
            {
                MensajeLogs.InicioEjecucion(logger, nameof(AddMovimiento));
                var movimientoToInsert = mapper.Map<MovimientosTarjeta>(request);

                await unitOfWork.Repository<MovimientosTarjeta>().InsertAsync(movimientoToInsert);
                await unitOfWork.SaveChangesAsync();

                MensajeLogs.FinalizoEjecucion(logger, nameof(AddMovimiento));
                return Unit.Value;
            }
        }
    }
}
