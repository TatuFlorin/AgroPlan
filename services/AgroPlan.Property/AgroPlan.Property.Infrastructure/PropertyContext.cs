using AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections;
using AgroPlan.Property.AgroPlan.Property.Infrastructure.Extensions; 
using AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate;
using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore.Proxies;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;
using MediatR;
using System;

namespace AgroPlan.Property.AgroPlan.Property.Infrastructure
{
    public class PropertyContext : DbContext, IUnitOfWork
    {

        private readonly CommandConnection connString;
        private readonly IMediator _mediator;

        public PropertyContext(CommandConnection connString)
        {
            this.connString = connString ?? throw new ArgumentNullException(nameof(PropertyContext));
        }

        public PropertyContext(CommandConnection connString, IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(PropertyContext));
            this.connString = connString ?? throw new ArgumentNullException(nameof(PropertyContext));
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
            await _mediator.TriggerDomainEvents(this);
            
            var res = await base.SaveChangesAsync(cancellationToken);

            return res != 0;
        }
    }
}