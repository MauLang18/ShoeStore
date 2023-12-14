namespace ShoeStore.Application.Dtos.Usuario.Request
{
    public class UsuarioRequestDto
    {
        public string? Nombre { get; set; }
        public string? PrimerApellido { get; set; }
        public string? SegundoApellido { get; set; }
        public string? Telefono { get; set; }
        public string? Provincia { get; set; }
        public string? Canton { get; set; }
        public string? Distrito { get; set; }
        public string? Correo { get; set; }
        public string? Contrasena { get; set; }
        public int Rol { get; set; }        
        public int Estado { get; set; }
    }
}