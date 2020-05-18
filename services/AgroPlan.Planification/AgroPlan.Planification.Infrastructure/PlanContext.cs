using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AgroPlan.Planification.Core.Model.Interfaces;
using AgroPlan.Planification.Infrastructure.DbConnections;
using Microsoft.EntityFrameworkCore;
using core = AgroPlan.Planification.Core.Model.Aggregate;

namespace AgroPlan.Planification.Infrastructure
{
    public class PlanContext : DbContext, IUnitOfWork
    {

        private readonly CommandConnectionString _connString;
        protected PlanContext() { }

        public PlanContext(CommandConnectionString connString)
        {
            this._connString = connString;
        }

        // public PlanContext(DbContextOptions<PlanContext> options)
        // : base(options) { }

        public DbSet<core.Planification> Planifications { get; set; }
        public DbSet<core.Client> Clients { get; set; }
        public DbSet<core.CropType> CropTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseNpgsql(
                    _connString.Value,
                    x => x.MigrationsAssembly("AgroPlan.Planification.Api"))
                .UseSnakeCaseNamingConvention()
                .UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }

        public async Task<bool> SaveChangesEventsAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync();
            return true;
        }
    }
}