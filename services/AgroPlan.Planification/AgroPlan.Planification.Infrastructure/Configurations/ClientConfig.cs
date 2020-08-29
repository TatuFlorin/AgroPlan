using AgroPlan.Planification.Core.Aggregate;
using AgroPlan.Planification.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using core = AgroPlan.Planification.Core.Aggregate;

namespace AgroPlan.Planification.Infrastructure.Configurations
{
    public class ClientConfig : IEntityTypeConfiguration<core.Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("clients");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.Name, m =>
            {
                m.Property(x => x.FirstName)
                    .HasColumnName("first_name").IsRequired();
                m.Property(x => x.LastName)
                    .HasColumnName("last_name").IsRequired();
            });

            builder.OwnsOne(x => x.Phone, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("phone_number").IsRequired();
            });

            builder.OwnsOne(x => x.UsageSurface, m =>
            {
                m.Property(x => x.Value)
                    .HasColumnName("usage_surface").IsRequired();
            });
        }
    }
}