using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Direccion.Request;
using ShoeStore.Application.Dtos.Direccion.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IDireccionApplication
    {
        Task<BaseResponse<BaseEntityResponse<DireccionResponseDto>>> ListDirecciones(BaseFiltersRequest filters);
        Task<BaseResponse<DireccionResponseDto>> DireccionById(int id);
        Task<BaseResponse<IEnumerable<DireccionSelectResponseDto>>> ListSelectDireccionByUser(int userId);
        Task<BaseResponse<BaseEntityResponse<DireccionResponseDto>>> ListDireccionesByUser(BaseFiltersRequest filters, int userId);
        Task<BaseResponse<bool>> RegisterDireccion(DireccionRequestDto request);
        Task<BaseResponse<bool>> EditDireccion(int id, DireccionRequestDto request);
        Task<BaseResponse<bool>> RemoveDireccion(int id);
    }
}
