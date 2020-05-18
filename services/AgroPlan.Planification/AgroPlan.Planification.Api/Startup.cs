using coreInt = AgroPlan.Planification.Core.Model.Interfaces;
using AgroPlan.Planification.Infrastructure;
using repos = AgroPlan.Planification.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using core = AgroPlan.Planification.Core.Model.Aggregate;
using MediatR;
using System.Reflection;
using AgroPlan.Planification.Api.Infrastructure.QueryRepositories;
using FluentValidation.AspNetCore;
using AgroPlan.Planification.Api.Application.Behaviors;
using AgroPlan.Planification.Infrastructure.DbConnections;

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
            services.AddSingleton<coreInt.IUnitOfWork, PlanContext>();
            services.AddTransient<coreInt.IPlanificationRepository, repos.PlanificationRepository>();
            services.AddTransient<coreInt.IRepository<core.Client, string>, repos.ClientRepository>();
            services.AddTransient<coreInt.ICropTypeRepostitory, repos.CropTypeRepository>();
            services.AddDbContext<PlanContext>();

            //FluentValidation
            services.AddControllers()
            .AddNewtonsoftJson()
            .AddFluentValidation(f => f.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            //Query Repositories
            services.AddTransient<ICropRepository, CropRepository>();
            services.AddTransient<IPlanificationRepository, PlanificationRepository>();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddCors(options =>
            {
                options.AddPolicy("MyCoresOptions", builder =>
                {
                    builder.WithOrigins("http://localhost:3000");
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
