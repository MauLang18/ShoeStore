using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.CuentaVendedor.Request;
using ShoeStore.Application.Dtos.CuentaVendedor.Response;
using ShoeStore.Application.Dtos.Pedido.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface ICuentaVendedorApplication
    {
        Task<BaseResponse<BaseEntityResponse<CuentaVendedorResponseDto>>> ListCuentasVendedores(BaseFiltersRequest filters);
        Task<BaseResponse<CuentaVendedorResponseDto>> CuentaVendedorById(int id);
        Task<BaseResponse<CuentaVendedorResponseDto>> CuentaVendedorByUser(int userId);
        Task<BaseResponse<bool>> RegisterCuentVendedor(CuentaVendedorRequestDto request);
        Task<BaseResponse<bool>> EditCuentaVendedor(int id, CuentaVendedorRequestDto request);
        Task<BaseResponse<bool>> RemoveCuentaVendedor(int id);
    }
}
