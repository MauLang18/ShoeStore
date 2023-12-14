using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbCuentaVendedorConfiguration : IEntityTypeConfiguration<TbCuentaVendedor>
    {
        public void Configure(EntityTypeBuilder<TbCuentaVendedor> builder)
        {
            builder.ToTable("TB_CUENTA_VENDEDOR");

            builder.Property(e => e.CostoTotal).HasColumnType("decimal(10, 2)");

            builder.Property(e => e.VentaTotal).HasColumnType("decimal(10, 2)");

            builder.HasOne(d => d.VendedorNavigation)
                .WithMany(p => p.TbCuentaVendedors)
                .HasForeignKey(d => d.Vendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VENDEDOR_CUENTA_VENDEDOR");
        }
    }
}
