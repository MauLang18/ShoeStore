namespace ShoeStore.Application.Dtos.Usuario.Response
{
    public class UsuarioResponseDto
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Telefono { get; set; }
        public string? Provincia { get; set; }
        public string? Canton { get; set; }
        public string? Distrito { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public string? Rol { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoUsuario { get; set; }
    }
}