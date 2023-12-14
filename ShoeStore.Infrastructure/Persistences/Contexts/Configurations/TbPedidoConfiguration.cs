using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbPedidoConfiguration : IEntityTypeConfiguration<TbPedido>
    {
        public void Configure(EntityTypeBuilder<TbPedido> builder)
        {
            builder.ToTable("TB_PEDIDO");

            builder.Property(e => e.Total).HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.CompradorNavigation)
                .WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.Comprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_COMPRADOR_PEDIDO");

            builder.HasOne(d => d.DireccionEnvioNavigation)
                .WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.DireccionEnvio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DIRECCION_PEDIDO");

            builder.HasOne(d => d.MetodoPagoNavigation)
                .WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.MetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_METODO_PAGO_PEDIDO");

            builder.HasOne(d => d.ProductoNavigation)
                .WithMany(p => p.TbPedidos)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTO_PEDIDO");
        }
    }
}
