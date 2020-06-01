using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Reflection;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure{
    public class PropertyContext : DbContext {

        private readonly CommandConnection connString;

        public PropertyContext(CommandConnection connString)
        {
            this.connString = connString;
        }
        
        public PropertyContext(){}

        public DbSet<Owner> Owners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
           optionsBuilder.UseSqlServer(connString.ConnectionString, x => {
               x.MigrationsAssembly("AgroPlan.Property.Api");
           });
           optionsBuilder.UseLazyLoadingProxies();
           base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}