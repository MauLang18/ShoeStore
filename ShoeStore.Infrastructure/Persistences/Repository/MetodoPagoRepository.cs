using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class MetodoPagoRepository : GenericRepository<TbMetodoPago>, IMetodoPagoRepository
    {
        public MetodoPagoRepository(SHOESTOREContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbMetodoPago>> ListMetodosPagos(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbMetodoPago>();

            var metodoPago = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        metodoPago = metodoPago.Where(x => x.Metodo!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                metodoPago = metodoPago.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                metodoPago = metodoPago.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await metodoPago.CountAsync();
            response.Items = await Ordering(filters, metodoPago, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}
