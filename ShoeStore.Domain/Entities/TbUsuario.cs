namespace ShoeStore.Domain.Entities
{
    public partial class TbUsuario : BaseEntity
    {
        public TbUsuario()
        {
            TbCalificacionResenas = new HashSet<TbCalificacionResena>();
            TbCarritoCompras = new HashSet<TbCarritoCompra>();
            TbCuentaVendedors = new HashSet<TbCuentaVendedor>();
            TbDireccions = new HashSet<TbDireccion>();
            TbPedidos = new HashSet<TbPedido>();
            TbProductos = new HashSet<TbProducto>();
        }

        public string Nombre { get; set; } = null!;
        public string PrimerApellido { get; set; } = null!;
        public string? SegundoApellido { get; set; }
        public string? Telefono { get; set; }
        public string Provincia { get; set; } = null!;
        public string Canton { get; set; } = null!;
        public string Distrito { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public int Rol { get; set; }

        public virtual TbRol RolNavigation { get; set; } = null!;
        public virtual ICollection<TbCalificacionResena> TbCalificacionResenas { get; set; }
        public virtual ICollection<TbCarritoCompra> TbCarritoCompras { get; set; }
        public virtual ICollection<TbCuentaVendedor> TbCuentaVendedors { get; set; }
        public virtual ICollection<TbDireccion> TbDireccions { get; set; }
        public virtual ICollection<TbPedido> TbPedidos { get; set; }
        public virtual ICollection<TbProducto> TbProductos { get; set; }
    }
}
