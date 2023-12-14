using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class PedidoRepository : GenericRepository<TbPedido>, IPedidoRepository
    {
        private readonly SHOESTOREContext _context;

        public PedidoRepository(SHOESTOREContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbPedido>> ListPedidos(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbPedido>();

            var pedidos = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.CompradorNavigation)
                .Include(x => x.ProductoNavigation)
                .Include(x => x.MetodoPagoNavigation)
                .Include(x => x.DireccionEnvioNavigation)
                .AsNoTracking();

            /*if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        pedidos = pedidos.Where(x => x.!.Contains(filters.TextFilter));
                        break;
                }
            }*/

            if (filters.StateFilter is not null)
            {
                pedidos = pedidos.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                pedidos = pedidos.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await pedidos.CountAsync();
            response.Items = await Ordering(filters, pedidos, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<TbPedido>> ListPedidosByUser(int userId)
        {
            var pedidos = await _context.TbPedidos
                .Where(x => x.Estado.Equals((int)StateTypes.Activo) && x.Comprador.Equals(userId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.ProductoNavigation)
                .AsNoTracking()
                .ToListAsync();

            return pedidos;
        }

        public async Task<bool> DeletePedidoByUser(int userId)
        {
            var pedidos = await _context.TbPedidos
                .Where(x => x.Comprador.Equals(userId))
                .ToListAsync();

            if (pedidos != null && pedidos.Any())
            {
                foreach (var pedido in pedidos)
                {
                    pedido.UsuarioEliminacionAuditoria = 1;
                    pedido.FechaEliminacionAuditoria = DateTime.Now;
                    _context.Update(pedido);
                }

                var recordsAffected = await _context.SaveChangesAsync();
                return recordsAffected > 0;
            }

            return false;
        }

        public async Task<bool> EditPedidoByProductoByUser(int productoId, int userId, int cantidad)
        {
            var pedido = await _context.TbPedidos
                .FirstOrDefaultAsync(x => x.Producto == productoId && x.Comprador == userId && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            if (pedido != null)
            {
                pedido.Cantidad = cantidad;
                _context.Update(pedido);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<TbPedido> PedidoByProdcutoByUser(int productoId, int userId)
        {
            var pedido = await _context.TbPedidos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Estado.Equals((int)StateTypes.Activo) && x.Producto.Equals(productoId) && x.Comprador.Equals(userId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            return pedido!;
        }
    }
}