namespace ShoeStore.Application.Dtos.Producto.Response
{
    public class ProductoResponseDto
    {
        public int Id { get; set; }
        public string? Producto { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string? Disponibilidad { get; set; }
        public string? Categoria { get; set; }
        public string? Imagen { get; set; }
        public string? OpcionEnvio { get; set; }
        public string? NombreVendedor { get; set; }
        public string? ApellidoVendedor { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoProducto { get; set; }
        public int Cantidad { get; set; }
    }
}