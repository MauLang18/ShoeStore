namespace ShoeStore.Domain.Entities
{
    public partial class TbCategorium : BaseEntity
    {
        public TbCategorium()
        {
            TbProductos = new HashSet<TbProducto>();
        }

        public string Categoria { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<TbProducto> TbProductos { get; set; }
    }
}
