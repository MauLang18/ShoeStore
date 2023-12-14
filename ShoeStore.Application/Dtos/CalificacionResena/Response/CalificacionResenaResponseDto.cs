namespace ShoeStore.Application.Dtos.CalificacionResena.Response
{
    public class CalificacionResenaResponseDto
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? ApellidoUsuario { get; set; }
        public string? Producto { get; set; }
        public int Calificacion { get; set; }
        public string? Resena { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoCalificacionResena { get; set; }
    }
}
