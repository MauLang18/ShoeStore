using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class DireccionRepository : GenericRepository<TbDireccion>, IDireccionRepository
    {
        private readonly SHOESTOREContext _context;

        public DireccionRepository(SHOESTOREContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbDireccion>> ListDirecciones(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbDireccion>();

            var direcciones = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.CompradorNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        direcciones = direcciones.Where(x => x.Direccion!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                direcciones = direcciones.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                direcciones = direcciones.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await direcciones.CountAsync();
            response.Items = await Ordering(filters, direcciones, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<BaseEntityResponse<TbDireccion>> ListDireccionesByUser(BaseFiltersRequest filters, int userId)
        {
            var response = new BaseEntityResponse<TbDireccion>();

            var direcciones = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.Comprador.Equals(userId))
                .Include(x => x.CompradorNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        direcciones = direcciones.Where(x => x.Direccion!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                direcciones = direcciones.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                direcciones = direcciones.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await direcciones.CountAsync();
            response.Items = await Ordering(filters, direcciones, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<IEnumerable<TbDireccion>> ListSelectDireccionByUser(int userId)
        {
            var direcciones = await _context.TbDireccions
                .Where(x => x.Estado.Equals((int)StateTypes.Activo) && x.Comprador.Equals(userId))
                .AsNoTracking()
                .ToListAsync();

            return direcciones;
        }
    }
}
