using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbCategoriumConfiguration : IEntityTypeConfiguration<TbCategorium>
    {
        public void Configure(EntityTypeBuilder<TbCategorium> builder)
        {
            builder.ToTable("TB_CATEGORIA");

            builder.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false);

            builder.Property(e => e.Descripcion).IsUnicode(false);
        }
    }
}
