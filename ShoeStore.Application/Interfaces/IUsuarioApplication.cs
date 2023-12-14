using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Usuario.Request;
using ShoeStore.Application.Dtos.Usuario.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IUsuarioApplication
    {
        Task<BaseResponse<BaseEntityResponse<UsuarioResponseDto>>> ListUsuarios(BaseFiltersRequest filters);
        Task<BaseResponse<UsuarioResponseDto>> UsuarioById(int id);
        Task<BaseResponse<bool>> RegisterUsuario(UsuarioRequestDto request);
        Task<BaseResponse<bool>> EditUsuario(int id, UsuarioRequestDto request);
        Task<BaseResponse<bool>> RemoveUsuario(int id);
    }
}