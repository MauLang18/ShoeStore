namespace ShoeStore.Domain.Entities
{
    public partial class TbCuentaVendedor : BaseEntity
    {
        public int Vendedor { get; set; }
        public int EstatusVendedor { get; set; }
        public decimal VentaTotal { get; set; }
        public decimal CostoTotal { get; set; }

        public virtual TbUsuario VendedorNavigation { get; set; } = null!;
    }
}
