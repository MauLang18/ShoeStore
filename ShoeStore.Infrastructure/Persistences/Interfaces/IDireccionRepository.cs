using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IDireccionRepository : IGenericRepository<TbDireccion>
    {
        Task<BaseEntityResponse<TbDireccion>> ListDirecciones(BaseFiltersRequest filters);
        Task<IEnumerable<TbDireccion>> ListSelectDireccionByUser(int userId);
        Task<BaseEntityResponse<TbDireccion>> ListDireccionesByUser(BaseFiltersRequest filters, int userId);
    }
}
