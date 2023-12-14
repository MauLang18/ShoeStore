namespace ShoeStore.Application.Dtos.CalificacionResena.Response
{
    public class CalificacionResenaSelectResponseDto
    {
        public int Id { get; set; }
        public string? NombreUsuario { get; set; }
        public string? ApellidoUsuario { get; set; }
        public string? Producto {  get; set; }
        public int Calificacion { get; set; }
        public string? Resena { get; set; }
    }
}
