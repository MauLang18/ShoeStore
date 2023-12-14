namespace ShoeStore.Domain.Entities
{
    public partial class TbCalificacionResena : BaseEntity
    {
        public int Usuario { get; set; }
        public int Producto { get; set; }
        public int Calificacion { get; set; }
        public string Resena { get; set; } = null!;

        public virtual TbProducto ProductoNavigation { get; set; } = null!;
        public virtual TbUsuario UsuarioNavigation { get; set; } = null!;
    }
}
