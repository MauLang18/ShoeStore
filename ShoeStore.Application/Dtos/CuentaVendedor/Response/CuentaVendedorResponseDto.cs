namespace ShoeStore.Application.Dtos.CuentaVendedor.Response
{
    public class CuentaVendedorResponseDto
    {
        public int Id { get; set; }
        public string? NombreVendedor { get; set; }
        public string? ApellidoVendedor { get; set; }
        public int EstatusVendedor { get; set; }
        public string? Estatus { get; set; }
        public decimal? VentaTotal { get; set; }
        public decimal? CostoTotal { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoCuentaVendedor { get; set; }
    }
}