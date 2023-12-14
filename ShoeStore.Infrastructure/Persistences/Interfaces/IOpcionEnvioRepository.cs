using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IOpcionEnvioRepository : IGenericRepository<TbOpcionEnvio>
    {
        Task<BaseEntityResponse<TbOpcionEnvio>> ListOpcionesEnvios(BaseFiltersRequest filters);
    }
}