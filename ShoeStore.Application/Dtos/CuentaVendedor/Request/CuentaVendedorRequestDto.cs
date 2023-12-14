namespace ShoeStore.Application.Dtos.CuentaVendedor.Request
{
    public class CuentaVendedorRequestDto
    {
        public int Vendedor { get; set; }
        public int EstatusVendedor { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal CostoTotal { get; set; }
        public int Estado { get; set; }
    }
}