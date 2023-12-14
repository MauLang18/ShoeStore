namespace ShoeStore.Application.Dtos.OpcionEnvio.Response
{
    public class OpcionEnvioResponseDto
    {
        public int Id { get; set; }
        public string? Opcion { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoOpcionEnvio { get; set; }
    }
}