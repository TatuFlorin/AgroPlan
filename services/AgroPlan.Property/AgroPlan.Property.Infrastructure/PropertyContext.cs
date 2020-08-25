using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections;
using Microsoft.EntityFrameworkCore.Proxies;
using System.Reflection;
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure
{
    public class PropertyContext : DbContext, IUnitOfWork
    {

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

        public async Task<bool> SaveChangesAsyncEvents(CancellationToken cancellationToken = default)
        {

            //here are triggered domain events
            //after or before base.SaveChangesAsync()
            
            var res = await base.SaveChangesAsync(cancellationToken);

            return res != 0;
        }
    }
}