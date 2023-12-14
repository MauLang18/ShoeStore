namespace ShoeStore.Application.Dtos.Categoria.Response
{
    public class CategoriaResponseDto
    {
        public int Id { get; set; }
        public string? Categoria { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaCreacionAuditoria { get; set; }
        public int Estado { get; set; }
        public string? EstadoCategoria { get; set; }
    }
}
