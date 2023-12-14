using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface ICarritoCompraRepository : IGenericRepository<TbCarritoCompra>
    {
        Task<BaseEntityResponse<TbCarritoCompra>> ListCarritoCompra(BaseFiltersRequest filters);
        Task<IEnumerable<TbCarritoCompra>> ListCarritoCompraByUser(int userId);
        Task<bool> DeleteCarritoCompraByUser(int userId);
        Task<bool> EditCarritoCompraByProductoByUser(int productoId, int userId, int cantidad);
        Task<TbCarritoCompra> CarritoCompraByProdcutoByUser(int productoId, int userId);
    }
}