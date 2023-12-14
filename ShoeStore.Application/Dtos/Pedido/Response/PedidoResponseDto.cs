namespace ShoeStore.Application.Dtos.Pedido.Response
{
    public class PedidoResponseDto
    {
        public int Id { get; set; }
        public string? NombreComprador { get; set; }
        public string? ApellidoComprador { get; set; }
        public string? Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int EstatusPedido { get; set; }
        public string? Estatus { get; set; }
        public string? MetodoPago { get; set; }
        public string? DireccionEnvio { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoPedido { get; set; }
    }
}