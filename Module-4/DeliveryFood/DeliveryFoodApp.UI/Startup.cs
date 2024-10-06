using DeliveryFood.Core.Ports;
using DeliveryFood.Infrastructure.Adapters.Http;
using DeliveryFood.Infrastructure.Adapters.Internal;
using DeliveryFoodApp.UI.Adapters.BackgroundJobs;
using Microsoft.OpenApi.Models;
using Quartz;

namespace DeliveryFoodApp.UI
{
    public class Startup
    {
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuration
            // services.Configure<Settings>(options => Configuration.Bind(options));
            // var connectionString = Configuration["CONNECTION_STRING"];
            // var rabbitMqHost = Configuration["RABBIT_MQ_HOST"];
            //var geoServiceGrpcHost = Configuration["GEO_SERVICE_GRPC_HOST"];
            
            services.AddSingleton<IOrderRepository, OrderInternalRepository>();
            services.AddSingleton<IDishRepository, DishInternalRepository>();
            services.AddTransient<IPaymentClient, PaymentClient>();

            // services.AddTransient<IGeoClient>(x => new Client(geoServiceGrpcHost));
            
            //Commands, Queries
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Startup>());
            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(DeliveryFood.Core.Application.UseCases.Queries.GetOrders.Handler).Assembly); 
            });

            // //HTTP Handlers
            services.AddControllers();
           
            //Swagger
             services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo
                 {
                     Title = "Сервис доставки",
                     Description = "Сервис доставки (ASP.NET Core 8.0)",
                 });
             });
             services.AddControllers();
             services.AddEndpointsApiExplorer();
             services.AddHealthChecks();
             
            //  // gRPC
            //  services.AddGrpcClient<Client>(options => 
            //  { 
            //      options.Address = new Uri (geoServiceGrpcHost); 
            //  });
            // 
            
            // CRON Jobs
            services.AddQuartz(configure =>
            {
                var acceptOrdersJobKey = new JobKey(nameof(AcceptOrdersJob));
                var deliveryOrderJobKey = new JobKey(nameof(DeliveryOrderJob));
                configure
                    .AddJob<AcceptOrdersJob>(acceptOrdersJobKey)
                    .AddTrigger(
                        trigger => trigger.ForJob(acceptOrdersJobKey)
                            .WithSimpleSchedule(
                                schedule => schedule.WithIntervalInSeconds(30)
                                    .RepeatForever()))
                    .AddJob<DeliveryOrderJob>(deliveryOrderJobKey)
                    .AddTrigger(
                        trigger => trigger.ForJob(deliveryOrderJobKey)
                            .WithSimpleSchedule(
                                schedule => schedule.WithIntervalInSeconds(30)
                                    .RepeatForever()));
                configure.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Сервис доставки");
                options.RoutePrefix = string.Empty;
            });
            app.UseHealthChecks("/health");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}