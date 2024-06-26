using API.Core.Application.Contracts.Repositories;
using API.Core.Application.Features.FeatureTarjeta.ViewModels;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Infraestructure.Services
{
    public class RepositoryTarjeta : IRepositoryTarjeta
    {
        private readonly SqlConnection con;

        public RepositoryTarjeta(SqlConnection cnn)
        {
            con = cnn;
        }

        async Task<TarjetaCreditoVM> IRepositoryTarjeta.GetByIdAsync(int id, string NumeroTarjeta)
        {   
            var Tarjeta = await con.QueryFirstAsync<TarjetaCreditoVM>(@"exec sp_ObtenerValoresTarjeta @id, @NumeroTarjeta", new {id,NumeroTarjeta });

            return Tarjeta;
        }
    }
}
