using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class CategoriumRepository : GenericRepository<TbCategorium>, ICategoriumRepository
    {
        public CategoriumRepository(SHOESTOREContext context) : base(context) { }

        public async Task<BaseEntityResponse<TbCategorium>> ListCategorias(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbCategorium>();

            var categorias = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        categorias = categorias.Where(x => x.Categoria!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                categorias = categorias.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                categorias = categorias.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await categorias.CountAsync();
            response.Items = await Ordering(filters, categorias, !(bool)filters.Download!).ToListAsync();

            return response;
        }
    }
}