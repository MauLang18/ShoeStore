using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Producto.Request;
using ShoeStore.Application.Dtos.Producto.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IProductoApplication
    {
        Task<BaseResponse<BaseEntityResponse<ProductoResponseDto>>> ListProductos(BaseFiltersRequest filters);
        Task<BaseResponse<ProductoResponseDto>> ProductoById(int id);
        Task<BaseResponse<BaseEntityResponse<ProductoResponseDto>>> ListProductosByUser(BaseFiltersRequest filters, int userId);
        Task<BaseResponse<bool>> RegisterProducto(ProductoRequestDto request);
        Task<BaseResponse<bool>> EditProducto(int id, ProductoRequestDto request);
        Task<BaseResponse<bool>> RemoveProducto(int id);
    }
}
