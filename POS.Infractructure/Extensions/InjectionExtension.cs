using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Infractructure.Persistences.Context;
using POS.Infractructure.Persistences.Interfaces;
using POS.Infractructure.Persistences.Repositories;

namespace POS.Infractructure.Extensions
{
    public static class InjectionExtension
    {
        public static IServiceCollection AddInjectionInfractructure(this IServiceCollection services, IConfiguration configuration )
        {
            var assembly = typeof(PosContext).Assembly.FullName;

            services.AddDbContext<PosContext>(
                option => option.UseSqlServer(
                    configuration.GetConnectionString("POSConnection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
