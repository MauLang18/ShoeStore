using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Usuario.Request;

namespace ShoeStore.Application.Interfaces
{
    public interface IAuthApplication
    {
        Task<BaseResponse<string>> Login(TokenRequestDto requestDto);
    }
}