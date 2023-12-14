using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IRolRepository : IGenericRepository<TbRol>
    {
        Task<BaseEntityResponse<TbRol>> ListRoles(BaseFiltersRequest filters);
    }
}