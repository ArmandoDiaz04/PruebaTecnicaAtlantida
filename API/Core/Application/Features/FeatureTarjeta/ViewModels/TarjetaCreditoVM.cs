using API.Core.Domain.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Core.Application.Features.FeatureTarjeta.ViewModels
{
    public class TarjetaCreditoVM
    {
        public int id {  get; set; }
        public string NombreTitular { get; set; }
        public string NumeroTarjeta { get; set; }
        public decimal MontoOtorgado { get; set; }
        public decimal PorceInteres { get; set; }
        public decimal PorceInteresMinimo { get; set; }
        public decimal SaldoTotal { get; set; }
        public decimal CuotaMinima { get; set; }
        public decimal InteresBonificable { get; set; }
        public decimal TotalContadoConInteres { get; set; }
        public decimal Disponible { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creadoPor { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string? modificadoPor { get; set; }
        public string status { get; set; }
    }
}
