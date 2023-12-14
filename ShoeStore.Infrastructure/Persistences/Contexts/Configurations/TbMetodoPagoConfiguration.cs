using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoeStore.Domain.Entities;

namespace ShoeStore.Infrastructure.Persistences.Contexts.Configurations
{
    public class TbMetodoPagoConfiguration : IEntityTypeConfiguration<TbMetodoPago>
    {
        public void Configure(EntityTypeBuilder<TbMetodoPago> builder)
        {
            builder.ToTable("TB_METODO_PAGO");

            builder.Property(e => e.Metodo)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
