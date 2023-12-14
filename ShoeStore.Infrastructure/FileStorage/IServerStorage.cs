using Microsoft.AspNetCore.Http;

namespace ShoeStore.Infrastructure.FileStorage
{
    public interface IServerStorage
    {
        Task<string> SaveFile(IFormFile file);
    }
}