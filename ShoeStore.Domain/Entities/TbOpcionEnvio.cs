namespace ShoeStore.Domain.Entities
{
    public partial class TbOpcionEnvio : BaseEntity
    {
        public TbOpcionEnvio()
        {
            TbProductos = new HashSet<TbProducto>();
        }

        public string Opcion { get; set; } = null!;

        public virtual ICollection<TbProducto> TbProductos { get; set; }
    }
}
