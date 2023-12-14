using ShoeStore.Domain.Entities;
using ShoeStore.Infrastructure.Commons.Bases.Request;
using ShoeStore.Infrastructure.Commons.Bases.Response;

namespace ShoeStore.Infrastructure.Persistences.Interfaces
{
    public interface ICuentaVendedorRepository : IGenericRepository<TbCuentaVendedor>
    {
        Task<BaseEntityResponse<TbCuentaVendedor>> ListCuentasVendedores(BaseFiltersRequest filters);
        Task<TbCuentaVendedor> CuentaVendedorByUser(int userId);
    }
}