using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class OpcionEnvioRepository : GenericRepository<TbOpcionEnvio>, IOpcionEnvioRepository
    {
        public OpcionEnvioRepository(SHOESTOREContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbOpcionEnvio>> ListOpcionesEnvios(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbOpcionEnvio>();

            var opcionesEnvio = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        opcionesEnvio = opcionesEnvio.Where(x => x.Opcion!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                opcionesEnvio = opcionesEnvio.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                opcionesEnvio = opcionesEnvio.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await opcionesEnvio.CountAsync();
            response.Items = await Ordering(filters, opcionesEnvio, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}