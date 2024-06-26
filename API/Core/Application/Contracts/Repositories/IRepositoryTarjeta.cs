
using API.Core.Application.Features.FeatureTarjeta.ViewModels;
using API.Core.Domain;

namespace API.Core.Application.Contracts.Repositories
{
    public interface IRepositoryTarjeta
    {
        Task<TarjetaCreditoVM> GetByIdAsync(int id, string NumeroTarjeta);
    }
}
