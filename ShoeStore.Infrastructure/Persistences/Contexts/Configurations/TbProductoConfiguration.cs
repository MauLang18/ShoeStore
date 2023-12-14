using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbProductoConfiguration : IEntityTypeConfiguration<TbProducto>
    {
        public void Configure(EntityTypeBuilder<TbProducto> builder)
        {
            builder.ToTable("TB_PRODUCTO");

            builder.Property(e => e.Descripcion).IsUnicode(false);

            builder.Property(e => e.Imagen).IsUnicode(false);

            builder.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            builder.Property(e => e.Producto)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.CategoriaNavigation)
                .WithMany(p => p.TbProductos)
                .HasForeignKey(d => d.Categoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CATEGORIA_PRODUCTO");

            builder.HasOne(d => d.OpcionEnvioNavigation)
                .WithMany(p => p.TbProductos)
                .HasForeignKey(d => d.OpcionEnvio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OPCION_ENVIO_PRODUCTO");

            builder.HasOne(d => d.VendedorNavigation)
                .WithMany(p => p.TbProductos)
                .HasForeignKey(d => d.Vendedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VENDEDOR_PRODUCTO");
        }
    }
}
