using AgroPlan.Planification.Core.Model.Aggregate;
using AgroPlan.Planification.Core.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core = AgroPlan.Planification.Core.Model.Aggregate;

namespace AgroPlan.Planification.Infrastructure.Configurations
{
    public sealed class CropConfig : IEntityTypeConfiguration<core.Crop>
    {
        public void Configure(EntityTypeBuilder<Crop> builder)
        {
            builder.ToTable("crops");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Planification)
                .WithMany()
                .HasForeignKey("planification_id")
                .IsRequired();

            builder.HasOne(x => x.Type)
                .WithMany().HasForeignKey("type_id")
                .IsRequired();

            builder.Ignore(x => x.State);

            builder.OwnsOne(x => x.Surface, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("surface")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Duration, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("duration")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.PhysicalBlock, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("physical_block_code")
                    .IsRequired();
            });

            builder.OwnsOne(x => x.Parcel, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("parcel_code")
                    .IsRequired();
            });
        }
    }
}