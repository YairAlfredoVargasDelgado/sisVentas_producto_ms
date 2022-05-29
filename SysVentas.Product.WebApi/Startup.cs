using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using SysVentas.Products.WebApi.Infrastructure;
using SysVentas.Products.Domain.Base;
using SysVentas.Products.Infrastructure.Data;
using SysVentas.Products.Infrastructure.Data.Base;

namespace SysVentas.Products.WebApi
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
            var connectionString = Configuration["ConnectionString"];
            services.AddDbContext<ProductDataContext>
                (opt => opt.UseSqlServer(connectionString));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorPipelineBehavior<,>));
            InyeccionFluentValidations(services);
            services.AddScoped<IDbContext, ProductDataContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddMediatR(Assembly.Load("SysVentas.Product.Application"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SysVentas.Product.WebApi", Version = "v1" });
            });


        }

        private void InyeccionFluentValidations(IServiceCollection services)
        {
            AssemblyScanner.FindValidatorsInAssembly(Assembly.Load("SysVentas.Product.Application")).ForEach(pair =>
            {
                // RegisterValidatorsFromAssemblyContaing does this:
                services.Add(ServiceDescriptor.Scoped(pair.InterfaceType, pair.ValidatorType));
                // Also register it as its concrete type as well as the interface type
                services.Add(ServiceDescriptor.Scoped(pair.ValidatorType, pair.ValidatorType));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SysVentas.Product.WebApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            InicializarDatabase(app, env);
        }

        private static void InicializarDatabase(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();

            scope.ServiceProvider.GetRequiredService<ProductDataContext>().Database.Migrate();
            var context = scope.ServiceProvider.GetRequiredService<ProductDataContext>();
            if (env.IsDevelopment())
            {
                
            }
        }
    }
}
