using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class ProductoRepository : GenericRepository<TbProducto>, IProductoRepository
    {
        public ProductoRepository(SHOESTOREContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbProducto>> ListProductoByUser(BaseFiltersRequest filters, int userId)
        {
            var response = new BaseEntityResponse<TbProducto>();

            var productos = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.Vendedor.Equals(userId))
                .Include(x => x.CategoriaNavigation)
                .Include(x => x.OpcionEnvioNavigation)
                .Include(x => x.VendedorNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        productos = productos.Where(x => x.Descripcion!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                productos = productos.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                productos = productos.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await productos.CountAsync();
            response.Items = await Ordering(filters, productos, !(bool)filters.Download!).ToListAsync();

            return response; throw new NotImplementedException();
        }

        public async Task<BaseEntityResponse<TbProducto>> ListProductos(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbProducto>();

            var productos = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.CategoriaNavigation)
                .Include(x => x.OpcionEnvioNavigation)
                .Include(x => x.VendedorNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        productos = productos.Where(x => x.Descripcion!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                productos = productos.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                productos = productos.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await productos.CountAsync();
            response.Items = await Ordering(filters, productos, !(bool)filters.Download!).ToListAsync();

            return response; throw new NotImplementedException();
        }
    }
}