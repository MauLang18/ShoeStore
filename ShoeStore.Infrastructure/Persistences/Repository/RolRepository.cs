using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class RolRepository : GenericRepository<TbRol>, IRolRepository
    {
        public RolRepository(SHOESTOREContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbRol>> ListRoles(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbRol>();

            var roles = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        roles = roles.Where(x => x.Rol!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                roles = roles.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                roles = roles.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await roles.CountAsync();
            response.Items = await Ordering(filters, roles, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}