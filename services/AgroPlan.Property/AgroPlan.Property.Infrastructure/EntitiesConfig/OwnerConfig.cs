using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure.EntitiesConfigs{
    public class OwnerConfig : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("Owners");

            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, v => {
                v.Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .HasMaxLength(20)
                    .IsRequired(true);
                    
                v.Property(x => x.LastName)
                    .HasColumnName("LastName")
                    .HasMaxLength(20)
                    .IsRequired(true);
            });

            builder.OwnsOne(x => x.TotalSurface, y => {
                y.Property(x => x.Value)
                    .HasColumnName("TotalSurface")
                    .IsRequired(true);
            });

            builder.HasMany(x => x.Properties)
                .WithOne(x => x.Owner)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent
                .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}