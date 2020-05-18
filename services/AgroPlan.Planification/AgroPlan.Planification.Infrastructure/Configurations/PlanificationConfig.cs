using core = AgroPlan.Planification.Core.Model.Aggregate;
using AgroPlan.Planification.Core.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgroPlan.Planification.Infrastructure.Configurations
{
    public class PlanificationConfig : IEntityTypeConfiguration<core.Planification>
    {
        public void Configure(EntityTypeBuilder<core.Planification> builder)
        {
            builder.ToTable("planification");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Year)
                .HasColumnName("planification_year")
                .IsRequired();

            builder.OwnsOne(x => x.Surface, m =>
            {
                m.Property(x => x.Value)
                .HasColumnName("surface")
                .IsRequired();
            });

            builder.HasOne(x => x.Client)
                .WithMany()
                .HasForeignKey("client_id");

            builder.HasMany(x => x.Crops)
                .WithOne(x => x.Planification)
                .OnDelete(DeleteBehavior.Cascade)
                .Metadata.PrincipalToDependent.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}