using API.Core.Domain.Generic;

namespace API.Core.Domain
{
    public class TarjetasCredito : BaseDomain
    {

        public string? NombreTitular { get; set; }

        public string? NumeroTarjeta { get; set; }

        public decimal? MontoOtorgado { get; set; }

        public decimal? PorceInteres { get; set; }

        public decimal? PorceInteresMinimo { get; set; }

        public virtual ICollection<MovimientosTarjeta> MovimientosTarjeta { get; set; } = new List<MovimientosTarjeta>();

    }
}
