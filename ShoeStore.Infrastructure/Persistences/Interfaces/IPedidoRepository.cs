using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IPedidoRepository : IGenericRepository<TbPedido>
    {
        Task<BaseEntityResponse<TbPedido>> ListPedidos(BaseFiltersRequest filters);
        Task<IEnumerable<TbPedido>> ListPedidosByUser(int userId);
        Task<bool> DeletePedidoByUser(int userId);
        Task<bool> EditPedidoByProductoByUser(int productoId, int userId, int cantidad);
        Task<TbPedido> PedidoByProdcutoByUser(int productoId, int userId);
    }
}