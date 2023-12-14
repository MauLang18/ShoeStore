namespace ShoeStore.Application.Dtos.CarritoCompra.Response
{
    public class CarritoCompraSelectResponseDto
    {
        public int Id { get; set; }
        public int Comprador {  get; set; }
        public string? Producto { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }
}
