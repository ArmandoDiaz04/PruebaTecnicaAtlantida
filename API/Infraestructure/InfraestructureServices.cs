using API.Core.Application.Contracts;
using API.Infraestructure.Persistence;
using API.Infraestructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace API.Infraestructure
{
    public static class InfraestructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TarjetaCreditoDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("default"),
                    b => b.MigrationsAssembly(typeof(TarjetaCreditoDbContext).Assembly.FullName)));


            services.TryAddSingleton<ISystemClock, SystemClock>();


            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}

