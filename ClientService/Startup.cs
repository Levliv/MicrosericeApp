using System.Runtime.InteropServices;
using ClientService.Business;
using ClientService.Business.Interfaces;
using ClientService.Consumers;
using ClientService.EF.Data;
using ClientService.EF.Data.Interfaces;
using ClientService.Mappers;
using ClientService.Mappers.Interfaces;
using ClientService.Models.Requests;
using ClientService.Validation;
using ClientService.Validation.Interfaces;
using FluentValidation;
using MassTransit;
using MassTransit.MultiBus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
            string dbConnectionString = Configuration.GetConnectionString("DefaultSqlServerConnectionString");;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                dbConnectionString = Configuration.GetConnectionString("SqlServerConnectionStringForLinux");
            }
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(dbConnectionString));
            services.AddControllers();

            services.AddMassTransit(massTransit =>
            {
                massTransit.AddConsumer<GetExampleConsumer>();
                massTransit.UsingRabbitMq((context, config) =>
                {
                    config.Host("localhost", "/", host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });
                    
                    config.ReceiveEndpoint("getexample", ep => ep.ConfigureConsumer<GetExampleConsumer>(context));
                });
            });
            services.AddMassTransitHostedService();
            
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IBakedGoodRepository, BakedGoodRepository>();
            
            services.AddTransient<IValidator<CreateCustomerRequest>, CreateCustomerRequestValidator>();
            services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddTransient<IDbCreateCustomerMapper, DbCreateCustomerMapper>();

            
            services.AddTransient<IGetCustomerOrdersRequestValidator, GetCustomerOrdersRequestValidator>();
            services.AddTransient<IGetCustomerOrdersCommand, GetCustomerOrdersCommand>();
            services.AddTransient<IGetCustomerOrdersMapper, GetCustomerOrdersMapper>();

            services.AddTransient<IDbBakedGoodToGetBakedGoodResponseMapper, DbBakedGoodToGetBakedGoodResponseMapper>();
            services.AddTransient<IDbOrderToGetOrderResponseMapper, DbOrderToGetOrderResponseMapper>();
            
            services.AddTransient<IUpdateCustomerPersonalInfoCommand, UpdateCustomerPersonalInfoCommand>();
            services
                .AddTransient<IDbCustomerToEditCustomerPersonalInfoResponse,
                    DbCustomerToEditCustomerPersonalInfoResponse>();

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
