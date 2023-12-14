using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbRolConfiguration : IEntityTypeConfiguration<TbRol>
    {
        public void Configure(EntityTypeBuilder<TbRol> builder)
        {
            builder.ToTable("TB_ROL");

            builder.Property(e => e.Rol)
                .HasMaxLength(255)
                .IsUnicode(false);
        }
    }
}
