using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.EntitiesConfig{
    public class PhysicalBlockConfig : IEntityTypeConfiguration<PhysicalBlock>
    {
        public void Configure(EntityTypeBuilder<PhysicalBlock> builder)
        {
            builder.ToTable("PhysicalBlocks");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Code, y => {
                y.Property(x => x.Value)
                    .IsRequired(true)
                    .HasColumnName("PBCode");
            });

            builder.Property(x => x.Name)
                .HasMaxLength(20)
                .HasColumnName("Name")
                .IsRequired(false)
                .HasField("_name");
        }
    }
}