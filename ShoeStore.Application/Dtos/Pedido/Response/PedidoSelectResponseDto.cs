namespace ShoeStore.Application.Dtos.Pedido.Response
{
    public class PedidoSelectResponseDto
    {
        public int Id { get; set; }
        public int Comprador { get; set; }
        public string? Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int EstatusPedido { get; set; }
        public string? Estatus { get; set; }
        public string? MetodoPago { get; set; }
        public string? DireccionEnvio { get; set;}
    }
}
