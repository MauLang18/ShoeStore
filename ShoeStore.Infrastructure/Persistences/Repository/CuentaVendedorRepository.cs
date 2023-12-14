using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class CuentaVendedorRepository : GenericRepository<TbCuentaVendedor>, ICuentaVendedorRepository
    {
        private readonly SHOESTOREContext _context;

        public CuentaVendedorRepository(SHOESTOREContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbCuentaVendedor>> ListCuentasVendedores(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbCuentaVendedor>();

            var cuentasVendedores = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.VendedorNavigation)
                .AsNoTracking();

            if (filters.StateFilter is not null)
            {
                cuentasVendedores = cuentasVendedores.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                cuentasVendedores = cuentasVendedores.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await cuentasVendedores.CountAsync();
            response.Items = await Ordering(filters, cuentasVendedores, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<TbCuentaVendedor> CuentaVendedorByUser(int userId)
        {
            var cuentaVendedor = await _context.TbCuentaVendedors
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Estado.Equals((int)StateTypes.Activo) && x.Vendedor.Equals(userId) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null);

            return cuentaVendedor!;
        }
    }
}