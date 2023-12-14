using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbCalificacionResenaConfiguration : IEntityTypeConfiguration<TbCalificacionResena>
    {
        public void Configure(EntityTypeBuilder<TbCalificacionResena> builder)
        {
            builder.ToTable("TB_CALIFICACION_RESENA");

            builder.Property(e => e.Resena).IsUnicode(false);

            builder.HasOne(d => d.ProductoNavigation)
                .WithMany(p => p.TbCalificacionResenas)
                .HasForeignKey(d => d.Producto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PRODUCTO_CALIFICACION_RESENA");

            builder.HasOne(d => d.UsuarioNavigation)
                .WithMany(p => p.TbCalificacionResenas)
                .HasForeignKey(d => d.Usuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_CALIFICACION_RESENA");
        }
    }
}
