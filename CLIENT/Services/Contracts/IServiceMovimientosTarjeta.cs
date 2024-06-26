using CLIENT.Models.ViewModels;

namespace CLIENT.Services.Contracts
{
    public interface IServiceMovimientosTarjeta
    {
        Task<List<MovimientosTarjetaVM>> ObtenerMovimientosTarjeta(int idTarjeta, string numeroTarjeta,int tipo,int mes, int anio);
        Task<string> GuardarMovimiento(MovimientosTarjetaVM movimiento);
    }
}
