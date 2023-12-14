using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface ICategoriumRepository : IGenericRepository<TbCategorium>
    {
        Task<BaseEntityResponse<TbCategorium>> ListCategorias(BaseFiltersRequest filters);
    }
}