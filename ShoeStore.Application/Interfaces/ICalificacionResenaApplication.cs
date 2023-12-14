using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CalificacionResena.Request;
using ShoeStore.Application.Dtos.CalificacionResena.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface ICalificacionResenaApplication
    {
        Task<BaseResponse<BaseEntityResponse<CalificacionResenaResponseDto>>> ListCalificacionesResenas(BaseFiltersRequest filters);
        Task<BaseResponse<CalificacionResenaResponseDto>> CalificacionResenaById(int id);
        Task<BaseResponse<IEnumerable<CalificacionResenaSelectResponseDto>>> CalificacionesResenasByProductoId(int productoId);
        Task<BaseResponse<bool>> RegisterCalificacionResena(CalificacionResenaRequestDto request);
        Task<BaseResponse<bool>> EditCalificacionResena(int id, CalificacionResenaRequestDto request);
        Task<BaseResponse<bool>> RemoveCalificacionResena(int id);
    }
}
