using Microsoft.AspNetCore.Http;

namespace ShoeStore.Application.Dtos.Producto.Request
{
    public class ProductoRequestDto
    {
        public string? Producto { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public IFormFile? Imagen { get; set; }
        public int Disponibilidad { get; set; }
        public int Categoria { get; set; }
        public int? OpcionEnvio { get; set; }
        public int? Vendedor { get; set; }
        public int Estado { get; set; }
        public int Cantidad { get; set; }
    }
}