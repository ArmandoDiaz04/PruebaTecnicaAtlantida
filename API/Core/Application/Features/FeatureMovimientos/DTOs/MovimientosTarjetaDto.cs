namespace API.Core.Application.Features.FeatureMovimientos.DTOs
{
    public class MovimientosTarjetaDto
    {
        public int id { get; set; }
        public int IdTarjeta { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public decimal Monto { get; set; }
        public string TipoMovimiento { get; set; }
    }
}
