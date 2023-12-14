namespace ShoeStore.Application.Dtos.Pedido.Request
{
    public class PedidoRequestDto
    {
        public int Comprador { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int EstatusPedido { get; set; }
        public int MetodoPago { get; set; }
        public int DireccionEnvio { get; set; }
        public int Estado { get; set; }
    }
}