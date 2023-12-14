using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SellTech.Application.Services;
using ShoeStore.Application.Extensions.WatchDog;
using ShoeStore.Application.Interfaces;
using ShoeStore.Application.Services;
using System.Reflection;

namespace ShoeStore.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            /*services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });*/

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthApplication, AuthApplication>();
            services.AddScoped<ICalificacionResenaApplication, CalificacionResenaApplication>();
            services.AddScoped<ICarritoCompraApplication, CarritoCompraApplication>();
            services.AddScoped<ICategoriaApplication, CategoriaApplication>();
            services.AddScoped<ICuentaVendedorApplication, CuentaVendedorApplication>();
            services.AddScoped<IDireccionApplication, DireccionApplication>();
            services.AddScoped<IMetodoPagoApplication, MetodoPagoApplication>();
            services.AddScoped<IOpcionEnvioApplication, OpcionEnvioApplication>();
            services.AddScoped<IPedidoApplication, PedidoApplication>();
            services.AddScoped<IProductoApplication, ProductoApplication>();
            services.AddScoped<IRolApplication, RolApplication>();
            services.AddScoped<IUsuarioApplication, UsuarioApplication>();

            services.AddWatchDog();

            return services;
        }
    }
}