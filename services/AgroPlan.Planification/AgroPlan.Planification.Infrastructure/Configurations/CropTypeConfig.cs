using core = AgroPlan.Planification.Core.Aggregate;
using AgroPlan.Planification.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AgroPlan.Planification.Core.Aggregate;

namespace AgroPlan.Planification.Infrastructure.Configurations
{
    public sealed class CropTypeMap : IEntityTypeConfiguration<core.CropType>
    {
        public void Configure(EntityTypeBuilder<CropType> builder)
        {
            builder.ToTable("croptypes");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.CropCode, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("crop_code")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.CropName, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("crop_name")
                    .IsRequired();
            });
        }
    }
}