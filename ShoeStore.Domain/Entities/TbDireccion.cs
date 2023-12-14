namespace ShoeStore.Domain.Entities
{
    public partial class TbDireccion : BaseEntity
    {
        public TbDireccion()
        {
            TbPedidos = new HashSet<TbPedido>();
        }

        public string Direccion { get; set; } = null!;
        public int Comprador { get; set; }

        public virtual TbUsuario CompradorNavigation { get; set; } = null!;
        public virtual ICollection<TbPedido> TbPedidos { get; set; }
    }
}
