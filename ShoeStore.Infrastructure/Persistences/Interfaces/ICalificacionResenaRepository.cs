using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface ICalificacionResenaRepository : IGenericRepository<TbCalificacionResena>
    {
        Task<BaseEntityResponse<TbCalificacionResena>> ListCalificacionesResenas(BaseFiltersRequest filters);
        Task<IEnumerable<TbCalificacionResena>> ListCalificacionesResenasByProductoId(int productoId);
    }
}