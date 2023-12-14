using Microsoft.Extensions.Configuration;
using ShoeStore.Infrastructure.FileStorage;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SHOESTOREContext _context;

        public ICalificacionResenaRepository CalificacionResena { get; private set; }

        public ICarritoCompraRepository CarritoCompra { get; private set; }

        public ICuentaVendedorRepository CuentaVendedor { get; private set; }

        public IMetodoPagoRepository MetodoPago { get; private set; }

        public IOpcionEnvioRepository OpcionEnvio { get; private set; }

        public IPedidoRepository Pedido { get; private set; }

        public IProductoRepository Producto { get; private set; }

        public ICategoriumRepository Categoria { get; private set; }

        public IRolRepository Rol { get; private set; }

        public IUsuarioRepository Usuario { get; private set; }

        public IServerStorage ServerStorage { get; private set; }

        public IDireccionRepository Direccion { get; private set; }

        public UnitOfWork(SHOESTOREContext context, IConfiguration configuration)
        {
            _context = context;
            CalificacionResena = new CalificacionResenaRepository(_context);
            CarritoCompra = new CarritoCompraRepository(_context);
            CuentaVendedor = new CuentaVendedorRepository(_context);
            MetodoPago = new MetodoPagoRepository(_context);
            OpcionEnvio = new OpcionEnvioRepository(_context);
            Pedido = new PedidoRepository(_context);
            Producto = new ProductoRepository(_context);
            Categoria = new CategoriumRepository(_context);
            Rol = new RolRepository(_context);
            Usuario = new UsuarioRepository(_context);
            ServerStorage = new ServerStorage(configuration);
            Direccion = new DireccionRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}