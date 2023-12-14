using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbDireccionConfiguration : IEntityTypeConfiguration<TbDireccion>
    {
        public void Configure(EntityTypeBuilder<TbDireccion> builder)
        {
            builder.ToTable("TB_DIRECCION");

            builder.Property(e => e.Direccion).IsUnicode(false);

            builder.HasOne(d => d.CompradorNavigation)
                .WithMany(p => p.TbDireccions)
                .HasForeignKey(d => d.Comprador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_USUARIO_DIRECCION");
        }
    }
}
