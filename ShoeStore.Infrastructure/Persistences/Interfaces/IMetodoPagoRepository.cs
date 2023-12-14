using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IMetodoPagoRepository : IGenericRepository<TbMetodoPago>
    {
        Task<BaseEntityResponse<TbMetodoPago>> ListMetodosPagos(BaseFiltersRequest filters);
    }
}