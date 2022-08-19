using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClientService.Business;
using ClientService.Business.Interfaces;
using ClientService.EF.Data;
using ClientService.EF.Data.Interfaces;
using ClientService.Mappers;
using ClientService.Models.Requests;
using ClientService.Models.Responses;
using ClientService.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ClientService
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
            string dbConnectionString = Configuration.GetConnectionString("SqlServerConnectionString");
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddControllers();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            
            services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddTransient<IDbCreateCustomerMapper, DbCreateCustomerMapper>();

            services.AddTransient<IGetCustomerInfoCommand, GetCustomerInfoCommand>();
            services.AddTransient<IGetCustomerInfoMapper, GetCustomerInfoMapper>();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ClientService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ClientService v1"));
            }

            app.UseHttpsRedirection();

            using IServiceScope serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using ApplicationDbContext context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>()!;
            
            context.Database.Migrate();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
