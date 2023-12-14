using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbUsuarioConfiguration : IEntityTypeConfiguration<TbUsuario>
    {
        public void Configure(EntityTypeBuilder<TbUsuario> builder)
        {
            builder.ToTable("TB_USUARIO");

            builder.Property(e => e.Canton)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Contrasena)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Distrito)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.PrimerApellido)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Provincia)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.Property(e => e.SegundoApellido)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false);

            builder.HasOne(d => d.RolNavigation)
                .WithMany(p => p.TbUsuarios)
                .HasForeignKey(d => d.Rol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ROL_USUARIO");
        }
    }
}
