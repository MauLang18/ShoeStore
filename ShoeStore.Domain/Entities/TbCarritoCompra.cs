namespace ShoeStore.Domain.Entities
{
    public partial class TbCarritoCompra : BaseEntity
    {
        public int Comprador { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }

        public virtual TbUsuario CompradorNavigation { get; set; } = null!;
        public virtual TbProducto ProductoNavigation { get; set; } = null!;
    }
}
