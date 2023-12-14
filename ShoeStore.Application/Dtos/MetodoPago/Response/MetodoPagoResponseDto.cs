namespace ShoeStore.Application.Dtos.MetodoPago.Response
{
    public class MetodoPagoResponseDto
    {
        public int Id { get; set; }
        public string? Metodo { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoMetodoPago { get; set; }
    }
}