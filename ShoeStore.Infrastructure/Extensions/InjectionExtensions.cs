using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoeStore.Infrastructure.FileStorage;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Infrastructure.Persistences.Repository;

namespace ShoeStore.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(SHOESTOREContext).Assembly.FullName;

            services.AddDbContext<SHOESTOREContext>(
                options => options.UseSqlServer(
                       configuration.GetConnectionString("Connection"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IServerStorage, ServerStorage>();
            //services.AddTransient<IGenerateExcel, GenerateExcel>();

            return services;
        }
    }
}