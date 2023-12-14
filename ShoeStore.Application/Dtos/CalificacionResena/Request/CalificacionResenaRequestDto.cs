namespace ShoeStore.Application.Dtos.CalificacionResena.Request
{
    public class CalificacionResenaRequestDto
    {
        public int Usuario { get; set; }
        public int Producto { get; set; }
        public int Calificacion { get; set; }
        public string? Resena { get; set; }
        public int Estado { get; set; }
    }
}
