using System.ComponentModel.DataAnnotations;

namespace CLIENT.Models.ViewModels
{
    public class MovimientosTarjetaVM
    {
        public int? id { get; set; }
        public int? idTarjeta { get; set; }
        [Required(ErrorMessage = "La descripción es requerida")]
        public string descripcion { get; set; }
        [Required(ErrorMessage = "La fecha de movimiento es requerida")]
        public DateTime fechaMovimiento { get; set; }
        [Required(ErrorMessage = "El monto es requerido")]
        [DataType(DataType.Currency, ErrorMessage = "Por favor, introduzca un valor válido para el campo monto.")]
        public decimal monto { get; set; }
        public string tipoMovimiento { get; set; }
    }
}
