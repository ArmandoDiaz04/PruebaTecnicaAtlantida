

using API.Core.Application.Features.FeatureMovimientos.ViewModels;

namespace API.Core.Application.Contracts.Repositories
{
    public interface IRepositoryMovimiento
    {
        Task<List<MovimientoTarjetaVM>> GetMovimientosAsync(int idTarjeta, string numeroTarjeta, int tipo, int mes, int anio);
    }
}
