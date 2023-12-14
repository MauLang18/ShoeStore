namespace ShoeStore.Application.Dtos.CarritoCompra.Response
{
    public class CarritoCompraResponseDto
    {
        public int Id { get; set; }
        public string? NombreComprador { get; set; }
        public string? ApellidoComprador { get; set; }
        public string? Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoCarritoCompra { get; set; }
    }
}
