using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.OpcionEnvio.Request;
using ShoeStore.Application.Dtos.OpcionEnvio.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IOpcionEnvioApplication
    {
        Task<BaseResponse<BaseEntityResponse<OpcionEnvioResponseDto>>> ListOpcionesEnvios(BaseFiltersRequest filters);
        Task<BaseResponse<OpcionEnvioResponseDto>> OpcionEnvioById(int id);
        Task<BaseResponse<IEnumerable<OpcionEnvioSelectResponseDto>>> ListSelectOpcionesEnvios();
        Task<BaseResponse<bool>> RegisterOpcionEnvio(OpcionEnvioRequestDto request);
        Task<BaseResponse<bool>> EditOpcionEnvio(int id, OpcionEnvioRequestDto request);
        Task<BaseResponse<bool>> RemoveOpcionEnvio(int id);
    }
}