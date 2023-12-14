using ShoeStore.Infrastructure.FileStorage;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICalificacionResenaRepository CalificacionResena { get; }
        ICarritoCompraRepository CarritoCompra { get; }
        ICuentaVendedorRepository CuentaVendedor { get; }
        IDireccionRepository Direccion { get; }
        IMetodoPagoRepository MetodoPago { get; }
        IOpcionEnvioRepository OpcionEnvio { get; }
        IPedidoRepository Pedido { get; }
        IProductoRepository Producto { get; }
        ICategoriumRepository Categoria { get; }
        IRolRepository Rol { get; }
        IUsuarioRepository Usuario { get; }
        IServerStorage ServerStorage { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
