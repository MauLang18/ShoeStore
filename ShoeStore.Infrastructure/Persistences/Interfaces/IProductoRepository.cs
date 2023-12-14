using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IProductoRepository : IGenericRepository<TbProducto>
    {
        Task<BaseEntityResponse<TbProducto>> ListProductos(BaseFiltersRequest filters);
        Task<BaseEntityResponse<TbProducto>> ListProductoByUser(BaseFiltersRequest filters, int userId);
    }
}
