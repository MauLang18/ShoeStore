using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Pedido.Request;
using ShoeStore.Application.Dtos.Pedido.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IPedidoApplication
    {
        Task<BaseResponse<BaseEntityResponse<PedidoResponseDto>>> ListPedidos(BaseFiltersRequest filters);
        Task<BaseResponse<PedidoResponseDto>> PedidoById(int id);
        Task<BaseResponse<IEnumerable<PedidoSelectResponseDto>>> ListPedidosByUser(int userId);
        Task<BaseResponse<bool>> RegisterPedido(PedidoRequestDto request);
        Task<BaseResponse<bool>> EditPedido(int id, PedidoRequestDto request);
        Task<BaseResponse<bool>> EditPedidoByProductoByUser(int productoId, int userId, PedidoRequestDto request);
        Task<BaseResponse<bool>> RemovePedido(int id);
        Task<BaseResponse<bool>> RemoveAllPedidos(int userId);
        Task<BaseResponse<PedidoResponseDto>> PedidoByProductoByUser(int productoId, int userId);
    }
}
