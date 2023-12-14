using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;
using ShoeStore.Infrastructure.Persistences.Contexts;
using ShoeStore.Infrastructure.Persistences.Interfaces;
using ShoeStore.Utilities.Static;

namespace ShoeStore.Infrastructure.Persistences.Repository
{
    public class UsuarioRepository : GenericRepository<TbUsuario>, IUsuarioRepository
    {
        private readonly SHOESTOREContext _context;

        public UsuarioRepository(SHOESTOREContext context) : base(context)
        {
            _context = context;
        }

        public async Task<BaseEntityResponse<TbUsuario>> ListUsuarios(BaseFiltersRequest filters)
        {
            var response = new BaseEntityResponse<TbUsuario>();

            var usuario = GetEntityQuery(x => x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null)
                .Include(x => x.RolNavigation)
                .AsNoTracking();

            if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
            {
                switch (filters.NumFilter)
                {
                    case 1:
                        usuario = usuario.Where(x => x.Correo!.Contains(filters.TextFilter));
                        break;
                }
            }

            if (filters.StateFilter is not null)
            {
                usuario = usuario.Where(x => x.Estado.Equals(filters.StateFilter));
            }

            if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
            {
                usuario = usuario.Where(x => x.FechaCreacionAuditoria >= Convert.ToDateTime(filters.StartDate) && x.FechaCreacionAuditoria <= Convert.ToDateTime(filters.EndDate).AddDays(1));
            }

            filters.Sort ??= "Id";

            response.TotalRecords = await usuario.CountAsync();
            response.Items = await Ordering(filters, usuario, !(bool)filters.Download!).ToListAsync();

            return response;
        }

        public async Task<TbUsuario> UserByEmail(string email)
        {
            var user = await _context.TbUsuarios.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Correo!.Equals(email) && x.UsuarioEliminacionAuditoria == null && x.FechaEliminacionAuditoria == null && x.Estado.Equals((int)StateTypes.Activo));

            return user!;
        }
    }
}