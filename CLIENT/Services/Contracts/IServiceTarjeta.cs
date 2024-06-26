using CLIENT.Models.ViewModels;

namespace CLIENT.Services.Contracts
{
    public interface IServiceTarjeta
    {
        Task<List<TarjetaCreditoVM>> ObtenerTarjetasCredito();
        Task<TarjetaCreditoVM> getTarjetaInfo(int id, string numeroTarjeta);
    }
}
