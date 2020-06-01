using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure{
    public class ParcelCOnfig : IEntityTypeConfiguration<Parcel>
    {
        public void Configure(EntityTypeBuilder<Parcel> builder)
        {
            builder.ToTable("Pracels");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.ParcelCode, y => {
                y.Property(x => x.Value)
                    .HasColumnName("ParcelCode")
                    .IsRequired(true);
            });

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(20)
                .IsRequired(false)
                .HasField("_name");
        }
    }
}