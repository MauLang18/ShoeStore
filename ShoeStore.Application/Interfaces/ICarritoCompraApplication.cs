using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CarritoCompra.Request;
using ShoeStore.Application.Dtos.CarritoCompra.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface ICarritoCompraApplication
    {
        Task<BaseResponse<BaseEntityResponse<CarritoCompraResponseDto>>> ListCarritoCompra(BaseFiltersRequest filters);
        Task<BaseResponse<CarritoCompraResponseDto>> CarritoCompraById(int id);
        Task<BaseResponse<IEnumerable<CarritoCompraSelectResponseDto>>> ListCarritoCompraByUser(int userId);
        Task<BaseResponse<bool>> RegisterCarritoCompra(CarritoCompraRequestDto request);
        Task<BaseResponse<bool>> EditCarritoCompra(int id, CarritoCompraRequestDto request);
        Task<BaseResponse<bool>> EditCarritoCompraByProductoByUser(int productoId, int userId, CarritoCompraRequestDto request);
        Task<BaseResponse<bool>> RemoveCarritoCompra(int id);
        Task<BaseResponse<bool>> RemoveAllCarritoCompra(int userId);
        Task<BaseResponse<CarritoCompraResponseDto>> CarritoCompraByProductoByUser(int productoId, int userId);
    }
}
