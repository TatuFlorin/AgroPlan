using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AgroPlan.Property.AgroPlan.Property.Core.PhysicalBlockAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.EntitiesConfig{
    public class PropertyConfig : IEntityTypeConfiguration<core.Property>
    {
        public void Configure(EntityTypeBuilder<core.Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Surface)
                .HasField("_surface")
                .IsRequired();
            
            builder.OwnsOne(x => x.Neighbors, y => {
                y.Property(x => x.North_Neighbor)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasColumnName("NorthNeighbor");

                y.Property(x => x.South_Neighbor)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasColumnName("SouthNeighbor");

                y.Property(x => x.West_Neighbor)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasColumnName("WestHeighbor");
                    
                y.Property(x => x.East_Neighbor)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasColumnName("EastNeighbor");
            });

            builder.Property(x => x.ParcelId)
                .IsRequired(true)
                .HasColumnName("ParcelId")
                .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(x => x.PhysicalBlockId)
                .IsRequired(true)
                .HasColumnName("PhysicalBlockId")
                .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Ignore(x => x.EntityState);
        }
    }
}