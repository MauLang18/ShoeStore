namespace ShoeStore.Domain.Entities
{
    public partial class TbMetodoPago : BaseEntity
    {
        public TbMetodoPago()
        {
            TbPedidos = new HashSet<TbPedido>();
        }

        public string Metodo { get; set; } = null!;

        public virtual ICollection<TbPedido> TbPedidos { get; set; }
    }
}
