namespace ShoeStore.Domain.Entities
{
    public partial class TbPedido : BaseEntity
    {
        public int Comprador { get; set; }
        public int Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public int EstatusPedido { get; set; }
        public int MetodoPago { get; set; }
        public int DireccionEnvio { get; set; }

        public virtual TbUsuario CompradorNavigation { get; set; } = null!;
        public virtual TbDireccion DireccionEnvioNavigation { get; set; } = null!;
        public virtual TbMetodoPago MetodoPagoNavigation { get; set; } = null!;
        public virtual TbProducto ProductoNavigation { get; set; } = null!;
    }
}
