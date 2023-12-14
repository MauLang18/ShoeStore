namespace ShoeStore.Application.Dtos.Direccion.Response
{
    public class DireccionResponseDto
    {
        public int Id { get; set; }
        public string? Direccion { get; set; }
        public string? NombreComprador { get; set; }
        public string? ApellidoComprador { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoDireccion { get; set; }
    }
}
