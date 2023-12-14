using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbCarritoCompraConfiguration : IEntityTypeConfiguration<TbCarritoCompra>
    {
        public void Configure(EntityTypeBuilder<TbCarritoCompra> builder)
        {
            builder.ToTable("TB_CARRITO_COMPRA");

            builder.HasOne(d => d.CompradorNavigation)
                .WithMany(p => p.TbCarritoCompras)
                .HasForeignKey(d => d.Comprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPRADOR_CARRITO_COMPRA");

            builder.HasOne(d => d.ProductoNavigation)
                .WithMany(p => p.TbCarritoCompras)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTO_CARRITO_COMPRA");
        }
    }
}
