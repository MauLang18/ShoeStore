using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class CarritoCompraRepository : GenericRepository<TbCarritoCompra>, ICarritoCompraRepository
    {
        private readonly SHOESTOREContext _context;

        public CarritoCompraRepository(SHOESTOREContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbCarritoCompra>> ListCarritoCompra(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbCarritoCompra>();

            var carritoCompra = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.ProductoNavigation)
                .Include(x => x.CompradorNavigation)
                .AsNoTracking();

            if (filters.StateFilter is not null)
            {
                carritoCompra = carritoCompra.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                carritoCompra = carritoCompra.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await carritoCompra.CountAsync();
            response.Items = await Ordering(filters, carritoCompra, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<TbCarritoCompra>> ListCarritoCompraByUser(int userId)
        {
            var carritoCompra = await _context.TbCarritoCompras
                .Where(x => x.Estado.Equals((int)StateTypes.Activo) && x.Comprador.Equals(userId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.ProductoNavigation)
                .AsNoTracking()
                .ToListAsync();

            return carritoCompra;
        }

        public async Task<bool> DeleteCarritoCompraByUser(int userId)
        {
            var carritos = await _context.TbCarritoCompras
                .Where(x => x.Comprador.Equals(userId))
                .ToListAsync();

            if (carritos != null && carritos.Any())
            {
                foreach (var carrito in carritos)
                {
                    carrito.UsuarioEliminacionAuditoria = 1;
                    carrito.FechaEliminacionAuditoria = DateTime.Now;
                    _context.Update(carrito);
                }

                var recordsAffected = await _context.SaveChangesAsync();
                return recordsAffected > 0;
            }

            return false;
        }

        public async Task<bool> EditCarritoCompraByProductoByUser(int productoId, int userId, int cantidad)
        {
            var carrito = await _context.TbCarritoCompras
                .FirstOrDefaultAsync(x => x.Producto == productoId && x.Comprador == userId && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            if (carrito != null)
            {
                carrito.Cantidad = cantidad;
                _context.Update(carrito);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<TbCarritoCompra> CarritoCompraByProdcutoByUser(int productoId, int userId)
        {
            var carritoCompra = await _context.TbCarritoCompras
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Estado.Equals((int)StateTypes.Activo) && x.Producto.Equals(productoId) && x.Comprador.Equals(userId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            return carritoCompra!;
        }
    }
}