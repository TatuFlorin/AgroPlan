using AgroPlan.Property.AgroPlan.Property.Core.Interfaces;
using AgroPlan.Property.AgroPlan.Property.Infrastructure;
using AgroPlan.Property.AgroPlan.Property.Infrastructure.DbConnections;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using System.Reflection;

namespace AgroPlan.Property.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()  
                .AddNewtonsoftJson();
            
            services.AddSingleton(
                new CommandConnection(Configuration.GetConnectionString("CommandConnection"))
            );

            services.AddDbContext<PropertyContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            //Command repositories
            services.AddTransient<IOwnerRepository, OwnerRepository>();

            //SOON: Query repositories
            // ->
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
