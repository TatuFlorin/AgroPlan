using Microsoft.EntityFrameworkCore;
using AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections;
using System.Reflection;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure{
    public class PropertyContext : DbContext {

        private readonly CommandConnection connString;

        public PropertyContext(CommandConnection connString)
        {
            this.connString = connString;
        }
        
        public PropertyContext(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
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