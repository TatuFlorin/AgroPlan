using core = AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.EntitiesConfig{
    public class PropertyConfig : IEntityTypeConfiguration<core.Property>
    {
        public void Configure(EntityTypeBuilder<core.Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Surface, y => {
                y.Property(x => x.Value)
                    .IsRequired(true)
                    .HasColumnName("Surface");
            });

            builder.OwnsOne(x => x.Neighbors, y => {
                y.Property(x => x.North_Neighbor)
                    .HasMaxLength(30)
                    .IsRequired(true)
                    .HasColumnName("NorthNeighbor");

                y.Property(x => x.South_NeighBor)
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

            builder.HasOne(x => x.PhysicalBlock)
                .WithOne()
                .HasForeignKey<core.PhysicalBlock>(x => x.Id)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction)
                .Metadata.PrincipalToDependent
                .SetPropertyAccessMode(PropertyAccessMode.Field);
            
            builder.HasOne(x => x.Parcel)
                .WithOne()
                .HasForeignKey<core.Parcel>(x => x.Id)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction)
                .Metadata.PrincipalToDependent
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}