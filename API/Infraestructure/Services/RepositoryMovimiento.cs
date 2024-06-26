using API.Core.Application.Contracts.Repositories;
using API.Core.Application.Features.FeatureMovimientos.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;


namespace API.Infraestructure.Services
{
    public class RepositoryMovimiento : IRepositoryMovimiento
    {
        private readonly SqlConnection con;

        public RepositoryMovimiento(SqlConnection cnn)
        {
            con = cnn;
        }

        public async Task<List<MovimientoTarjetaVM>> GetMovimientosAsync(int idTarjeta, string numeroTarjeta, int tipo, int mes, int anio)
        {
            List<MovimientoTarjetaVM>Movimientos = (List<MovimientoTarjetaVM>)await con.QueryAsync<MovimientoTarjetaVM>(@"exec sp_ObtenerMovimientosTarjeta @idTarjeta, @numeroTarjeta, @tipo, @mes, @anio", new { idTarjeta, numeroTarjeta, tipo, mes, anio });

            return Movimientos;
        }
    }
}
