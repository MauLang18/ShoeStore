using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.MetodoPago.Request;
using ShoeStore.Application.Dtos.MetodoPago.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IMetodoPagoApplication
    {
        Task<BaseResponse<BaseEntityResponse<MetodoPagoResponseDto>>> ListMetodosPagos(BaseFiltersRequest filters);
        Task<BaseResponse<MetodoPagoResponseDto>> MetodoPagoById(int id);
        Task<BaseResponse<IEnumerable<MetodoPagoSelectResponseDto>>> ListSelectMetodosPagos();
        Task<BaseResponse<bool>> RegisterMetodoPago(MetodoPagoRequestDto request);
        Task<BaseResponse<bool>> EditMetodoPago(int id, MetodoPagoRequestDto request);
        Task<BaseResponse<bool>> RemoveMetodoPago(int id);
    }
}
