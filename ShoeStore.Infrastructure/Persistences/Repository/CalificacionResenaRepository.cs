using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class CalificacionResenaRepository : GenericRepository<TbCalificacionResena>, ICalificacionResenaRepository
    {
        private readonly SHOESTOREContext _context;

        public CalificacionResenaRepository(SHOESTOREContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbCalificacionResena>> ListCalificacionesResenas(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbCalificacionResena>();

            var calificacionesResenas = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.UsuarioNavigation)
                .Include(x => x.ProductoNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        calificacionesResenas = calificacionesResenas.Where(x => x.Resena!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                calificacionesResenas = calificacionesResenas.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                calificacionesResenas = calificacionesResenas.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await calificacionesResenas.CountAsync();
            response.Items = await Ordering(filters, calificacionesResenas, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<TbCalificacionResena>> ListCalificacionesResenasByProductoId(int productoId)
        {
            var calificacionesResenas = await _context.TbCalificacionResenas
                .Where(x => x.Estado.Equals((int)StateTypes.Activo) && x.Producto.Equals(productoId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.UsuarioNavigation)
                .Include(x => x.ProductoNavigation)
                .AsNoTracking()
                .ToListAsync();

            return calificacionesResenas!;
        }
    }
}