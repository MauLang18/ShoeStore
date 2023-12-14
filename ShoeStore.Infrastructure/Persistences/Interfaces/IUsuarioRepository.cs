using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<TbUsuario>
    {
        Task<BaseEntityResponse<TbUsuario>> ListUsuarios(BaseFiltersRequest filters);
        Task<TbUsuario> UserByEmail(string email);
    }
}