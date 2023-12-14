using Microsoft.EntityFrameworkCore;
using ShoeStore.Domain.Entities;
using System.Reflection;

namespace ShoeStore.Infrastructure.Persistences.Contexts
{
    public partial class SHOESTOREContext : DbContext
    {
        public SHOESTOREContext()
        {
        }

        public SHOESTOREContext(DbContextOptions<SHOESTOREContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbCalificacionResena> TbCalificacionResenas { get; set; } = null!;
        public virtual DbSet<TbCarritoCompra> TbCarritoCompras { get; set; } = null!;
        public virtual DbSet<TbCategorium> TbCategoria { get; set; } = null!;
        public virtual DbSet<TbCuentaVendedor> TbCuentaVendedors { get; set; } = null!;
        public virtual DbSet<TbDireccion> TbDireccions { get; set; } = null!;
        public virtual DbSet<TbMetodoPago> TbMetodoPagos { get; set; } = null!;
        public virtual DbSet<TbOpcionEnvio> TbOpcionEnvios { get; set; } = null!;
        public virtual DbSet<TbPedido> TbPedidos { get; set; } = null!;
        public virtual DbSet<TbProducto> TbProductos { get; set; } = null!;
        public virtual DbSet<TbRol> TbRols { get; set; } = null!;
        public virtual DbSet<TbUsuario> TbUsuarios { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational.Collaction", "Modern_Spanish_CI_AS");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
