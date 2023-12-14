namespace ShoeStore.Application.Dtos.CarritoCompra.Request
{
    public class CarritoCompraRequestDto
    {
        public int Comprador { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public int Estado { get; set; }
    }
}