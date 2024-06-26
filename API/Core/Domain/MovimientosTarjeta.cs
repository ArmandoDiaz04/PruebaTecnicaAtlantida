using API.Core.Domain.Generic;

namespace API.Core.Domain
{
    public class MovimientosTarjeta : BaseDomain
    {

        public int IdTarjeta { get; set; }

        public string? Descripcion { get; set; }

        public DateTime? FechaMovimiento { get; set; }

        public decimal? Monto { get; set; }

        public string? TipoMovimiento { get; set; }
        public virtual TarjetasCredito IdTarjetaNavigation { get; set; } = null!;
    }

}
