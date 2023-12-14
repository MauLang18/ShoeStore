using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Rol.Request;
using ShoeStore.Application.Dtos.Rol.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface IRolApplication
    {
        Task<BaseResponse<BaseEntityResponse<RolResponseDto>>> ListRoles(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<RolSelectResponseDto>>> ListSelectRoles();
        Task<BaseResponse<RolResponseDto>> RolById(int id);
        Task<BaseResponse<bool>> RegisterRol(RolRequestDto request);
        Task<BaseResponse<bool>> EditRol(int id, RolRequestDto request);
        Task<BaseResponse<bool>> RemoveRol(int id);
    }
}
