using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbOpcionEnvioConfiguration : IEntityTypeConfiguration<TbOpcionEnvio>
    {
        public void Configure(EntityTypeBuilder<TbOpcionEnvio> builder)
        {
            builder.ToTable("TB_OPCION_ENVIO");

            builder.Property(e => e.Opcion)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
