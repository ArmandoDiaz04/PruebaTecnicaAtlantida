using API.Core.Domain;

namespace API.Core.Application.Features.FeatureTarjeta.DTOs
{
    public class TarjetasCreditoDto
    {
        public int Id { get; set; }
        public string? NombreTitular { get; set; }

        public string? NumeroTarjeta { get; set; }

        public decimal? MontoOtorgado { get; set; }

        public decimal? PorceInteres { get; set; }

        public decimal? PorceInteresMinimo { get; set; }

    }
}
