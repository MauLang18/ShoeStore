namespace ShoeStore.Domain.Entities
{
    public partial class TbProducto : BaseEntity
    {
        public TbProducto()
        {
            TbCalificacionResenas = new HashSet<TbCalificacionResena>();
            TbCarritoCompras = new HashSet<TbCarritoCompra>();
            TbPedidos = new HashSet<TbPedido>();
        }

        public string Producto { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public decimal Precio { get; set; }
        public string Imagen { get; set; } = null!;
        public int Disponibilidad { get; set; }
        public int Categoria { get; set; }
        public int OpcionEnvio { get; set; }
        public int Vendedor { get; set; }
        public int Cantidad { get; set; }

        public virtual TbCategorium CategoriaNavigation { get; set; } = null!;
        public virtual TbOpcionEnvio OpcionEnvioNavigation { get; set; } = null!;
        public virtual TbUsuario VendedorNavigation { get; set; } = null!;
        public virtual ICollection<TbCalificacionResena> TbCalificacionResenas { get; set; }
        public virtual ICollection<TbCarritoCompra> TbCarritoCompras { get; set; }
        public virtual ICollection<TbPedido> TbPedidos { get; set; }
    }
}
