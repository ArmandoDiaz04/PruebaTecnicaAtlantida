namespace CLIENT.Models.ViewModels
{
    public class TarjetaCreditoVM
    {
        public int id { get; set; }
        public string? nombreTitular { get; set; }

        public string? numeroTarjeta { get; set; }

        public decimal montoOtorgado { get; set; }

        public decimal? porceInteres { get; set; }

        public decimal? porceInteresMinimo { get; set; }
        public decimal saldoTotal { get; set; }
        public decimal cuotaMinima { get; set; }
        public decimal interesBonificable { get; set; }
        public decimal totalContadoConInteres { get; set; }
        public decimal disponible { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string creadoPor { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string? modificadoPor { get; set; }
        public string status { get; set; }

    }
}
