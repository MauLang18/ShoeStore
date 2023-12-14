using ShoeStore.Application.Commons.Bases;
using ShoeStore.Application.Dtos.Categoria.Request;
using ShoeStore.Application.Dtos.Categoria.Response;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Application.Interfaces
{
    public interface ICategoriaApplication
    {
        Task<BaseResponse<BaseEntityResponse<CategoriaResponseDto>>> ListCategorias(BaseFiltersRequest filters);
        Task<BaseResponse<IEnumerable<CategoriaSelectResponseDto>>> ListSelectCategorias();
        Task<BaseResponse<CategoriaResponseDto>> CategoriaById(int id);
        Task<BaseResponse<bool>> RegisterCategoria(CategoriaRequestDto request);
        Task<BaseResponse<bool>> EditCategoria(int id, CategoriaRequestDto request);
        Task<BaseResponse<bool>> RemoveCategoria(int id);
    }
}
