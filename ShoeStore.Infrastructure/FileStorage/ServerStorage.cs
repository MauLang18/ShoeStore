using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ShoeStore.Infrastructure.FileStorage
{
    public class ServerStorage : IServerStorage
    {
        private readonly string _connectionString;

        public ServerStorage(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RutaServer")!;
        }

        public async Task<string> SaveFile(IFormFile file)
        {
            string ruta = Path.Combine(_connectionString, file.FileName);

            try
            {
                using (FileStream newFile = System.IO.File.Create(ruta))
                {
                    await file.CopyToAsync(newFile);
                    newFile.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Se ha producido una excepción: {ex.Message}");
            }

            string rutaRelativa = Path.Combine("http://localhost/imagenes/", file.FileName);

            return rutaRelativa;
        }
    }
}