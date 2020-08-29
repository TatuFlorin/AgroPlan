using AgroPlan.Planification.Core.Interfaces;
using AgroPlan.Planification.Infrastructure;
using repos = AgroPlan.Planification.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using core = AgroPlan.Planification.Core.Aggregate;
using MediatR;
using System.Reflection;
using AgroPlan.Planification.Api.Infrastructure.QueryRepositories;
using FluentValidation.AspNetCore;
using AgroPlan.Planification.Api.Application.Behaviors;
using AgroPlan.Planification.Infrastructure.DbConnections;
using Microsoft.OpenApi.Models;

namespace AgroPlan.Planification.Api
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
            //Connection strings
            var commandConnection = new CommandConnectionString(
                Configuration.GetConnectionString("CommandConnection")
            );
            var queryConnection = new QueryConnectionString(
                Configuration.GetConnectionString("QueryConnection")
            );
            services.AddSingleton(commandConnection);
            services.AddSingleton(queryConnection);

            //Command Repositories
            services.AddSingleton<PlanContext>();
            services.AddSingleton<IUnitOfWork, PlanContext>();
            services.AddTransient<IPlanificationRepository, repos.PlanificationRepository>();
            services.AddTransient<IRepository<core.Client, string>, repos.ClientRepository>();
            services.AddTransient<ICropTypeRepostitory, repos.CropTypeRepository>();
            services.AddDbContext<PlanContext>();

            //FluentValidation
            services.AddControllers()
            .AddNewtonsoftJson()
            .AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //Query Repositories
            services.AddTransient<ICropQueryRepository, CropQueryRepository>();
            services.AddTransient<IPlanificationQueryRepository, PlanificationQueryRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCors(options =>
            {
                options.AddPolicy("MyCoresOptions", builder =>
                {
                    builder.WithOrigins("http://localhost:3000");
                });
            });

            services.AddSwaggerGen( o => {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Planification API", 
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(o => {
                o.SerializeAsV2 = true;
            });

            app.UseSwaggerUI( o => {
                o.SwaggerEndpoint("/swagger/v1/swagger.json", "Planification Api");
                o.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseCors("MyCoresOptions");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
