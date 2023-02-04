using Accounting.Api.Repository.IRepository;
using Accounting.Api.Repository;
using System.Reflection;
using Accounting.Api.Consumer;
using EventBus.Messages.Events;
using MassTransit;

namespace Accounting.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddScoped<IAccountingRepository, AccountingRepository>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMassTransit(x =>
            {
                x.AddConsumer<AccountingCheckOrderStatusConsumer>(c=>c.UseConcurrentMessageLimit(1));

                x.SetKebabCaseEndpointNameFormatter();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.ConfigureEndpoints(context);
                });
                x.AddRequestClient<OrderSubmittedResponse>();
            });
            services.AddScoped<AccountingCheckOrderStatusConsumer>();

            // Add services to the container.
            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();

        }
    }
}
