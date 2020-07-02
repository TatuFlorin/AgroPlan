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
using Microsoft.OpenApi.Models;
using AgroPlan.Property.AgroPlan.Property.Api.Infrastructure.Repositories;

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
            services.AddSingleton(
              new QueryConnection(Configuration.GetConnectionString("QueryConnection"))  
            );

            services.AddDbContext<PropertyContext>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSwaggerGen(o => {
                o.SwaggerDoc("v1", new OpenApiInfo{ Title = "My Api", Version = "v1" });
            });

            //Command repositories
            services.AddTransient<IOwnerRepository, OwnerRepository>();

            //SOON: Query repositories
            services.AddTransient<IOwnerQueryRepository, OwnerQueryRepository>();
            // ->
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(o =>{
                o.SerializeAsV2 = true;
            });
            
            app.UseSwaggerUI(o => {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                o.RoutePrefix = string.Empty;
            });

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
